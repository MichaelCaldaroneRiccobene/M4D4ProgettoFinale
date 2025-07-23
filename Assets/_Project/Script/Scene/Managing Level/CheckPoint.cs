using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Game_Manager Game_Manager {  get; set; }
    private Renderer reder;
    private void Start() => reder = GetComponentInChildren<Renderer>();

    public void ChangeColor(Color color) => reder.material.color = color;

    private void OnTriggerEnter(Collider other)
    {
        Player_Return_To_CheckPoint player_Return_To_CheckPoint = other.GetComponent<Player_Return_To_CheckPoint>();
        if(player_Return_To_CheckPoint != null )
        {
            player_Return_To_CheckPoint.PosLastCheckPoint = transform.position;
            player_Return_To_CheckPoint.RotLastCheckPoint = transform.rotation;

            Game_Manager.CheckPointPress(this);
        }
    }
}
