using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI enemyCountText;
    private List<ZombieEvent> l_enemies = new List<ZombieEvent>();
    private Dictionary<System.Type, int> enemyTypePointsMap = new Dictionary<System.Type, int>()
    {
        {typeof(ZombieEvent), 2},
        {typeof(FastZombieEvent), 1},
        {typeof(HeavyZombieEvent), 3},
        {typeof(RegularChaseZombie), 3},
        {typeof(FastChaseZombie), 2},
        {typeof(HeavyChaseZombie), 4},
        {typeof(PurpleZombieEvent), 2}
    };
    private int score = 0;
    [SerializeField] private float gameOverTimer = 5f;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        highScoreText.text = "Score: " + 0;
        l_enemies.AddRange(FindObjectsOfType<ZombieEvent>());
        UpdateEnemyCountText();
        ZombieEvent[] enemies = FindObjectsOfType<ZombieEvent>();
        foreach (ZombieEvent enemy in enemies)
        {
            enemy.OnEnemyDeath += () => AddScore(enemy);
        }
    }
    private void Update()
    {
        GameOverTimer();
        LevelCompleteTimer();
    }

    private void AddScore(ZombieEvent enemy)
    {
        int points = enemyTypePointsMap[enemy.GetType()];
        score += points;
        highScoreText.text = "Score: " + score;
        l_enemies.Remove(enemy);
        UpdateEnemyCountText();
    }
    private void UpdateEnemyCountText()
    {
        enemyCountText.text = "Enemies: " + l_enemies.Count;
    }
    private void GameOverTimer()
    {
        GameObject character = GameObject.Find("RobotCharacter");
        RobotCharacterController robotCharacterControllerScript = character.GetComponent<RobotCharacterController>();
        if (robotCharacterControllerScript.health <= 0)
        {
            gameOverTimer -= Time.deltaTime;
            GameOver();
        }
    }
    private void GameOver()
    {
        if (gameOverTimer <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }
    private void LevelComplete()
    {
        if (gameOverTimer <= 0)
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
    private void LevelCompleteTimer()
    {
        if (l_enemies.Count <= 0)
        {
            gameOverTimer -= Time.deltaTime;
            LevelComplete();
        }
    }
}

