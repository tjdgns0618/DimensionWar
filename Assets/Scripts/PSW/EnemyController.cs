using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType // ���� Ÿ��
    {
        Ground,
        Air
    }

    public EnemyType enemyType; // ���� ����
    public float DamageToPlayer = 20f; // ���� �÷��̾�� ���ϴ� ������

    // �̵��� ���Ǵ� ������
    private GameObject player; // �÷��̾� ������Ʈ
    private NavMeshAgent navMeshAgent; // NavMesh ������Ʈ
    private Transform[] pathPoints; // ��θ� �̷�� ������
    private int currentPathIndex = 0; // ���� ��� �ε���
    public float movementSpeed; // ���� �̵��ӵ� (NavMesh ������Ʈ�� ���ǵ� �����)

    // Ÿ���� ���� ����
    private Tower currentTower; // ���� �浹�� Ÿ��

    // ���ݿ� ���Ǵ� ������
    public float attackDamage = 10f; // ���� ���ݷ�
    public float attackCooldown = 1f; // ���� ���� ��ٿ� �ð�
    private bool isAttacking = false; // ���� Ÿ���� ���� ������ ����

    public float health = 100f; // ���� ü��

    private Animator animator;
    public List<string> attackAnimationNames = new List<string> { "Attack01", "Attack02", "Attack03", "Attack04", "Attack05" }; // ���� �ִϸ��̼� ����Ʈ
    private bool isWalking = false; // �ȱ� ���� ����

    void Start()
    {
        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();
            
        // �÷��̾� ���� ������Ʈ�� �±׷� ã��
        player = GameObject.FindGameObjectWithTag("Player");
        // NavMesh ������Ʈ�� ������
        navMeshAgent = GetComponent<NavMeshAgent>();
        // NavMesh ������Ʈ�� ���ǵ� ���
        movementSpeed = navMeshAgent.speed;
        // ��θ� �̷�� �������� ������
        GameObject pathParent = GameObject.FindGameObjectWithTag("PathParent");
        pathPoints = new Transform[pathParent.transform.childCount];
        for (int i = 0; i < pathParent.transform.childCount; i++)
        {
            pathPoints[i] = pathParent.transform.GetChild(i);
        }

        // �ʱ� ������ ����
        SetDestinationToNextPathPoint();
    }

    void Update()
    {
        // ü���� 0 ������ ��� Die()�Լ� ȣ��
        if (health <= 0f)
        {
            Die();
        }

        //�������� �����ߴ��� Ȯ��
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            // �ȱ� �ִϸ��̼� ����
            animator.SetBool("IsWalking", false);
            SetDestinationToNextPathPoint(); // ���� �������� ����
        }

        // Ÿ���� ������ �� �ִ� �����̸� ����
        if (isAttacking && currentTower != null && currentTower.gameObject != null)
        {
            // �� Ÿ���� Ground�� ��쿡�� Ÿ���� ����
            if (enemyType == EnemyType.Ground)
            {
                AttackTower(currentTower);
            }
        }

        // �ȱ� ���� ����
        isWalking = navMeshAgent.velocity.magnitude > 0.1f;

        // �ȱ� ���¿� ���� �ִϸ��̼� ����
        animator.SetBool("IsWalking", isWalking);
    }

    // Ÿ�� ����
    public void SetTower(Tower tower)
    {
        currentTower = tower;
    }

    // Ÿ�� ����
    public void RemoveTower()
    {
        currentTower = null;
    }

    void TakeDamage(float damage)
    {
        health -= damage; // ���� ü�� ����
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (enemyType == EnemyType.Ground && other.GetComponent<Tower>()) // �� Ÿ���� Ground�̰� �浹�� ������Ʈ�� "Tower" �±׸� ���� Ÿ���� ���
        {
            currentTower = other.GetComponent<Tower>(); // �浹�� Ÿ�� ��������

            // Ÿ���� �� ���� �ִ�ġ���� ���� ��쿡�� �� �߰� �� �̵� ����
            if (currentTower.currentEnemyCount < currentTower.maxEnemiesPerTower)
            {
                currentTower.AddEnemy(); // Ÿ���� �� �� ����
                currentTower.enemiesInRange.Add(this); // ���� Ÿ���� �� ����Ʈ�� �߰�
                navMeshAgent.isStopped = true; // �̵��� ����
                isAttacking = true; // ���� ���·� ����
            }
            else // �ִ� �� �� �ʰ��� ���� Ÿ���� �����ϰ� �ٽ� �̵�
            {
                currentTower = null; // ���� Ÿ�� �ʱ�ȭ
            }
        }
        else if (other.CompareTag("Player")) // �浹�� ������Ʈ�� "Player" �±׸� ���� �÷��̾��� ���
        {
            PlayerController playerController = other.GetComponent<PlayerController>(); // �÷��̾� ������Ʈ ��������
            if (playerController != null)
            {
                float damageToPlayer = DamageToPlayer;
                playerController.TakeDamage(damageToPlayer);
            }
            Destroy(gameObject); // �� ������Ʈ �ı�
        }
    }

    void OnTriggerExit(Collider other) // Ÿ���� ����� ��
    {
        // �� Ÿ���� Air�� ��� ����
        if (enemyType == EnemyType.Air)
        {
            return;
        }

        if (other.CompareTag("Tower"))
        {
            if (currentTower != null)
            {
                currentTower.RemoveEnemy(); // Ÿ���� �� �� ����
                currentTower = null; // ���� Ÿ�� �ʱ�ȭ
                StartMoving(); // �̵� �簳
            }
        }
    }

    // ���� �̵� ����
    public void StartMoving()
    {
        SetDestinationToNextPathPoint(); // ��������Ʈ�� �̵�
        navMeshAgent.isStopped = false; // �̵� ����
    }

    void AttackTower(Tower tower)
    {
        // ���� ��ٿ� �ð��� ���� ������ Ÿ�� ����
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0f)
        {
            // �������� ���� �ִϸ��̼� ����
            string randomAttackAnimation = GetRandomAttackAnimation();

            // ���õ� ���� �ִϸ��̼��� Ʈ���� ���
            animator.SetTrigger(randomAttackAnimation);

            tower.TakeDamage(attackDamage); // Ÿ�� ü�� ���� 
            attackCooldown = 1f; // ��ٿ� �ʱ�ȭ
        }
    }

    void Die()
    {
        // Die �ִϸ��̼��� ���
        Debug.Log("Die Ʈ���� ���");
        animator.SetTrigger("Die");

        // �� Ÿ���� �� �� ����
        if (enemyType == EnemyType.Ground && currentTower != null)
        {
            currentTower.RemoveEnemy();
        }

        // Die �ִϸ��̼��� ���� Ȯ�� �� �ı� ����
        Debug.Log("�ڷ�ƾ ���");
        StartCoroutine(DestroyAfterAnimation(0.1f)); 
    }

    IEnumerator DestroyAfterAnimation(float delay)
    {
        // Die �ִϸ��̼��� ���̸�ŭ ���
        Debug.Log("���");
        yield return new WaitForSeconds(delay);

        // �� ������Ʈ �ı�
        Debug.Log("�ı�");
        Destroy(gameObject);
    }

    string GetRandomAttackAnimation()
    {
        // ���� �ִϸ��̼ǵ� �� �ϳ��� �������� �����Ͽ� ��ȯ
        int randomIndex = Random.Range(0, attackAnimationNames.Count);
        return attackAnimationNames[randomIndex];
    }

    void SetDestinationToNextPathPoint()
    {
        if (currentPathIndex < pathPoints.Length - 1) // ���� �������� �̵�
        {
            currentPathIndex++;
            Vector3 destination = pathPoints[currentPathIndex].position;
            navMeshAgent.SetDestination(destination);
            // �ȱ� �ִϸ��̼� ���
            animator.SetBool("IsWalking", true);
        }
        else // ����� ���� ������ ��� �÷��̾� ������ �̵�
        {
            navMeshAgent.SetDestination(player.transform.position);
            // �ȱ� �ִϸ��̼� ���
            animator.SetBool("IsWalking", true);
        }
    }

    public void OnDamage(float Dmg)
    {
        health -= Dmg;
        health -= GameManager.Instance.BonusDamage;
        if (health<=0)
        {
            GameManager.Instance.Killcount++;
            Die();
        }
    }
    public void MoveSpeedCtrl(float speed)
    {
        movementSpeed += speed;
    }
    public void AttackDmgCtrl(float attackdamage)
    {
        attackDamage -= attackdamage;
    }
}