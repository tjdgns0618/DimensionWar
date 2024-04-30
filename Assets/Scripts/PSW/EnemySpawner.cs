using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyWave
    {
        public List<GameObject> enemyPrefabs; // 적 프리팹 리스트
        public List<int> numberOfEnemiesPerPrefab; // 프리팹당 적의 수 리스트
        public float spawnInterval; // 스폰 간격
    }

    public List<EnemyWave> EnemyWaves; // 적 스폰 정보 리스트
    public Transform[] spawnPoints; // 적을 소환할 위치들의 배열
    public Button startWaveButton; // 웨이브 시작 버튼

    private List<GameObject>[] enemyPools; // 적의 오브젝트 풀들의 리스트
    private int currentWaveIndex = 0; // 현재 웨이브 인덱스
    private bool isWaveInProgress = false; // 현재 웨이브가 진행 중인지 여부

    void Start()
    {
        startWaveButton.onClick.AddListener(StartNextWave);
        InitializeEnemyPools();
    }

    void InitializeEnemyPools()
    {
        // 적의 오브젝트 풀들을 초기화
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
                // 적 프리팹 선택
                GameObject enemyPrefab = EnemyWaves[currentWaveIndex].enemyPrefabs[j];

                // 소환 위치 선택
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                // 오브젝트 풀에서 비활성화된 적 가져오기
                GameObject enemy = GetPooledEnemy(enemyPrefab);

                // 가져온 적이 있을 경우 위치 설정하고 활성화
                if (enemy != null)
                {
                    enemy.transform.position = spawnPoint.position;
                    enemy.SetActive(true);
                }

                // 다음 소환을 위한 간격만큼 대기
                yield return new WaitForSeconds(EnemyWaves[currentWaveIndex].spawnInterval);
            }
        }

        currentWaveIndex++;

        if (currentWaveIndex >= EnemyWaves.Count)
        {
            // 마지막 웨이브이므로 버튼 비활성화
            startWaveButton.interactable = false;
        }

        isWaveInProgress = false;
    }

    GameObject GetPooledEnemy(GameObject enemyPrefab)
    {
        // 해당 종류의 적 오브젝트 풀에서 비활성화된 적을 찾아 반환
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
        // 주어진 적 프리팹에 대한 인덱스 반환
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
