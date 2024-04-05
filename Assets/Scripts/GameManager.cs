using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public List<GameObject> towers = new List<GameObject>();

    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
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
    public bool clicked = false;

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

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if(tower != null && clicked)
            FollowCam();
        removeNullTower();
    }

    public void FollowCam()
    {
        Vector3 direction = (tower.transform.position - cam.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        Quaternion rotateValue = Quaternion.RotateTowards(cam.transform.rotation, rotation, 0.1f * Time.unscaledTime);

        cam.transform.rotation = rotateValue;
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
