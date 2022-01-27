using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    #region CONSTANTS
    const int STARTING_HEALTH = 100;
    const int STARTING_MONEY = 100;
    #endregion

    #region PRIVATE
    private static int _currentHealth;
    private static int _currentMoney;
    private static Dictionary<ItemSO, int> _inventory = new Dictionary<ItemSO, int>();
    #endregion

    public static System.Action onValueChanged;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(instance);
        _currentHealth = PlayerPrefs.HasKey("Health") ? PlayerPrefs.GetInt("Health") : STARTING_HEALTH;
        _currentMoney = PlayerPrefs.HasKey("Money") ? PlayerPrefs.GetInt("Money") : STARTING_MONEY;
    }

    #region HEALTH
    public static int GetPlayerHealth()
    {
        return _currentHealth;
    }

    public static void ChangeHealth(int amount)
    {
        _currentHealth += amount;
        onValueChanged.Invoke();
    }

    public static void SetHealth(int amount)
    {
        _currentHealth = amount;
        onValueChanged.Invoke();
    }
    #endregion

    #region MONEY
    public static int GetPlayerMoney()
    {
        return _currentMoney;
    }

    public static void ChangePlayerMoney(int amount)
    {
        _currentMoney += amount;
        onValueChanged.Invoke();
    }

    public static void SetPlayerMoney (int amount)
    {
        _currentMoney = amount;
        onValueChanged.Invoke();
    }
    #endregion

    #region INVENTORY
    public static void AddToInventory(ItemSO item, int num)
    {
        int v;
        if(_inventory.TryGetValue(item, out v))
        {
            _inventory[item] = v + num;
        }
        else
        {
            _inventory.Add(item, num);
        }
    }

    public static void RemoveFromInventory(ItemSO item, int num)
    {
        int v;
        if (_inventory.TryGetValue(item, out v))
        {
            if (v - num <= 0)
            {
                _inventory.Remove(item);
            }
            else
            {
                _inventory[item] = v - num;
            }
        }
    }

    public static Dictionary<ItemSO, int> GetInventory()
    {
        return _inventory;
    }
    #endregion

    #region PLAYER PREFS
    public void SavePlayerStats()
    {
        PlayerPrefs.SetInt("Health", _currentHealth);
        PlayerPrefs.SetInt("Money", _currentMoney);
    }
    #endregion

    private void OnDestroy()
    {
        SavePlayerStats();
    }
}
