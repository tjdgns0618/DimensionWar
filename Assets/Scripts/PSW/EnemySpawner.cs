using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyWave
    {
        [System.Serializable]
        public class EnemyPrefabData
        {
            public GameObject enemyPrefab; // 적 프리팹
            public int numberOfEnemies; // 해당 프리팹의 적의 수
            public float enemyHealth; // 적의 체력
            public int goldDropAmount; // 적이 드랍하는 골드의 양
        }

        public List<EnemyPrefabData> enemyPrefabs; // 적 프리팹 데이터 리스트
        public float spawnInterval; // 스폰 간격
        public bool increaseSpeed; // 해당 웨이브에서 적의 속도를 증가시킬지 여부
        public float speedMultiplier; // 속도 증가 배수
        public bool lastEnemySpawned; // 마지막 적이 스폰되었는지 여부

        [HideInInspector]
        public GameObject poolParent; // 해당 웨이브의 오브젝트 풀 Parent
        [HideInInspector]
        public List<GameObject> enemyPool; // 해당 웨이브의 적 오브젝트 풀
    }

    public List<EnemyWave> EnemyWaves; // 적 스폰 정보 리스트
    public Transform[] spawnPoints; // 적을 소환할 위치들의 배열
    public Button startWaveButton; // 웨이브 시작 버튼

    public int count;
    private int currentWaveIndex = 0; // 현재 웨이브 인덱스
    private bool isWaveInProgress = false; // 현재 웨이브가 진행 중인지 여부


    // 적이 모두 사망했음을 알리는 이벤트
    public event Action OnAllEnemiesDead;

    void Start()
    {
        // startWaveButton.onClick.AddListener(StartNextWave);
        InitializeEnemyPools();
    }

    void InitializeEnemyPools()
    {
        // 각 웨이브마다 오브젝트 풀 초기화
        foreach (var wave in EnemyWaves)
        {
            wave.poolParent = new GameObject("Wave" + EnemyWaves.IndexOf(wave) + "_EnemyPool");
            wave.enemyPool = new List<GameObject>();

            foreach (var prefabData in wave.enemyPrefabs)
            {
                for (int i = 0; i < prefabData.numberOfEnemies; i++)
                {
                    GameObject enemy = Instantiate(prefabData.enemyPrefab);
                    // enemy.SetActive(false);
                    enemy.transform.SetParent(wave.poolParent.transform);
                    wave.enemyPool.Add(enemy);
                }
            }
        }
    }

    public void StartNextWave()
    {
        if (!isWaveInProgress)
        {
            count = 0;
            isWaveInProgress = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        EnemyWave currentWave = EnemyWaves[currentWaveIndex]; // 현재 웨이브 가져오기

        // 해당 웨이브의 모든 적을 소환
        foreach (var prefabData in currentWave.enemyPrefabs)
        {
            for (int i = 0; i < prefabData.numberOfEnemies; i++)
            {
                // 적 프리팹 선택
                GameObject enemyPrefab = prefabData.enemyPrefab;

                // 소환 위치 선택
                System.Random random = new System.Random();
                int index = random.Next(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[index];

                // 오브젝트 풀에서 비활성화된 적 가져오기
                GameObject enemy = GetPooledEnemy(currentWave.enemyPool);

                // 가져온 적이 있을 경우 위치 설정하고 활성화
                if (enemy != null)
                {
                    enemy.transform.position = spawnPoint.position;
                    enemy.SetActive(true);
                    count++;
                    Debug.Log(count);

                    // 적의 속도 증가 체크 여부에 따라 속도 증가
                    if (currentWave.increaseSpeed)
                    {
                        IncreaseEnemySpeed(enemy, currentWave.speedMultiplier);
                    }
                }

                //마지막 적인 경우에는 플래그 설정
                //if (i == prefabData.numberOfEnemies - 1 && prefabData == currentWave.enemyPrefabs[currentWave.enemyPrefabs.Count - 1])
                //{
                //    currentWave.lastEnemySpawned = true;
                //}
                if (count == prefabData.numberOfEnemies - 1)
                {
                    currentWave.lastEnemySpawned = true;
                }

                // 다음 소환을 위한 간격만큼 대기
                yield return new WaitForSeconds(currentWave.spawnInterval);
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
        // CheckAllEnemiesDead();
    }

    GameObject GetPooledEnemy(List<GameObject> enemyPool)
    {
        // 해당 오브젝트 풀에서 비활성화된 적을 찾아 반환
        foreach (GameObject enemy in enemyPool)
        {
            //if(enemyPool.FirstOrDefault().activeSelf == true)
            //{
            //    enemyPool[0].transform.SetAsLastSibling();
            //}
            if (!enemy.activeSelf)
            {
                return enemy;
            }
        }
        return null;
    }

    int GetEnemySpawnIndex(GameObject enemyPrefab)
    {
        // 주어진 적 프리팹에 대한 인덱스 반환
        for (int i = 0; i < EnemyWaves.Count; i++)
        {
            if (EnemyWaves[i].enemyPrefabs.Exists(data => data.enemyPrefab == enemyPrefab))
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

    bool CheckAllEnemiesDead()
    {
        if (GameManager.Instance != null) // GameManager.Instance가 null이 아닌지 확인
        {
            bool allDead = true;
            foreach (var wave in EnemyWaves)
            {
                foreach (var enemy in wave.enemyPool)
                {
                    if (enemy != null && enemy.activeInHierarchy)
                    {
                        allDead = false;
                        break;
                    }
                }
            }

            // 마지막 적이 죽었는지 확인
            EnemyWave currentWave = EnemyWaves[currentWaveIndex];
            if (currentWave.lastEnemySpawned && allDead && OnAllEnemiesDead != null)
            {
                GameManager.Instance.meleeRespawn(); // GameManager.Instance가 null이 아닐 때만 호출
                GameManager.Instance.diamond += 3;
                OnAllEnemiesDead.Invoke();
            }

            return allDead; // 모든 적이 사망한지 여부 반환
        }
        return false; // GameManager.Instance가 null인 경우에는 모든 적이 사망한 것으로 간주하지 않음
    }

    void Update()
    {
        // 모든 적이 사망한 경우에만 버튼 활성화
        bool allEnemiesDead = CheckAllEnemiesDead();
        startWaveButton.interactable = !isWaveInProgress && allEnemiesDead;
    }
}