using UnityEngine;

public class FeatureDoor : MapFeature
{
    //private bool open;

    /*public FeatureDoor(int facing, bool open)
    {
        this.facing = facing;
        this.active = open;
    }*/



    public bool isOpen() { return this.active; }
}
