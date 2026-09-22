using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 1f;

    [SerializeField] private GameObject bulletDestroyParticles;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 1.6f);
    }

    private void Start()
    {
        rigidbody.linearVelocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>()) return;

        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.Damage(damage);
        }

        DestroyBullet();
    }

    private void DestroyBullet()
    {
        ParticlesManager.SpawnParticles(bulletDestroyParticles, transform.position, -transform.right);
        Destroy(gameObject);
    }

    public static void SpawnBullet(Vector3 position, Vector2 direction, GameObject bulletPrefab)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = position;
        bullet.transform.right = direction;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * bullet.GetComponent<Bullet>().speed;
        Destroy(bullet, 1.6f);
    }
}
