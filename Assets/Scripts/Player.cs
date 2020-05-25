using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float deltaReload;
    public float DeltaReload
    {
        get => deltaReload;
        set => deltaReload = value;
    }

    [SerializeField] private float speed = 4;
    public float Speed
    {
        get => speed;
        set 
        {
            if (value > 0.5f)
                speed = value;
        }
    }
    [SerializeField] private float forse = 11;
    public float Forse
    {
        get => forse;
        set
        {
            if (value > 6f)
                forse = value;
        }
    }
    [SerializeField] private float shootForse;
    public float ShootForse
    {
        get => shootForse;
        set
        {
            if (value > 1f)
                shootForse = value;
        }
    }
    [SerializeField] private bool isRelaoding;
    public bool IsRelaoding => isRelaoding;
    [SerializeField] private Health health;
    public Health Health => health;

    [SerializeField] private bool stop = false;
    [SerializeField] private bool isCheat;
    [SerializeField] private GameObject areaAtackEnemy;
    [SerializeField] private GroundDetection groundDetection;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Arrow arrow;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private BuffReciever buffReciever;
    [SerializeField] private TriggerDamage triggerDamage;
    [SerializeField] private int damageBonus;
    [SerializeField] private UICharacterController uiController;
    [SerializeField] private int arrowCount = 3;
    private List<Arrow> arrowsPool;

    private float minimalHight = -15f;
    public float MinimalHight
    {
        get => minimalHight;
        set
        {
            if (value > -20f && value < -10f)
                minimalHight = value;
        }
    }

    private Arrow currentArrow;
    private Vector3 direction;
    private bool isJumping;

    #region Singleton
    public static Player instance { get; private set; }
    #endregion
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        
        arrowsPool = new List<Arrow>();
        for (int i = 0; i < arrowCount; i++)
        {
            var arrowTemp = Instantiate(arrow, arrowSpawnPoint);
            arrowsPool.Add(arrowTemp);
            arrowTemp.gameObject.SetActive(false);
        }
        buffReciever.OnBuffsChanged += Buffer;
    }

    public void InitUIController(UICharacterController uiController)
    {
        this.uiController = uiController;
        this.uiController.Jump.onClick.AddListener(Jump);
        this.uiController.Atack.onClick.AddListener(Shooting);

    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
#endif
    }
    void FixedUpdate()
    {
        Move();
        animator.SetFloat("Speed", Mathf.Abs(direction.x));
        IsCheat();
    }
    public void Move()
    {
        animator.SetBool("IsGrounded", groundDetection.IsGrounded);

        if (rb.velocity.y < -0.01f && !isJumping && !groundDetection.IsGrounded)
        {
            animator.SetTrigger("StartFall");
        }

        isJumping = isJumping && !groundDetection.IsGrounded;
        direction = Vector3.zero;
#if UNITY_EDITOR
        if (!stop)
        {
            if (Input.GetKey(KeyCode.A))
                direction = Vector3.left;
            if (Input.GetKey(KeyCode.D))
                direction = Vector3.right;
        }
#endif
        if (!stop)
        {
            if (uiController.Left.IsPressed)
                direction = Vector3.left;
            if (uiController.Right.IsPressed)
                direction = Vector3.right;
        }

        direction *= speed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
    public void Jump()
    {
        if (groundDetection.IsGrounded)
        {
            rb.AddForce(Vector2.up * forse, ForceMode2D.Impulse);
            animator.SetTrigger("StartJump");
            isJumping = true;
        }
    }
    private void IsCheat()
    {
        if (transform.position.y < minimalHight && isCheat)
        {
            rb.velocity = new Vector2(0, 0);
            transform.position = new Vector2(0, 0);
        }
        else if (transform.position.y < minimalHight && !isCheat)
            Destroy(gameObject);
        
    }
    private void Shooting()
    {
        if (groundDetection.IsGrounded && !isRelaoding)
        {
            animator.SetTrigger("Shoot");
            StartCoroutine(AnimationShoot());
        }
    }
    public void CheckShoot()
    {
        if (!isRelaoding)
        {

            currentArrow = GetArrowFromPool();
            currentArrow.SetImpulse
                (Vector3.right,
                spriteRenderer.flipX ? -forse * shootForse : forse * shootForse,
                this,
                areaAtackEnemy);
            StartCoroutine(Reload());
        }

    }
    private void Buffer(Buff buff)
    {
        if (buff.type == BuffType.Armor)
        {
            health.SetHealth((int)buff.additiveBonus);
        }
        if (buff.type == BuffType.Damage)
        {
            for (int i = 0; i < arrowsPool.Count; i++)
            {
                arrowsPool[i].TriggerDamage.Damage += (int)buff.additiveBonus;
            }
            StartCoroutine(IsActivePotionDamage());
        }
        if (buff.type == BuffType.Force)
        {
            Forse += (int)buff.additiveBonus;
        }
    }
    private IEnumerator IsActivePotionDamage()
    {
        int a = 10;
        while (a > 0)
        {
            yield return new WaitForSeconds(1f);
            a--;
            Debug.Log("Осталось " + a + " секунд до окончания действия зелья");
        }
        yield return new WaitForSeconds(10f);
        for (int i = 0; i < arrowsPool.Count; i++)
        {
            arrowsPool[i].TriggerDamage.Damage = arrow.TriggerDamage.Damage;
        }
        yield break;
    }
    private IEnumerator AnimationShoot()
    {
        stop = true;
        direction = Vector3.zero;
        yield return new WaitForSeconds(0.4f);
        stop = false;
        yield break;
    }
    private IEnumerator StateDeltaReload()
    {
        while (isRelaoding)
        {
            yield return new WaitForEndOfFrame();
            deltaReload += Time.deltaTime/2;
        }
        yield break;
    }
    private IEnumerator Reload()
    {
        isRelaoding = true;
        StartCoroutine(StateDeltaReload());
        yield return new WaitForSeconds(2f);
        isRelaoding = false;
        yield break;
    }
    private Arrow GetArrowFromPool()
    {
        if (arrowsPool.Count > 0)
        {
            var arrowTemp = arrowsPool[0];
            arrowsPool.Remove(arrowTemp);
            arrowTemp.gameObject.SetActive(true);
            arrowTemp.transform.parent = null;
            arrowTemp.transform.position = arrowSpawnPoint.transform.position;
            return arrowTemp;
        }
        return Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity);
    }
    public void ReturnArrowToPool(Arrow arrowTemp)
    {
        if (!arrowsPool.Contains(arrowTemp))
        {
            arrowsPool.Add(arrowTemp);
        }
        arrowTemp.transform.parent = arrowSpawnPoint;
        arrowTemp.transform.position = arrowSpawnPoint.transform.position;
        arrowTemp.gameObject.SetActive(false);
    }
}
