using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulDamge : MonoBehaviour
{
    //private Collider myColLastHit = null;
    private void OnTriggerEnter(Collider col)
    {
        //if (col.GetComponent<IlastCollide>())
        //{
        //    myColLastHit = col.GetComponent<IlastCollide>().iLastEntered;
        //}
        
        PlayerDamageable damageable = col.GetComponent<PlayerDamageable>();
        if (damageable)
        {
            damageable.InflictDamage(1.0f);
        }
    }
}
