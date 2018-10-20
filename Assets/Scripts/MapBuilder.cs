using UnityEngine;
using System.Collections;

public class MapBuilder : MonoBehaviour
{
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
          mapcell.gameObj = buildGameObj(mapcell, map);
        }
      }
    }

    return map;
  }

  /*
  *   Create a GameObject for a map cell on the grid, add walls, floors, decorations etc.
  */
  private GameObject buildGameObj(MapCell cell, Map map)
  {
    GameObject cellGameObj = new GameObject();

    // Floor and ceiling 
    cellGameObj.name = "Map cell: " + cell.x + ", " + cell.y;
    GameObject floor = (GameObject)Instantiate(Resources.Load("Floor"), new Vector3(0, 0, 0), Quaternion.identity);
    GameObject ceiling = (GameObject)Instantiate(Resources.Load("Ceiling"), new Vector3(0, 0, 0), Quaternion.identity);
    floor.transform.parent = cellGameObj.transform;
    ceiling.transform.parent = cellGameObj.transform;

    // Look for walls, and build them
    if (map.getCellSouth(cell) == null || map.getCellSouth(cell).solid) {
      addWall(0, cellGameObj, cell);
    }
    if (map.getCellWest(cell) == null || map.getCellWest(cell).solid) {
      addWall(90, cellGameObj, cell);
    }
    if (map.getCellNorth(cell) == null || map.getCellNorth(cell).solid) {
      addWall(180, cellGameObj, cell);
    }
    if (map.getCellEast(cell) == null || map.getCellEast(cell).solid) {
      addWall(270, cellGameObj, cell);
    }

    // Look for features; doors, pillars, pits, statues 
    if (cell.hasFeature()) {
      GameObject feat = null;

      if (cell.getFeature().type == "door") {
        feat = (GameObject)Instantiate(Resources.Load("Door"), new Vector3(0, 0, 0), Quaternion.identity);
      }
      if (cell.getFeature().type == "pillar") {
        feat = (GameObject)Instantiate(Resources.Load("Pillar"), new Vector3(0, 0, 0), Quaternion.identity);
      }
      
      int featureRotate = 0;
      switch (cell.getFeature().facing) {
        case Map.SOUTH: featureRotate = 0; break;
        case Map.WEST: featureRotate = 90; break;
        case Map.NORTH: featureRotate = 180; break;
        case Map.EAST: featureRotate = 270; break;
      }
      feat.transform.rotation = Quaternion.Euler(0, featureRotate, 0);
      feat.transform.parent = cellGameObj.transform;
      cell.getFeature().gameObj = feat;
    }

    // Puddles and floor decals, grime etc
    if (Random.Range(0, 100) > 50) {
      GameObject decal = (GameObject)Instantiate(Resources.Load("FloorSlime"), new Vector3(0, 0, 0), Quaternion.identity);
      decal.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
      float s = Random.Range(0.3f, 1.9f);
      decal.transform.localScale = new Vector3(s, s, s);
      decal.transform.parent = cellGameObj.transform;
    }

    if (cell.monster != null) {
      addMonster(cell, cellGameObj);
    }

    // Place into the world
    cellGameObj.transform.position = new Vector3(cell.x * Main.CELL_SIZE, 0, -(cell.y * Main.CELL_SIZE));

    return cellGameObj;
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
    if (!mc.hasFeature() && Random.Range(0, 100) > 35)
    {
      GameObject[] wall_deco = Resources.LoadAll<GameObject>("Decor/");

      GameObject torch_obj = (GameObject)Instantiate(wall_deco[Random.Range(0, wall_deco.Length)], new Vector3(0, 0, 0), Quaternion.identity);
      torch_obj.transform.rotation = Quaternion.Euler(0, rot, 0);
      torch_obj.transform.parent = cell.transform;
    }
  }

  /*
  *   Adds a monster into a cell
  */
  public static void addMonster(MapCell mapCell, GameObject cell)
  {
    mapCell.monster.gameObj = (GameObject)Instantiate(Resources.Load("Monster"), new Vector3(0, 0, 0), Quaternion.identity);

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

    mapCell.monster.gameObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + randomMon);
    if (randomMon == "mon_ghost") mapCell.monster.gameObj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.7f);
    mapCell.monster.gameObj.transform.parent = cell.transform;
  }
}
