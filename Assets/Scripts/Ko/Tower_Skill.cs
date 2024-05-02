using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower_Skill : MonoBehaviour
{
    [SerializeField]
    private int id;
    float SkillDmg;
    Transform EnemyTrans;
    public GameObject[] SkillPrefabs;
    public int tower_Level;
    GameObject g;
    EnemyController Enemy;
    public Collider[] colliders;
    public GameObject skillPos;
    float time;
    public float SkillDamage;
    public GameObject skillParent;
    // Start is called before the first frame update
    void Awake()
    {
        initSkill();
        skillParent = GameObject.Find("SkillEffects");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }
    void initSkill()
    {

        id = this.gameObject.GetComponent<Tower>().Tower_id;
        SkillDmg = this.gameObject.GetComponent<Tower>().Damage;
        
    }

    public void skill(int _id)
    {
       
        EnemyTrans = GetComponent<Tower>().nearestTarget;
        switch (id)
        {
            case 0:
                StartCoroutine(skill_0());
                break;
            case 1:
                skill_1();
                break;
            case 2:
                StartCoroutine(skill_2());
                break;
            case 3:
                StartCoroutine(skill_3());
                break;
            case 4:
                StartCoroutine(skill_4());
                break;
            case 5:
                StartCoroutine(skill_5());
                break;
            case 6:
                StartCoroutine(skill_6());
                break;
            case 7:
                StartCoroutine(skill_7());
                break;
            case 8:
                StartCoroutine(skill_8());
                break;
            case 9:
                StartCoroutine(skill_9());
                break;
            case 10:
                StartCoroutine(skill_10());
                break;
            case 11:
                StartCoroutine(skill_11());
                break;
            case 12:
                StartCoroutine(skill_12());
                break;
            case 13:
                StartCoroutine(skill_13());
                break;
            case 14:
                StartCoroutine(skill_14());
                break;
            case 15:
                StartCoroutine (skill_15());
                break;
            case 16:
                StartCoroutine(skill_16()); 
                break;
            case 17:
                StartCoroutine(skill_17());
                break;

        }
    }
    IEnumerator skill_0() // ���ݼӵ�����
    {
        float temp = gameObject.GetComponent<Tower>().AttackDel;
        SkillPrefabs[tower_Level].SetActive(true);
        Debug.Log("skill1");
        gameObject.GetComponent<Tower>().AttackDel -= temp / 2;
        gameObject.GetComponent<Tower>().anim.speed *= 2;


        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<Tower>().AttackDel += temp / 2;
        gameObject.GetComponent<Tower>().anim.speed /= 2;
        SkillPrefabs[tower_Level].SetActive(false);
    }
    void skill_1() //10%��������  �� 30% ���ݷ´ٿ�
    {
        g = Instantiate(SkillPrefabs[tower_Level], EnemyTrans.position, EnemyTrans.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 0.1f, id);
        g.transform.parent = skillParent.transform;
    }
    IEnumerator skill_2()//�ʴ�10%������
    {

        g = Instantiate(SkillPrefabs[tower_Level], skillPos.transform.position, skillPos.transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 0.1f, id);
        g.transform.parent = skillParent.transform;

        yield return new WaitForSeconds(2);


    }
    IEnumerator skill_3()//200%�������� �̼Ӱ��� 30% 
    {
        g = Instantiate(SkillPrefabs[tower_Level], EnemyTrans.position, EnemyTrans.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 2.0f, id);
        g.transform.parent = skillParent.transform;

        yield return new WaitForSeconds(2f);

    }
    IEnumerator skill_4()//���ݷ� 100% �������� �ָ鼭 3�ʰ� ���� ���� �д�
    {
        g = Instantiate(SkillPrefabs[tower_Level], transform.position, transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg, id);
        g.transform.parent = skillParent.transform;

        yield return new WaitForSeconds(3f);
    }
    IEnumerator skill_5()// ���� �ȿ� ����ִ� ��� �Ʊ� Ÿ�� ��ų�������� 20%ȸ�� �Ѵ�
    {
        g = Instantiate(SkillPrefabs[tower_Level], transform.position, transform.rotation);
        foreach (RaycastHit ray in gameObject.GetComponent<Tower>().targets)
        {
            if (ray.transform.CompareTag("Tower"))
            {
                ray.transform.gameObject.GetComponent<Tower>().SkillCount++;
            }
        }
        g.transform.parent = skillParent.transform;

        yield return new WaitForSeconds(1f);
    }
    IEnumerator skill_6()// ���Ｑ�� ��� ���鿡�� ���ݷ� 300%�� �������� �ش�
    {
        g = Instantiate(SkillPrefabs[tower_Level], transform.position, transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 3, id);
        g.transform.parent = skillParent.transform;

        yield return null;
    }
    IEnumerator skill_7()// 1.5�� ���� ������ �ִٰ� ��� ���ݷ� 500%�������� �ش�.������ ��ų
    {
        g = Instantiate(SkillPrefabs[tower_Level], transform.position, transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 5, id);
        g.transform.parent = skillParent.transform;

        yield return null;
    }
    IEnumerator skill_8()// �⸦ ��Ҵٰ� 3�ʵ��� �������� ���. ���ݷ��� 20%�� �ʴ絥�������ش�
    {
        g = Instantiate(SkillPrefabs[tower_Level], skillPos.transform.position, transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 0.2f, id);
        g.transform.parent = skillParent.transform;

        yield return null;
    }
    // pixel
    IEnumerator skill_9()
    {
        g = Instantiate(SkillPrefabs[tower_Level], skillPos.transform.position, transform.rotation);
        Debug.Log(g.name);
        g.GetComponent<Skill>().ShotInit(SkillDmg * 0.15f, 1, EnemyTrans.transform.position - transform.position, id);
        // g.GetComponent<Bullet_Skill>().Init(SkillDmg*0.15f, 5, EnemyTrans.transform.position- transform.position);
        g.transform.parent = skillParent.transform;


        yield return null;
    }
    IEnumerator skill_10()
    {
        g = Instantiate(SkillPrefabs[tower_Level], skillPos.transform.position, transform.rotation);
        g.GetComponent<Skill>().ShotInit(SkillDmg*0.1f, 5, EnemyTrans.transform.position - transform.position, id);
        g.transform.parent = skillParent.transform;

        yield return null;
    }
    IEnumerator skill_11()
    {
        g = Instantiate(SkillPrefabs[tower_Level], skillPos.transform.position, skillPos.transform.rotation);
        g.transform.LookAt(EnemyTrans.transform.position);
        g.GetComponent<Skill>().init(SkillDmg * 0.15f, id);
        g.transform.parent = skillParent.transform;

        yield return null;
    }
    IEnumerator skill_12()
    {
        g = Instantiate(SkillPrefabs[tower_Level], skillPos.transform.position, skillPos.transform.rotation);
        g.transform.LookAt(EnemyTrans.transform.position);
        g.GetComponent<Skill>().init(SkillDmg * 1f, id);
        g.transform.parent = skillParent.transform;

        yield return null;
    }
    IEnumerator skill_13()
    {
       time = 0;
       SkillPrefabs[tower_Level].SetActive(true);
        
        while (time<15f) { 
            foreach (RaycastHit ray in gameObject.GetComponent<Tower>().targets)
            {
                if (ray.transform.CompareTag("Tower"))
                {
                    ray.transform.gameObject.GetComponent<Tower>().health += SkillDmg * 0.15f;
                    Debug.Log(name);
                }
            }
            g.transform.parent = skillParent.transform;

            yield return new WaitForSeconds(1);
        }
        SkillPrefabs[tower_Level].SetActive(false);
        g.transform.parent = skillParent.transform;

        yield return null;
    }
    IEnumerator skill_14()
    {
        g = Instantiate(SkillPrefabs[tower_Level], EnemyTrans.position, EnemyTrans.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 1.3f, id);
        g.transform.parent = skillParent.transform;

        yield return null;
    }
    IEnumerator skill_15()
    {
        g = Instantiate(SkillPrefabs[tower_Level], EnemyTrans.position, EnemyTrans.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 0.5f, id);
        g.transform.parent = skillParent.transform;

        yield return null;
    }
    IEnumerator skill_16()
    {
        g = Instantiate(SkillPrefabs[tower_Level], EnemyTrans.position, EnemyTrans.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 0.5f, id);
        g.transform.parent = skillParent.transform;

        yield return null;
    }IEnumerator skill_17()
    {
        Debug.Log("skill17_1");
        gameObject.GetComponent<Tower>().isBuff = true;
        g = Instantiate(SkillPrefabs[tower_Level], transform.position,SkillPrefabs[tower_Level].transform.rotation);
        g.transform.parent = skillParent.transform;

        Debug.Log("skill17");
        yield return new WaitForSeconds(15);
        gameObject.GetComponent<Tower>().isBuff = false;
        yield return null;

    }
}
