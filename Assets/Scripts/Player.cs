using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Rigidbody2D rigidbody;
    public float speed = 5.5f;
    [SerializeField] private float maxHp = 100f;
    private float _hp;
    public float hp { get { return _hp; } private set { value = Mathf.Clamp(value, 0f, maxHp);  _hp = value; HudHandler.Instance.updateHealth(value); } }

    public GameObject bulletPrefab;
    [SerializeField] private float shootCooldown = 0.18f;
    private float curShootCooldown;

    private Vector2 moveInput;
    Vector2 lastMoveInput;

    public static event Action onPlayerDied;

    void Awake()
    {
        Instance = this;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        hp = maxHp;
    }

    void Update()
    {
        curShootCooldown -= Time.deltaTime;

        moveInput = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        ).normalized;

        if (moveInput.magnitude > 0)
            lastMoveInput = moveInput;

        if (Input.GetKey(KeyCode.Space))
        {
            ShootIfCanTo();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody.AddForce(moveInput.normalized * speed * 10, ForceMode2D.Force);
    }

    private void ShootIfCanTo()
    {
        if (CanShoot())
            Shoot();
    }

    private void Shoot()
    {
        try
        {
            Bullet.SpawnBullet(transform.position, lastMoveInput, bulletPrefab);
            curShootCooldown = shootCooldown;
        }
        catch
        {
            /*GameObject b = new GameObject("bullet");
            b.transform.position = transform.position;
            b.transform.parent = null;
            var sr = b.AddComponent<SpriteRenderer>();
            var my = GetComponent<SpriteRenderer>();
            if (my != null) sr.sprite = my.sprite;
            sr.color = new Color(1f, 1f, 0.2f, 1f);
            sr.sortingOrder = 10;
            var rb = b.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(lastDir * 12f, 0f);
            var col = b.AddComponent<CircleCollider2D>();
            col.isTrigger = true;
            col.radius = 0.12f;
            Destroy(b, 1.6f);*/
        }
    }

    private bool CanShoot()
    {
        return curShootCooldown <= 0f;

    }

    public void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0f)
            Die();
    }

    private void Die()
    {
        onPlayerDied?.Invoke();
    }
}
