using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] private bool isDestroingAfterCollision;
    [SerializeField] private GameObject parent;
    public GameObject Parent
    {
        get => parent;
        set => parent = value;
    }
    [SerializeField] private GameObject areaAtackEnemy;
    public GameObject AreaAtackEnemy
    {
        get => areaAtackEnemy;
        set => areaAtackEnemy = value;
    }
    [SerializeField] private int damage;
    public int Damage
    {
        get => damage;
        set => damage = value;
    }
    [SerializeField] private IObjectDestroyer destroyer;
   

    public void Init(IObjectDestroyer destroyer)
    {
        this.destroyer = destroyer;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == parent ||
            collision.gameObject == areaAtackEnemy)//Игнорирование коллизии с Player и с AreaAtackEnemy
            return;

        if (GameManager.instance.healthsContainer.ContainsKey(collision.gameObject))
        {
            var health = GameManager.instance.healthsContainer[collision.gameObject];
            health.TakeHit(damage);
        }
        if(isDestroingAfterCollision)
        {
            if (destroyer == null) 
                Destroy(gameObject);
            else 
                destroyer.Destroy(gameObject);
        }
       
    }
}
public interface IObjectDestroyer
{
    void Destroy(GameObject gameObject);
}
