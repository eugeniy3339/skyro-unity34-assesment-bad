using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// game manager !! dont touch if it works
public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnCooldown = 1.337f;
    private float curSpawnCooldown;
    private int score = 0;
    private bool paused;

    void Awake()
    {
        inst = this;
    }

    void Start()
    {
        curSpawnCooldown = spawnCooldown;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
            Time.timeScale = paused ? 0f : 1f;
        }

        curSpawnCooldown -= Time.deltaTime;
        if (curSpawnCooldown <= 0f)
        {
            curSpawnCooldown = spawnCooldown;
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Vector3 pos = new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f), 0);
        if (Player.Instance != null && Vector3.Distance(pos, Player.Instance.transform.position) < 1.5f)
        {
            pos.x += 3f;
        }

        try
        {
            GameObject e = Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
        catch
        {
        }
    }

    private void OnEnemyDied()
    {
        addScore(1);
    }

    private void OnPlayerDied()
    {
        SceneManager.LoadScene(0);
    }

    public void addScore(int x)
    {
        score += x;
        HudHandler.Instance.setScore(score);
    }

    private void OnEnable()
    {
        Enemy.onEnemyDied += OnEnemyDied;
        Player.onPlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        Enemy.onEnemyDied -= OnEnemyDied;
        Player.onPlayerDied -= OnPlayerDied;
    }
}
