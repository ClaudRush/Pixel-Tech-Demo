using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] private TriggerDamage triggerDamage;
    public TriggerDamage TriggerDamage => triggerDamage;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    private Player player;

    [SerializeField] private float lifeTime;
    public float LifeTime 
    { 
        get => lifeTime;
        set { if (value > 1 && value < 4) lifeTime = value; } 
    }

    public void SetImpulse(Vector3 direction, float forse, Player player, GameObject areaAtackEnemy)
    {
        triggerDamage.Init(this);
        this.player = player;
        triggerDamage.Parent = player.gameObject;
        triggerDamage.AreaAtackEnemy = areaAtackEnemy;
        rb.AddForce(direction * forse, ForceMode2D.Impulse);
        if (forse < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);      
        
        StartCoroutine(DestroyArrow());
    }
    public IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        yield break;// yield return null;
    }

    public void Destroy(GameObject gameObject)
    {
        player.ReturnArrowToPool(this);
    }
}
