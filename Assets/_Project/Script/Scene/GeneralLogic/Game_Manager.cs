using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private Transform coins;
    [SerializeField] private Transform checkPoint;
    [SerializeField] private Transform turretsInScene;
    [SerializeField] private Transform parentBulletTurret;

    [SerializeField] private Player_Controller player_Controller;

    public UnityEvent<bool> finishLevel;
    private List<CheckPoint> checkPointList = new List<CheckPoint>();

    private int coinTake;
    private int coinToT;


    private void Start()
    {
        Time.timeScale = 1;
        FindItems();
        
    }

    private void FindItems()
    {
        FindCoins();
        FindCheckPoints();
        FindAndSetUpTurrets();
    }

    private void FindCoins()
    {
        for (int i = 0; i < coins.childCount; i++)
        {
            Coin coin = coins.transform.GetChild(i).GetComponent<Coin>();
            if (coin != null)
            {
                coin.game_Manager = this;
                coinToT++;
            }
        }
    }

    private void FindCheckPoints()
    {
        for (int i = 0; i < checkPoint.childCount; i++)
        {
            CheckPoint point = checkPoint.transform.GetChild(i).GetComponent<CheckPoint>();
            if (point != null)
            {
                checkPointList.Add(point);
                point.Game_Manager = this;
            }
        }
    }

    private void FindAndSetUpTurrets()
    {
        for (int i = 0; i < turretsInScene.childCount; i++)
        {
            Control_Turrent control_Turrent = turretsInScene.transform.GetChild(i).GetComponent<Control_Turrent>();
            if (control_Turrent != null)
            {
                control_Turrent.player_Controller = player_Controller;
                if(parentBulletTurret != null) control_Turrent.ParentBulletTurret = parentBulletTurret;
            }
        }
    }

    public void CheckPointPress(CheckPoint point)
    {
        foreach (CheckPoint checkPoint in checkPointList) checkPoint.ChangeColor(Color.red);
        point.ChangeColor(Color.green);
    }

    private void isLastCoin()
    {
        Debug.Log("Go To Exit");
        finishLevel?.Invoke(true);
    }

    public void TakeACoin()
    {
        coinTake++;

        if(coinTake >= coinToT) isLastCoin();
    }
}
