using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int coinsCount;
    public int CoinsCount { get => coinsCount; set => coinsCount = value; }
    [SerializeField] public Text coinsText;
    public BuffReciever buffReciever;

    private List<Item> items;
    public List<Item> Items => items;

    private void Start()
    {
        coinsText.text = coinsCount.ToString();
        items = new List<Item>();
    }

    #region Singleton
    private void Awake()
    {
        instance = this;
    }
    public static PlayerInventory instance { get; set; }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.itemsContainer.ContainsKey(collision.gameObject))
        {
            var itemComponent = GameManager.instance.itemsContainer[collision.gameObject];
            items.Add(itemComponent.Item);
            itemComponent.Destroy(collision.gameObject);
        }
    }
}
