using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsDisplay : MonoBehaviour
{
    [SerializeField]
    private Text _healthText;
    [SerializeField]
    private Text _moneyText;

    void Start()
    {
        PlayerData.onValueChanged += UpdateStatsDisplay;
        UpdateStatsDisplay();
    }

    private void UpdateStatsDisplay()
    {
        if(_healthText.text != PlayerData.GetPlayerHealth().ToString())
        {
            _healthText.text = PlayerData.GetPlayerHealth().ToString();
        }

        if (_moneyText.text != PlayerData.GetPlayerMoney().ToString())
        {
            _moneyText.text = PlayerData.GetPlayerMoney().ToString();
        }
    }

    private void OnDestroy()
    {
        PlayerData.onValueChanged -= UpdateStatsDisplay;
    }
}
