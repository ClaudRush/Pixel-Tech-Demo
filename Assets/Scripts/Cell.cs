using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Cell : MonoBehaviour
{
    public Action Reload;
    [SerializeField] private Image icon;
    [SerializeField] private Item item;

    private void Awake()
    {
        icon.sprite = null;
    }
    public void Init(Item item)
    {
        this.item = item;
        icon.sprite = item.Icon;
    }
    public void onClickCell()
    {
        if (Reload != null)
        {
            Reload();
        }
        if (item == null)
        {
            return;
        }
        PlayerInventory.instance.Items.Remove(item);
        Buff buff = new Buff(item.Type, item.Value);
        PlayerInventory.instance.buffReciever.AddBuff(buff);
        CellClear();
    }
    public void CellClear()
    {
        icon.sprite = null;
        item = null;
    }
}
