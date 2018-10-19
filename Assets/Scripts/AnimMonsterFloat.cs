using UnityEngine;
using System.Collections;

public class AnimMonsterFloat: MonoBehaviour
{
    public float bob_scale = 1.0F;
    public float speed = 1.0F;

    private float perlin_offset1;
    private Vector3 init_pos;

    void Start()
    {
        perlin_offset1 = Random.Range(0f, 1000f);
        init_pos = transform.localPosition;
    }

    void Update()
    {
        float bob = bob_scale * Mathf.PerlinNoise(Time.time * speed, perlin_offset1) - 0.5f;
        transform.localPosition = new Vector3(init_pos.x, Mathf.Floor(init_pos.y + bob), init_pos.z); 
    }
}
