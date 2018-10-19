using UnityEngine;
using System.Collections;

public class AnimLightBob : MonoBehaviour {
    public float light_scale = 1.0F;
    public float light_min = 1.0F;
    public float bob_scale = 1.0F;
    public float speed = 1.0F;

    private float perlin_offsety_x;
    private float perlin_offsety_y;
    private float perlin_offsety_z;

    void Start()
    {
        perlin_offsety_x = Random.Range(0f, 1000f);
        perlin_offsety_y = Random.Range(0f, 1000f);
        perlin_offsety_z = Random.Range(0f, 1000f);
    }

    void Update()
    {
        // Update is called once per frame
        float light_amp = light_scale * Mathf.PerlinNoise(0 + (Time.time * speed), 0.0F);

        float bobx = bob_scale * (Mathf.PerlinNoise((Time.time * speed), perlin_offsety_x) - 0.5f);
        float boby = bob_scale * (Mathf.PerlinNoise((Time.time * speed), perlin_offsety_y) - 0.5f);
        float bobz = bob_scale * (Mathf.PerlinNoise((Time.time * speed), perlin_offsety_z) - 0.5f);

        Light light = GetComponent<Light>();
        light.intensity = light_min + light_amp;

        Vector3 light_pos = light.transform.position;
        light_pos.y = Main.LIGHT_HEIGHT + boby;
        light_pos.x = 0 + bobx;
        light_pos.z = 1 + bobz;
        light.transform.localPosition = light_pos;// new Vector3(0, Main.LIGHT_HEIGHT + boby, 2);// = light_pos;
       
    }
}
