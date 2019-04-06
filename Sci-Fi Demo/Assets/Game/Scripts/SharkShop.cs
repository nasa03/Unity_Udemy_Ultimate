using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SharkShop : MonoBehaviour
{ 
    [SerializeField]
    private int WeaponPrice = 0;
    private LevelManager TheLevelManager = null;
   
    // Start is called before the first frame update
    void Start()
    {
        TheLevelManager = GameObject.FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
    }

    // Update is called once per frame
    /*
    void Update()
    {
        
    }
    */

    void OnTriggerStay(Collider other)
    {
        if(other.tag=="Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(TheLevelManager.GetTotalCoins() > 0)
                {
                    TheLevelManager.SubtractCoins(WeaponPrice);
                    TheLevelManager.GameWin();
                }else{
                    Debug.Log("Get out of here!!");
                }
                
            }
        }
    }
}
