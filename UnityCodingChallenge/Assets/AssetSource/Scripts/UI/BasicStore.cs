using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BasicStore : MonoBehaviour
{
    public static StoreSO currentStore;

    [SerializeField]
    private Text _storeNameText;
    [SerializeField]
    private Text _heldMoneyText;
    [SerializeField]
    private Button _backButton;
    [SerializeField]
    private RectTransform _content;
    [SerializeField]
    private GameObject _storeListingPrefab;

    private void Start()
    {
        _backButton.onClick.AddListener(BackButton);
        _storeNameText.text = currentStore.storeName;

        UpdateHeldMoney();
        PlayerData.onValueChanged += UpdateHeldMoney;

        PopulateStore();
    }

    private void PopulateStore()
    {
        foreach (ItemSO item in currentStore.inventory)
        {
            GameObject newListing = Instantiate(_storeListingPrefab, _content);
            newListing.GetComponent<StoreListing>().Setup(item);
        }
    }

    private void UpdateHeldMoney()
    {
        _heldMoneyText.text = PlayerData.GetPlayerMoney().ToString();
    }

    private void BackButton()
    {
        SceneManager.LoadScene("CodingChallengeScene");
    }

    private void OnDestroy()
    {
        PlayerData.onValueChanged -= UpdateHeldMoney;
    }
}
