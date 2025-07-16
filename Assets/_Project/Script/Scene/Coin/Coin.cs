using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] protected float speed = 5;
    [SerializeField] protected bool isRandomSpeed;
    [SerializeField] protected float minSpeed = 0.5f;
    [SerializeField] protected float maxSpeed = 3;

    public Game_Manager game_Manager;
    private void FixedUpdate()
    {
        transform.Rotate(0, speed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player_Controller player_Controller = other.GetComponent<Player_Controller>();
        if(player_Controller != null) DestroyCoin();
    }
    private void AddCoin()
    {
        game_Manager.TakeACoin();
    }

    private void DestroyCoin()
    {
        AddCoin();
        Destroy(gameObject);
    }

}
