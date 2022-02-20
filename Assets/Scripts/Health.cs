using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 100;
    private float infectingDegree;

    private bool isDead;
    private bool isInfected;

    public Text HealthText;
    public Text GameStat;

    private void Start()
    {

        isDead = false;
        isInfected = false;
    }

    private void Update()
    {
        UpdateHealthInfo(health.ToString());
        StartCoroutine(PauseGame());
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (!isDead && health <= 0)
        {
            isDead = true;
        }
    }

    public void AddHealth(float amount)
    {
        health += amount;

        if(health >= 100)
        {
            health = 100;
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

    IEnumerator PauseGame ()
    {
        if(isDead)
        {
            GameStat.text = "Game Over!";
            yield return new WaitForSecondsRealtime(1);
            Time.timeScale = 0f;
        }
        // else 
        // {
        //     GameStat.text = "";
        //     Time.timeScale = 1;
        // }
    }

}
