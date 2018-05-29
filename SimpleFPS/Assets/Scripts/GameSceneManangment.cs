using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManangment : MonoBehaviour, IMessage
{
    [Header("UI things")]
    public Canvas gameUI;
    public Canvas restartMenuUI;

    [Header("Spawning Enemies")]
    public Transform enemiesParent;
    public GameObject enemyPrefab;
    public Shooting playerShootingScript;
    public Transform[] spawnArenaCorners;


    RestartWindowUpdater restartMenuUpdater;
    int aliveEnemies = GameData.enemiesToSpawn;
    bool isEndHandled;

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReciveGameObject(GameObject enemy)
    {
        aliveEnemies--;
    }

    void OnEnable()
    {
        CreateEnemies();
    }

    void CreateEnemies()
    {
        Rigidbody playerRb = playerShootingScript.gameObject.GetComponent<Rigidbody>();

        for (int i = 0; i < GameData.enemiesToSpawn; i++)
        {
            Vector3 spawnPos = DrawEnemyPosition(i);
            EnemyHandler newEnemy = CreateEnemy(spawnPos);
            newEnemy.message = this;
            newEnemy.player = playerRb;
        }
    }

    Vector3 DrawEnemyPosition(int i)
    {
        Vector3 pos = new Vector3(0, enemyPrefab.transform.position.y, 0);
        pos.x = Random.Range(spawnArenaCorners[0].position.x, (spawnArenaCorners[2].position.x));
        pos.z = Random.Range(spawnArenaCorners[0].position.z, (spawnArenaCorners[1].position.z));

        return pos;
    }

    EnemyHandler CreateEnemy(Vector3 pos)
    {
        return Instantiate<GameObject>(enemyPrefab, pos, Quaternion.identity, enemiesParent).GetComponent<EnemyHandler>();
    }


    void Start()
    {
        restartMenuUpdater = restartMenuUI.gameObject.GetComponent<RestartWindowUpdater>();
    }

    void Update()
    {
        if (!isEndHandled && aliveEnemies == 0)
            EndRound();

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    void EndRound()
    {
        processAccuracy();
        changeDisplayedUI();
        isEndHandled = true;
    }

    void processAccuracy()
    {
        float playerAccuracy = playerShootingScript.Accuracy;

        if (IsNewRecord(playerAccuracy))
        {
            GameData.playerTopAccuracy = playerAccuracy;
            PlayerPrefs.SetFloat("accuracy", playerAccuracy);
            PlayerPrefs.Save();
        }

        restartMenuUpdater.UpdateText(playerAccuracy, GameData.playerTopAccuracy);
    }

    bool IsNewRecord(float playerAccuracy)
    {
        return playerAccuracy > GameData.playerTopAccuracy;
    }

    void changeDisplayedUI()
    {
        gameUI.enabled = false;
        restartMenuUI.enabled = true;
    }


}
