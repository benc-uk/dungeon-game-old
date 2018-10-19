using UnityEngine;
using System.Collections;

public class PlayerData
{
    public bool placed { get; set; }
    public int x;
    public int y;
    public int facing;
    private GameObject player_obj;

    public PlayerData(GameObject obj)
    {
        placed = false;
        facing = Map.NORTH;
        player_obj = obj;
    }

    public void setPlayerLocation(int x, int y)
    {
        this.x = x;
        this.y = y;
        player_obj.transform.position = new Vector3(x * Main.CELL_SIZE, 0, -(y * Main.CELL_SIZE));
    }

    public void turnRight()
    {
        facing++;
        if (facing > Map.WEST) facing = Map.NORTH;
    }

    public void turnLeft()
    {
        facing--;
        if (facing < Map.NORTH) facing = Map.WEST;
    }
}
