using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyExplosion;
    [SerializeField]
    private float speed = 3.0f;
    private Vector3 spawnPosition;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

   
    void Movement()
    {
        float camHeight;
        float camWidth;
        Camera cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        Vector3 Direction = new Vector3(0, -1, transform.position.z);

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
            // transform.position = new Vector3(transform.position.x, cam.transform.position.y - camHeight / 2.0f, transform.position.z);
            //ReSpawn();
            Destroy(gameObject);
        }
    }

    void ReSpawn()
    {
        float camHeight;
        float camWidth;
        Camera cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        float min = cam.transform.position.x - camWidth / 2.0f;
        float max = cam.transform.position.x + camWidth / 2.0f;
        Vector3 position = new Vector3(Random.Range(min, max), cam.transform.position.y + camHeight / 2.0f, transform.position.z);
        transform.position = position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            
            Instantiate(EnemyExplosion, transform.position, Quaternion.identity);
            //ReSpawn();
            _uiManager.UpdateScore(5);
            Destroy(gameObject);
            
        }

        if (other.tag=="Projectile")
        {
            Instantiate(EnemyExplosion, transform.position, Quaternion.identity);
            //ReSpawn();
            _uiManager.UpdateScore(10);
            Destroy(gameObject);
        }
    }


}
