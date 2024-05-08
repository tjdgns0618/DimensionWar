using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Tower_Skill : MonoBehaviour
{
    [SerializeField]
    private int id;
    float SkillDmg;
    public Transform EnemyTrans;
    public GameObject[] SkillPrefabs;
    public int tower_Level;
    GameObject g;
    EnemyController Enemy;
    public Collider[] colliders;
    public GameObject skillPos;
    public GameObject[] skillPose;
    float time;
    public float SkillDamage;
    public GameObject skillParent;
    public int posNum; 
    public int curTowwer_Skill_Level = 0;
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
                StartCoroutine(skill_1());
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
                StartCoroutine(skill_15());
                break;
            case 16:
                StartCoroutine(skill_16());
                break;
            case 17:
                StartCoroutine(skill_17());
                break;
            case 18:
                StartCoroutine(skill_18());
                break;
            case 19:
                StartCoroutine(skill_19());
                break;
            case 20:
                StartCoroutine(skill_20());
                break;
            case 21:
                StartCoroutine(skill_21());
                break;
            case 22:
                StartCoroutine(skill_22());
                break;
            case 23:
                StartCoroutine(skill_23());
                break;
            case 24:
                StartCoroutine(skill_24());
                break;
            case 25:
                StartCoroutine(skill_25());
                break;
            case 26:
                StartCoroutine(skill_26());
                break;
        }
    }
    IEnumerator skill_0() // ���ݼӵ�����
    {
        float temp = gameObject.GetComponent<Tower>().AttackDel;
        SkillPrefabs[tower_Level].SetActive(true);
        gameObject.GetComponent<Tower>().AttackDel -= temp / 2;
        gameObject.GetComponent<Tower>().anim.speed *= 2;
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<Tower>().AttackDel += temp / 2;
        gameObject.GetComponent<Tower>().anim.speed /= 2;
        SkillPrefabs[tower_Level].SetActive(false);
    }
    IEnumerator skill_1() //10%��������  �� 30% ���ݷ´ٿ�
    {
        g = Instantiate(SkillPrefabs[tower_Level], EnemyTrans.position, EnemyTrans.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 0.1f, id);
        //g.transform.parent = skillParent.transform;

        yield return new WaitForSeconds(5f);
        Destroy(g);
    }
    IEnumerator skill_2()//�ʴ�10%������
    {

        g = Instantiate(SkillPrefabs[tower_Level], skillPos.transform.position, skillPos.transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 0.1f, id);
        //g.transform.parent = skillParent.transform;

        yield return new WaitForSeconds(2f);
        Destroy(g);

    }
    IEnumerator skill_3()//200%�������� �̼Ӱ��� 30% 
    {
        g = Instantiate(SkillPrefabs[tower_Level], EnemyTrans.position, EnemyTrans.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 2.0f, id);
        //g.transform.parent = skillParent.transform;

        yield return new WaitForSeconds(2f);
        Destroy(g);
    }
    IEnumerator skill_4()//���ݷ� 100% �������� �ָ鼭 3�ʰ� ���� ���� �д�
    {
        g = Instantiate(SkillPrefabs[tower_Level], transform.position, transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg, id);
        //g.transform.parent = skillParent.transform;

        yield return new WaitForSeconds(3f);
        Destroy(g);
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
        //g.transform.parent = skillParent.transform;

        yield return new WaitForSeconds(1f);
        Destroy(g);
    }
    IEnumerator skill_6()// ���Ｑ�� ��� ���鿡�� ���ݷ� 300%�� �������� �ش�
    {
        switch (curTowwer_Skill_Level)
        {
            case 0:
                g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], transform.position, transform.rotation);
                g.GetComponentInChildren<Skill>().init(SkillDmg * 3, id); g.SetActive(true); 
                break;

            case 1:
                g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], transform.position, transform.rotation);
                g.GetComponent<Skill>().init(SkillDmg * 6, id); g.SetActive(true); 
                break;

        }
        curTowwer_Skill_Level++;
        if (curTowwer_Skill_Level > tower_Level)
            curTowwer_Skill_Level = 0;
        //g.transform.parent = skillParent.transform;
        yield return null;
    }
    IEnumerator skill_7()// 1.5�� ���� ������ �ִٰ� ��� ���ݷ� 500%�������� �ش�.������ ��ų
    {
        switch (curTowwer_Skill_Level)
        {
            case 0:
                g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], transform.position, transform.rotation);
                g.GetComponent<Skill>().init(SkillDmg * 5, id);
                break;
                
            case 1:
                g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], EnemyTrans.position +transform.up*5, transform.rotation);
                g.GetComponent<Multi_Skill>().init(SkillDmg * 1, id);
                break;

        }
        curTowwer_Skill_Level++;
        if (curTowwer_Skill_Level > tower_Level)
            curTowwer_Skill_Level = 0;
        //g.transform.parent = skillParent.transform;s

        yield return null;
    }
    IEnumerator skill_8()// �⸦ ��Ҵٰ� 3�ʵ��� �������� ���. ���ݷ��� 20%�� �ʴ絥�������ش�
    {
   

        switch (curTowwer_Skill_Level)
        {
            case 0:
                g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], skillPos.transform.position, transform.rotation);
                g.GetComponent<Skill>().init(SkillDmg * 0.2f, id);
                //g.transform.parent = skillParent.transform;
                break;
            case 1:
                g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], EnemyTrans.position, SkillPrefabs[curTowwer_Skill_Level].transform.rotation);
                g.GetComponent<Skill>().init(SkillDmg * 0.5f, id);
                break;
        }
        curTowwer_Skill_Level++;
        if (curTowwer_Skill_Level > tower_Level)
            curTowwer_Skill_Level = 0;
        yield return null;

    }
    // pixel
    IEnumerator skill_9()
    {
        g = Instantiate(SkillPrefabs[tower_Level], skillPos.transform.position, transform.rotation);
        Debug.Log(g.name);
        g.GetComponent<Skill>().ShotInit(SkillDmg * 0.15f, 1, EnemyTrans.transform.position - transform.position, id);

        yield return new WaitForSeconds(4f);
        Destroy(g);
    }
    IEnumerator skill_10()
    {
        if(GetComponent<Tower>().nearestTarget)
        {
        g = Instantiate(SkillPrefabs[tower_Level], skillPos.transform.position, transform.rotation);
        g.GetComponent<Bullet_Skill>().Init(SkillDmg * 0.1f, 3, EnemyTrans.transform.position - transform.position,id);
        // g.transform.parent = skillParent.transform;

        yield return new WaitForSeconds(5f);
        Destroy(g);
        }
    }
    IEnumerator skill_11()
    {
        g = Instantiate(SkillPrefabs[tower_Level], skillPos.transform.position, skillPos.transform.rotation);
        g.transform.LookAt(EnemyTrans.transform.position);
        g.GetComponent<Skill>().init(SkillDmg * 0.15f, id);
        //g.transform.parent = skillParent.transform;

        yield return new WaitForSeconds(4f);
        Destroy(g);
    }
    IEnumerator skill_12()
    {
        var skill_12_dir = EnemyTrans.transform.position - transform.position;
        g = Instantiate(SkillPrefabs[tower_Level], EnemyTrans.transform.position, UnityEngine.Quaternion.Euler(0, transform.rotation.y, 270));
        //g.transform.rotation = UnityEngine.Quaternion.Euler(0, skill_12_dir.y,270);
        g.GetComponent<Skill>().init(SkillDmg * 1f, id);
        //g.transform.parent = skillParent.transform;

        yield return new WaitForSeconds(2f);
        Destroy(g);
    }
    IEnumerator skill_13()
    {
        time = 0;
        SkillPrefabs[tower_Level].SetActive(true);

        while (time < 15f) {
            foreach (RaycastHit ray in gameObject.GetComponent<Tower>().targets)
            {
                if (ray.transform.CompareTag("Tower"))
                {
                    ray.transform.gameObject.GetComponent<Tower>().health += SkillDmg * 0.15f;
                    Debug.Log(name);
                }
            }
            //g.transform.parent = skillParent.transform;

            yield return new WaitForSeconds(1);
        }
        SkillPrefabs[tower_Level].SetActive(false);
        //g.transform.parent = skillParent.transform;

        yield return null;
    }
    IEnumerator skill_14()
    {

        g = Instantiate(SkillPrefabs[tower_Level], EnemyTrans.position + transform.up * 5, SkillPrefabs[tower_Level].transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 1.3f, id);
        g.transform.parent = skillParent.transform;

        yield return new WaitForSeconds(4f);
        Destroy(g);
    }
    
    IEnumerator skill_15()
    {

        g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], EnemyTrans.position, SkillPrefabs[curTowwer_Skill_Level].transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 0.5f, id);
        curTowwer_Skill_Level++;
        if (curTowwer_Skill_Level > tower_Level)
            curTowwer_Skill_Level = 0;
        //g.transform.parent = skillParent.transform;
        g.SetActive(true);
        yield return new WaitForSeconds(5f);
        Destroy(g);
    }
    IEnumerator skill_16()
    {
        g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], EnemyTrans.position, SkillPrefabs[curTowwer_Skill_Level].transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 0.5f, id);

        //g.transform.parent = skillParent.transform;
        curTowwer_Skill_Level++;
        if (curTowwer_Skill_Level > tower_Level)
            curTowwer_Skill_Level = 0;
        g.SetActive(true);
        yield return new WaitForSeconds(5f);
        Destroy(g);
    }
    IEnumerator skill_17()
    {
        switch(curTowwer_Skill_Level)
        {
            case 0:
                Debug.Log("skill17_1");
                gameObject.GetComponent<Tower>().isBuff = true;
                g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], transform.position, SkillPrefabs[curTowwer_Skill_Level].transform.rotation);
                //g.transform.parent = skillParent.transform;
                Debug.Log("skill17");
                yield return new WaitForSeconds(15);
                gameObject.GetComponent<Tower>().isBuff = false;
                Destroy(g);
                break;
            case 1:
                g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], EnemyTrans.position, SkillPrefabs[curTowwer_Skill_Level].transform.rotation);
                g.GetComponentInChildren<Skill>().init(SkillDmg * 0.5f, id);
                yield return new WaitForSeconds(5);
                Destroy(g);
                break;
        }
        curTowwer_Skill_Level++;
        if (curTowwer_Skill_Level > tower_Level)
            curTowwer_Skill_Level = 0;
        yield return null;
    }
    IEnumerator skill_18()
    {
        yield return null;
    }

    IEnumerator skill_19()
    {
        g = Instantiate(SkillPrefabs[tower_Level], skillPos.transform.position, transform.rotation);
        g.GetComponentInChildren<Skill>().init(SkillDmg * 1.5f, id);
        g.GetComponentInChildren<Skill>().parentTower = gameObject;

        yield return new WaitForSeconds(4);
        Destroy(g);
    }
    IEnumerator skill_20()
    {
        g = Instantiate(SkillPrefabs[tower_Level], skillPose[posNum].transform.position, skillPose[posNum].transform.rotation);
        g.GetComponentInChildren<Skill>().init(SkillDmg * 1.5f, id);
        g.GetComponentInChildren<Skill>().parentTower = gameObject;
        yield return new WaitForSeconds(2f);
        Destroy(g.gameObject);
    }
    IEnumerator skill_21()
    {
        GameObject[] bulletpos = GetComponent<Tower_3D>().bulletPose;
        g = Instantiate(SkillPrefabs[tower_Level], bulletpos[1].transform.position, bulletpos[1].transform.rotation);
        g.GetComponent<Skill>().init(SkillDmg * 1f, id);
        yield return new WaitForSeconds(5f);
        Destroy(g);
    }
    IEnumerator skill_22()
    {
        g = Instantiate(SkillPrefabs[tower_Level], transform.position, transform.rotation);
        g.GetComponentInChildren<Skill>().init(SkillDmg * 1f, id);
      //  g.GetComponentInChildren<Skill>().parentTower = gameObject;
        yield return new WaitForSeconds(5f);
        Destroy(g);
    }
    IEnumerator skill_23()
    {
        g = Instantiate(SkillPrefabs[tower_Level], EnemyTrans.transform.position, EnemyTrans.transform.rotation);
        g.GetComponent<Multi_Skill>().init(SkillDmg * 1f, id);

        yield return new WaitForSeconds(5f);
        Destroy(g);
    }
    IEnumerator skill_24()
    {
        switch (curTowwer_Skill_Level)
        {
            case 0:
                g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], transform.position, transform.rotation);
                g.GetComponentInChildren<Skill>().init(SkillDmg * 1f, id);
                g.GetComponentInChildren<Skill>().parentTower = gameObject;
                g.SetActive(true);
                break;

            case 1:
                g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], transform.position, transform.rotation);
                g.GetComponentInChildren<Skill>().init(SkillDmg * 1f, id);
                g.GetComponentInChildren<Skill>().parentTower = gameObject;
                g.SetActive(true);
                break;
        }
        curTowwer_Skill_Level++;
        if (curTowwer_Skill_Level > tower_Level)
            curTowwer_Skill_Level = 0;

        yield return new WaitForSeconds(5f);
        Destroy(g);
    }
    IEnumerator skill_25()
    {
        switch (curTowwer_Skill_Level)
        {
            case 0:
                g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], transform.position, transform.rotation);
                g.GetComponentInChildren<Skill>().init(SkillDmg * 1f, id);
                g.GetComponentInChildren<Skill>().parentTower = gameObject;
                g.SetActive(true);
                break;

            case 1:
                g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], EnemyTrans.position, transform.rotation);
                g.GetComponent<Skill>().init(SkillDmg * 5, id); 
                g.SetActive(true);
                yield return new WaitForSeconds(2);
                Destroy(g);
                break;

        }
        curTowwer_Skill_Level++;
        if (curTowwer_Skill_Level > tower_Level)
            curTowwer_Skill_Level = 0;

        yield return new WaitForSeconds(5f);
        Destroy(g);
    }
    IEnumerator skill_26()
    {
        switch (curTowwer_Skill_Level)
        {
            case 0:
                g = Instantiate(SkillPrefabs[tower_Level], transform.position, transform.rotation);
                g.GetComponentInChildren<Skill>().init(SkillDmg * 2f, id);
                g.GetComponentInChildren<Skill>().parentTower = gameObject;
                g.SetActive(true);
                break;

            case 1:
                g = Instantiate(SkillPrefabs[curTowwer_Skill_Level], EnemyTrans.position, transform.rotation);
                g.GetComponent<Skill>().init(SkillDmg * 5, id);
                g.SetActive(true);
                yield return new WaitForSeconds(2);
                Destroy(g);
                break;

        }
        curTowwer_Skill_Level++;
        if (curTowwer_Skill_Level > tower_Level)
            curTowwer_Skill_Level = 0;
    }

    private void OnEnable()
    {
        foreach (GameObject g in SkillPrefabs)
        {
            if (SkillPrefabs is null || gameObject.GetComponent<Tower>().tower_type != Tower.Tower_Type.Meele)
                break;
            else
                g.gameObject.SetActive(false);
        }
    }
}