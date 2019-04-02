using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyShipPrefab=null;
    [SerializeField]
    private GameObject[] PowerUpsPrefab = null;
    private bool isSpawingEnemy=false;
    private bool isSpawingPoweup = false;
    private GameManager _gameManager= null;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        EnemyShipPrefab = Instantiate(EnemyShipPrefab, Vector3.zero, Quaternion.identity);
        EnemyShipPrefab.SetActive(false);

        PowerUpsPrefab[0]=Instantiate(PowerUpsPrefab[0], Vector3.zero, Quaternion.identity);
        PowerUpsPrefab[0].SetActive(false);
        PowerUpsPrefab[1] = Instantiate(PowerUpsPrefab[1], Vector3.zero, Quaternion.identity);
        PowerUpsPrefab[1].SetActive(false);
        PowerUpsPrefab[2] = Instantiate(PowerUpsPrefab[2], Vector3.zero, Quaternion.identity);
        PowerUpsPrefab[2].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(_gameManager !=null)
        {
            if(!_gameManager.getGameOver())
            {
                if (!isSpawingEnemy)
                {
                    SpawnEnemyOn();
                }

                if (!isSpawingPoweup)
                {
                    SpawnPowerUpOn();
                }
            }
        }
        
    }

    private void SpawnEnemyOn()//Spawns a new enemy in the game
    {
        isSpawingEnemy = true;
        StartCoroutine(SpawnEnemyRoutine());

        float camHeight;
        float camWidth;
        Camera cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        float min = cam.transform.position.x - camWidth / 2.0f;
        float max = cam.transform.position.x + camWidth / 2.0f;
        Vector3 position = new Vector3(Random.Range(min, max), cam.transform.position.y + camHeight / 2.0f, 0);
        //Instantiate(EnemyShipPrefab, position, Quaternion.identity);
        EnemyShipPrefab.SetActive(true);
        EnemyShipPrefab.transform.position = position;
    }

    public IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(5);
        isSpawingEnemy = false;
    }

    private void SpawnPowerUpOn()
    {
        isSpawingPoweup = true;
        StartCoroutine(SpawnPowerUpRoutine());

        float camHeight;
        float camWidth;
        Camera cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        float min = cam.transform.position.x - camWidth / 2.0f;
        float max = cam.transform.position.x + camWidth / 2.0f;
        Vector3 position = new Vector3(Random.Range(min, max), cam.transform.position.y + camHeight / 2.0f, 0);
        int RandomPower = Random.Range(0, 3);
        //Instantiate(PowerUpsPrefab[RandomPower], position, Quaternion.identity);
        PowerUpsPrefab[RandomPower].SetActive(true);
        PowerUpsPrefab[RandomPower].transform.position = position;
    }

    public IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(10);
        isSpawingPoweup = false;
    }
}
