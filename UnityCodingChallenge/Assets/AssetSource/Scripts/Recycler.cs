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

    /// <summary>
    /// Attempt to get an existing object object with a specific component attached
    /// </summary>
    /// <returns></returns>
    public static GameObject TryGet<T> ()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy && pooledObjects[i].GetComponent<T>() != null)
            {
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Add an object to the pool
    /// </summary>
    /// <param name="newObj"></param>
    public static void AddToPool (GameObject newObj)
    {
        pooledObjects.Add(newObj);
    }
}
