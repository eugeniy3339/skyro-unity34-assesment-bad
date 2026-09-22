using UnityEngine;
using UnityEngine.UI;

public class HudHandler : MonoBehaviour
{
    public static HudHandler Instance { get; private set; }

    [SerializeField] private Text hpText;
    [SerializeField] private Text scoreText;

    private void Awake()
    {
        Instance = this;
    }

    public void updateHealth(float health)
    {
        if (hpText != null)
        {
            hpText.text = "hp " + health;
        }
    }

    public void setScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "score " + score;
        }
    }
}
