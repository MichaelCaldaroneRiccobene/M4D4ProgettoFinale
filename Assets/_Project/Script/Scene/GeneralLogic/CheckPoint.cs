using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Game_Manager Game_Manager {  get; set; }
    private Renderer reder;
    private void Start() => reder = GetComponentInChildren<Renderer>();

    public void ChangeColor(Color color) => reder.material.color = color;

    private void OnTriggerEnter(Collider other)
    {
        Player_Controller player_Controller = other.GetComponent<Player_Controller>();
        if(player_Controller != null )
        {
            player_Controller.PosLastCheckPoint = transform.position;
            player_Controller.RotLastCheckPoint = transform.rotation;
            Game_Manager.CheckPointPress(this);
        }
    }
}
