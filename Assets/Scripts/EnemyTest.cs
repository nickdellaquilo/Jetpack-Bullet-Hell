﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    private CharacterController cc;
    private bool dead = true;
    [SerializeField] private Animator[] EnemyAnims;
    //Patrol speed is the speed at which the enemy moves between patrol points
    public float patrolSpeed = 2f;
    //Chase speed is the speed at which the enemy moves towards the player when chasing
    public float chaseSpeed = 4f;
    //Spotting distance is the distance at which the enemy will start chasing the player
    public float spottingDistance = 10f;
    //Patrol points are the points that the enemy will move between
    private Transform[] patrolPoints;
    public int numberOfPatrolPoints = 5;
    public Vector2 minPatrolBounds = new Vector2(-10, -10);
    public Vector2 maxPatrolBounds = new Vector2(10, 10);
    private int currentPatrolPoint = 0;
    private Transform player;
    private bool isChasing = false;
    private int currentHealth;
    

    //Enemies start by patrolling between patrol points
    void Start()
    {
        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Animation_1_Idle();

        patrolPoints = new Transform[numberOfPatrolPoints];
        for (int i = 0; i < numberOfPatrolPoints; i++)
        {
            GameObject go = new GameObject("PatrolPoint" + i);
            go.transform.position = new Vector3(
                Random.Range(minPatrolBounds.x, maxPatrolBounds.x),
                Random.Range(minPatrolBounds.y, maxPatrolBounds.y),
                0 
            );
            patrolPoints[i] = go.transform;
        }

        StartCoroutine(Patrol());
        currentHealth = Random.Range(1, 4);
    }

    //Patrol is a coroutine so that we can pause the enemy for a few seconds at each patrol point
    IEnumerator Patrol()
    {
        while (!isChasing)
        {
            Transform targetPoint = patrolPoints[currentPatrolPoint];
            MoveTowards(targetPoint, patrolSpeed);

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.5f)
            {
                currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
                yield return new WaitForSeconds(2f);  // pause for 2 seconds at each patrol point
            }

            if (Vector3.Distance(transform.position, player.position) <= spottingDistance)
            {
                isChasing = true;
                Animation_2_Run();
                StartCoroutine(ChasePlayer());
                yield break;
            }
            
            yield return null;
        }
    }

    //MoveTowards moves the enemy towards a target at a specified speed
    void MoveTowards(Transform target, float speed)
    {
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        if (dir.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (dir.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    //ChasePlayer is a coroutine so that we can stop chasing the player when they are out of range
    IEnumerator ChasePlayer()
    {
        while (isChasing)
        {
            MoveTowards(player, chaseSpeed);

            if (Vector3.Distance(transform.position, player.position) > spottingDistance * 1.5f)
            {
                isChasing = false;
                Animation_1_Idle();
                StartCoroutine(Patrol());
                yield break;
            }

            if (Vector3.Distance(transform.position, player.position) <= 1.5f)
            {
                Animation_6_Attack();
                yield return new WaitForSeconds(1f);
                Animation_1_Idle();
                yield return new WaitForSeconds(1f);
            }
            
            yield return null;
        }
    }

    //If the enemy collides with a bullet, play the animation, and destroy the enemy
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Animation_3_Hit();
        if(currentHealth <= 0)
        {
            Animation_4_Death();
            if(dead){ //adds fuel to player when enemy dies. bool is here because the delay on death meant this code was running multiple times
                cc.currentFuel += 2;
                dead = false;
            }
            
            Destroy(gameObject, 0.75f);
        }
    }


    //Animation Functions
    public void Animation_1_Idle()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            if (EnemyAnims[i].gameObject.activeSelf == true)
            {
                EnemyAnims[i].SetBool("Run", false);
                Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is Idling");
            }
        }
    }
    public void Animation_2_Run()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            if (EnemyAnims[i].gameObject.activeSelf == true)
            {
                EnemyAnims[i].SetBool("Run", true);
                Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is Running");

            }
        }
    }
    public void Animation_3_Hit()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            if (EnemyAnims[i].gameObject.activeSelf == true)
            {
                EnemyAnims[i].SetBool("Run", false);
                EnemyAnims[i].SetTrigger("Hit");
                Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is being Hit");
            }
        }
    }
    public void Animation_4_Death()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            if (EnemyAnims[i].gameObject.activeSelf == true)
            {
                EnemyAnims[i].SetBool("Run", false);
                EnemyAnims[i].SetTrigger("Death");
                Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " has died");
            }
        }
    }

    public void Animation_6_Attack()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            if (EnemyAnims[i].gameObject.activeSelf == true)
            {
                EnemyAnims[i].SetBool("Run", false);
                EnemyAnims[i].SetTrigger("Attack");
                Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is using using its Primary Attack");
            }
        }
    }

    public void Animation_7_Attack2()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            if (EnemyAnims[i].gameObject.activeSelf == true)
            {
                EnemyAnims[i].SetBool("Run", false);
                EnemyAnims[i].SetTrigger("Attack 2");
                Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is using using its Secondary Attack");
            }
        }
    }
    public void Animation_8_Attack3()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            if (EnemyAnims[i].gameObject.activeSelf == true)
            {
                EnemyAnims[i].SetBool("Run", false);
                EnemyAnims[i].SetTrigger("Attack 3");
                Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is using using its Tertiary Attack");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spottingDistance);
    }

}