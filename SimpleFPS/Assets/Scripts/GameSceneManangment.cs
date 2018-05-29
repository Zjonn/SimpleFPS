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

    [Header("HealthBars")]
    public Transform healthBarParent;
    public GameObject healthBarPrefab;
    public float healthBarHeight;

    RestartWindowUpdater restartMenuUpdater;
    Dictionary<EnemyHandler, HpFollowingBar> aliveEnemies;
    bool isEndHandled;

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DeadMessage(GameObject enemy)
    {
        EnemyHandler key = null;

        foreach (var row in aliveEnemies)
        {
            if (row.Key.gameObject.Equals(enemy))
            {
                key = row.Key;
                Destroy(row.Value);
                break;
            }
        }
        aliveEnemies.Remove(key);
    }

    void OnEnable()
    {
        CreateEnemies();
    }

    void CreateEnemies()
    {
        aliveEnemies = new Dictionary<EnemyHandler, HpFollowingBar>();
        for (int i = 0; i < GameData.enemiesToSpawn; i++)
        {
            Vector3 spawnPos = DrawEnemyPosition(i);
            EnemyHandler newEnemy = CreateEnemy(spawnPos);
            HpFollowingBar enemyHealtBar = CreateEnemyHealthBar(newEnemy).GetComponent<HpFollowingBar>();
            aliveEnemies.Add(newEnemy, enemyHealtBar);
            newEnemy.GetComponent<EnemyHandler>().message = this;
        }
    }

    Vector3 DrawEnemyPosition(int i)
    {
        Vector3 pos = new Vector3(0,enemyPrefab.transform.position.y,0);
        pos.x = Random.Range(spawnArenaCorners[0].position.x, (spawnArenaCorners[2].position.x));
        pos.z = Random.Range(spawnArenaCorners[0].position.z, (spawnArenaCorners[1].position.z));

        return pos;
    }

    EnemyHandler CreateEnemy(Vector3 pos)
    {
        return Instantiate<GameObject>(enemyPrefab, pos, Quaternion.identity, enemiesParent).GetComponent<EnemyHandler>();
    }

    HpFollowingBar CreateEnemyHealthBar(EnemyHandler enemy)
    {
        Vector3 pos = HealthBarPosition(enemy.transform.position);
        Quaternion lookAt = playerShootingScript.transform.rotation;

        GameObject healthBar = Instantiate<GameObject>(healthBarPrefab, pos, lookAt, healthBarParent);
        HpFollowingBar bar = healthBar.GetComponent<HpFollowingBar>();

        return bar;
    }

    Vector3 HealthBarPosition(Vector3 enemyPos)
    {
        Vector3 pos = enemyPos;
        pos.y += healthBarHeight;
        return pos;
    }

    void Start()
    {
        restartMenuUpdater = restartMenuUI.gameObject.GetComponent<RestartWindowUpdater>();
        foreach (var row in aliveEnemies)
        {
            row.Value.Init(row.Key.maxHP);
        }
    }

    void Update()
    {
        if (!isEndHandled && aliveEnemies.Count == 0)
            EndRound();

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);

        UpdateHealtBars();
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

    void UpdateHealtBars()
    {
        foreach (var row in aliveEnemies)
        {
            row.Value.transform.position = HealthBarPosition(row.Key.transform.position);
            row.Value.transform.rotation = playerShootingScript.transform.rotation;
            row.Value.UpdateHP(row.Key.HP);
        }
    }




}
