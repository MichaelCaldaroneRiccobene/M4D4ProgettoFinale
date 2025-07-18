using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ground_Check : MonoBehaviour
{
    [SerializeField] float raySphere = 0.2f;
    [SerializeField] private Player_Animation player_Animation;
    [SerializeField] private string tagPlayer = "Player";

    [SerializeField] private UnityEvent<bool> onGround;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raySphere);
    }

    public bool IsOnGround()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, raySphere);
        foreach (Collider hit in hits)
        {
            if(hit.tag != tagPlayer) return true;
        }
        return false;
    }

    private void Update()
    {
        bool wasOnGround = IsOnGround();

        //if (wasOnGround != IsOnGround()) onGround?.Invoke(wasOnGround);
        player_Animation.IsOnGround(IsOnGround());
    }
}
