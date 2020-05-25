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
    [SerializeField] private float damageForce;
    public int GetHealth => health;
    private void Start()
    {
        GameManager.instance.healthsContainer.Add(gameObject, this);
        animator = GetComponent<Animator>();
    }
    public void TakeHit(int damage, GameObject attacker)
    {
        health -= damage;
        //onHealthChange(health);
        if (Player.instance != null)
        {
            Player.instance.IsBlockMovement = true;
            Player.instance.Rb.AddForce(transform.position.x < attacker.transform.position.x ? 
                new Vector2(-damageForce, 3) : new Vector2(damageForce, 3), ForceMode2D.Impulse);
        }
        animator.SetTrigger("TakeDamageTrigger");
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
