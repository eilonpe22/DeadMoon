using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;

    [SerializeField] protected bool isDead;


    private void Start()
    {
        InitVariables();
    }

    public void CheckHealth()
    {
        if(health <= 0)
        {
            health = 0;
            isDead = true;

        }
        if(health >= maxHealth)
        {

            health = maxHealth;
        }
    }

    public void Die()
    {
        isDead = true;
    }

    public void SetHealthTo(int healthSetTo)
    {
        health = healthSetTo;
        CheckHealth();

    }

    public void TakeDamage(int damage)
    {
        int healtAfterDamage = health - damage;
        SetHealthTo(healtAfterDamage);
    }

    public void Heal(int heal)
    {
        int healthAfterHeal = health + heal;
        SetHealthTo(healthAfterHeal);
    }

    public void InitVariables()
    {
        maxHealth = 100;
        SetHealthTo(maxHealth);
        isDead = false;
    }
   
}
