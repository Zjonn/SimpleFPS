using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManangment : MonoBehaviour, IMessage
{
    public Canvas gameUI;
    public Canvas restartMenuUI;
    public Transform enemiesParent;
    public GameObject enemyPrefab;
    public Shooting playerShootingScript;

    RestartWindowUpdater restartMenuUpdater;
    List<GameObject> liveEnemies;
    bool isEndHandled;

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DeadMessage(GameObject enemy)
    {
        liveEnemies.Remove(enemy);
    }

    void OnEnable()
    {
        liveEnemies = new List<GameObject>();
        for (int i = 0; i < GameData.enemiesToSpawn; i++)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.y += 1;
            spawnPos.x += i * 20;
            GameObject newEnemy = Instantiate<GameObject>(enemyPrefab, spawnPos, Quaternion.identity, enemiesParent);
            liveEnemies.Add(newEnemy);
            newEnemy.GetComponent<EnemyHandler>().message = this;
        }
    }

    void Start()
    {
        restartMenuUpdater = restartMenuUI.gameObject.GetComponent<RestartWindowUpdater>();
    }

    void Update()
    {
        if (!isEndHandled && liveEnemies.Count == 0)
            EndRound();
    }

    void EndRound()
    {
        processAccuracy();
        changeDisplayedUI();
    }

    void processAccuracy()
    {
        float playerAccuracy = playerShootingScript.Accuracy;

        restartMenuUpdater.UpdateText(playerAccuracy, GameData.PlayerTopAccuracy);
    }

    void changeDisplayedUI()
    {
        gameUI.enabled = false;
        restartMenuUI.enabled = true;
    }

}
