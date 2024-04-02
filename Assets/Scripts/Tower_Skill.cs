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

    IEnumerator skill_1() //공속증가
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
   IEnumerator skill_2() //상대 공격력 감소
    {
        g = Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);

        EnemyTrans.gameObject.GetComponent<EnemyController>().attackDamage = 10;
        Debug.Log("skill2");
        yield return new WaitForSeconds(3);

        EnemyTrans.gameObject.GetComponent<EnemyController>().attackDamage = 20;
    }
    IEnumerator skill_3()//초당 공격력의 10%의 데미지를 준다.
    {
        g = Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);
        yield return new WaitForSeconds(3);
        
    }
    IEnumerator skill_4()//공격력의 200% 범위 데미지를 주면서 주변 적 이속 30% 감속
    {
        g = Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 0f, id);
        Debug.Log("얼음스킬");
        EnemyTrans.gameObject.GetComponent<TestEnemy>().speed = 10;
        yield return new WaitForSeconds(2f);
        EnemyTrans.gameObject.GetComponent<TestEnemy>().speed = 20;
    }
    IEnumerator skill_5()//공격력의 100%데미지를 주면서 적을묶어 둔다
    {
        g = Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);
        yield return null;
    }
    IEnumerator skill_6()//범위안의 들어있는 모든 타워의 스킬게이지를 20% 회복시킨다.
    {
        CreateSkill();
        yield return new WaitForSeconds(1.5f);
    }
    IEnumerator skill_7()//일직선상에 모든 적들에게 공격력 300%에 데미지를 준다
    {
        CreateSkill();
        yield return new WaitForSeconds(1.5f);
    }
    IEnumerator skill_8()//1.5초뒤 가만히 있다가 쏜다 공격력의 500%데미지를 준다 범위형스킬 
    {
        yield return new WaitForSeconds(1.5f);
        CreateSkill();

    }
    IEnumerator skill_9()//기를 모았다가 3초동안 레이저를 쏜다 공격력의 20%데미지를 초당데미지로 준다.
    {
        CreateSkill();
        yield return new WaitForSeconds(1.5f);
    }

    void CreateSkill()
    {
        Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);
    }

}
