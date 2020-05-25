using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;
    public int GetDamage => damage;
    [SerializeField]
    private Animator animator;
    private Health health;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.instance.healthsContainer.ContainsKey(collision.gameObject))
        {
            this.health = GameManager.instance.healthsContainer[collision.gameObject];   
        }
    }

    private void SetDamage()
    {
        if (health != null)
        {
            health.TakeHit(damage, gameObject);
        }
        health = null;
    }
}
