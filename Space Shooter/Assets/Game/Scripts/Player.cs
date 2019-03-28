using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    
   // public Transform Camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    private void Movement()
    {
        float camHeight;
        float camWidth;
        Camera cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        float horizontalInput = Input.GetAxis("Horizontal");
        float vecticallInput = Input.GetAxis("Vertical");
        Vector3 Direction = new Vector3(horizontalInput, vecticallInput, transform.position.z);
        transform.Translate(Direction * Time.deltaTime * speed);

        if (transform.position.x > cam.transform.position.x + camWidth / 2.0f)
        {
            //transform.position = new Vector3(cam.transform.position.x + camWidth / 2.0f, transform.position.y, transform.position.z);
            transform.position = new Vector3(cam.transform.position.x - camWidth / 2.0f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < cam.transform.position.x - camWidth / 2.0f)
        {
            //transform.position = new Vector3(cam.transform.position.x - camWidth / 2.0f, transform.position.y, transform.position.z);
            transform.position = new Vector3(cam.transform.position.x + camWidth / 2.0f, transform.position.y, transform.position.z);
        }


        if (transform.position.y > cam.transform.position.y + camHeight / 2.0f)
        {
            transform.position = new Vector3(transform.position.x, cam.transform.position.y + camHeight / 2.0f, transform.position.z);
        }
        else if (transform.position.y < cam.transform.position.y - camHeight / 2.0f)
        {
            transform.position = new Vector3(transform.position.x, cam.transform.position.y - camHeight / 2.0f, transform.position.z);
        }
    }
}
