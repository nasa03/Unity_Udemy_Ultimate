using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public Player PlayerObject;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space)) && gameOver)
        {
            
            gameOver = false;

            Instantiate(PlayerObject, Vector3.zero, Quaternion.identity);
            _uiManager.SetMainMenu(false);
            _uiManager.UpdateScore(0);
        }
    }

    public void setGameOver(bool isActive)
    {
        gameOver = isActive;
    }

    public bool getGameOver()
    {
        return gameOver;
    }
}
