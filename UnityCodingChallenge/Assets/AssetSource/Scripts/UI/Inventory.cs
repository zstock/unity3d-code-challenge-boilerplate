using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject _inventoryListingPrefab;
    [SerializeField]
    private RectTransform _inventoryContents;
    [SerializeField]
    private Button _closeButton;

    void Start()
    {
        _closeButton.onClick.AddListener(CloseButtonClicked);
        DisplayInventory();
    }

    private void DisplayInventory()
    {
        foreach (KeyValuePair<ItemSO,int> item in PlayerData.GetInventory())
        {
            CreateInventoryListing(item.Key.itemName, item.Value);
        }
    }

    private void CreateInventoryListing(string name, int amount)
    {
        InventoryListing newListing = Instantiate(_inventoryListingPrefab, _inventoryContents).GetComponent<InventoryListing>();
        newListing.Setup(name, amount);
    }

    private void CloseButtonClicked()
    {
        Destroy(gameObject);
    }
}
