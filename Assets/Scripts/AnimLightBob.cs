using UnityEngine;
using System.Collections;

public class AnimLightBob : MonoBehaviour {
    public float lightScale = 1.0F;
    public float lightMin = 1.0F;
    public float bobSize = 1.0F;
    public float speed = 1.0F;

    private float perlinOffsetX;
    private float perlinOffsetY;
    private float perlinOffsetZ;

    void Start()
    {
        perlinOffsetX = Random.Range(0f, 1000f);
        perlinOffsetY = Random.Range(0f, 1000f);
        perlinOffsetZ = Random.Range(0f, 1000f);
    }

    void Update()
    {
        // Update is called once per frame
        float light_amp = lightScale * Mathf.PerlinNoise(0 + (Time.time * speed), 0.0F);

        float bobx = bobSize * (Mathf.PerlinNoise((Time.time * speed), perlinOffsetX) - 0.5f);
        float boby = bobSize * (Mathf.PerlinNoise((Time.time * speed), perlinOffsetY) - 0.5f);
        float bobz = bobSize * (Mathf.PerlinNoise((Time.time * speed), perlinOffsetZ) - 0.5f);

        Light light = GetComponent<Light>();
        light.intensity = lightMin + light_amp;

        Vector3 light_pos = light.transform.position;
        light_pos.y = Main.LIGHT_HEIGHT + boby;
        light_pos.x = 0 + bobx;
        light_pos.z = 1 + bobz;
        light.transform.localPosition = light_pos;// new Vector3(0, Main.LIGHT_HEIGHT + boby, 2);// = light_pos;
       
    }
}
