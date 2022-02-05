using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange;
    public bool playerInSightRange;

    private Animation anime;

    private void Awake()
    {
        player = GameObject.Find("FPCharacterControlller").transform;
        agent = GetComponent<NavMeshAgent>();

        anime = GetComponent<Animation>();
    }

    private void Update()
    {
        //check if player is in sight
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange)
        {
            Idle();
        }
        else
        {
            ChasePlayer();
        }

    }

    private void Idle()
    {
        anime.Play("Idle");
    }

    private void ChasePlayer()
    {
        anime.Play("Run");

        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        anime.Play("Attack1");

        agent.SetDestination(player.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //TODO:attack code here

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;

    }

    public void TakeDemage(int demage)
    {
        health -= demage;

        if(health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
