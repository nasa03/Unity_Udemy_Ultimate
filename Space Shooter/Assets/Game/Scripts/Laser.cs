﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if(!gameObject.GetComponent<Renderer>().isVisible)
        {
            //Destroy(gameObject);
            if (gameObject.transform.parent != null)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Enemy")
        {
            if(transform.parent != null)
            {
                //Destroy(transform.parent.gameObject);
            }
            //Destroy(gameObject);
            if (gameObject.transform.parent != null)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }
}
