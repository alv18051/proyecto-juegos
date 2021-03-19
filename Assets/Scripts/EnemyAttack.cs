using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] int minAtkDamage = 5;
    [SerializeField] int maxAtkDamage = 15;
    [SerializeField] float atkCooldown = 1.5f;

    NavMeshAgent agent;

    bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with Player!");
            if (canAttack)
            {

                collision.gameObject.SendMessage("takeDamage", Random.Range(minAtkDamage, maxAtkDamage));
                Debug.Log("Attacked Player!");
                StartCoroutine(AttackCooldown(atkCooldown));
            }
        }
    }

    IEnumerator AttackCooldown(float atkCooldown)
    {
        canAttack = false;
        agent.isStopped = true;
        yield return new WaitForSeconds(atkCooldown);
        canAttack = true;
        agent.isStopped = false;
    }
}
