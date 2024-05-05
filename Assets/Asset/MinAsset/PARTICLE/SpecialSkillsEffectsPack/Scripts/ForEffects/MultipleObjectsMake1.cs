using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleObjectsMake1 : _ObjectsMakeBase
{
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

    
    public Vector3 m_pos;

    public Vector3 m_pos1;

    public float parent_skillDmg;
    Vector3 skillpos;
    public float upperPos;
    void Start()
    {
        m_Time = m_Time2 = Time.time;
        m_scalefactor = VariousEffectsScene.m_gaph_scenesizefactor; //transform.parent.localScale.x; 
        skillpos = gameObject.GetComponent<Skill>().parentTower.GetComponent<Tower_Skill>().EnemyTrans.position;
        m_pos1 = skillpos+Vector3.up* upperPos;
        //parent_skillDmg = parent.GetComponent<Skill>().Damage;
        
    }


    void Update()
    {
        if (Time.time > m_Time + m_startDelay)
        {
            if (Time.time > m_Time2 + m_makeDelay && m_count < m_makeCount)
            {
                m_pos = GetRandomVector(m_randomPos) * m_scalefactor;

                Quaternion m_rot = transform.rotation * Quaternion.Euler(GetRandomVector(m_randomRot));


                for (int i = 0; i < m_makeObjs.Length; i++)
                {
                    GameObject m_obj = Instantiate(m_makeObjs[i], m_pos1+m_pos, m_rot);
                    Vector3 m_scale = (m_makeObjs[i].transform.localScale + GetRandomVector2(m_randomScale));
                    if(isObjectAttachToParent)
                        m_obj.transform.parent = this.transform;
                    m_obj.transform.localScale = m_scale;
                    m_obj.GetComponent<DropEffect>().parent = gameObject;

                }

                m_Time2 = Time.time;
                m_count++;
            }
        }
    }
}
