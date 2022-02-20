using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulDamge : MonoBehaviour
{   
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.GetType());
        PlayerDamageable damageable = collision.collider.GetComponent<PlayerDamageable>();
        if (damageable)
        {
            damageable.InflictDamage(25.0f);
        }
    }
}
