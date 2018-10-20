using UnityEngine;
using System.Collections;

public class AnimMonsterShuffle : MonoBehaviour
{
    public float bob_scale = 1.0F;
    public float bob_min_y = 1.0F;
    public float speed = 1.0F;

    private float perlin_offset1;
    private float perlin_offset2;
    private Vector3 init_pos;

    void Start()
    {
        perlin_offset1 = Random.Range(0f, 1000f);
        perlin_offset2 = Random.Range(0f, 1000f);
        init_pos = transform.localPosition;
    }

    void Update()
    {
        float bob1 = bob_scale * (Mathf.PerlinNoise((Time.time * speed), perlin_offset1) - 0.5f);
        float bob2 = bob_scale * (Mathf.PerlinNoise((Time.time * speed), perlin_offset2) - 0.5f);
        transform.localPosition = new Vector3(Mathf.Floor(init_pos.x + bob1), init_pos.y, Mathf.Floor(init_pos.z + bob2));
    }
}
