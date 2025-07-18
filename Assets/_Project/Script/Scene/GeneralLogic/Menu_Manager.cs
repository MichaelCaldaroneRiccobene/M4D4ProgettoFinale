using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    [SerializeField] private GameObject panneLevel;
    [SerializeField] private GameObject pannelOption;

    [SerializeField] private string LevelOne = "Level1";
    [SerializeField] private string LevelTwo = "Level2";


    private bool isPannelOption = false;
    private bool isPannelLevel = false;

    private void Start()
    {
        panneLevel.SetActive(false);
        pannelOption.SetActive(false);
    }

    public void GoToOption()
    {
        isPannelOption = !isPannelOption;
        isPannelLevel = false;
        panneLevel.SetActive(isPannelLevel);
        pannelOption.SetActive(isPannelOption);
    }

    public void GoToLevel()
    {
        isPannelLevel = !isPannelLevel;
        isPannelOption = false;
        panneLevel.SetActive(isPannelLevel);
        pannelOption.SetActive(isPannelOption);
    }

    public void GoLevelOne() => SceneManager.LoadScene(LevelOne);

    public void GoLevelTwo() => SceneManager.LoadScene(LevelTwo);

    public void QuitGame()
    {
        //EditorApplication.ExitPlaymode();
        Application.Quit();
    }
}
