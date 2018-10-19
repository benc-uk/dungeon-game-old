using UnityEngine;
using System.Collections;

public class Monster
{
    private string id;
    private string monname;
    private int x;
    private int y;
    private int num;
    public GameObject g_obj;

    public Monster(string id, int x, int y, int n)
    {
        this.id = id;
        this.x = x;
        this.y = y;
        this.num = n;
    }

    public void attack()
    {
        g_obj.GetComponent<Animation>().Play("Monster Attack");
    }
}
