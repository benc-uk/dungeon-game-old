using UnityEngine;

public class Map
{
    public const int NORTH = 0;
    public const int EAST = 1;
    public const int SOUTH = 2;
    public const int WEST = 3;
    public const int SIZE = 32;

    public MapCell[,] data;
    public MapCell player_start;

    public Map()
    {
        data = new MapCell[SIZE, SIZE];

        for (int yy = 0; yy < SIZE; yy++) {
            for (int xx = 0; xx < SIZE; xx++) {
                data[xx, yy] = new MapCell(xx, yy);
            }
        }
    }
    
    public void loadLevel(string level)
    {
        string line;
        int x = 0;
        int y = 0;

        // Read the level file and parse line by line
        System.IO.StreamReader file = new System.IO.StreamReader(MapBuilder.MAP_DIR + level + ".dat");
        while ((line = file.ReadLine()) != null) {
            x = 0;
            foreach (char c in line) {
                if(c == '.') {
                    data[x, y].solid = true;
                } else {
                    data[x, y].solid = false;
                }
                if(c == 's') {
                    data[x, y].solid = false;
                    data[x, y].player_start = true;
                    player_start = data[x, y];
                    Main.player.setPlayerLocation(x, y);
                }
                if (c == '|') {
                    data[x, y].solid = false;
                    MapFeature door = new MapFeature("door", Map.NORTH, false);
                    data[x, y].addFeature(door);
                }
                if (c == '-') {
                    data[x, y].solid = false;
                    MapFeature door = new MapFeature("door", Map.EAST, false);
                    data[x, y].addFeature(door);
                }
                if (c == 'p') {
                    data[x, y].solid = false;
                    MapFeature p = new MapFeature("pillar", Map.EAST, false);
                    data[x, y].addFeature(p);
                }
                if (c == 'm') {
                    data[x, y].solid = false;
                    Monster mon = new Monster("rat", x, y, 1);
                    data[x, y].monster = mon;
                }
                x++;
            }
            y++;
        }
        file.Close();
	}

    public MapCell getCellNorth(int x, int y)
    {
        if (y == 0) return null;
        return data[x, y - 1];
    }
    public MapCell getCellNorth(MapCell mc)
    {
        return getCellNorth(mc.x, mc.y);
    }

    public MapCell getCellEast(int x, int y)
    {
        if (x == Map.SIZE) return null;
        return data[x + 1, y];
    }
    public MapCell getCellEast(MapCell mc)
    {
        return getCellEast(mc.x, mc.y);
    }

    public MapCell getCellSouth(int x, int y)
    {
        if (y == Map.SIZE) return null;
        return data[x, y + 1];
    }
    public MapCell getCellSouth(MapCell mc)
    {
        return getCellSouth(mc.x, mc.y);
    }

    public MapCell getCellWest(int x, int y)
    {
        if (x == 0) return null;
        return data[x - 1, y];
    }
    public MapCell getCellWest(MapCell mc)
    {
        return getCellWest(mc.x, mc.y);
    }
}
