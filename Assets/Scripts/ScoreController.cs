using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI enemyCountText;
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
    public List<ZombieEvent> l_enemies = new List<ZombieEvent>();
    private int score = 0;
    private void Start()
    {
        highScoreText.text = "Score: " + 0;
        ZombieEvent[] enemies = FindObjectsOfType<ZombieEvent>();
        foreach (ZombieEvent enemy in enemies)
        {
            enemy.OnEnemyDeath += () => AddScore(enemy);
            l_enemies.Add(enemy);
            UpdateEnemyCountText();
        }
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
 }


