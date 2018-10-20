using UnityEngine;
using System.Collections;

public class ObjectClicker : MonoBehaviour {

    private GameObject player_obj;
    private PlayerAudio player_audio;

	// Use this for initialization
	void Start ()
    {
        player_obj = GameObject.FindWithTag("Player");
        player_audio = player_obj.GetComponentInChildren<PlayerAudio>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnMouseUpAsButton()
    {
        float distance = Vector3.Distance(transform.position, player_obj.transform.position);
        if (distance > Main.CELL_SIZE) return;

        // TODO! 
        // Remove hardcoded logic!
        MapFeature door = (MapFeature)Main.map.data[2, 2].getFeature();
        door.toggle();
        door.blocking = !door.active;

        door = (MapFeature)Main.map.data[3, 4].getFeature();
        door.toggle();
        door.blocking = !door.active;

        player_audio.playMisc(PlayerAudio.CLICK);
        Animation anim = this.GetComponent<Animation>();
        anim.Play();
    }
}
