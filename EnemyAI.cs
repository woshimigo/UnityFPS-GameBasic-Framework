using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    public NavMeshAgent agent;
    public Transform player;
    public float health = 50f;

    [Header("行为参数")]
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public int attackDamage = 10;

    void Update() {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange) {
            agent.SetDestination(player.position);

            if (distance <= attackRange) {
                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0f) Die();
    }

    void Die() {
        Destroy(gameObject);
    }
}