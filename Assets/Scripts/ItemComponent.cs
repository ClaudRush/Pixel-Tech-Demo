using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] private ItemType type;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Item item;
    public Item Item => item;

    public void Destroy(GameObject gameObject)
    {
        MonoBehaviour.Destroy(gameObject);
    }

    private void Start()
    {
        GameManager.instance.itemsContainer.Add(gameObject, this);
        item = GameManager.instance.itemDataBase.GetItemOfID((int)type);
        spriteRenderer.sprite = item.Icon;
    }
}

public enum ItemType
{
    ForcePoiton = 1,
    DamagePoiton = 2,
    ArmorPoiton = 3
}