using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : MonoBehaviour
{
    private Health health;
    private void Awake()
    {
        health = GetComponent<Health>();
    }

    public void InflictDamage(float damage)
    {
        health.TakeDamage(damage);
    }
}
