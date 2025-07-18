using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Game_Manager game_Manager {  get; set; }

    private void OnTriggerEnter(Collider other)
    {
        Player_Controller player_Controller = other.GetComponent<Player_Controller>();

        if(player_Controller != null) DestroyCoin();
    }
    private void AddCoin() => game_Manager.TakeACoin();

    private void DestroyCoin()
    {
        AddCoin();
        Destroy(gameObject);
    }
}
