using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private string goNextLevel = "Level1";

    private bool canFinishLevel;
    private void OnTriggerEnter(Collider other)
    {
        Player_Controller player = other.GetComponent<Player_Controller>();
        if (player != null && canFinishLevel)
        {
            Debug.Log("End");
            SceneManager.LoadScene(goNextLevel);
        }
    }
    public void FinishLevels(bool heCan) => canFinishLevel = heCan;
}
