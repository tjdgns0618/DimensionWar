using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public GameObject enemyPrefab; // �� ������
        public int numberOfEnemiesToSpawn; // ��ȯ�� ���� ��
        public float spawnInterval; // ���� ����
        public float SpawnDelay; // ���� info���� ������
    }

    public List<EnemySpawnInfo> enemySpawnInfos; // �� ���� ���� ����Ʈ
    public Transform[] spawnPoints; // ���� ��ȯ�� ��ġ���� �迭

    private List<GameObject>[] enemyPools; // ���� ������Ʈ Ǯ���� ����Ʈ

    void Start()
    {
        // ���� ������Ʈ Ǯ���� �ʱ�ȭ
        InitializeEnemyPools();

        // ���� �������� ���� ��ȯ�ϴ� �ڷ�ƾ ����
        StartCoroutine(SpawnEnemies());
    }

    void InitializeEnemyPools()
    {
        // ���� ������Ʈ Ǯ���� �ʱ�ȭ
        enemyPools = new List<GameObject>[enemySpawnInfos.Count];
        for (int i = 0; i < enemySpawnInfos.Count; i++)
        {
            enemyPools[i] = new List<GameObject>();
            for (int j = 0; j < enemySpawnInfos[i].numberOfEnemiesToSpawn; j++)
            {
                GameObject enemy = Instantiate(enemySpawnInfos[i].enemyPrefab);
                enemy.SetActive(false);
                enemyPools[i].Add(enemy);
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemySpawnInfos.Count; i++)
        {
            float nextSpawnDelay = enemySpawnInfos[i].SpawnDelay; // ���� info���� ������
            yield return new WaitForSeconds(nextSpawnDelay);

            for (int j = 0; j < enemySpawnInfos[i].numberOfEnemiesToSpawn; j++)
            {
                // �� ������ ����
                GameObject enemyPrefab = enemySpawnInfos[i].enemyPrefab;

                // ��ȯ ��ġ ����
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                // ������Ʈ Ǯ���� ��Ȱ��ȭ�� �� ��������
                GameObject enemy = GetPooledEnemy(enemyPrefab);

                // ������ ���� ���� ��� ��ġ �����ϰ� Ȱ��ȭ
                if (enemy != null)
                {
                    enemy.transform.position = spawnPoint.position;
                    enemy.SetActive(true);
                }

                // ���� ��ȯ�� ���� ���ݸ�ŭ ���
                yield return new WaitForSeconds(enemySpawnInfos[i].spawnInterval);
            }
        }
    }

    GameObject GetPooledEnemy(GameObject enemyPrefab)
    {
        // �ش� ������ �� ������Ʈ Ǯ���� ��Ȱ��ȭ�� ���� ã�� ��ȯ
        int index = GetEnemySpawnIndex(enemyPrefab);
        if (index >= 0 && index < enemyPools.Length)
        {
            foreach (GameObject enemy in enemyPools[index])
            {
                if (enemy != null && !enemy.activeInHierarchy)
                {
                    return enemy;
                }
            }
        }
        return null;
    }

    int GetEnemySpawnIndex(GameObject enemyPrefab)
    {
        // �־��� �� �����տ� ���� �ε��� ��ȯ
        for (int i = 0; i < enemySpawnInfos.Count; i++)
        {
            if (enemySpawnInfos[i].enemyPrefab == enemyPrefab)
            {
                return i;
            }
        }
        return -1;
    }
}
