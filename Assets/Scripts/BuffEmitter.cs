using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEmitter : MonoBehaviour
{
    [SerializeField] private Buff buff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.buffRecieverContainer.ContainsKey(collision.gameObject))
        {
            var reciever = GameManager.instance.buffRecieverContainer[collision.gameObject];
            reciever.AddBuff(buff);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.instance.buffRecieverContainer.ContainsKey(collision.gameObject))
        {
            var reciever = GameManager.instance.buffRecieverContainer[collision.gameObject];
            reciever.RemoveBuff(buff);
        }
    }
}

[System.Serializable]
public class Buff
{
    public BuffType type;
    public float additiveBonus;
    public float multipleBonus;

    public Buff(BuffType type, float additiveBonus)
    {
        this.type = type;
        this.additiveBonus = additiveBonus;
    }
}

public enum BuffType : byte
{
    Damage,
    Force,
    Armor
}