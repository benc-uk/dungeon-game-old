using UnityEngine;
using System.Collections;

public class MapBuilder : MonoBehaviour
{
    //public GameObject[] walls;
    //public GameObject[] floors;
    //public GameObject[] ceilings;
    //public GameObject[] wall_deco;
    //public GameObject[] floor_deco;
    //public GameObject[] ceil_deco;
    //public GameObject[] doors;
    //public GameObject[] monsters;
    //public GameObject[] floor_decals;
    //public GameObject monster;
    //public GameObject mon_sprite;

    public static string MAP_DIR = "Maps/";

    /*
    *  Create the map, load the input file and construct the objects for the whole map
    */
    public Map buildMap(string level_name)
    {
        // Create map
        Map map = new Map();
        // Load and parse map file 
        map.loadLevel(level_name);

        // Loop across all the cells in the map
        for (int y = 0; y < Map.SIZE; y++) {
            for (int x = 0; x < Map.SIZE; x++) {

                MapCell mapcell = map.data[x, y];

                // Only process non-solid cells
                if (!mapcell.solid) {
                    // Build the GameObject for the cell
                    GameObject cell_obj = buildCell(mapcell, map);
                    
                    // Place into the world
                    cell_obj.transform.position = new Vector3(x * Main.CELL_SIZE, 0, -(y * Main.CELL_SIZE));

                    // Link GameObject to MapCell
                    mapcell.cell_object = cell_obj;
                }
            }
        }

        return map;
    }

    /*
    *   Create a GameObject for a map cell on the grid, add walls, floors, decorations etc.
    */
    private GameObject buildCell(MapCell mc, Map map)
    {
        GameObject cell = new GameObject();

        // Floor and ceiling 
        cell.name = "Map cell: " + mc.x + ", " + mc.y;
        GameObject floor = (GameObject)Instantiate(Resources.Load("Floor"), new Vector3(0, 0, 0), Quaternion.identity);
        GameObject ceil = (GameObject)Instantiate(Resources.Load("Ceiling"), new Vector3(0, 0, 0), Quaternion.identity);
        floor.transform.parent = cell.transform;
        ceil.transform.parent = cell.transform;

        // Look for walls, and build them
        if (map.getCellSouth(mc) == null || map.getCellSouth(mc).solid) {
            addWall(0, cell, mc);
        }
        if (map.getCellWest(mc) == null || map.getCellWest(mc).solid) {
            addWall(90, cell, mc);
        }
        if (map.getCellNorth(mc) == null || map.getCellNorth(mc).solid) {
            addWall(180, cell, mc);
        }
        if (map.getCellEast(mc) == null || map.getCellEast(mc).solid) {
            addWall(270, cell, mc);
        }

        // Look for features; doors, pillars, pits, statues 
        if (mc.hasFeature()) {
            GameObject feat = null;

            if (mc.getFeature().type == "door") {
                feat = (GameObject)Instantiate(Resources.Load("Door"), new Vector3(0, 0, 0), Quaternion.identity);
            }
            if (mc.getFeature().type == "pillar") {
                feat = (GameObject)Instantiate(Resources.Load("Pillar"), new Vector3(0, 0, 0), Quaternion.identity);
            }
            int r = 0;
            switch(mc.getFeature().facing) {
                case Map.SOUTH: r = 0; break;
                case Map.WEST: r = 90; break;
                case Map.NORTH: r = 180; break;
                case Map.EAST: r = 270; break;
            }
            feat.transform.rotation = Quaternion.Euler(0, r, 0);
            feat.transform.parent = cell.transform;
            mc.getFeature().game_object = feat;
        }

        // Puddles and floor decals, grime etc
        if (Random.Range(0, 100) > 50) {
            GameObject decal = (GameObject)Instantiate(Resources.Load("FloorSlime"), new Vector3(0, 0, 0), Quaternion.identity);
            decal.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            float s = Random.Range(0.3f, 1.9f);
            decal.transform.localScale = new Vector3(s, s, s);
            decal.transform.parent = cell.transform;
        }

        if(mc.monster != null) {
            addMonster(cell, mc);
        }

        return cell;
    }

    /*
    *   Logic for inserting a wall and associated decorations into a cell
    */
    private void addWall(int rot, GameObject cell, MapCell mc)
    {
        GameObject wall = (GameObject)Instantiate(Resources.Load("Wall"), new Vector3(0, 0, 0), Quaternion.identity);
        wall.transform.rotation = Quaternion.Euler(0, rot, 0);
        wall.transform.parent = cell.transform;

        // Wall deco, randomized
        if (!mc.hasFeature() && Random.Range(0, 100) > 54 || (mc.x == 3 && mc.y == 1 && rot == 0)) {
            GameObject[] wall_deco = Resources.LoadAll<GameObject>("Decor/");

            GameObject torch_obj = (GameObject)Instantiate(wall_deco[Random.Range(0, wall_deco.Length)], new Vector3(0, 0, 0), Quaternion.identity);
            torch_obj.transform.rotation = Quaternion.Euler(0, rot, 0);
            torch_obj.transform.parent = cell.transform;
        }
    }

    /*
    *   Adds a monster into a cell
    */
    public static void addMonster(GameObject cell, MapCell mc)
    {
        GameObject mon_go = (GameObject)Instantiate(Resources.Load("MonsterGroup"), new Vector3(0, 0, 0), Quaternion.identity);
        mc.monster.g_obj = mon_go;
        GameObject mon_sprite_go = (GameObject)Instantiate(Resources.Load("MonsterSprite"), new Vector3(0, 0, 0), Quaternion.identity);
        string randomMon = "mon_skel";
        int r = Random.Range(0, 100);
        if (r > 0 && r <= 20) 
            randomMon = "mon_orc";
        if (r > 20 && r <= 40) 
            randomMon = "mon_skel";
        if (r > 40 && r <= 60) 
            randomMon = "mon_mummy";
        if (r > 60 && r <= 80) 
            randomMon = "mon_ghost";            
        if (r > 80 && r <= 100)
            randomMon = "mon_eye";

        mon_sprite_go.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/"+randomMon);
        if(randomMon == "mon_ghost") mon_sprite_go.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.7f);
        mon_sprite_go.transform.parent = mon_go.transform;
        mon_go.transform.parent = cell.transform;
    }
}
