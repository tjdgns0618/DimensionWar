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
    public GameObject SkillPrefabs;
    GameObject g;
    EnemyController Enemy;
    public Collider[] colliders;
    public GameObject skillPos;
    // Start is called before the first frame update
    void Awake()
    {
        initSkill();
    }

    // Update is called once per frame
    void Update()
    {

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
                StartCoroutine(skill_1());
                break;
            case 1:
                StartCoroutine(skill_2());
                break;
            case 2:
                StartCoroutine(skill_3());
                break;
            case 3:
                StartCoroutine(skill_4());
                break;
            case 4:
                StartCoroutine(skill_5());
                break;
            case 5:
                StartCoroutine(skill_6());
                break;
            case 6:
                StartCoroutine(skill_7());
                break;
            case 7:
                StartCoroutine(skill_8());
                break;
            case 8:
                StartCoroutine(skill_9());
                break;
            case 9:
                break;

        }
    }
    IEnumerator skill_1() // ���ݼӵ�����
    {
        float temp = gameObject.GetComponent<Tower>().AttackDel;
        SkillPrefabs.SetActive(true);
        Debug.Log("skill1");
        gameObject.GetComponent<Tower>().AttackDel -= temp / 2;
        gameObject.GetComponent<Tower>().anim.speed *= 2;


        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<Tower>().AttackDel += temp / 2;
        gameObject.GetComponent<Tower>().anim.speed /= 2;
        SkillPrefabs.SetActive(false);
    }
    IEnumerator skill_2() //30%��������  �� 10% ���ݷ´ٿ�
    {
        g = Instantiate(SkillPrefabs, EnemyTrans.position, EnemyTrans.rotation);
        

        g.GetComponent<Skill>().init(SkillDmg * 0.3f, id);
        yield return new WaitForSeconds(5f);
        


    }
    IEnumerator skill_3()//�ʴ�10%������
    {

        g = Instantiate(SkillPrefabs, skillPos.transform.position, skillPos.transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 0.1f, id);
        yield return new WaitForSeconds(2);


    }
    IEnumerator skill_4()//200%�������� �̼Ӱ��� 30% 
    {
        g = Instantiate(SkillPrefabs, EnemyTrans.position, EnemyTrans.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 2.0f, id);

        yield return new WaitForSeconds(2f);

    }
    IEnumerator skill_5()//���ݷ� 100% �������� �ָ鼭 3�ʰ� ���� ���� �д�
    {
        g = Instantiate(SkillPrefabs, transform.position, transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg, id);
        foreach (RaycastHit ray in gameObject.GetComponent<Tower>().targets)
        {
            if (ray.transform.CompareTag("Enemy"))
            {

            }
            yield return new WaitForSeconds(3f);

        }

    }
    IEnumerator skill_6()// ���� �ȿ� ����ִ� ��� �Ʊ� Ÿ�� ��ų�������� 20%ȸ�� �Ѵ�
    {
        g = Instantiate(SkillPrefabs, transform.position, transform.rotation);
        foreach (RaycastHit ray in gameObject.GetComponent<Tower>().targets)
        {
            if (ray.transform.CompareTag("Tower"))
            {
                ray.transform.gameObject.GetComponent<Tower>().SkillCount++;
            }
        }
        yield return new WaitForSeconds(1f);
    }
    IEnumerator skill_7()// ���Ｑ�� ��� ���鿡�� ���ݷ� 300%�� �������� �ش�
    {
        g = Instantiate(SkillPrefabs, transform.position, transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 3, id);
        yield return null;
    }
    IEnumerator skill_8()// 1.5�� ���� ������ �ִٰ� ��� ���ݷ� 500%�������� �ش�.������ ��ų
    {
        g = Instantiate(SkillPrefabs, transform.position, transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 5, id);
        yield return null;
    }
    IEnumerator skill_9()// �⸦ ��Ҵٰ� 3�ʵ��� �������� ���. ���ݷ��� 20%�� �ʴ絥�������ش�
    {
        g = Instantiate(SkillPrefabs,skillPos.transform.position, transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 0.2f, id);
        yield return null;
    }
    
}
