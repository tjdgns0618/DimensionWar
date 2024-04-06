using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // ��ȯ�� �� �����յ��� ����Ʈ
    public List<int> numberOfEnemiesToSpawn; // �� �� �����ո��� ��ȯ�� ���� ��
    public float spawnInterval = 2f; // ���� ��ȯ�ϴ� ����
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
        enemyPools = new List<GameObject>[enemyPrefabs.Count];
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            enemyPools[i] = new List<GameObject>();
            for (int j = 0; j < numberOfEnemiesToSpawn[i]; j++)
            {
                GameObject enemy = Instantiate(enemyPrefabs[i]);
                enemy.SetActive(false);
                enemyPools[i].Add(enemy);
            }
        }
    }

    System.Collections.IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            for (int j = 0; j < numberOfEnemiesToSpawn[i]; j++)
            {
                // �� ������ ����
                GameObject enemyPrefab = enemyPrefabs[i];

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
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    GameObject GetPooledEnemy(GameObject enemyPrefab)
    {
        // �ش� ������ �� ������Ʈ Ǯ���� ��Ȱ��ȭ�� ���� ã�� ��ȯ
        int index = enemyPrefabs.IndexOf(enemyPrefab);
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
}
