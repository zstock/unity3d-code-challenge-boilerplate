using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Llama : MonoBehaviour
{
    #region CONSTANTS
    const int DEATH_HEALTH = 0;
    const float MAX_STARTING_DISTANCE = 20;

    private readonly Vector3 DEFAULT_STARTING_POSITION = new Vector3(-5, 1, 5);
    #endregion

    #region LLAMA CUSTOMIZATION
    [SerializeField]
    private List<Color> _possibleColours;
    #endregion

    #region LLAMA STATS
    private int _health;
    public int health
    {
        get { return _health; }
        set
        {
            if (_health != value)
            {
                _health = value;
            }
        }
    }

    private int _age;
    public int age
    {
        get { return _age; }
        set
        {
            if (_age != value)
            {
                _age = value;
            }
        }
    }

    private LlamaShepherd.LlamaDiet diet;
    #endregion

    #region LLAMA NAVIGATION
    private NavMeshAgent _agent;
    [SerializeField]
    [Tooltip("How far should the aim to wander when a new destination is selected?")]
    private float _wanderRadius = 5;
    [SerializeField]
    [Tooltip("Time before Llama should select a new wander destination")]
    private float _newDestinationTime = 5.0f;
    private float _wanderTimer = 0.0f;
    #endregion

    public System.Action<Llama> onDeath;

    #region MONO
    /// <summary>
    /// Use OnEnable instead of Start because Start won't run if this Llama is being reused by the Recycler.
    /// </summary>
    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        transform.position = RandomNavSphere(Vector3.zero, MAX_STARTING_DISTANCE, -1);
        SelectNewDestination();

        int colIndex = Random.Range(0, _possibleColours.Count);
        GetComponentInChildren<Renderer>().material.color = _possibleColours[colIndex];
    }

    private void Update()
    {
        _wanderTimer += Time.deltaTime;
        if(_wanderTimer >= _newDestinationTime)
        {
            SelectNewDestination();
            _wanderTimer = 0.0f;
        }
    }
    #endregion
    #region LLAMA STATS
    public void SetDiet(LlamaShepherd.LlamaDiet newDietType)
    {
        if(newDietType != diet)
            diet = newDietType;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (CheckForDeath())
            Death();
    }

    public bool CheckForDeath()
    {
        if (health <= DEATH_HEALTH)
            return true;

        return false;
    }

    public void Death()
    {
        onDeath?.Invoke(this);
        gameObject.SetActive(false);//Set to inactive instead of delete so the Recycler can reuse this Llama
    }

    #endregion

    #region LLAMA NAVIGATION
    private void SelectNewDestination()
    {
        Vector3 newTarget = RandomNavSphere(transform.position, _wanderRadius, -1);
        _agent.SetDestination(newTarget);
    }

    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask))
        {
            return navHit.position;

        }
        else
        {
            return DEFAULT_STARTING_POSITION;
        }           
    }
    #endregion

    #region INTERACTION WITH PLAYER
    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogFormat("Collision with {0}", collision.gameObject.name);
        if (collision.gameObject.tag != "Player")//Not interested in collisions with anything other than the player. For now, at least.
            return;

        Death();
    }


    #endregion
}
