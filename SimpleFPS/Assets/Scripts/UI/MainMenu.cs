using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text accuracy;

    public void Exit()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    // Use this for initialization
    void Start()
    {
        GameData.playerTopAccuracy = PlayerPrefs.GetFloat("accuracy");
        accuracy.text = GameData.playerTopAccuracy + "% trafień";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Exit();
    }
}
