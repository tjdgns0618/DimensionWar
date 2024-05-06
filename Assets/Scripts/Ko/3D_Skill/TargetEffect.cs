using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetEffect : _ObjectsMakeBase
{
    Vector3 Target;
    public float m_startDelay;
    public int m_makeCount;
    public float m_makeDelay;
    public Vector3 m_randomPos;
    public GameObject target;
    public Vector3 m_randomRot;
    public Vector3 m_randomScale;
    public bool isObjectAttachToParent = true;

    float m_Time;
    float m_Time2;
    float m_delayTime;
    float m_count;
    float m_scalefactor;
    public GameObject parent;
    public float parent_skillDmg;
    public int parent_skill_id;
    
    Vector3 skillpos;
    void Start()
    {

        //Target = parent.GetComponent<Skill>().parentTower.GetComponent<Tower_Skill>().EnemyTrans.transform.position;
        //parent_skillDmg = parent.GetComponentInParent<Skill>().Damage;

        m_Time = m_Time2 = Time.time;
        m_scalefactor = VariousEffectsScene.m_gaph_scenesizefactor;
 
    }


    void Update()
    {
        if (Time.time > m_Time + m_startDelay)
        {
            if (Time.time > m_Time2 + m_makeDelay && m_count < m_makeCount)
            {

                Vector3 m_pos = transform.position + GetRandomVector(m_randomPos) * m_scalefactor;
                Quaternion m_rot = transform.rotation * Quaternion.Euler(GetRandomVector(m_randomRot));


                for (int i = 0; i < m_makeObjs.Length; i++)
                {
                    GameObject m_obj = Instantiate(m_makeObjs[i], Target, m_rot);
                    //m_obj.GetComponent<Skill>().init(parent_skillDmg,parent_skill_id);
                    Vector3 m_scale = (m_makeObjs[i].transform.localScale + GetRandomVector2(m_randomScale));
                    if (isObjectAttachToParent)
                        m_obj.transform.parent = this.transform;
                    m_obj.transform.localScale = m_scale;
                }

                m_Time2 = Time.time;
                m_count++;
            }
        }
    }
}
