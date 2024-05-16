using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public int id;
    public int towerSkillLevel;
    public float SpeedCtrl;
    public List<GameObject> hitObject = new List<GameObject>();

    public GameObject ps;
    GameObject parents;
    bool isDmg = true;
    private Rigidbody rigid;
    public GameObject parentTower;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
        Destroy(gameObject,5);
        if (id == 9)
            rigid = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TrackingEnemy(EnemyController enemy)
    {
        transform.LookAt(enemy.transform.position);
    }


    private void OnParticleCollision(GameObject other)
    {   
        Debug.Log(other.tag);
        if(other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            Debug.Log(other.tag);
            switch (id)
            {
                case 4:
                    enemy.OnDamage(Damage+Damage * GameManager.Instance.SkillDamage);
                    enemy.StartCoroutine(enemy.OnStop(3));
                    break;
                case 7:
                    id_7(enemy);
                    Debug.Log("1");
                    break;
               
                case 17:
                    enemy.OnDamage(Damage + Damage * GameManager.Instance.SkillDamage);
                    break;
                case 19:
                    enemy.OnDamage(Damage + Damage * GameManager.Instance.SkillDamage);
                    break;
                case 20:
                    enemy.OnDamage(Damage + Damage * GameManager.Instance.SkillDamage);
                    Debug.Log(enemy.health);
                    break;
                case 22:
                    enemy.OnDamage(Damage + Damage * GameManager.Instance.SkillDamage);
                    break;
            }
        
        }

    }
    public void ShotInit(float damage, int Speed, Vector3 dir,int id)
    {
        this.Damage = damage;
        this.Speed = Speed;
        this.id = id;
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = dir * Speed;
            if (dir.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        
    }
    public void init(float Damage,int id)
    {
        this.Damage = Damage;
        this.id = id;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;
            Debug.Log(enemy.name);
            if (!hitObject.Contains(enemy))
            {
                hitObject.Add(enemy);
                ApplyDamage(enemy);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;
         
        }
    }
    public void ApplyDamage(GameObject enemy)
    {
        Debug.Log("1");
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        if(enemyController != null)
        {
            switch (id)
            {
                case 1:
                    StartCoroutine(enemyController.OnDamageDown(0.3f, 3));
                    StartCoroutine(DotDeal(enemyController,1));
                    break;
                case 2:
                    StartCoroutine(DotDeal(enemyController,1f));
                    break;
                case 3:
                    StartCoroutine(enemyController.OnSpeedDown(0.3f,3));
                    enemyController.OnDamage(Damage + Damage * GameManager.Instance.SkillDamage);
                    break;
                case 4:
                    enemyController.OnDamage(Damage + Damage * GameManager.Instance.SkillDamage);
                    StartCoroutine(enemyController.OnStop(3));
                    break;
                case 6:
                    enemyController.OnDamage(Damage + Damage * GameManager.Instance.SkillDamage);
                    break;
                case 8:
                    StartCoroutine(DotDeal(enemyController, 1f));
                    break;
                case 9:
                    id_9(enemyController);
                    break;
                case 10:
                    StartCoroutine(DotDeal(enemyController, 1f));
                    break;
                case 11:
                    StartCoroutine(DotDeal(enemyController,1f));
                    break;
                case 12:
                    enemyController.OnDamage(Damage + Damage * GameManager.Instance.SkillDamage);
                    break;
                case 14:
                    StartCoroutine(DotDeal(enemyController, 1f));
                    break;
                case 15:
                    StartCoroutine(DotDeal(enemyController, 1f));
                    break;
                case 16:
                    StartCoroutine(DotDeal(enemyController, 1f));
                    break;
                case 18:
                    enemyController.OnDamage(Damage + Damage * GameManager.Instance.SkillDamage);
                    StartCoroutine(enemyController.OnStun(3));
                    break;
                case 21:
                    StartCoroutine(DotDeal(enemyController, 1f));
                    break;
                case 24:
                    StartCoroutine(DotDeal(enemyController, 1f));
                    break;
                case 25:
                    enemyController.OnDamage(Damage + Damage * GameManager.Instance.SkillDamage);
                    break;
                case 26:
                    enemyController.OnDamage(Damage + Damage * GameManager.Instance.SkillDamage);
                    break;
            }
        }
    }
    
    IEnumerator DotDeal(EnemyController enemy,float time)
    {
        while (true)
        {
            enemy.OnDamage(Damage + Damage * GameManager.Instance.SkillDamage);
            Debug.Log(enemy.name + enemy.health);
            yield return YieldCache.WaitForSeconds(time);
        }
    }
    void AttackDamageDown(EnemyController enemy)
    {
        enemy.attackDamage *= (1 - 0.3f);
    }
    void ResetAttackDamage(EnemyController enemy)
    {
        enemy.attackDamage /= (1 - 0.3f);
    }


    void id_7(EnemyController enemy)
    {
        Collider[] c = Physics.OverlapSphere(enemy.transform.position, 2);
        foreach(Collider hit in c)
        {
            hit.gameObject.GetComponent<EnemyController>().OnDamage(Damage + Damage * GameManager.Instance.SkillDamage);    
        }

    }
    void id_9(EnemyController enemy)
    {
        enemy.OnDamage(Damage + Damage * GameManager.Instance.SkillDamage);
        parents = Instantiate(ps, enemy.transform.position, enemy.transform.rotation);
        parents.transform.parent = enemy.transform;
        Destroy(gameObject);
    }

}
