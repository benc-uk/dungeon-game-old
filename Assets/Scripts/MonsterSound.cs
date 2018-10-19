using UnityEngine;
using System.Collections;

public class MonsterSound : MonoBehaviour {

    private AudioSource source;
    private float time = 0;
    private float int_time = 0;
    public float min_time = 4f;
    public float max_time = 4f;
    public float min_pitch = 0.6f;
    public float max_pitch = 1.0f;


    // Use this for initialization
    void Start ()
    {
        source = GetComponent<AudioSource>();
        int_time = 1;// Random.Range(min_time, max_time);
    }
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;
        if (time > int_time) {
            source.pitch = Random.Range(min_pitch, max_pitch);
            source.Play();// OneShot(source.clip, source.volume);
            time = 0;
            int_time = Random.Range(min_time, max_time);
        }
	}
}
