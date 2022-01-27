using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Store", order = 1)]
public class StoreSO : ScriptableObject
{
    public string storeName;
    public bool playerCanSell;
    public List<ItemSO> inventory;
}
