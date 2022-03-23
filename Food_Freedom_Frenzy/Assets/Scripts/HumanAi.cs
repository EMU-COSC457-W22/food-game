using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanAi : MonoBehaviour
{
    [Header("Set Dynamically")]
    public float speed;
    public Transform target;
    public float killRange;
    public float visionDistance;

    // Patrol specific
    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    public float waitTime;

    private int _currentPointIndex = 0;
    
    private void Update() 
    {
       RaycastHit hitInfo;

       if (Physics.Raycast(transform.position, transform.forward, out hitInfo, visionDistance)) {
            if (hitInfo.collider.tag == "TempPlayer") {
                Debug.DrawLine(transform.position, hitInfo.point, Color.red);
                Chase(hitInfo);
            }
              
       } else {
            Patrol();
            Debug.DrawLine(transform.position, transform.position + transform.forward * visionDistance, Color.green);
       }
    }

    private void Patrol()
    {
        if (transform.position != patrolPoints[_currentPointIndex].position) {
            transform.LookAt(patrolPoints[_currentPointIndex]);
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[_currentPointIndex].position, speed * Time.deltaTime);
            if (transform.position == patrolPoints[_currentPointIndex].position) {
                if (_currentPointIndex == patrolPoints.Length - 1) {
                    _currentPointIndex = 0;
                } else {
                    _currentPointIndex++;
                }
                
            }
        } 
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if (_currentPointIndex + 1 < patrolPoints.Length) {
            _currentPointIndex++;
        } else {
            _currentPointIndex = 0;
        }
    }

   private void Chase(RaycastHit hitInfo)
   {
        transform.LookAt(target);
        if (Vector3.Distance(transform.position, target.position) > killRange && Vector3.Distance(transform.position, target.position) < visionDistance) {
            transform.position = Vector3.MoveTowards(transform.position, hitInfo.point, speed * Time.deltaTime * 1.25f); 
            if (Vector3.Distance(transform.position, target.position) <= killRange) {
                // Debug.DrawLine(transform.position, hitInfo.point, Color.red);
                Destroy(target.gameObject);
            }
        }
   }  
}
