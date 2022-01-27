using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 2)]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int purchasePrice;
    public int sellPrice;
}
