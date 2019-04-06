using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int coinValue = 1;
    private LevelManager TheLevelManager = null;
    // Start is called before the first frame update
    void Start()
    {
        TheLevelManager = GameObject.FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Coin::OnTriggerStay");
            if(Input.GetKeyDown(KeyCode.E))
            {
                //Destroy(gameObject);
                TheLevelManager.AddCoins(coinValue);
                gameObject.SetActive(false);
            }
            
        }
    }
}
