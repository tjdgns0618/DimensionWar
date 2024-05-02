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
    private float originalSpeed;
    public float increasedSpeedMultiplier = 1.5f; // �ӵ� ���� ���

    // Ÿ���� ���� ����
    [SerializeField]
    private Tower currentTower; // ���� �浹�� Ÿ��

    // ���ݿ� ���Ǵ� ������
    public float attackDamage = 10f; // ���� ���ݷ�
    public float attackCooldown = 1f; // ���� ���� ��ٿ� �ð�
    private bool isAttacking = false; // ���� Ÿ���� ���� ������ ����

    public float health = 100f; // ���� ü��

    private Animator WeaponAnimator; // ���� �ִϸ�����

    private Animator animator; // ���� �ִϸ�����
    public List<string> attackAnimationNames = new List<string> { "Attack01", "Attack02", "Attack03", "Attack04", "Attack05" }; // ���� �ִϸ��̼� ����Ʈ
    private bool isWalking = false; // �ȱ� ���� ����

    private bool isDead = false; // ��� ���� ����

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

        // ���� �ӵ� ���
        originalSpeed = navMeshAgent.speed;

        // �ʱ� ������ ����
        SetDestinationToNextPathPoint();
    }

    void Update()
    {
        // ��� ������ ��� �ƹ��͵� ���� ����
        if (isDead)
        {
            return;
        }
        else
        {
            // ü���� 0 ������ ��� Die()�Լ� ȣ��
            if (health <= 0f)
            {
                Die();
                return; // ����� ��� ���� �ڵ带 �������� ����
            }

            // �������� �����ߴ��� Ȯ��
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
        if ((enemyType == EnemyType.Ground) && other.GetComponent<Tower>()) // �� Ÿ���� Ground�̰� �浹�� ������Ʈ�� "Tower" �±׸� ���� Ÿ���� ���
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
            }
        }
    }
        
    // ���� �̵� ����
    public void StartMoving()
    {
        // ��� ������ ��� �̵��� �������� ����
        if (isDead)
        {
            return;
        }

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


    public void OnDamage(float damage)
    {
        health -= damage;
    }

    void Die()
    {
        if (isDead) // �̹� ����� ��쿡�� �� �̻� �������� ����
        {
            return;
        }
        isDead = true; // ��� ���·� ����

        GameManager.Instance.gold += 10; // ����� ��� ŉ��
        GameManager.Instance.Killcount++; // ����� ųī��Ʈ 1����

        // Die �ִϸ��̼��� ���
        animator.SetTrigger("Die");

        // �� Ÿ���� �� �� ����
        if ((enemyType == EnemyType.Ground) && currentTower != null)
        {
            currentTower.RemoveEnemy();
        }

        // �׺���̼� �޽� ������Ʈ ��Ȱ��ȭ
        navMeshAgent.enabled = false;

        // �ݶ��̴� ��Ȱ��ȭ
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Air Ÿ���� ��� ���������� �߶��ϴ� ȿ�� �ο�
        if (enemyType == EnemyType.Air)
        {
            StartCoroutine(FallDownCoroutine());
        }
        else // Ground Ÿ���� ���� ��� �ı�
        {
            StartCoroutine(DestroyAfterDelay(2f));
        }

    }

    IEnumerator FallDownCoroutine()
    {
        float totalTime = 2f; // �߶��ϴµ� �ɸ��� ��ü �ð�
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition - Vector3.up * 2f; // �߶� �Ÿ� ����

        while (elapsedTime < totalTime)
        {
            // ���� �ð��� ���� ������ ������ ��ġ�� ����
            float t = elapsedTime / totalTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            // �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // ������ �߶��� �� �ı�
        Destroy(gameObject);
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        // delay �ð� ���
        yield return new WaitForSeconds(delay);

        // �� ������Ʈ �ı�
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

    // ���̺갡 ����� �� ȣ��Ǵ� �Լ�
    public void IncreaseSpeedForWave(float multiplier)
    {
        // �ӵ��� ������ �ӵ��� ����
        navMeshAgent.speed = originalSpeed * multiplier;
    }
}