using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanAi : MonoBehaviour
{
    [Header("Set in Inspector")]
    
    public NavMeshAgent agent;
    public Transform player;
    public Transform startPosition;

    [Header("Patrol Settings")]
    public float speed;
    public float chaseAcceleration;
    public float maxWaitingTime;
    public float currentWaitingTime;
    public float sightRange;
    public float attackRange;
    public Transform[] patrolPoints;
    private int _randomSpot;
    
    
    private void Awake() 
    {
        player = GameObject.Find("Player").transform;
        startPosition = GameObject.Find("StartPosition").transform;
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
        _randomSpot = Random.Range(0, patrolPoints.Length);
        
    }
    
    private void Update() 
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, sightRange)) {
            if (hit.collider.tag == "TempPlayer") {
                Chase(hit);
            } else {
                Patrol();
            }
        } else {
            Patrol();
        }
    }

    private void Patrol()
    {

        // agent.destination = patrolPoints[_randomSpot].position;
        Debug.DrawLine(transform.position, transform.position + transform.forward * sightRange, Color.green);
        if (agent.remainingDistance < 0.5f) {
            if (maxWaitingTime == 0)
            {
                maxWaitingTime = Random.Range(2, 5);
            }

            if (currentWaitingTime >= maxWaitingTime)
            {
                maxWaitingTime = 0;
                currentWaitingTime = 0;
                _randomSpot = Random.Range(0, patrolPoints.Length);
                agent.SetDestination(patrolPoints[_randomSpot].position);
            } else {
                currentWaitingTime += Time.deltaTime;
            }
        }
    }

   private void Chase(RaycastHit hit)
   {
        Debug.DrawLine(transform.position, hit.point, Color.red);
        transform.LookAt(player);
        if (Vector3.Distance(transform.position, player.position) > attackRange && Vector3.Distance(transform.position, player.position) < sightRange)
        {
            // transform.position = Vector3.MoveTowards(transform.position, hit.point, speed * Time.deltaTime * 1.1f);
            agent.SetDestination(player.position);
            agent.speed = speed * chaseAcceleration;
            
        } 

        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            // Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            // Destroy(player.gameObject);
            player.position = startPosition.position;
        }
   }
}
