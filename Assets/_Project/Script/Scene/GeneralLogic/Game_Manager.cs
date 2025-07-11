using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private Transform coins;

    public UnityEvent<bool> finishLevel;

    private int coinTake;
    private int coinToT;


    private void Start()
    {
        Time.timeScale = 1;
        int t = coins.transform.childCount;

        for (int i = 0; i < t; i++)
        {
            Coin coin = coins.transform.GetChild(i).GetComponent<Coin>();
            if (coin != null)
            {
                coin.game_Manager = this;
                coinToT++;
            }
        }
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
