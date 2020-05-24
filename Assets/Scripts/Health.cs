using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    //public Action<int> onHealthChange = delegate { };
    [SerializeField] private int maxHealth;
    public int MaxHealth => maxHealth;
    [SerializeField] private int health;
    private Animator animator;
    public int GetHealth => health;
    private void Start()
    {
        GameManager.instance.healthsContainer.Add(gameObject, this);
        animator = GetComponent<Animator>();
    }
    public void TakeHit(int damage)
    {
        health -= damage;
        //onHealthChange(health);
        animator.SetTrigger("TakeDamage");
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetHealth(int bonusHealth)
    {
        health += bonusHealth;
        //onHealthChange(health);
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
