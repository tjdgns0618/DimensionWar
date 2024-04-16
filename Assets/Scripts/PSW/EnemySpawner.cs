using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public GameObject enemyPrefab; // 적 프리팹
        public int numberOfEnemiesToSpawn; // 소환할 적의 수
        public float spawnInterval; // 스폰 간격
        public float SpawnDelay; // 이전 info와의 딜레이
    }

    public List<EnemySpawnInfo> enemySpawnInfos; // 적 스폰 정보 리스트
    public Transform[] spawnPoints; // 적을 소환할 위치들의 배열

    private List<GameObject>[] enemyPools; // 적의 오브젝트 풀들의 리스트

    void Start()
    {
        // 적의 오브젝트 풀들을 초기화
        InitializeEnemyPools();

        // 일정 간격으로 적을 소환하는 코루틴 시작
        StartCoroutine(SpawnEnemies());
    }

    void InitializeEnemyPools()
    {
        // 적의 오브젝트 풀들을 초기화
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
            float nextSpawnDelay = enemySpawnInfos[i].SpawnDelay; // 이전 info와의 딜레이
            yield return new WaitForSeconds(nextSpawnDelay);

            for (int j = 0; j < enemySpawnInfos[i].numberOfEnemiesToSpawn; j++)
            {
                // 적 프리팹 선택
                GameObject enemyPrefab = enemySpawnInfos[i].enemyPrefab;

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
                yield return new WaitForSeconds(enemySpawnInfos[i].spawnInterval);
            }
        }
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
