using UnityEngine;
using System.Collections;

public class Monster
{
    private string id;
    private string name;
    private int x;
    private int y;
    private int num;
    public GameObject gameObj;

    public Monster(string id, int x, int y, int n)
    {
        this.id = id;
        this.x = x;
        this.y = y;
        this.num = n;
    }

    public void attack()
    {
        gameObj.GetComponent<Animation>().Play("Monster Attack");
    }
}
