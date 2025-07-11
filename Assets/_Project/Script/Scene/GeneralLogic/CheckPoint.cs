using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player_Controller player_Controller = other.GetComponent<Player_Controller>();
        if(player_Controller != null )
        {
            player_Controller.PosLastCheckPoint = transform.position;
        }
    }
}
