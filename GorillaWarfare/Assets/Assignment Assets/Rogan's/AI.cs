using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public float health;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Check that the player meets conditions to be attacked
    private void Update()
    {
      playerInAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
     playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);  
     if (!playerInSightRange && !playerInAttackRange) Patrolling();
     if (playerInSightRange && !playerInAttackRange) ChasePlayer();
     if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

         //walkPoint Reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random walk range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    } 

    private void AttackPlayer()
    {
        //make sure enemy does not move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Shoots at player


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <=0 ) Invoke(nameof(DestroyEnemy), 0.5f);

    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

   // private void OnDrawGizmosSelected()
   // {//Displays the different stages of detection for testing
      //  Gizmos.color = color.red;
     //   Gizmos.DrawWireSphere(transform.position, attackRange);
       // gizmos.color = color.yellow;
      //  Gizmos.DrawWireSphere(transform.position, sightRange);
   // }
}