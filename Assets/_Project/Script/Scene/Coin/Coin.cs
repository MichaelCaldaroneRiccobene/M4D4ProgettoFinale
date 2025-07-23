using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public UnityEvent AnimationTakeCoin;
    public Game_Manager Game_Manager { get; set; }

    private bool isCoinTake = false;

    private void OnTriggerEnter(Collider other)
    {
        Player_Controller player_Controller = other.GetComponent<Player_Controller>();

        if(player_Controller != null) OnDestroyCoin();
    }

    private void OnDestroyCoin()
    {
        if(isCoinTake) return;

        isCoinTake = true;
        Game_Manager.OnTakeCoin();
        AnimationTakeCoin?.Invoke();
    }
}
