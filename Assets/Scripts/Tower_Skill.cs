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

    IEnumerator skill_1() // 해골소환 범위스킬
    {

        yield return new WaitForSeconds(0.5f);

        g = Instantiate(SkillPrefabs[id], EnemyTrans.position,EnemyTrans.rotation);
        g.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(1.5f);
        g.GetComponent<Collider>().enabled = true;
        g.GetComponent<Skill>().init(SkillDmg * 2.5f, id);
        

    }
   IEnumerator skill_2() // 공격력증가
    {
        float temp;
        float dmg;
        temp = GetComponent<Tower>().Damage;
        dmg = temp;
        gameObject.GetComponent<Tower>().Damage = dmg * 1.5f;

        Debug.Log("Skill2");
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<Tower>().Damage = temp;
    }
    IEnumerator skill_3()//원거리 단일스킬
    {
        g = Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);
        yield return new WaitForSeconds(3);
    }
    IEnumerator skill_4()//범위공격 얼음소환
    {
        g = Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 0f, id);
        Debug.Log("얼음스킬");
        EnemyTrans.gameObject.GetComponent<TestEnemy>().speed = 10;
        yield return new WaitForSeconds(2f);
        EnemyTrans.gameObject.GetComponent<TestEnemy>().speed = 20;
    }
    IEnumerator skill_5()//범위공격 
    {
        g = Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);
        yield return null;
    }

    

}
