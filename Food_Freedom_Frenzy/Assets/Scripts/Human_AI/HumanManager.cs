using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class HumanManager : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    [Header("States")]
    public State currentState;
    public IdleState idle;
    public PatrolState patrol;
    public ChaseState chase;
    public AttackState attack;
    public SuspiciousState suspicious;

    [Header("Set In Inspector")]
    public LayerMask detectionLayer;
    public LayerMask obstacleLayer;

    [Header("Set Dynamically")]
    public Transform currentTarget;
    public Transform player;
    public float distanceFromPlayer;
    public string currentScene;

    [Header("Field of View Settings")]
    public float viewRadius;
    public float detectionRadius;
    public float attackRadius;

    [Range(0, 360)]
    public float viewAngle;

    [Header("Idle Settings")]
    public float maxWaitingTime;
    public float currentWaitingTime;

    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    public int randomSpot;

    [Header("Suspicious Settings")]
    public float maxSuspicionTime = 5f;
    public float currentSuspicionTime;
    public float turnSpeed = 100f;

    [Header("Chase Settings")]
    public float maxChaseOutsideRangeTime = 2f;
    public float currentChaseOutsideRangeTime;

    [HideInInspector]
    public NavMeshAgent agent;

    private void Start() 
    {
        /* Get the name of the current scene. May be helpful for scene reloading from a gameover screen */
        currentScene = SceneManager.GetActiveScene().name;

        /* Initialize Components */
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        /* Initialize states for state machine */
        GameObject states = transform.Find("States").gameObject;
        idle = states.GetComponentInChildren<IdleState>();
        patrol = states.GetComponentInChildren<PatrolState>();
        chase = states.GetComponentInChildren<ChaseState>();
        attack = states.GetComponentInChildren<AttackState>();
        suspicious = states.GetComponentInChildren<SuspiciousState>();

        /* Set the player position as something to constantly be aware of */
        player = GameObject.Find("Player").transform;

        /* Start with the patrol state */
        currentState = patrol;
        currentState.EnterState(this);
        animator.SetBool(isWalkingHash, true);
        animator.SetBool(isRunningHash, false);
    }

    void Update()
    {
        HandleDetection();
        currentState.UpdateState(this);
        HandleAnimationStates();

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

    private void OnDrawGizmosSelected()
    {
        // Visualizes view radius and FOV in the scene window 
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

                /* target cannot be detected if there's an obstacle in the way */
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer))
                {
                    
                    if (target.CompareTag("PickUp_FoodTrail")) {
                        currentTarget = player.transform;
                    } else {
                        currentTarget = target.transform;
                    }
                }
            }
        }
    }

    public void HandleAnimationStates()
    {
        /* Play walking animation in patrol state */
        if (currentState == patrol)
        {
            animator.SetBool(isWalkingHash, true);
            animator.SetBool(isRunningHash, false);
        }

        /* Play idle animation in idle or suspicious state */
        if (currentState == idle || currentState == suspicious)
        {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunningHash, false);
        }

        /* Play running animation in chase state */
        if (currentState == chase)
        {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunningHash, true);
        }
    }
}
