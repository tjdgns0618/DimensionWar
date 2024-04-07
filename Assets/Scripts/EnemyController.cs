using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float attackDamage = 10f; // ���� ���ݷ�
    public float attackCooldown = 1f; // ���� ���� �ӵ�
    public float movementSpeed = 3f; // ���� �̵� �ӵ�
    public float health = 100f; // ���� ü��

    private Tower currentTower; // ���� �浹�� Ÿ��
    private GameObject player; // �÷��̾� ������Ʈ
    private bool isAttackingTower = false; // Ÿ���� ���� ������ ����
    private float attackTimer = 0f; // ���� ���� ��ٿ� Ÿ�̸�

    void Start()
    {
        // �÷��̾� ���� ������Ʈ�� �±׷� ã��
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
          //  Debug.LogError("Player not found!"); // �÷��̾ ���� ��� ���� �޽��� ��� �� �Լ� ����
            return;
        }

        // �÷��̾� ���� ���ϵ��� ���� ���� ���� (Y ���� �����Ͽ� �������� �������� �ʵ��� ��)
        Vector3 playerPos = player.transform.position;
        playerPos.y = transform.position.y;
        transform.LookAt(playerPos);
    }

    void Update()
    {
        if(!player)
        {
            return;
        }
        // Ÿ���� ���� ������ ���� ��� �÷��̾� ������ �̵�
        if (!isAttackingTower)
        {
            Vector3 targetPos = player.transform.position;
            targetPos.y = transform.position.y;
            transform.Translate((targetPos - transform.position).normalized * movementSpeed * Time.deltaTime, Space.World);
        }
        else // Ÿ���� ���� ���� ���
        {
            // Ÿ���� �����ϰ� Ÿ�� ���� ������Ʈ�� �����ϴ� ��쿡�� ���� ��ٿ� ����
            if (currentTower != null && currentTower.gameObject != null)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackCooldown) // ���� ��ٿ��� ������ Ÿ���� ����
                {
                    AttackTower(currentTower);
                    attackTimer = 0f; // ���� ��ٿ� Ÿ�̸� �ʱ�ȭ
                }
            }
            else // Ÿ���� �ı��Ǿ��ų� ���� ��� ���� ���� ����
            {
                isAttackingTower = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        // �浹�� ������Ʈ�� Ÿ���̰� ���� Ÿ���� ���� ������ ���� ���
        if (other.GetComponent<Tower>().isWall && currentTower == null)
        {
            currentTower = other.GetComponent<Tower>(); // �浹�� Ÿ�� ����
            // ���� Ÿ���� �� ���� �ִ� �� ������ ���� ��
            if (currentTower.currentEnemyCount < currentTower.maxEnemiesPerTower)
            {
                currentTower.AddEnemy(); // Ÿ���� �� �� ����
                isAttackingTower = true; // Ÿ���� ���� ���� ���·� ����
            }
        }
        // �浹�� ������Ʈ�� �÷��̾��� ���
        else if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>(); // �浹�� �÷��̾� ��Ʈ�ѷ�
            if (player != null)
            {
                player.TakeDamage(attackDamage); // �÷��̾�� ������ �ֱ�
            }
            Destroy(gameObject); // �� ������Ʈ �ı�
        }
    }

    void AttackTower(Tower tower)
    {
        tower.TakeDamage(attackDamage); // Ÿ���� ������ �ֱ�
    }
}