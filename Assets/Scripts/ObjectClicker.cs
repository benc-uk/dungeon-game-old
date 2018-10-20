using UnityEngine;
using System.Collections;

public class ObjectClicker : MonoBehaviour {

    private GameObject playerObj;
    private PlayerAudio playerAudio;

	// Use this for initialization
	void Start ()
    {
        playerObj = GameObject.FindWithTag("Player");
        playerAudio = playerObj.GetComponentInChildren<PlayerAudio>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnMouseUpAsButton()
    {
        float distance = Vector3.Distance(transform.position, playerObj.transform.position);
        if (distance > Main.CELL_SIZE) return;

        // TODO! 
        // Remove hardcoded logic!
        MapFeature door = (MapFeature)Main.map.data[2, 2].getFeature();
        door.toggle();
        door.blocking = !door.active;

        door = (MapFeature)Main.map.data[3, 4].getFeature();
        door.toggle();
        door.blocking = !door.active;

        playerAudio.playMisc(PlayerAudio.CLICK);
        Animation anim = this.GetComponent<Animation>();
        anim.Play();
    }
}
