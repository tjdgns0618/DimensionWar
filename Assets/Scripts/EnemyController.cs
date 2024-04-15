using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // �̵��� ���Ǵ� ������
    private GameObject player; // �÷��̾� ������Ʈ
    private NavMeshAgent navMeshAgent; // NavMesh ������Ʈ
    private Transform[] pathPoints; // ��θ� �̷�� ������
    private int currentPathIndex = 0; // ���� ��� �ε���

    // Ÿ���� ���� ����
    private Tower currentTower; // ���� �浹�� Ÿ��

    // ���ݿ� ���Ǵ� ������
    public float attackDamage = 10f; // ���� ���ݷ�
    public float attackCooldown = 1f; // ���� ���� ��ٿ� �ð�
    private bool isAttacking = false; // ���� Ÿ���� ���� ������ ����

    public float health = 100f; // ���� ü��

    public float movementSpeed;

    void Start()
    {
        // �÷��̾� ���� ������Ʈ�� �±׷� ã��
        player = GameObject.FindGameObjectWithTag("Player");
        // NavMesh ������Ʈ�� ������
        navMeshAgent = GetComponent<NavMeshAgent>();
        
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
        // ü���� 0 ������ ��� �ı��ϰ� ����
        if (health <= 0f)
        {
            if (currentTower != null)
            {
                currentTower.RemoveEnemy(); // Ÿ���� �� �� ����
            }
            Destroy(gameObject); // �� ������Ʈ �ı�
            return;
        }

        // �������� �����ߴ��� Ȯ��
        //if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        //{
        //    Debug.Log("������ ����. ���� ������ ����");
        //    SetDestinationToNextPathPoint(); // ���� �������� ����
        //}

        // Ÿ���� ������ �� �ִ� �����̸� ����
        if (isAttacking && currentTower != null && currentTower.gameObject != null)
        {
            Debug.Log("Ÿ��������");
            AttackTower(currentTower);
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
        if (other.GetComponent<Tower>()) // �浹�� ������Ʈ�� "3D_Tower" �±׸� ���� Ÿ���� ���
        {
            currentTower = other.GetComponent<Tower>(); // �浹�� Ÿ�� ��������

            // Ÿ���� �� ���� �ִ�ġ���� ���� ��쿡�� �� �߰� �� �̵� ����
            if (currentTower.currentEnemyCount < currentTower.maxEnemiesPerTower)
            {
                currentTower.AddEnemy(); // Ÿ���� �� �� ����
                currentTower.enemiesInRange.Add(this); // ���� Ÿ���� �� ����Ʈ�� �߰�
                navMeshAgent.isStopped = true; // �̵��� ����
                Debug.Log("isStopped = true");
                isAttacking = true; // ���� ���·� ����
            }
            else // �ִ� �� �� �ʰ��� ���� Ÿ���� �����ϰ� �ٽ� �̵�
            {
                currentTower = null; // ���� Ÿ�� �ʱ�ȭ
            }
        }
        else if (other.CompareTag("Player")) // �浹�� ������Ʈ�� "Player" �±׸� ���� �÷��̾��� ���
        {
            PlayerController player = other.GetComponent<PlayerController>(); // �÷��̾� ������Ʈ ��������
            if (player != null)
            {
                player.TakeDamage(attackDamage); // �÷��̾�� ������ �ֱ�
            }
            Destroy(gameObject); // �� ������Ʈ �ı�
        }
    }

    void OnTriggerExit(Collider other) // Ÿ���� ����� ��
    {
        Debug.Log("OnTriggerExit");
        if (other.GetComponent<Tower>() && other.GetComponent<Tower>().isMelea)
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
        Debug.Log("StartMoving");
        SetDestinationToNextPathPoint(); // ��������Ʈ�� �̵�
        navMeshAgent.isStopped = false; // �̵� ����
    }

    void AttackTower(Tower tower)
    {
        // ���� ��ٿ� �ð��� ���� ������ Ÿ�� ����
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0f)
        {
            tower.TakeDamage(attackDamage); // Ÿ���� ������ �ֱ�
            attackCooldown = 1f; // ��ٿ� �ʱ�ȭ
        }
    }

    void SetDestinationToNextPathPoint()
    {
        Debug.Log("SetDestinationToNextPathPoint");
        if (currentPathIndex < pathPoints.Length - 1) // ���� �������� �̵�
        {
            currentPathIndex++;
            Vector3 destination = pathPoints[currentPathIndex].position;
            navMeshAgent.SetDestination(destination);
        }
        else // ����� ���� ������ ��� �÷��̾� ������ �̵�
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    public void OnDamage(float Dmg)
    {
        health -= Dmg;
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