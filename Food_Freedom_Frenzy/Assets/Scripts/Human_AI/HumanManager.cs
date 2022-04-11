using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class HumanManager : MonoBehaviour
{
    [Header("States")]
    public State currentState;
    public IdleState idle;
    public PatrolState patrol;
    public ChaseState chase;
    public AttackState attack;
    public SuspiciousState suspicious;
    public string currentScene;

    // public LocomotionManager locomotionManager;

    [Header("Field of View Settings")]
    public float viewRadius;
    public float detectionRadius;
    public float attackRadius;

    [Range(0, 360)]
    public float viewAngle;

    [Header("Set In Inspector")]
    public LayerMask detectionLayer;
    public LayerMask obstacleLayer;
    
    
    [Header("Set Dynamically")]
    public Transform currentTarget;
    public Transform player;
    public float distanceFromPlayer;

    [Header("Suspicious Settings")]
    public float maxSuspicionTime;
    public float currentSuspicionTime;
    public float turnSpeed;

    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    public float maxWaitingTime;
    public float currentWaitingTime;

    [Header("Chase Settings")]
    public float maxChaseOutsideRangeTime;
    public float currentChaseOutsideRangeTime;

    [HideInInspector]
    public NavMeshAgent agent;

    private void Awake() 
    {
        currentScene = SceneManager.GetActiveScene().name;

        /* Initialize states for state machine */
        GameObject states = GameObject.Find("States");
        idle = states.GetComponentInChildren<IdleState>();
        patrol = states.GetComponentInChildren<PatrolState>();
        chase = states.GetComponentInChildren<ChaseState>();
        attack = states.GetComponentInChildren<AttackState>();
        suspicious = states.GetComponentInChildren<SuspiciousState>();

        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;

        /* Start with the patrol state */
        currentState = patrol;
        currentState.EnterState(this);
    }

    void Update()
    {
        HandleDetection();
        currentState.UpdateState(this);
    }

    public void SwitchState(State state)
    {
        currentState = state;
        state.EnterState(this);
    }
    
    /* Field of View and Detection Methods */
    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    /* Visualizes view radius and FOV in the scene window */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Vector3 viewAngleA = DirectionFromAngle(-viewAngle / 2, false);
        Vector3 viewAngleB = DirectionFromAngle(viewAngle / 2, false);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);

    }

    public void HandleDetection()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.position);

        Collider[] collidersInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, detectionLayer);

        for (int i = 0; i < collidersInViewRadius.Length; i++)
        {
            Transform target = collidersInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer))
                {
                    currentTarget = target;
                }
            }
        }
    }
}
