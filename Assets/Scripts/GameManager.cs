using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _intance;

    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    

    [Header("# Player Info")]
    public int level;
    public float Health;
    public float maxHealth = 100;

    [Header("# GameObject")]
    public Tower[] towers;
    
    
 
    // Start is called before the first frame update
    void Awake()
    {
        if (_intance == null)
        {
            _intance = this;

        }
        else if (_intance != this)
        {
            Destroy(gameObject);

        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static GameManager Instance
    {
        get
        {
            if (!_intance)
            {
                _intance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_intance == null)
                {
                    Debug.Log("GameManger is Null");
                }
            }

            return _intance;
        }

        
    }
    public void add_tower(GameObject tower)
    {
        
    }
}
