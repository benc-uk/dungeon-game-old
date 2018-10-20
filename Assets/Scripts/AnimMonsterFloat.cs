using UnityEngine;
using System.Collections;

public class AnimMonsterFloat: MonoBehaviour
{
    public float bobSize = 1.0F;
    public float speed = 1.0F;

    private float perlinOffset;
    private Vector3 initPosition;

    void Start()
    {
        perlinOffset = Random.Range(0f, 1000f);
        initPosition = transform.localPosition;
    }

    void Update()
    {
        float bob = bobSize * Mathf.PerlinNoise(Time.time * speed, perlinOffset) - 0.5f;
        transform.localPosition = new Vector3(initPosition.x, Mathf.Floor(initPosition.y + bob), initPosition.z); 
    }
}
