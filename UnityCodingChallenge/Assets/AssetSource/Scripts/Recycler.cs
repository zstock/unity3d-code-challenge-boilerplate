using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recycler : MonoBehaviour
{
    public static Recycler instance;

    #region PRIVATE
    private static List<GameObject> pooledObjects = new List<GameObject>();
    #endregion

    void Awake()
    {
        instance = this;
    }

    public static GameObject TryGet<T> (T type)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy && pooledObjects[i].GetComponent<T>() != null)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
