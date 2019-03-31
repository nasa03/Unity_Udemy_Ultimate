using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject ShieldObject;
    [SerializeField]
    private GameObject Explosion;
   // [SerializeField]
    //private int Lives;
    [SerializeField]
    private int Energy=3;
    private bool isShielActive = false;
    private bool isTripleShot = false;
    private bool isSpeedBoost = false;
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject tripleShot;
    private bool isShooting;
    public float shootingTime;
    private float currentShootingTime;
    //public Transform projecileSpawnPoint;
    public int pooledAmount;
    private List<GameObject> ProjectileList;
    public GameObject ObjectPooler;
    private UIManager _uiManager;
    private GameManager _gameManager;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        ShieldObject.SetActive(false);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        if (_uiManager !=null)
        {
            _uiManager.UpdateLives(Energy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
   
        Shooting();
        

    }

    private void Shooting()
    {
        if (Input.GetMouseButton(0) && !isShooting)
        //if (!isShooting)
        {
            isShooting = true;
            currentShootingTime = shootingTime;
            Fire();
        }

        if (currentShootingTime > 0)
        {
            currentShootingTime -= Time.deltaTime;
        }
        else
        {
            isShooting = false;
        }
        
    }

    void Fire()
    {
        _audioSource.Play();
        Vector3 LaserPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if(isTripleShot)
        {
            Instantiate(tripleShot, LaserPos, Quaternion.identity);
        }
        else
        {
            Instantiate(laser, LaserPos, Quaternion.identity);
        }
        
        //Instantiate(laserPrefab);
        /*
        GameObject obj = ObjectPooler.GetComponent<ObjectPooler>().GetPooledObject();
        if (obj == null)
        {
            return;
        }

        obj.transform.position = projecileSpawnPoint.position;
        obj.transform.rotation = transform.rotation;
        obj.transform.localScale = transform.localScale;
        obj.SetActive(true);
        obj.GetComponent<Projectile>().isAlive = true;
        obj.GetComponent<Projectile>().isAllied = true;
        obj.GetComponent<Projectile>().shootDirection1 = new Vector3(1.0f, 0.0f, 0.0f);
        */
    }


    private void Movement()
    {
        float camHeight;
        float camWidth;
        Camera cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        
        Vector3 mousePosition;
        
        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if (isSpeedBoost)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(mousePosition.x, mousePosition.y,0), Time.deltaTime * (speed * 1.5f));
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(mousePosition.x, mousePosition.y, 0), Time.deltaTime * (speed));
            }
           
        }else if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") !=0)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float vecticallInput = Input.GetAxis("Vertical");
            Vector3 Direction = new Vector3(horizontalInput, vecticallInput, transform.position.z);

            
            if (isSpeedBoost)
            {
                transform.Translate(Direction * Time.deltaTime * (speed * 1.5f));
            }else
            {
                transform.Translate(Direction * Time.deltaTime * speed);
            }
            
        }

        

        // 


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


    public void ShieldPowerUpOn()
    {
        isShielActive = true;
        ShieldObject.SetActive(isShielActive);
        StartCoroutine(ShieldRoutine());
    }

    public IEnumerator ShieldRoutine()
    {
        yield return new WaitForSeconds(10);
        isShielActive = false;
        ShieldObject.SetActive(isShielActive);
    }

    public void SpeedPowerUpOn()
    {
        isSpeedBoost = true;
        StartCoroutine(SpeedPowerUpRoutine());
    }

    public IEnumerator SpeedPowerUpRoutine()
    {
        yield return new WaitForSeconds(5);
        isSpeedBoost = false;
    }

    public void TripleShowPowerUpOn()
    {
        isTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        isTripleShot = false;
    }

    void Damage()
    {
        if (isShielActive)
        {
            isShielActive = false;
            ShieldObject.SetActive(isShielActive);
            return;
        }

        Energy--;
        if (_uiManager != null)
        {
            _uiManager.UpdateLives(Energy);
            if (Energy<=0)
            {
                _uiManager.UpdateLives(0);
            }
            
        }

        if (Energy <= 0)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            if(_uiManager!=null)
            {
                _uiManager.SetMainMenu(true);
            }

            if(_gameManager !=null)
            {
                _gameManager.setGameOver(true);
            }
           
            
            Destroy(gameObject);
        }
    }

    public int getCurrentEnergy()
    {
        return Energy;
    }

    public void setCurrentEnergy(int currentEnergy)
    {
        Energy = currentEnergy;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Enemy")
        {
            Damage();
        }
    }
}
