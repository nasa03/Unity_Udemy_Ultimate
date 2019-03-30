using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyShipPrefab;
    [SerializeField]
    private GameObject[] PowerUpsPrefab;
    private bool isSpawingEnemy=false;
    private bool isSpawingPoweup = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSpawingEnemy)
        {
            SpawnEnemyOn();
        }

        if(!isSpawingPoweup)
        {
            SpawnPowerUpOn();
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
        Instantiate(EnemyShipPrefab, position, Quaternion.identity);
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
        Instantiate(PowerUpsPrefab[RandomPower], position, Quaternion.identity);
    }

    public IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(10);
        isSpawingPoweup = false;
    }
}
