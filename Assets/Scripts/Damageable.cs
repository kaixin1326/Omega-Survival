using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private EnemyController enemy;
    private void Awake()
    {
        enemy = GetComponent<EnemyController>();
    }
    public void InflictDamage(float damage)
    {
        enemy.TakeDamage(damage);
    }
}
