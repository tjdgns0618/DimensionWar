using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEffect : MonoBehaviour
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
    public float parent_skillDmg;
    Vector3 skillpos;
    GameObject g;
    Rigidbody rig;
    private void Start()
    {
        Destroy(gameObject);           
        parent = transform.parent.gameObject;
        parent_skillDmg = parent.GetComponent<Skill>().Damage;
        m_scalefactor = VariousEffectsScene.m_gaph_scenesizefactor;//transform.parent.localScale.x;
        time = Time.time;
        rig = GetComponent<Rigidbody>();
        skillpos = parent.GetComponent<Skill>().parentTower.GetComponent<Tower_Skill>().EnemyTrans.position - parent.GetComponent<Skill>().parentTower.GetComponent<Tower_Skill>().skillPos.transform.position;
    }

    void LateUpdate()
    {
        rig.velocity = skillpos.normalized * MoveSpeed * m_scalefactor;
        //transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed * m_scalefactor);

        
    }
    void OnTriggerEnter(Collider other)
    {
        if (!ishit)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                //ishit = true;
                if (m_gameObjectTail)
                    m_gameObjectTail.transform.parent = null;
                MakeHitObject(other.transform);

                //if (isShieldActive)
                //{
                //    ShieldActivate m_sc = other.transform.GetComponent<ShieldActivate>();
                //    if (m_sc)
                //        m_sc.AddHitObject(other.ClosestPoint(transform.position));
                //}
                other.GetComponent<EnemyController>().OnDamage(parent_skillDmg);
                if(isDestroy)
                {
                    Destroy(gameObject);
                    Destroy(m_gameObjectTail, TailDestroyTime);
                    Destroy(m_makedObject, HitObjectDestroyTime);
                }
            }
        }
    }

    void MakeHitObject(Transform point)
    {
        if (isHitMake == false)
            return;
        m_makedObject = Instantiate(m_hitObject, point.transform.position, point.rotation).gameObject;
        m_makedObject.transform.parent = transform.parent;
        m_makedObject.transform.localScale = new Vector3(1, 1, 1);
    }

   
}
