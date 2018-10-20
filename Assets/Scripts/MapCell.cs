using UnityEngine;

public class MapCell 
{
    public bool solid;
    public bool player_start;
    public GameObject cell_object;
    public int x;
    public int y;
    private MapFeature feature;
    public Monster monster;

    // Use this for initialization
    public MapCell(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.solid = true;
        this.player_start = false;
        this.feature = null;
	}

    public void addFeature(MapFeature f)
    {
        this.feature = f;
    }

    public bool hasFeature()
    {
        return (this.feature != null);
    }

    public MapFeature getFeature()
    {
        return this.feature;
    }

    public bool blocksMove()
    {
        if (this.solid) return true;
        if (this.hasFeature() && this.feature.blocking) return true;
        // monsters block
        if (this.monster != null) return true;

        return false;
    }

}
