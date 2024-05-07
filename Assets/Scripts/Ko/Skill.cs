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

    private IEnumerator particleDestroy()
    {
        yield return new WaitForSeconds(gameObject.GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }


    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.tag);
        if(other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            switch (id)
            {
                case 4:
                    StartCoroutine(id_4(enemy));
                    SpeedDown(enemy);
                    break;
                case 7:
                    id_7(enemy);
                    Debug.Log("1");
                    break;
                case 14:
                    id_14(enemy);
                    break;
                case 20:
                    StartCoroutine(id_20(enemy));
                    Debug.Log(enemy.health);
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
            if (!hitObject.Contains(enemy))
            {
                hitObject.Add(enemy);
                ApplyDamage(enemy);
            }
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
                    AttackDamageDown(enemyController);
                    StartCoroutine(id_1(enemyController));
                    break;
                case 2:
                    StartCoroutine(id_2(enemyController));
                    break;
                case 3:
                    SpeedDown(enemyController);
                    StartCoroutine(id_3(enemyController));
                    break;
                case 4:
                    StartCoroutine(id_4(enemyController));
                    //StopCtl();
                    break;
                case 5:
                    break;
                case 6:
                    StartCoroutine(id_6(enemyController));
                    break;
                case 7:
                    break;
                case 8:
                    StartCoroutine(id_8(enemyController));
                    break;
                case 9:
                    id_9(enemyController);
                    break;
                case 10:
                    StartCoroutine(id_10(enemyController));
                    break;
                case 11:
                    StartCoroutine(id_1(enemyController));
                    break;
                case 12:
                    id_12(enemyController);
                    break;
                case 15:
                    StartCoroutine(id_15(enemyController));
                    break;
                case 16:
                    StartCoroutine(id_16(enemyController));
                    break;
                case 18:
                    id_18(enemyController);
                    Debug.Log("id_18");
                    break;
                case 21:
                    StartCoroutine(id_21(enemyController));
                    break;
                case 24:
                    switch (towerSkillLevel)
                    {
                        case 0:
                            StartCoroutine(id_24(enemyController));
                            break;
                        case 1:
                            StartCoroutine(id_24(enemyController));
                            enemyController.OnStun(3);
                            break;
                    }
                    break;
                case 25:
                    StartCoroutine(id_25(enemyController));
                    break;
                case 26:
                    StartCoroutine(id_26(enemyController));
                    break;
            }
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
    void SpeedDown(EnemyController enemy)//加档皑家
    {
        enemy.movementSpeed *= (1 - SpeedCtrl);
    }void ResetSpeed(EnemyController enemy)//加档皑家
    {
        enemy.movementSpeed /= (1 - SpeedCtrl);
    }
    IEnumerator id_1(EnemyController enemy)
    {
        while (true) { 
            enemy.OnDamage(Damage);
            Debug.Log(enemy.name+enemy.health);
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator id_2(EnemyController enemy)
    {
        while (true)
        {
            enemy.OnDamage(Damage);
            Debug.Log(enemy.name + enemy.health);
            yield return new WaitForSeconds(1);
        }

    }
    IEnumerator id_3(EnemyController enemy)
    {
        enemy.OnDamage(Damage);
        yield return new WaitForSeconds(1f);
    }
    IEnumerator id_4(EnemyController enemy)
    {
        isDmg = false;
        enemy.OnDamage(Damage);
        Debug.Log(enemy.health);
        yield return new WaitForSeconds(gameObject.GetComponent<ParticleSystem>().duration);
    }
    IEnumerator id_6(EnemyController enemy)
    {
        enemy.OnDamage(Damage);
        Debug.Log(enemy.health);
        yield return  null;
    }
    void id_7(EnemyController enemy)
    {
       // enemy.OnDamage(Damage);
        Collider[] c = Physics.OverlapSphere(enemy.transform.position, 2);
        
        foreach(Collider hit in c)
        {
            hit.gameObject.GetComponent<EnemyController>().OnDamage(Damage);    
        }

    }
    IEnumerator id_8(EnemyController enemy)
    {
        while (true)
        {
            enemy.OnDamage(Damage);
            Debug.Log(enemy.name + enemy.health);
            yield return new WaitForSeconds(1);
        }
    }
    void id_9(EnemyController enemy)
    {
        enemy.OnDamage(Damage);
        
        parents = Instantiate(ps, enemy.transform.position, enemy.transform.rotation);
        parents.transform.parent = enemy.transform;
        Destroy(gameObject);
    }
    IEnumerator id_10(EnemyController enemy)
    {

        while (true)
        {
            enemy.OnDamage(Damage);
            Debug.Log(enemy.name + enemy.health);
            yield return new WaitForSeconds(1);
        }
    }

    void id_12(EnemyController enemy)
    {

        enemy.OnDamage(Damage);
    }
    void id_14(EnemyController enemy)
    { 
        enemy.OnDamage(Damage);
    }
    IEnumerator id_15(EnemyController enemy)
    {
        while (true)
        {
            enemy.OnDamage(Damage);
            Debug.Log(enemy.name + enemy.health);
            yield return new WaitForSeconds(1);
        }

    }

    IEnumerator id_16(EnemyController enemy)
    {
        
        while (true)
        {
            enemy.OnDamage(Damage);
            Debug.Log(enemy.name + enemy.health);
            yield return new WaitForSeconds(1);
        }
    }
    void id_18(EnemyController enemy)
    {
        enemy.OnDamage(Damage);
        //  StartCoroutine(enemy.OnStun(1.5f));
    }
    IEnumerator id_20(EnemyController enemy)
    {
        enemy.OnDamage(Damage);
        yield return new WaitForSeconds(1);
    }
    IEnumerator id_21(EnemyController enemy)
    {
        while (true)
        {
            enemy.OnDamage(Damage);
            Debug.Log(enemy.name + enemy.health);
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator id_24(EnemyController enemy)
    {
        while (true)
        {
            enemy.OnDamage(Damage);
            Debug.Log(enemy.name + enemy.health);
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator id_25(EnemyController enemy)
    {
            enemy.OnDamage(Damage);
            Debug.Log(enemy.name + enemy.health);
            yield return new WaitForSeconds(1);
    }
    IEnumerator id_26(EnemyController enemy)
    {
        while (true)
        {
            enemy.OnDamage(Damage);
            Debug.Log(enemy.name + enemy.health);
            yield return new WaitForSeconds(1);
        }
    }
    public void OnDestroy()
    {
        if (id == 1)
        {
            foreach (var enemy in hitObject)
            {
                ResetAttackDamage(enemy.GetComponent<EnemyController>());
            }
        }
        //if(id == 3 || id == 4)
        //{
        //    foreach (var enemy in hitObject)
        //    {
        //        ResetSpeed(enemy.GetComponent<EnemyController>());
        //    }
        //}
    }
}
