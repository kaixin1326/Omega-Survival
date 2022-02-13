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

    private void OnCollisionEnter(Collision collision)
    {
        GameObject col = collision.gameObject;
        // Debug.Log(col.GetComponent<EnemyController>().state);
        if (col.GetComponent<EnemyController>().state == "attacking")
        {
            TakeDemage(25);
        }
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