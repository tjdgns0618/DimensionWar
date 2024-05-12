using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Tower : MonoBehaviour
{
    public List<bool> upgrade = new List<bool>();
    public int maxEnemiesPerTower = 3;
    public int currentEnemyCount = 0;
    public float health = 100f;
    public float tempHealth;


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

    public float Damage;
    public float BuffDamage;

    public LayerMask targetLayer;
    public RaycastHit[] targets;
    public Transform nearestTarget;
    public float attTime;
    public GameObject[] bullet;
    public GameObject buffbullet;
    public bool isBuff;
    public bool isSkill =false;

    public bool createbullet= false;
    public Vector3 dir;
    Vector3 scale;

    public Animator anim;
    public GameObject bulletPos;
    public AudioClip attackClip;
    public AudioMixerGroup mixerGroup;
    AudioSource audio;

    // 적 목록
    public List<EnemyController> enemiesInRange = new List<EnemyController>();

    void Awake()
    {
        upgrade.Add(false);
        upgrade.Add(false);
        AudioSetting();
        tempHealth = health;
        tower_state = Tower_State.Idle;
        scale = gameObject.transform.localScale;
        attTime = 0f;
        Init();
    }

    public void AudioSetting()
    {
        audio = GetComponent<AudioSource>();
        audio.outputAudioMixerGroup = mixerGroup;
        audio.clip = attackClip;
    }

    public void AttackSoundPlay()
    {
        audio.Play();
    }

    void Update()
    {

        targets = Physics.SphereCastAll(transform.position, AttackRange, Vector3.up, 0);

        nearestTarget = Scan();
        if (nearestTarget != null)
        {
            dir = (nearestTarget.position - transform.position);
            Look();
        }
        else 
        {
            tower_state = Tower_State.Idle;
        }
        
        if (nearestTarget != null && tower_state != Tower_State.Skill)
        {
            
            Attack();
           
            if (SkillCount >= SkillCost)
            {
                tower_state = Tower_State.Skill;
                SkillCount = 0;
            }
        }
        else if (tower_state == Tower_State.Skill&& nearestTarget != null)
        {
            Skill();
        }
    }
    void Init()
    {
        Damage += GameManager.Instance.towerDamage;
        health += GameManager.Instance.towerHp;
        //AttackDel = GameManager.Instance.towerDamage;
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
            if(target.transform.CompareTag("Enemy"))
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
        }
        return result;
    }
    void Look()
    {
        if (tower_class == Tower_Class.Pixel)
        {
            if (dir.normalized.x >= 0)
            {
                transform.localScale = new Vector3(scale.x/2, scale.y, scale.z);
                GetComponent<TestScript>().ClickEffect.transform.localScale = new Vector3(scale.x, scale.y/2, scale.z);
                //Debug.Log("오른쪽");
            }
            else if (dir.normalized.x < 0)
            {
                transform.localScale = new Vector3(-scale.x/2, scale.y, scale.z);
                GetComponent<TestScript>().ClickEffect.transform.localScale = new Vector3(scale.x, scale.y/2, scale.z);
                //Debug.Log("왼쪽");
            }
        }
        else
        {
            Quaternion toRotation = Quaternion.LookRotation(dir);
            Vector3 rotateAngle = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 1).eulerAngles;
            transform.rotation = Quaternion.Euler(0, rotateAngle.y, 0);
        }
    }

    protected virtual void Attack()
    {
       
        attTime += Time.deltaTime;
        if (attTime >= AttackDel)          
        {          
            attTime = 0;          
            anim.SetTrigger("hit_1");
        }
    }
    public virtual void test()
    {
        AttackSoundPlay();
        if (isBuff)
        {
            if (tower_type == Tower_Type.Range)
            {
                GameObject g = Instantiate(buffbullet, nearestTarget.transform.position, buffbullet.transform.rotation);
                
                g.GetComponent<Bullet>().Init(Damage*BuffDamage, 10, dir.normalized, Tower_id);
                Destroy(g, 2);
            }
            else if (tower_type == Tower_Type.Meele)
            {
                nearestTarget.GetComponent<EnemyController>().OnDamage(Damage);
            }
        }
        else
        {
            if (tower_type == Tower_Type.Range)
            {
                if(createbullet)
                {
                    GameObject g = Instantiate(bullet[0], nearestTarget.transform.position, bullet[0].transform.rotation);

                    g.GetComponent<Bullet>().Init(Damage, 10, dir.normalized, Tower_id);
                    Destroy(g, 10);
                }
                else
                {
                    nearestTarget.GetComponent<EnemyController>().OnDamage(Damage);
                    if(bullet != null)
                    {
                        foreach (GameObject b in bullet)
                        {
                            b.SetActive(true);
                        }
                    }
                    
                }
            }
            else if (tower_type == Tower_Type.Meele)
            {
                nearestTarget.GetComponent<EnemyController>().OnDamage(Damage);
            }
        }
    }
    void BuffAttack()
    {
        
    }
    public void SkillCountUp()
    {
        if(!isBuff)
        SkillCount++;
        if(bullet != null&&tower_type != Tower_Type.Meele)
        {
            foreach (GameObject b in bullet)
            {
                b.SetActive(false);
            }
        }
    }
    public void skillEnd()
    {
        isSkill = false;
        
        tower_state = Tower_State.Attack;
    }
    public virtual void  Skill()
    {

        if (tower_state == Tower_State.Skill && gameObject.tag != "Preview"&&!isSkill)
        {
            anim.SetTrigger("skill");
            isSkill = true;
        }
    }

    public void AddEnemy()
    {
        currentEnemyCount++;
    }


    public void RemoveEnemy()
    {
        currentEnemyCount--;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            // transform.parent.gameObject.layer = 0;
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
    public void DestroyTower()
    {
        Destroy(gameObject);
    }

    // 타워가 파괴될 때 호출되는 메서드
    void OnDisable()
    {
        // 타워에 할당된 적이 있는지 확인하고, 있다면 이동을 재개하도록 함
        //Debug.Log("OnDestroy");
        if (enemiesInRange != null)
        {
            foreach (EnemyController enemy in enemiesInRange)
            {
                if (enemy != null)
                {
                    anim.speed = 1;
                    currentEnemyCount = 0;
                    enemy.RemoveTower();
                    enemy.StartMoving();
                    Debug.Log("enemy.StartMoving");
                }
            }
        }
    }
}