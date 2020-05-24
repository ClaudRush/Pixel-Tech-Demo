using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryUIController : MonoBehaviour
{
    public Action Reload;
    [SerializeField] Cell[] cells;
    [SerializeField] private int cellsCounte;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform rootParent;
    private void Start()
    {
        cellPrefab.Reload += ReloadInventory;
    }
    private void OnDisable()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].CellClear();
        }
    }
    private void OnEnable()
    {
        ReloadInventory();
    }

    public void ReloadInventory()
    {
        if (cells == null || cells.Length <= 0)
        {
            Init();
        }
        var inventory = PlayerInventory.instance;
        Debug.Log(inventory.Items.Count);
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            if (i < cells.Length)
            {
                cells[i].Init(inventory.Items[i]);
            }
        }
    }

    void Init()
    {
        cells = new Cell[cellsCounte];
        for (int i = 0; i < cellsCounte; i++)
        {
            cells[i] = Instantiate(cellPrefab, rootParent);
        }
        cellPrefab.gameObject.SetActive(false);
    }
}
