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
                skill_3();
                break;
            case 3:
                skill_4();
                break;
            case 4:
                skill_5();
                break;
        }
    }

    IEnumerator skill_1() // 해골소환 범위스킬
    {
        g = Instantiate(SkillPrefabs[id], EnemyTrans.position,EnemyTrans.rotation);
        g.GetComponent<Skill>().init(SkillDmg*2.5f,id);
       // g.GetComponent<>();
        yield return new WaitForSeconds(1.5f);
        Destroy(g);
        
    }
    IEnumerator skill_2() // 공격력증가
    {
        float temp;
        float dmg;
        temp = GetComponent<Tower>().Damage;
        dmg = temp;
        gameObject.GetComponent<Tower>().Damage = dmg * 1.5f;

        Debug.Log("Skill2");
        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<Tower>().Damage = temp;

        Debug.Log("Skill2End");
    }
    private void skill_3()//근거리 해골소환
    {
        g = Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);
        
    }  
    private void skill_4()//범위공격 얼음소환
    {
        g = Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);
    } 
    IEnumerator skill_5()//범위공격 
    {

        g = Instantiate(SkillPrefabs[id], EnemyTrans.position, EnemyTrans.rotation);
        yield return null;

    }

}
