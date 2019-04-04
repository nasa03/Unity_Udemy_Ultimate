using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float Speed = 3.5f;
    private float Gravity = 9.81f;
    private CharacterController TheCharacterController;
    
    // Start is called before the first frame update
    void Start()
    {
        TheCharacterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
            RaycastHit hitInfo;
            if(Physics.Raycast(rayOrigin,out hitInfo))
            {
                //if(!hitInfo.transform.name.Equals("Player"))
                {
                    Debug.Log("Hit: " + hitInfo.transform.name);
                }
                
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        Movement();
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * Speed;
        velocity.y -= Gravity;
        velocity = transform.transform.TransformDirection(velocity);
        TheCharacterController.Move(velocity * Time.deltaTime);
    }
}
