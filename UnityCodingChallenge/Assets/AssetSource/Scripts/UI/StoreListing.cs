using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreListing : MonoBehaviour
{
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _priceText;
    [SerializeField]
    private Button _purchaseButton;

    private ItemSO _item;
    
    void Start()
    {
        _purchaseButton.onClick.AddListener(PurchaseItem);
    }

    private void PurchaseItem()
    {
        if(PlayerData.GetPlayerMoney() >= _item.purchasePrice)
        {
            PlayerData.ChangePlayerMoney(-_item.purchasePrice);
            PlayerData.AddToInventory(_item, 1);
        }
    }

    public void Setup(ItemSO item)
    {
        _item = item;
        _nameText.text = _item.itemName;
        _priceText.text = _item.purchasePrice.ToString();
    }
}
