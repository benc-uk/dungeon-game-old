using UnityEngine;

public class MapFeature
{
    public bool blocking;
    public GameObject gameObj;
    public bool active;
    public int facing;
    public string type;

    public MapFeature()
    {
        this.blocking = true;
        this.active = false;
        this.facing = Map.NORTH;
    }

    public MapFeature(string type, int facing, bool active)
    {
        this.type = type;
        this.blocking = true;
        this.active = active;
        this.facing = facing;
    }

    public void activate()
    {
        Animation anim = this.gameObj.GetComponentInChildren<Animation>();
        AudioSource audio_source = this.gameObj.GetComponentInChildren<AudioSource>();

        foreach (AnimationState state in anim) {
            state.speed = 1;
            state.time = 0;
        }

        anim.Play();
        audio_source.Play();

        this.active = true;
    }

    public void deactivate()
    {
        Animation anim = this.gameObj.GetComponentInChildren<Animation>();
        AudioSource audio_source = this.gameObj.GetComponentInChildren<AudioSource>();

        foreach (AnimationState state in anim) {
            state.speed = -1;
            state.time = state.length;
        }

        anim.Play();
        audio_source.Play();

        this.active = false;
    }

    public void toggle()
    {
        if (this.active) {
            this.deactivate();
        }
        else {
            this.activate();
        }
    }
}
