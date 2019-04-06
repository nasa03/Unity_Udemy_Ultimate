using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float Speed = 3.5f;
    private float Gravity = 9.81f;
    private CharacterController TheCharacterController;
    [SerializeField]
    private GameObject MuzzleFlash;
    [SerializeField]
    private GameObject ObjectPooler;
    
    [SerializeField]
    private int reloadingTime = 5;
    private bool isReloading = false;
    private LevelManager TheLevelManager = null;
    [SerializeField]
    private GameObject TheWeapon = null; 
    private int currentAmmo = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        TheCharacterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        TheLevelManager = GameObject.FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
        currentAmmo=TheLevelManager.GetMaxAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        if(TheLevelManager.isWeaponActive() && !TheWeapon.gameObject.activeSelf)
        {
            TheWeapon.gameObject.SetActive(true);
        }
        Movement();

        Shooting();
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

    private void Shooting()
    {
        if(!TheLevelManager.isWeaponActive())
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        if (Input.GetMouseButton(0) && currentAmmo > 0 && !isReloading)
        {
            MuzzleFlash.SetActive(true);
            currentAmmo--;
            TheLevelManager.UpdateAmmo(currentAmmo);
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                
                GameObject obj = ObjectPooler.GetComponent<ObjectPooler>().GetPooledObject();
                if (obj != null)
                {
                    obj.SetActive(true);
                    obj.transform.position = hitInfo.point;
                    obj.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
                }
            }
        }else{
            MuzzleFlash.SetActive(false);
        }
        


        if (Input.GetKeyDown(KeyCode.Escape) && !Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Reload()
    {
        if(isReloading)
        {
            return;
        }
        isReloading = true;
        currentAmmo = TheLevelManager.GetMaxAmmo();
        TheLevelManager.ReloadAmmo();
        StartCoroutine(ReloadRoutine());
    }
    private IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(reloadingTime);
        isReloading = false;
    }

}
