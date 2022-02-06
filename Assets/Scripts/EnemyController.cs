using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer, whatIsObst;

    public float health;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    [Range(0,360)]
    public float angle;
    public bool playerInSightRange, playerInAttackRange;
    public Vector3 distance;
    public bool inSight;
    private Animation anime;

    public bool isDead = false;

    private void Awake()
    {
        player = GameObject.Find("FPCharacterControlller_copy").transform;
        agent = GetComponent<NavMeshAgent>();

        anime = GetComponent<Animation>();
        anime["Death"].wrapMode = WrapMode.Once;
    }

    private void Update()
    {
        //check if player is in sight
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        distance = Vector3.Normalize(transform.position - player.position)*2;
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        
        if (playerInSightRange){
            if (Vector3.Angle(transform.forward, directionToPlayer) < angle /2) 
            {
                float dist = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, directionToPlayer, dist, whatIsObst))
                {
                    inSight = true;
                }
                else{
                    inSight = false;
                }   
            }
            else{
                inSight = false;
            }
        }
        else if (inSight)
        {
            inSight = false;
        }
        

        if (!inSight && !playerInAttackRange && !isDead)
        {
            Idle();
        }
        if (inSight && !playerInAttackRange && !isDead)
        {
            ChasePlayer();
        }
        if (inSight && playerInAttackRange && !isDead)
        {
            AttackPlayer();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    private void Idle()
    {
        anime.Play("Idle");
    }

    private void ChasePlayer()
    {
        anime.Play("Run");
        
        agent.SetDestination(player.position + distance);
    }

    private void AttackPlayer()
    {
        anime.Play("Attack1");

        // agent.SetDestination(player.position + distance);

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

    public void TakeDemage(float demage)
    {
        health -= demage;

        if(!isDead && health <= 0)
        {
            isDead = true;

            anime.Play("Death");

            Invoke(nameof(DestroyEnemy), 1.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}

