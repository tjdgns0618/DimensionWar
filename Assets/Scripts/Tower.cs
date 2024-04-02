using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int maxEnemiesPerTower = 3;
    public int currentEnemyCount = 0;
    public float health = 100f;


    public int Tower_id;
    public enum Tower_State { Idle, Tagget, Attack, Skill }
    public enum Tower_Type { Meele, Range }

    public enum Tower_Class { Pixel, RowPoly, _3D };

    public Tower_State tower_state;
    public Tower_Type tower_type;
    public Tower_Class tower_class;


    public bool isMelea;        //근접타워인지 판별하기위한변수
    public bool isWall;         //근접타워가 현재 적을 막을수 있는 상태인지 확인하는 변수

    public float AttackRange = 5f;

    public float AttackDel = 3f;

    public float SkillCost = 0;
    public float SkillCount = 0;

    public float Damage = 10;

    public LayerMask targetLayer;
    public RaycastHit[] targets;
    public Transform nearestTarget;
    float attTime;
    GameObject tower;
    public GameObject bullet;
    public Vector3 dir;
    Vector3 scale;
    public Animator anim;
    void Awake()
    {
        tower_state = Tower_State.Idle;
        scale = gameObject.transform.localScale;
    }

    void Update()
    {
        targets = Physics.SphereCastAll(transform.position, AttackRange, Vector3.up, 0, targetLayer);

        nearestTarget = Scan();
        if (nearestTarget != null)
        {
            dir = (nearestTarget.position - transform.position);
        }
        if (nearestTarget != null && tower_state != Tower_State.Skill)
        {
            Attack();
            Look();
        }
        else if (tower_state == Tower_State.Skill || tower_state != Tower_State.Attack)
        {
            Skill();
        }
        else
        {
            tower_state = Tower_State.Idle;
        }
    }
    void Init()
    {

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
    Transform Scan()
    {
        //가장 가까운적 추척
        Transform result = null;
        float diff = Mathf.Infinity;

        foreach (RaycastHit target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);
            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }



        return result;
    }
    void Look()
    {
        if (tower_class == Tower_Class.Pixel)
        {
            if (dir.normalized.x >= 0)
            {
                transform.localScale = new Vector3(scale.x, scale.y, scale.z);
                Debug.Log("오른쪽");
            }
            else if (dir.normalized.x < 0)
            {
                transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
                Debug.Log("왼쪽");
            }
        }
        else
        {


            Quaternion toRotation = Quaternion.LookRotation(dir);


            Vector3 rotateAngle = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 1).eulerAngles;
            transform.rotation = Quaternion.Euler(0, rotateAngle.y, 0);
        }
    }
    void Attack()
    {
        tower_state = Tower_State.Attack;
        if (tower_state == Tower_State.Attack)
        {
            attTime += Time.deltaTime;
            if (attTime >= AttackDel)
            {
                attTime = 0;
                if (SkillCount >= SkillCost)
                {
                    tower_state = Tower_State.Skill;

                }
                if (tower_type == Tower_Type.Range)
                {

                    GameObject g = Instantiate(bullet, transform.position, transform.rotation);
                    g.GetComponent<Bullet>().Init(Damage, 5, dir.normalized);
                    anim.SetTrigger("hit_1");
                }
                else if (tower_type == Tower_Type.Meele)
                {
                    nearestTarget.GetComponent<TestEnemy>().Dameged(Damage);
                    //nearestTarget.GetComponent<EnemyController>().health -= 50;
                }
                SkillCount++;
            }
        }
    }

    void Skill()
    {
        if (tower_state == Tower_State.Skill)
        {
            gameObject.GetComponent<Tower_Skill>().skill(Tower_id);
            SkillCount = 0;
            tower_state = Tower_State.Attack;
        }
    }

    public void AddEnemy()
    {
        currentEnemyCount++;
    }

    // ���� Ÿ������ �����ϴ� �Լ�
    public void RemoveEnemy()
    {
        currentEnemyCount--;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Destroy(gameObject); // Ÿ�� �ı�
        }
    }
}