using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyWave
    {
        public List<GameObject> enemyPrefabs; // �� ������ ����Ʈ
        public List<int> numberOfEnemiesPerPrefab; // �����մ� ���� �� ����Ʈ
        public float spawnInterval; // ���� ����
    }

    public List<EnemyWave> EnemyWaves; // �� ���� ���� ����Ʈ
    public Transform[] spawnPoints; // ���� ��ȯ�� ��ġ���� �迭
    public Button startWaveButton; // ���̺� ���� ��ư

    private List<GameObject>[] enemyPools; // ���� ������Ʈ Ǯ���� ����Ʈ
    private int currentWaveIndex = 0; // ���� ���̺� �ε���
    private bool isWaveInProgress = false; // ���� ���̺갡 ���� ������ ����

    void Start()
    {
        startWaveButton.onClick.AddListener(StartNextWave);
        InitializeEnemyPools();
    }

    void InitializeEnemyPools()
    {
        // ���� ������Ʈ Ǯ���� �ʱ�ȭ
        enemyPools = new List<GameObject>[EnemyWaves.Count];
        for (int i = 0; i < EnemyWaves.Count; i++)
        {
            enemyPools[i] = new List<GameObject>();
            for (int j = 0; j < EnemyWaves[i].enemyPrefabs.Count; j++)
            {
                for (int k = 0; k < EnemyWaves[i].numberOfEnemiesPerPrefab[j]; k++)
                {
                    GameObject enemy = Instantiate(EnemyWaves[i].enemyPrefabs[j]);
                    enemy.SetActive(false);
                    enemyPools[i].Add(enemy);
                }
            }
        }
    }

    public void StartNextWave()
    {
        if (!isWaveInProgress)
        {
            isWaveInProgress = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        for (int j = 0; j < EnemyWaves[currentWaveIndex].enemyPrefabs.Count; j++)
        {
            for (int k = 0; k < EnemyWaves[currentWaveIndex].numberOfEnemiesPerPrefab[j]; k++)
            {
                // �� ������ ����
                GameObject enemyPrefab = EnemyWaves[currentWaveIndex].enemyPrefabs[j];

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
                yield return new WaitForSeconds(EnemyWaves[currentWaveIndex].spawnInterval);
            }
        }

        currentWaveIndex++;

        if (currentWaveIndex >= EnemyWaves.Count)
        {
            // ������ ���̺��̹Ƿ� ��ư ��Ȱ��ȭ
            startWaveButton.interactable = false;
        }

        isWaveInProgress = false;
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
        for (int i = 0; i < EnemyWaves.Count; i++)
        {
            if (EnemyWaves[i].enemyPrefabs.Contains(enemyPrefab))
            {
                return i;
            }
        }
        return -1;
    }
}
