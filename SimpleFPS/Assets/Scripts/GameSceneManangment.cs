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

    [Header("HealthBars")]
    public Transform healthBarParent;
    public GameObject healtBarPrefab;
    public float healtBarHeight;

    RestartWindowUpdater restartMenuUpdater;
    Dictionary<EnemyHandler, HeathBar> liveEnemies;
    bool isEndHandled;

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DeadMessage(GameObject enemy)
    {
        EnemyHandler key = null;

        foreach (var row in liveEnemies)
        {
            if (row.Key.gameObject.Equals(enemy))
            {
                key = row.Key;
                Destroy(row.Value);
                break;
            }
        }
        liveEnemies.Remove(key);
    }

    void OnEnable()
    {
        CreateEnemies();
    }

    void CreateEnemies()
    {
        liveEnemies = new Dictionary<EnemyHandler, HeathBar>();
        for (int i = 0; i < GameData.enemiesToSpawn; i++)
        {
            Vector3 spawnPos = DrawEnemyPosition(i);
            EnemyHandler newEnemy = CreateEnemy(spawnPos);
            HeathBar enemyHealtBar = CreateEnemyHealthBar(newEnemy).GetComponent<HeathBar>();
            liveEnemies.Add(newEnemy, enemyHealtBar);
            newEnemy.GetComponent<EnemyHandler>().message = this;
        }
    }

    Vector3 DrawEnemyPosition(int i)
    {
        Vector3 pos = transform.position;
        pos.y += 1;
        pos.x += i * 20;
        return pos;
    }

    EnemyHandler CreateEnemy(Vector3 pos)
    {
        return Instantiate<GameObject>(enemyPrefab, pos, Quaternion.identity, enemiesParent).GetComponent<EnemyHandler>();
    }

    HeathBar CreateEnemyHealthBar(EnemyHandler enemy)
    {
        Vector3 pos = HealthBarPosition(enemy.transform.position);
        Quaternion lookAt = playerShootingScript.transform.rotation;

        GameObject healtBar = Instantiate<GameObject>(healtBarPrefab, pos, lookAt, healthBarParent);
        HeathBar bar = healtBar.GetComponent<HeathBar>();

        return bar;
    }

    Vector3 HealthBarPosition(Vector3 enemyPos)
    {
        Vector3 pos = enemyPos;
        pos.y += healtBarHeight;
        return pos;
    }

    void Start()
    {
        restartMenuUpdater = restartMenuUI.gameObject.GetComponent<RestartWindowUpdater>();
        foreach (var row in liveEnemies)
        {
            row.Value.Init(row.Key.maxHP);
        }
    }

    void Update()
    {
        if (!isEndHandled && liveEnemies.Count == 0)
            EndRound();

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);

        UpdateHealtBars();
    }

    void UpdateHealtBars()
    {
        foreach (var row in liveEnemies)
        {
            row.Value.transform.position = HealthBarPosition(row.Key.transform.position);
            row.Value.transform.rotation = playerShootingScript.transform.rotation;
            row.Value.UpdateHP(row.Key.HP);
        }
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
