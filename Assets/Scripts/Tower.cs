using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int Tower_id;
    public enum Tower_State { Idle,Tagget,Attack,Skill}
    public enum Tower_Type { Meele,Range}

    public Tower_State tower_state;
    public Tower_Type tower_type;
    public bool isMelea;        //����Ÿ������ �Ǻ��ϱ����Ѻ���
    public bool isWall;         //����Ÿ���� ���� ���� ������ �ִ� �������� Ȯ���ϴ� ����
    
    public float AttackRange = 5f;
    
    public float AttackDel = 3f;
    
    private float SkillCost = 0;
    private float SkillCount = 0;

    public float Damage = 10;

    public LayerMask targetLayer;
    public RaycastHit[] targets;
    public Transform nearestTarget;
    float  attTime;

    public GameObject bullet;
    public Vector3 dir;
    void Awake()
    {
        tower_state = Tower_State.Idle;
    }

    void Update()
    {
        targets = Physics.SphereCastAll(transform.position, AttackRange, Vector3.up, 0, targetLayer);

        nearestTarget = Scan();
        
        if (nearestTarget!=null)
        {
            Attack();
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
        Gizmos.DrawSphere(transform.position, AttackRange);
    }
    Transform Scan()
    {
        //���� ������� ��ô
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
        tower_state = Tower_State.Attack;

        return result;
    }
    void Attack()
    {
        if(tower_state==Tower_State.Attack)
        {
            attTime += Time.deltaTime;
            if (attTime >= AttackDel)
            {
                attTime = 0;
                SkillCount++;
                Debug.Log("attack"+gameObject.name);
               if(tower_type==Tower_Type.Range)
                {
                    dir = (nearestTarget.position- transform.position).normalized;
                    GameObject g =  Instantiate(bullet, transform.position, transform.rotation);
                    g.GetComponent<Bullet>().Init(Damage, 5,dir);


                }
               else if(tower_type==Tower_Type.Meele)
                {
                    nearestTarget.GetComponent<TestEnemy>().Dameged(Damage);
                }
            }
            if(SkillCount >=SkillCost)
            {
                gameObject.GetComponent<Tower_Skill>().skill(Tower_id);
                SkillCount = 0;

            }
        }




    }
    

}
