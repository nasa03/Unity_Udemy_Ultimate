using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private UIManager TheUIManager = null;
    private int currentAmmo;
    [SerializeField]
    private int maxAmmo = 200;
    private int totalCoins=0;
    [SerializeField]
    private AudioSource ReloadSound = null;
    [SerializeField]
    private AudioSource PickUpSound=null;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        TheUIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        TheUIManager.UpdateAmmo(currentAmmo);
        TheUIManager.SetInventory(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(totalCoins > 0 && !TheUIManager.isInventoryActive())
        {
            TheUIManager.SetInventory(true);
        }
    }

    public void ReloadAmmo()
    {
        currentAmmo = maxAmmo;
        TheUIManager.UpdateAmmo(currentAmmo);
        ReloadSound.Play();
    }
    public void UpdateAmmo(int count)
    {
        currentAmmo=count;
        
        
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public void AddCoins(int value)
    {
        totalCoins += value;
        TheUIManager.UpdateCoins(totalCoins);
        PickUpSound.Play();
    }

    public void SubtractCoins(int value)
    {
        totalCoins -= value;
        TheUIManager.UpdateCoins(totalCoins);
    }
    public int GetTotalCoins()
    {
        return totalCoins;
    }
}
