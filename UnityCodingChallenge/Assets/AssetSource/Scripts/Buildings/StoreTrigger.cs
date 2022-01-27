using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreTrigger : MonoBehaviour
{
    [SerializeField]
    private StoreSO _storeType;
    private void OnMouseDown()
    {
        BasicStore.currentStore = _storeType;
        Debug.LogFormat("Store Trigger Entered for {0}", BasicStore.currentStore.storeName);

        SceneManager.LoadScene("StoreScene");
    }
}
