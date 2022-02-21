using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 100;
    public float infectionRate;
    public bool masked = false;
    public float infectionInterval;

    private bool isDead;
    private bool isInfected;
    private float nextInfectionTime = 0.0f;

    public Text HealthText;
    public Text InfectionText;
    public Text GameStat;

    private void Start()
    {

        isDead = false;
        isInfected = false;
        infectionRate = 25;
        infectionInterval = 3f;
    }

    private void Update()
    {
        UpdateHealthInfo(health.ToString());
        UpdateInfectionInfo(infectionRate.ToString());
        StartCoroutine(PauseGame());
        if (Time.time >= nextInfectionTime && !masked) {

            nextInfectionTime += infectionInterval;
            TakeInfection(1f);
        }
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
        infectionRate += infection;

        if (!isInfected && infectionRate >= 100)
        {
            isInfected = true;
        }
    }

    private void UpdateHealthInfo(string _health)
    {
        HealthText.text = "Health: " + _health;
    }

    private void UpdateInfectionInfo(string _infection)
    {
        InfectionText.text = "Infection: " + _infection;
    }

    IEnumerator PauseGame ()
    {   
        if(isDead)
        {
            GameStat.text = "Game Over!";
            yield return new WaitForSecondsRealtime(1);
            Time.timeScale = 0f;
        }

        if (isInfected)
        {
            GameStat.text = "You are infected!";
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