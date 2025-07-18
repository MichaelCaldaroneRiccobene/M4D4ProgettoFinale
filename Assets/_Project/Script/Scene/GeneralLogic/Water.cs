using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private int damageWater = 10;
    private void OnCollisionEnter(Collision collision)
    {
        I_IDamage i_IDamage = collision.collider.GetComponent<I_IDamage>();
        if (i_IDamage != null) i_IDamage.Damage(-damageWater);

        I_Touch_Water i_Touch_Water = collision.collider.gameObject.GetComponent<I_Touch_Water>();
        if (i_Touch_Water != null) i_Touch_Water.Water();
    }
}
