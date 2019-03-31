using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenu;
    public Sprite[] LivesSprites;
    public Image Player_Lives;
    public int Score=0;
    public Text ScoreText;
    private bool isMainMenu;
    


    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(Score);
        isMainMenu = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int currentLives)
    {
        Player_Lives.sprite = LivesSprites[currentLives];
    }

    public void UpdateScore(int currentScore)
    {
        if(currentScore<=0)
        {
            Score = currentScore;
        }
        Score += currentScore;
        ScoreText.text = "Score: " + Score;
    }

    public void SetMainMenu(bool isActive)
    {
        isMainMenu = isActive;
        MainMenu.SetActive(isMainMenu);
    }

}
