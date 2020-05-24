using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemBase))]
public class ItemBaseEditor : Editor
{
    private ItemBase itemBase;

    private void Awake()
    {
        itemBase = (ItemBase)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("New Item"))
            itemBase.CrateItem();
        if (GUILayout.Button("Remove"))
            itemBase.RemoveItem();
        if (GUILayout.Button("<="))
            itemBase.PrevItem();
        if (GUILayout.Button("=>"))
            itemBase.NextItem();

        GUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}
