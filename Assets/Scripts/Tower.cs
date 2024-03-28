using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int maxEnemiesPerTower = 3; // Ÿ���� ���� �� �ִ� �ִ� ���� ��
    public int currentEnemyCount = 0; // ���� Ÿ���� �ִ� ���� ��
    public float health = 100f; // Ÿ���� ü��

    // ���� Ÿ���� �߰��ϴ� �Լ�
    public void AddEnemy()
    {
        currentEnemyCount++;
    }

    // ���� Ÿ������ �����ϴ� �Լ�
    public void RemoveEnemy()
    {
        currentEnemyCount--;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Destroy(gameObject); // Ÿ�� �ı�
        }
    }
}
