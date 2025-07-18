using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Turrent : MonoBehaviour
{
    public Player_Controller player_Controller {  get; set; }
    public Transform ParentBulletTurret { get; set; }

    private void Start() => Invoke("FindTurret", 1);
    private void FindTurret()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Turret turret = transform.GetChild(i).GetComponent<Turret>();
            if (turret != null)
            {
                turret.Player_Controller = player_Controller;
                if(ParentBulletTurret != null) turret.ParentBulletTurret = ParentBulletTurret;
            }
        }
    }
}
