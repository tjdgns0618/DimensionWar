using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // 소환할 적 프리팹들의 리스트
    public List<int> numberOfEnemiesToSpawn; // 각 적 프리팹마다 소환할 적의 수
    public float spawnInterval = 2f; // 적을 소환하는 간격
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
                // 적 프리팹 선택
                GameObject enemyPrefab = enemyPrefabs[i];

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
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    GameObject GetPooledEnemy(GameObject enemyPrefab)
    {
        // 해당 종류의 적 오브젝트 풀에서 비활성화된 적을 찾아 반환
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
