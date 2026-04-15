using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject ShopPanel;

    private bool isShopOpen = false; // Open = true => Active

    public void OnShopToggle()
    {
        isShopOpen = !isShopOpen;
        ShopPanel.SetActive(isShopOpen);
    }
}
