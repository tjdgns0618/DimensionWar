using System;
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
        public bool increaseSpeed; // 해당 웨이브에서 적의 속도를 증가시킬지 여부
        public float speedMultiplier; // 속도 증가 배수
    }

    public List<EnemyWave> EnemyWaves; // 적 스폰 정보 리스트
    public Transform[] spawnPoints; // 적을 소환할 위치들의 배열
    public Button startWaveButton; // 웨이브 시작 버튼
    public GameObject poolParent;

    private List<GameObject>[] enemyPools; // 적의 오브젝트 풀들의 리스트
    private int currentWaveIndex = 0; // 현재 웨이브 인덱스
    private bool isWaveInProgress = false; // 현재 웨이브가 진행 중인지 여부

    // 적이 모두 사망했음을 알리는 이벤트
    public event Action OnAllEnemiesDead;

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
                    enemy.transform.SetParent(poolParent.transform);
                    GameManager.Instance.enemys.Add(enemy);
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
        // 해당 웨이브의 모든 적을 소환
        for (int j = 0; j < EnemyWaves[currentWaveIndex].enemyPrefabs.Count; j++)
        {
            for (int k = 0; k < EnemyWaves[currentWaveIndex].numberOfEnemiesPerPrefab[j]; k++)
            {
                // 적 프리팹 선택
                GameObject enemyPrefab = EnemyWaves[currentWaveIndex].enemyPrefabs[j];

                // 소환 위치 선택
                System.Random random = new System.Random();
                int index = random.Next(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[index];

                // 오브젝트 풀에서 비활성화된 적 가져오기
                GameObject enemy = GetPooledEnemy(enemyPrefab);

                // 가져온 적이 있을 경우 위치 설정하고 활성화
                if (enemy != null)
                {
                    enemy.transform.position = spawnPoint.position;
                    enemy.SetActive(true);

                    // 적의 속도 증가 체크 여부에 따라 속도 증가
                    if (EnemyWaves[currentWaveIndex].increaseSpeed)
                    {
                        IncreaseEnemySpeed(enemy, EnemyWaves[currentWaveIndex].speedMultiplier);
                    }
                }

                // 다음 소환을 위한 간격만큼 대기
                yield return new WaitForSeconds(EnemyWaves[currentWaveIndex].spawnInterval);
            }
        }

        // 현재 웨이브 인덱스 증가
        currentWaveIndex++;

        // 모든 웨이브를 완료했는지 확인
        if (currentWaveIndex >= EnemyWaves.Count)
        {
            // 마지막 웨이브이므로 버튼 비활성화
            startWaveButton.interactable = false;
        }

        // 현재 웨이브 종료
        isWaveInProgress = false;

        // 적이 모두 사망했는지 확인
        CheckAllEnemiesDead();
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

    // 적의 속도를 증가시키는 함수
    void IncreaseEnemySpeed(GameObject enemy, float multiplier)
    {
        UnityEngine.AI.NavMeshAgent agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            agent.speed *= multiplier;
        }
    }

    // 적이 모두 사망했는지 확인하는 함수
    void CheckAllEnemiesDead()
    {
        if (GameManager.Instance != null) // GameManager.Instance가 null이 아닌지 확인
        {
            bool allDead = true;
            foreach (var pool in enemyPools)
            {
                foreach (var enemy in pool)
                {
                    if (enemy != null && enemy.activeInHierarchy)
                    {
                        allDead = false;
                        break;
                    }
                }
            }

            if (allDead && OnAllEnemiesDead != null)
            {
                GameManager.Instance.meleeRespawn(); // GameManager.Instance가 null이 아닐 때만 호출
                OnAllEnemiesDead.Invoke();
            }
        }
    }
}