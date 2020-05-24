using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private int heal = 10;
    public int GetHeal => heal;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.healthsContainer.ContainsKey(collision.gameObject))
        {
            var health = GameManager.instance.healthsContainer[collision.gameObject];
            health.SetHealth(heal);
            Debug.Log("Вы вылечились на " + heal + ". Ваше текущее здоровье равно: " + health.GetHealth);
            StartDestroy();
        }
    }

    private void StartDestroy()
    {
        animator.SetTrigger("StartDestroy");
    }
    private void EndDestroy()
    {
        Destroy(gameObject);
    }
}
