using UnityEngine;
using System.Collections;

public class AnimLightFlicker : MonoBehaviour {
    public float scale = 1.0F;
    public float speed = 1.0F;
    public float min = 3.0F;

    private float perlinOffset;
    void Start()
    {
        perlinOffset = Random.Range(0f, 1000f);
    }

    // Update is called once per frame
    void Update ()
    {
        float amp = scale * Mathf.PerlinNoise(Time.time * speed, perlinOffset);
        GetComponent<Light>().intensity = min + amp;
    }
}
