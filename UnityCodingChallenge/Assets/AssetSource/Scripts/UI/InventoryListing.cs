using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryListing : MonoBehaviour
{
    [SerializeField]
    private Text _itemNameText;
    [SerializeField]
    private Text _itemQuantityText;


    public void Setup(string name, int num)
    {
        _itemNameText.text = name;
        _itemQuantityText.text = string.Format("x{0}",num.ToString());
    }
}
