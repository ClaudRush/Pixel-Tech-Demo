using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Coin : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
       
        animator = GetComponent<Animator>();
        GameManager.instance.coinContainer.Add(gameObject, this);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            PlayerInventory.instance.CoinsCount++;
            PlayerInventory.instance.coinsText.text = PlayerInventory.instance.CoinsCount.ToString();
            Debug.Log("Монет: " + PlayerInventory.instance.CoinsCount);
            StartDestroy();
        }
    }

    public void StartDestroy()
    {
        animator.SetTrigger("StartDestroy");
    }
    public void EndDestroy()
    {
        Destroy(gameObject);
    }
}
