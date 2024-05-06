using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Skill : MonoBehaviour
{
    public GameObject m_gameObjectMain;
    public GameObject m_gameObjectTail;
    GameObject m_makedObject;
    public Transform m_hitObject;
    public float maxLength;
    public bool isDestroy;
    public float ObjectDestroyTime;
    public float TailDestroyTime;
    public float HitObjectDestroyTime;
    public float maxTime = 1;
    public float MoveSpeed = 10;
    public bool isCheckHitTag;
    public string mtag;
    public bool isShieldActive = false;
    public bool isHitMake = true;

    float time;
    bool ishit;
    float m_scalefactor;
    public GameObject parent;

    public int parent_id;
    public float parent_skill_damage;
    public bool isMultiskill = false;
    private void Start()
    {
        m_scalefactor = VariousEffectsScene.m_gaph_scenesizefactor;//transform.parent.localScale.x;
                                                                   // parent_id = parent.GetComponent<Skill>().id;
                                                                   // parent_skill_damage = parent.GetComponent<Skill>().Damage;
        time = Time.time;
        parent = transform.parent.gameObject;
        parent_skill_damage = parent.gameObject.gameObject.GetComponent<MultipleObjectsMake1>().parent_skillDmg;
    }

    void LateUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed * m_scalefactor);


    }
    void OnTriggerEnter(Collider other)
    {
        if (!ishit)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                // other.gameObject.GetComponent<EnemyController>().OnDamage(parent_skill_damage);
            }
            ishit = true;
            if (m_gameObjectTail)
                m_gameObjectTail.transform.parent = null;
            MakeHitObject(other.transform);

            Destroy(gameObject);
            Destroy(m_gameObjectTail, TailDestroyTime);
            Destroy(m_makedObject, HitObjectDestroyTime);

        }
    }


    void MakeHitObject(Transform point)
    {
        if (isHitMake == false)
            return;
        m_makedObject = Instantiate(m_hitObject, point.transform.position, point.rotation).gameObject;
        if (!isMultiskill)
        {
            m_makedObject.GetComponent<Skill>().Damage = parent.GetComponent<Skill>().Damage;
            m_makedObject.GetComponent<Skill>().id = parent.GetComponent<Skill>().id;
        }
        m_makedObject.transform.parent = transform.parent;
        m_makedObject.transform.localScale = new Vector3(1, 1, 1);
        // m_makedObject.GetComponent<Skill>().init(parent_skill_damage,parent_id);
    }

}
