using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public GameObject playerObject;
    

    public LayerMask groundFloor;
    public LayerMask targetPlayer;
    public GameObject projectile;
    private int lookSpeed = 2;
    public GameObject firePoint;

    //Personal Moevement 

    public Vector3 walkPoint;
    bool walkPointsSet;
    public float walkingPointRange;

    //Attacking Player

    public float timeBetweenAttacks;
    bool attackOccured;

    //States

    public float enemySightRange, enemyAttackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        playerObject = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, enemySightRange, targetPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, enemyAttackRange, targetPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointsSet) SearchForWalkPoint();

        if (walkPointsSet)
            agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointsSet = false;
    }

    private void SearchForWalkPoint()
    {
        float randomZ = Random.Range(-walkingPointRange, walkingPointRange);
        float randomX = Random.Range(-walkingPointRange, walkingPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //if (Physics.Raycast(walkPoint, -transform.up, 2f, groundFloor))
            walkPointsSet = true;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        //transform.LookAt(player);
        FaceDirection(playerObject);

        if (!attackOccured)
        {
            //Attacking below
            Rigidbody rb = Instantiate(projectile, firePoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 3f, ForceMode.Impulse);

            //
            attackOccured = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        attackOccured = false;
    }

    public void FaceDirection(GameObject target)
    {
        Vector3 lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * lookSpeed);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemyAttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemySightRange);
    }
}
