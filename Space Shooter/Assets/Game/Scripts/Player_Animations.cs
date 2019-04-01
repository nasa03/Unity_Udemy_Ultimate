using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animations : MonoBehaviour
{
    private Animator TheAnimator;
    // Start is called before the first frame update
    void Start()
    {
        TheAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePosition;
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if (mousePosition.x > transform.position.x)
            {
                TheAnimator.SetBool("Turn_Right", true);
                TheAnimator.SetBool("Turn_Left", false);
            }
            else if (mousePosition.x < transform.position.x)
            {
                TheAnimator.SetBool("Turn_Right", false);
                TheAnimator.SetBool("Turn_Left", true);
            }
            else{
                TheAnimator.SetBool("Turn_Right", false);
                TheAnimator.SetBool("Turn_Left", false);
            }
        }

        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            TheAnimator.SetBool("Turn_Right", true);
            TheAnimator.SetBool("Turn_Left", false);
        } else if (Input.GetAxis("Horizontal") < 0.0f)
        {
            TheAnimator.SetBool("Turn_Right", false);
            TheAnimator.SetBool("Turn_Left", true);
        }
        else if(!Input.GetMouseButton(0))
        {
            TheAnimator.SetBool("Turn_Right", false);
            TheAnimator.SetBool("Turn_Left", false);
        }


    }
}
