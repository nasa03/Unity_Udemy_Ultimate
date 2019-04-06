using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text coinsText;
    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private Image inventoryCoin;

    public void SetInventory(bool isActive)
    {
        inventoryCoin.gameObject.SetActive(isActive);
    }

    public bool isInventoryActive()
    {
        return inventoryCoin.gameObject.activeSelf;
    }



    public void UpdateAmmo(int count)
    {
        ammoText.text = "Ammo: " + count;
    }

    public void UpdateCoins(int count)
    {
        coinsText.text = "Coins: " + count;
    }

}
