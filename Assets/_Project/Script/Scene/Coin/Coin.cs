using UnityEngine;

public class Coin : MonoBehaviour
{
    public Game_Manager Game_Manager { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        Player_Controller player_Controller = other.GetComponent<Player_Controller>();

        if(player_Controller != null) DestroyCoin();
    }

    private void DestroyCoin()
    {
        Game_Manager.OnTakeCoin();
        Destroy(gameObject);
    }
}
