using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManangment : MonoBehaviour
{
    public Canvas gameUI;
    public Canvas endUI;
    public Transform enemiesParent;
    public GameObject enemyPrefab;

    List<GameObject> enemies;

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnEnable()
    {
        enemies = new List<GameObject>();
        for (int i = 0; i < GameData.enemiesToSpawn; i++)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.x += i * 20;
            enemies.Add(Instantiate<GameObject>(enemyPrefab, spawnPos, Quaternion.identity, enemiesParent));
        }
    }

    void Update()
    {
        if (enemies.Capacity == 0)
            EndRound();
    }

    void EndRound()
    {
        displayEndUI();
    }

    void displayEndUI()
    {
        gameUI.enabled = false;
        endUI.enabled = true;
    }
}
