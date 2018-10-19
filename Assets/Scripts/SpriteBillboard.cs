using UnityEngine;
using System.Collections;

public class SpriteBillboard : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = FindObjectOfType<Camera>();
    }   

    void Update()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}