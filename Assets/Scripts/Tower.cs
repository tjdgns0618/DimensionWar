using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int maxEnemiesPerTower = 3; // 타워가 막을 수 있는 최대 적의 수
    public int currentEnemyCount = 0; // 현재 타워에 있는 적의 수
    public float health = 100f; // 타워의 체력

    // 적을 타워에 추가하는 함수
    public void AddEnemy()
    {
        currentEnemyCount++;
    }

    // 적을 타워에서 제거하는 함수
    public void RemoveEnemy()
    {
        currentEnemyCount--;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Destroy(gameObject); // 타워 파괴
        }
    }
}
