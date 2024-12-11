using UnityEngine;
using UnityEngine.AI;
using static InfimaGames.LowPolyShooterPack.Weapon;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Chase Settings")]
    public Transform target;
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    [Header("Attack Settings")]
    public float damage = 10f;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float attackCooldownTimer;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (target == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange)
        {
            Attack();
        }
        else if (distanceToTarget <= chaseRange)
        {
            Chase();
        }
        else
        {
            StopChasing();
        }

        // Cooldown timer for attacks
        attackCooldownTimer -= Time.deltaTime;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        // Play hurt animation
        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }

        if (currentHealth <= 0)
        {
            GetComponent<Collider>().enabled = false;
            Die();
        }
    }

    private void Die()
    {
        // Disable enemy functionality
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        navMeshAgent.enabled = false;
        GetComponent<Collider>().enabled = false;

        // Destroy the object after a delay
        Destroy(gameObject, 2f);
    }

    private void Chase()
    {
        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.SetDestination(target.position);
            if (animator != null)
            {
                animator.SetBool("IsChasing", true);
            }
        }
    }

    private void StopChasing()
    {
        if (animator != null)
        {
            animator.SetBool("IsChasing", false);
        }
        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.ResetPath();
        }
    }

    private void Attack()
    {
        if (attackCooldownTimer > 0) return;

        // Play attack animation
        if (animator != null)
        {
            animator.SetTrigger("Attack");
            Debug.Log("attack");
        }

        // Deal damage to the target if within range
        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            var damageable = target.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                
            }
        }

        // Reset attack cooldown
        attackCooldownTimer = attackCooldown;
    }
}
