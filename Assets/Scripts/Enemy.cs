using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2.4f;
    public float hp = 3f;
    [SerializeField] private float hitCD = 1f;
    private float curHitCD;
    [SerializeField] private float damage = 1f;

    private bool isTouchingPlayer = false;

    public static event Action onEnemyDied;

    void Update()
    {
        if (Player.Instance != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, speed * Time.deltaTime);
        }

        if(curHitCD > 0f)
        {
            curHitCD -= Time.deltaTime;
            if (curHitCD <= 0f)
                HitIfCanTo();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;

        if (other.GetComponent<Player>() != null)
        {
            isTouchingPlayer = true;
            HitIfCanTo();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
            isTouchingPlayer = false;
    }

    private void HitIfCanTo()
    {
        if (CanHit())
            Hit();
    }

    private void Hit()
    {
        Player.Instance.Damage(damage);
        curHitCD = hitCD;
    }

    private bool CanHit()
    {
        return isTouchingPlayer && curHitCD <= 0f;
    }

    public void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0f)
            Die();
    }

    private void Die()
    {
        onEnemyDied?.Invoke();
        Destroy(gameObject);
    }
}
