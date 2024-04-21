using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public List<GameObject> towers = new List<GameObject>();
    public GameObject SelectBlock;
    [HideInInspector]
    public float RoundTime = 0;         // 라운드 시간
    [HideInInspector]
    public Quaternion CAMtempRotation;  // 카메라 로테이션 초기화용
    public EnemySpawner enemySpawner;
    public int Killcount;
    public static GameManager Instance
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

    public UIManager uiManager;
    public bool blockClicked = false;
    public bool towerClicked = false;

    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    

    [Header("# Player Info")]
    public int level;
    public float Health;
    public float maxHealth = 100;

    [Header("# GameObject")]
    public Tower tower;
    public GameObject EnemyCleaner;

    private Camera cam;
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

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        cam = Camera.main.GetComponent<Camera>();
        CAMtempRotation = cam.transform.rotation;
    }
    

    // Update is called once per frame
    void Update()
    {
        //if(tower != null && clicked)
        //    FollowCam();

        removeNullTower();

        // RoundTime += Time.deltaTime;
        // uiManager.RoundTime.text = $"라운드 시작까지 {Mathf.FloorToInt(15-RoundTime)}";
        if(RoundTime >= 15)
        {
            enemySpawner.enabled = true;
            uiManager.RoundTime.gameObject.SetActive(false);
        }
        
    }

    public void FollowCam()
    {
        if (towerClicked)
        {
            Vector3 direction = (tower.transform.position - cam.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            Quaternion rotateValue = Quaternion.RotateTowards(cam.transform.rotation, rotation, 0.1f * Time.unscaledTime);
            cam.transform.rotation = rotateValue;
        }
        else
        {
            Quaternion toRotation = Quaternion.Euler(20, 0, 0);
            cam.transform.rotation = toRotation;
        }
    }

    public void removeNullTower()
    {
        for (int i = 0; i < towers.Count; i++)
        {
            if (!towers[i])
                towers.Remove(towers[i]);
        }
    }
}
