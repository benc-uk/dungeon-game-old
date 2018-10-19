using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip[] footsteps;
    public AudioClip[] grunts;
    public AudioClip[] misc;
    private AudioSource audio_source;

    public const int CLICK = 0;

    // Use this for initialization
    void Start () {
        audio_source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playFootstep()
    {
        int rand_clip = Random.Range(0, footsteps.Length);

        audio_source.clip = footsteps[rand_clip];
        audio_source.pitch = Random.Range(0.8f, 1.4f);
        //audio_source.panStereo = Random.Range(-1f, 1f);
        audio_source.Play();
    }

    public void playGrunt()
    {
        int rand_clip = Random.Range(0, grunts.Length);

        audio_source.clip = grunts[rand_clip];
        audio_source.pitch = Random.Range(0.7f, 0.9f);
        //audio_source.panStereo = Random.Range(-1f, 1f);
        audio_source.Play();
    }

    public void playMisc(int snd)
    {
        audio_source.clip = misc[snd];
        audio_source.Play();
    }
}
