using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
    public GameObject player_object;
    public Camera cam;

    public static Map map;
    public static PlayerData player;

    public const int CELL_SIZE = 20;
    public const float MAX_LERP_TIME = 0.6f;
    public const float HEAD_HEIGHT = 10f;
    public const float LIGHT_HEIGHT = 12f;

    // Use this for initialization
    void Start ()
    {
        // Init Player
        player = new PlayerData(player_object);

        player_object.GetComponentInChildren<Light>().transform.position.Set(0, LIGHT_HEIGHT, 20); // = LIGHT_HEIGHT;
        //player_object.GetComponent("Camera").transform.position.y = LIGHT_HEIGHT;

        // Load and init level and map
        MapBuilder mb = (MapBuilder)GetComponent("MapBuilder");
        map = mb.buildMap("level-1");

        float new_w = 1f - 240f / Screen.width;
        cam.rect = new Rect(0, 0, new_w, 1f);

    }

    // Update is called once per frame
    void Update ()
    {

    }

    public PlayerAudio getAudio()
    {
        return player_object.GetComponentInChildren<PlayerAudio>();
    }

}
