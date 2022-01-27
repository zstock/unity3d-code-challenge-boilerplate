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
    private GameObject _rewardPrefab;

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
    const int AGE_MIN = 0;
    const int AGE_MAX = 20;
    const int MIN_REWARDS = 1;
    const int MAX_REWARDS = 5;

    const float MAX_REWARD_SPAWN_DISTANCE = 3.0f;
    #endregion

    void Start()
    {
        CreateHerd();
        _spawnTimer = new Timer();
        _spawnTimer.onFinished += SpawnLlama;
    }

    void Update()
    {
        if (_spawnTimer != null)
        {
            _spawnTimer.Tick();
        }
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
        newLlama.age = Random.Range(AGE_MIN, AGE_MAX);
        newLlama.transform.localScale = CalculateAgeScale(newLlama.age);
        newLlama.SetDiet(GetRandomEnum<LlamaDiet>());
        newLlama.onDeath += OnLlamaDeath;
    }

    /// <summary>
    /// Deactivate the llama, and remove it from the herd.
    /// </summary>
    /// <param name="whoDied"></param>
    private void OnLlamaDeath(Llama whoDied)
    {
        SpawnRewards(whoDied.transform.position);

        whoDied.onDeath -= OnLlamaDeath;
        _herd.Remove(whoDied);

        float spawnTime = Random.Range(MIN_SPAWN_TIME, MAX_SPAWN_TIME);
        _spawnTimer.StartTimer(spawnTime);
    }

    private void SpawnRewards(Vector3 pos)
    {
        int numRewards = Random.Range(MIN_REWARDS, MAX_REWARDS);
        for (int i = 0; i < numRewards; i++)
        {
            GameObject newReward = Recycler.TryGet<Coin>();
            if(!newReward)
            {
                newReward = Instantiate(_rewardPrefab);
                Recycler.AddToPool(newReward);
            }

            float newX = Random.Range(0.0f, MAX_REWARD_SPAWN_DISTANCE);
            float newZ = Random.Range(0.0f, MAX_REWARD_SPAWN_DISTANCE);
            newReward.transform.position = pos + new Vector3(newX, 0.0f, newZ);
        }
    }
    #endregion
    #region UTILITY
    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    /// <summary>
    /// Calculate the size of the Llama based on its age.
    /// Keeping the age scale simple for now. Could make more complex later if needed.
    /// </summary>
    /// <param name="age">How old is this Llama?</param>
    /// <returns></returns>
    private Vector3 CalculateAgeScale(float age)
    {
        return Vector3.one * (1.0f + age / 100.0f);
    }
    #endregion
}
