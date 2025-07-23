using UnityEngine;
using UnityEngine.Events;

public class Ground_Check : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float raySphere = 0.2f;

    public UnityEvent<bool> onGround;

    public void IsOnGround()
    {
        bool isGrounded = Physics.CheckSphere(transform.position, raySphere, groundLayer);
        onGround?.Invoke(isGrounded);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, raySphere);
    }

    private void Update() => IsOnGround();
}
