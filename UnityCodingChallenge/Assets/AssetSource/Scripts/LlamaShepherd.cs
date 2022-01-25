using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamaShepherd : MonoBehaviour
{
    #region SERIALIZED PRIVATE
    [SerializeField]
    private GameObject _llamaPrefab;
    #endregion

    #region PUBLIC
    public enum LlamaDiet { Grass, Flower, Shrub };
    #endregion

    #region PRIVATE
    private List<Llama> _herd;

    #endregion

    #region CONSTANTS
    const int MAX_LLAMAS = 5;
    const int MIN_SPAWN_TIME = 3;
    const int MAX_SPAWN_TIME = 7;
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Add llamas to our current herd.
    /// </summary>
    /// <param name="fill">Should we create an entire herd, or just add a single llama?</param>
    private void ReplenishHerd(bool fill)
    {
        if (_herd.Count >= MAX_LLAMAS)
            return;


    }
}
