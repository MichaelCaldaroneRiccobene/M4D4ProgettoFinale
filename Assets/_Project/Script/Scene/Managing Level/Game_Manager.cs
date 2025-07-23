using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game_Manager : MonoBehaviour
{
    [Header("Level Setting")]
    [SerializeField] private int timeMinuts = 6;
    [SerializeField] private float timeSecond = 41;

    [Header("Take Important Stuff")]
    [SerializeField] private Transform coinsInScene;
    [SerializeField] private Transform checkPointInScene;

    [SerializeField] private Transform turretsInScene;
    [SerializeField] private Transform parentBulletTurret;

    [SerializeField] private Transform targetTurret;

    public UnityEvent onFinishGame;
    public UnityEvent onLoseInTime;

    public UnityEvent<int,float> updateTime;
    public UnityEvent<int,int> updateCoinText;

    private List<CheckPoint> checkPointList = new List<CheckPoint>();

    private int coinTake;
    private int coinToT;

    private bool isEndTime;

    private void Awake()
    {
        Time.timeScale = 1;
        FindItems();
        updateCoinText?.Invoke(coinTake, coinToT);
    }

    private void Update() => Clock();

    private void Clock()
    {
        if(!isEndTime)
        {
            timeSecond -= Time.deltaTime;
            updateTime?.Invoke(timeMinuts, timeSecond);
            if (timeSecond <= 0)
            {
                timeSecond = 60;
                timeMinuts--;
                if (timeMinuts <= 0) isEndTime = true;
            }     
        }
        else
        {
            updateTime?.Invoke(0, 0);
            onLoseInTime?.Invoke();
        }
    }

    private void FindItems()
    {
        FindCoins();
        FindCheckPoints();
        FindControlTurret();
    }

    private void FindCoins()
    {
        if (coinsInScene == null) return;
        for (int i = 0; i < coinsInScene.childCount; i++)
        {
            Coin coin = coinsInScene.transform.GetChild(i).GetComponent<Coin>();
            if (coin != null)
            {
                coin.Game_Manager = this;
                coinToT++;
            }
        }
    }

    private void FindCheckPoints()
    {
        if (checkPointInScene == null) return;
        for (int i = 0; i < checkPointInScene.childCount; i++)
        {
            CheckPoint point = checkPointInScene.transform.GetChild(i).GetComponent<CheckPoint>();
            if (point != null)
            {
                checkPointList.Add(point);
                point.Game_Manager = this;
            }
        }
    }

    private void FindControlTurret()
    {
        if(turretsInScene == null) return;
        for (int i = 0; i < turretsInScene.childCount; i++)
        {
            Control_Turrent control_Turrent = turretsInScene.transform.GetChild(i).GetComponent<Control_Turrent>();
            if (control_Turrent != null)
            {
                control_Turrent.Target = targetTurret;
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
        onFinishGame?.Invoke();
    }

    public void OnTakeCoin()
    {
        coinTake++;
        updateCoinText?.Invoke(coinTake, coinToT);
        if (coinTake >= coinToT) isLastCoin();
    }
}
