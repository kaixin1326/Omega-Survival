using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float health = 100;
    private float infectingDegree;

    private bool isDead;
    private bool isInfected;

    public Text HealthText;

    private void Start()
    {

        isDead = false;
        isInfected = false;
    }

    private void Update()
    {
        UpdateHealthInfo(health.ToString());
    }

    public void TakeDemage(float demage)
    {
        health -= demage;

        if (!isDead && health <= 0)
        {
            isDead = true;
        }
    }

    public void TakeInfection(float infection)
    {
        infectingDegree -= infectingDegree;

        if (!isInfected && infectingDegree <= 0)
        {
            isInfected = true;
        }
    }

    private void UpdateHealthInfo(string _health)
    {
        HealthText.text = "Health: " + _health;
    }
}
