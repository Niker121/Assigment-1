using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Collider collider;
    public NavMeshAgent agent;
    public Animator animator;
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;

    // Thêm biến cho máu
    public int currentHp;
    public int maxHp = 100; // Giá trị tối đa cho máu

    private bool isAttacking = false;

    // Thêm biến âm thanh
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    void Start()
    {
        currentHp = maxHp; // Khởi tạo máu hiện tại bằng tối đa
    }

    private void Update()
    {
        if (currentHp <= 0) // Kiểm tra nếu zombie đã chết
        {
            Die();
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer < detectionRange)
        {
            ChasePlayer();

            if (distanceToPlayer < attackRange && !isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("isRunning", true);
        animator.SetBool("isIdle", false);
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("attack"); // Kích hoạt hoạt ảnh tấn công

        // Chờ sau khi tấn công
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) 
        {
            Debug.Log("Hurt");
            TakeDamage(20);
        }
    }
    public void TakeDamage(int amount)
    {
        currentHp -= amount; // Giảm máu hiện tại
                             // Kiểm tra nếu máu <= 0
        if (audioClips.Length > 0 && audioSource != null)
        {
            audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
           
        }
        if (currentHp <= 0)
        {
            currentHp = 0; // Đảm bảo không có giá trị âm
            Die(); // Gọi phương thức chết
            audioSource.Play();
        }
       
        // Có thể phát hoạt ảnh cho bị thương
        animator.SetTrigger("hurt");
    }   

    public void Die()
    {
        animator.SetTrigger("die");
        agent.isStopped = true; // Dừng NavMeshAgent
        collider.enabled = false;
        // Thêm logic để loại bỏ zombie khỏi trò chơi
        Destroy(gameObject,2f); // Hủy đối tượng sau 2 giây
        Debug.Log("Zoobie is die animattion");
    }
}