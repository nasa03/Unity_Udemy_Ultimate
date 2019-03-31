using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float Speed = 3.0f;
    [SerializeField]
    private int powerUpId;//0= triple shot, 1= speed boost, 2= shields
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);

        float camHeight;
        float camWidth;
        Camera cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

        if (transform.position.y < cam.transform.position.y - camHeight / 2.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D " + other.name);
        if (other.tag=="Player")
        {
            Player player = FindObjectOfType<Player>();
            //Enable triple shot
            switch (powerUpId)
            {
                case 0://Triple shot
                    player.TripleShowPowerUpOn();
                    break;
                case 1://Speed boost
                    player.SpeedPowerUpOn();
                    break;
                case 2://Shields
                    player.ShieldPowerUpOn();
                    break;
            }
            
            Destroy(gameObject);
        }
       
    }
}
