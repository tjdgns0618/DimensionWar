using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;   // 싱글톤

    public List<GameObject> towers = new List<GameObject>();    // 현재 설치되있는 모든 타워들
    public List<GameObject> enemys = new List<GameObject>();    // 현재 살아있는 적들
    [HideInInspector]
    public float RoundTime = 0;         // 라운드 시간
    [HideInInspector]
    public Quaternion CAMtempRotation;  // 카메라 로테이션 초기화용
    public GameObject SelectBlock;      // 현재 선택된 블럭
    public UIManager uiManager;         // ui매니저
    public EnemySpawner enemySpawner;   // 적스포너
    public int Killcount;               // 현재 죽인 적 카운트

    public static GameManager Instance  // 싱글톤
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    public bool blockClicked = false;   // 현재 블럭이 선택되었는지
    public bool towerClicked = false;   // 현재 타워가 선택되었는지

    [Header("# Game Control")]
    public bool isLive;
    public bool pauseGame;
    public float gameTime;
    

    [Header("# Player Info")]
    public int level;
    public float Health;
    public float maxHealth = 100;
    public int gold = 0;

    [Header("# GameObject")]
    public Tower tower;                 // 현재 선택된 타워 정보
    public GameObject EnemyCleaner;

    [Header("# Augmenter")]
    public float towerDamage;
    public float towerSpeed;
    public float towerHp;
    public float BonusDamage;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();        
    }
    
    void Update()
    {
        RemoveNullTower();
        
        RemoveNullEnemy();

        // RoundTime += Time.deltaTime;
        // uiManager.RoundTime.text = $"라운드 시작까지 {Mathf.FloorToInt(15-RoundTime)}"; // 남은 라운드 시간 표시
        if (RoundTime >= 15)     // 라운드 시간이 전부 지났을경우 스포너 활성화
        {
            enemySpawner.enabled = true;
            uiManager.RoundTime.gameObject.SetActive(false);
        }
        uiManager.GoldText.text = gold.ToString();  // 현재 가지고있는 골드 출력

    }

    public void meleeRespawn()
    {
        for(int i = 0; i < towers.Count; i++)
        {
            if (towers[i].activeSelf == false)
            {
                towers[i].GetComponent<Tower>().health =
                    towers[i].GetComponent<Tower>().tempHealth;
                towers[i].SetActive(true);
            }
        }
    }

    public void RemoveNullEnemy()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            if (!enemys[i])
                enemys.RemoveAt(i);
        }
    }

    // towers에서 사라진 타워를 지워주는 함수
    public void RemoveNullTower()   
    {
        for (int i = 0; i < towers.Count; i++)
        {
            if (!towers[i])
                towers.RemoveAt(i);
        }
    }
}
