using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] Transform playerOBJ;
    [SerializeField] Transform eyeLinePos;
    [SerializeField] LayerMask enemyTargetLayer;
    [SerializeField] float enemyDetectionRange = 15.0f;

    NavMeshAgent agent;

    bool playerInRange = false;
    Color debugRayColor = Color.yellow;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerOBJ = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (playerOBJ != null)
        {
            Collider[] foundTargets = Physics.OverlapSphere(transform.position, enemyDetectionRange, enemyTargetLayer.value);
            if (foundTargets.Length > 0)
            {
                playerInRange = true;
                Vector3 targetPosition = foundTargets[0].transform.position;
                RaycastHit hitInfo;

                Debug.DrawRay(eyeLinePos.position, (targetPosition - transform.position).normalized * enemyDetectionRange, debugRayColor);
                if (Physics.Raycast(eyeLinePos.position, (targetPosition - transform.position).normalized, out hitInfo, enemyDetectionRange))
                {
                    if (hitInfo.collider.CompareTag("Player"))
                    {
                        playerOBJ = hitInfo.collider.gameObject.transform;
                        debugRayColor = Color.red;
                        agent.SetDestination(playerOBJ.position);
                    }
                    else
                    {
                        debugRayColor = Color.yellow;
                    }
                }
            }
            else
            {
                if (agent.hasPath)
                {
                    //Debug.Log("Lost Player");
                }
                playerInRange = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRange);
        if (playerInRange)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, enemyDetectionRange);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, enemyDetectionRange);
        }
    }

}
