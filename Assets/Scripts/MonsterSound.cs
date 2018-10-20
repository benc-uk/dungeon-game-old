using UnityEngine;
using System.Collections;

public class MonsterSound : MonoBehaviour {

    private AudioSource source;
    private float time = 0;
    private float intTime = 0;
    public float minTime = 4f;
    public float maxTime = 4f;
    public float minPitch = 0.6f;
    public float maxPitch = 2.0f;


    // Use this for initialization
    void Start ()
    {
        source = GetComponent<AudioSource>();
        intTime = 1;// Random.Range(min_time, max_time);
    }
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;
        if (time > intTime) {
            source.pitch = Random.Range(minPitch, maxPitch);
            source.Play();// OneShot(source.clip, source.volume);
            time = 0;
            intTime = Random.Range(minTime, maxTime);
        }
	}
}
