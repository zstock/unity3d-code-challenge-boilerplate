using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayerInventory : MonoBehaviour
{
    [SerializeField]
    private RectTransform _inventoryParent;
    [SerializeField]
    private GameObject _inventoryPrefab;

    private void OnMouseDown()
    {
        Instantiate(_inventoryPrefab, _inventoryParent);
    }
}
