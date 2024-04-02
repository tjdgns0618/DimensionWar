using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Skill : MonoBehaviour
{
    [SerializeField]
    private int id;
    float SkillDmg;
    Transform EnemyTrans;
    
    public GameObject[] SkillPrefabs;
    GameObject g;
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
        }
    }

    IEnumerator skill_1() //��������
    {

        float temp;
        float dmg;
        temp = GetComponent<Tower>().AttackDel;
        dmg = temp;
        gameObject.GetComponent<Tower>().AttackDel = dmg / 2; 
        Debug.Log("Skill1");
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<Tower>().AttackDel = temp;


    }
   IEnumerator skill_2() //��� ���ݷ� ����
    {
        g = Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);

        EnemyTrans.gameObject.GetComponent<EnemyController>().attackDamage = 10;
        Debug.Log("skill2");
        yield return new WaitForSeconds(3);

        EnemyTrans.gameObject.GetComponent<EnemyController>().attackDamage = 20;
    }
    IEnumerator skill_3()//�ʴ� ���ݷ��� 10%�� �������� �ش�.
    {
        g = Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);
        yield return new WaitForSeconds(3);
        
    }
    IEnumerator skill_4()//���ݷ��� 200% ���� �������� �ָ鼭 �ֺ� �� �̼� 30% ����
    {
        g = Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 0f, id);
        Debug.Log("������ų");
        EnemyTrans.gameObject.GetComponent<TestEnemy>().speed = 10;
        yield return new WaitForSeconds(2f);
        EnemyTrans.gameObject.GetComponent<TestEnemy>().speed = 20;
    }
    IEnumerator skill_5()//���ݷ��� 100%�������� �ָ鼭 �������� �д�
    {
        g = Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);
        yield return null;
    }
    IEnumerator skill_6()//�������� ����ִ� ��� Ÿ���� ��ų�������� 20% ȸ����Ų��.
    {
        CreateSkill();
        yield return new WaitForSeconds(1.5f);
    }
    IEnumerator skill_7()//�������� ��� ���鿡�� ���ݷ� 300%�� �������� �ش�
    {
        CreateSkill();
        yield return new WaitForSeconds(1.5f);
    }
    IEnumerator skill_8()//1.5�ʵ� ������ �ִٰ� ��� ���ݷ��� 500%�������� �ش� ��������ų 
    {
        yield return new WaitForSeconds(1.5f);
        CreateSkill();

    }
    IEnumerator skill_9()//�⸦ ��Ҵٰ� 3�ʵ��� �������� ��� ���ݷ��� 20%�������� �ʴ絥������ �ش�.
    {
        CreateSkill();
        yield return new WaitForSeconds(1.5f);
    }

    void CreateSkill()
    {
        Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);
    }

}
