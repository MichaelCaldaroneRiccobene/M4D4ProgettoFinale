using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private int damageWater = 10;
    private void OnCollisionEnter(Collision collision)
    {
        Player_Controller player_Controller = collision.collider.gameObject.GetComponent<Player_Controller>();
        if(player_Controller != null )
        {
            Life_Controller life = player_Controller.GetComponent<Life_Controller>();
            if (life != null)
            {
                life.UpdateHp(-damageWater);
                if(!life.isDead()) player_Controller.CheckPoint();
            }
        }
    }
}
