using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip[] footsteps;
    public AudioClip[] grunts;
    public AudioClip[] misc;
    private AudioSource audioSource;

    public const int CLICK = 0;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playFootstep()
    {
        int rand_clip = Random.Range(0, footsteps.Length);

        audioSource.clip = footsteps[rand_clip];
        audioSource.pitch = Random.Range(0.8f, 1.4f);
        //audio_source.panStereo = Random.Range(-1f, 1f);
        audioSource.Play();
    }

    public void playGrunt()
    {
        int rand_clip = Random.Range(0, grunts.Length);

        audioSource.clip = grunts[rand_clip];
        audioSource.pitch = Random.Range(0.7f, 0.9f);
        //audio_source.panStereo = Random.Range(-1f, 1f);
        audioSource.Play();
    }

    public void playMisc(int snd)
    {
        audioSource.clip = misc[snd];
        audioSource.Play();
    }
}
