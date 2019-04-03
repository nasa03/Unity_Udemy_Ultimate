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
    }

    // Update is called once per frame
    void Update()
    {
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
