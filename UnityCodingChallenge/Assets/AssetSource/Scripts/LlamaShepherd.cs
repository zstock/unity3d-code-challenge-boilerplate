using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LlamaShepherd : MonoBehaviour
{
    #region SERIALIZED PRIVATE
    [SerializeField]
    private GameObject _llamaPrefab;
    [SerializeField]
    private List<GameObject> _possibleRewards;

    #endregion

    #region PUBLIC
    public enum LlamaDiet { Grass, Flower, Shrub };
    #endregion

    #region PRIVATE
    private List<Llama> _herd = new List<Llama>();
    private Timer _spawnTimer;

    #endregion

    #region CONSTANTS
    const int MAX_LLAMAS = 5;
    const int MIN_SPAWN_TIME = 3;
    const int MAX_SPAWN_TIME = 7;
    const int MIN_STARTING_HEALTH = 50;
    const int MAX_STARTING_HEALTH = 100;
    const int STARTING_AGE = 0;
    const float MAX_STARTING_DISTANCE = 20;
    
    private readonly Vector3 DEFAULT_STARTING_POSITION = new Vector3(-5,1,5);
    #endregion

    void Start()
    {
        CreateHerd();
    }

    void Update()
    {
        if(_spawnTimer != null)
            _spawnTimer.Tick();
    }

    #region BIRTH/DEATH
    /// <summary>
    /// Create a herd of Llamas.
    /// </summary>
    private void CreateHerd()
    {
        while (_herd.Count < MAX_LLAMAS)
        {
            SpawnLlama();
        }
    }
    
    /// <summary>
    /// Create a single Llama.
    /// Attempts to reuse an existing Llama if possible.
    /// </summary>
    private void SpawnLlama()
    {
        GameObject newLlamaObj = Recycler.TryGet<Llama>();
        if (!newLlamaObj)
        {
            //If there were no available llamas, we'll spawn a new one and add it to the Recycler.
            newLlamaObj = Instantiate(_llamaPrefab);
            Recycler.AddToPool(newLlamaObj.gameObject);
        }

        Llama newLlama = newLlamaObj.GetComponent<Llama>();
        _herd.Add(newLlama);

        newLlama.health = Random.Range(MIN_STARTING_HEALTH, MAX_STARTING_HEALTH);
        newLlama.age = STARTING_AGE;
        newLlama.SetDiet(GetRandomEnum<LlamaDiet>());
        newLlama.onDeath += OnLlamaDeath;


        float range = Random.Range(-MAX_STARTING_DISTANCE, MAX_STARTING_DISTANCE);
        NavMeshHit hit;
        if(NavMesh.SamplePosition(Vector3.zero, out hit, range, NavMesh.AllAreas))
        {
            newLlamaObj.transform.position = hit.position;
        }
        else
        {
            //In case we fail to find a position on the mesh...
            newLlamaObj.transform.position = DEFAULT_STARTING_POSITION;
        }
    }

    /// <summary>
    /// Deactivate the llama, and remove it from the herd.
    /// </summary>
    /// <param name="whoDied"></param>
    private void OnLlamaDeath(Llama whoDied)
    {
        whoDied.onDeath -= OnLlamaDeath;
        _herd.Remove(whoDied);

        float spawnTime = Random.Range(MIN_SPAWN_TIME, MAX_SPAWN_TIME);
        _spawnTimer.StartTimer(spawnTime);
    }
    #endregion
    #region UTILITY
    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }
    #endregion
}
