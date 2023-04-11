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
    [SerializeField] private float gameOverTimer = 5f;
    [SerializeField] private ScoreController scoreController;

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

    }
    private void Update()
    {
        GameOverTimer();
        CeroEnemiesLeft();
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
    public void CeroEnemiesLeft()
    {
       if(scoreController.l_enemies.Count<= 0)
        { 
        gameOverTimer -= Time.deltaTime;
        LevelComplete();
        }
    }
}

