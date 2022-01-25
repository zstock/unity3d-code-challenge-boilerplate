using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llama : MonoBehaviour
{
    const int DEATH_HEALTH = 0;

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
        gameObject.SetActive(false);
    }
}
