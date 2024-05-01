using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnim : MonoBehaviour
{
    public GameObject parent;
    public int parentid;

    private void Start()
    {
        parentid = parent.GetComponent<Tower_3D>().Tower_id;
    }
    public void parentAttack()
    {
        parent.GetComponent<Tower_3D>().test();
    }
    public void parentSkillcountUp()
    {
        parent.GetComponent<Tower_3D>().SkillCountUp();
    }
    public void parentSkill()
    {
        parent.GetComponent<Tower_Skill>().skill(parentid);
    }
    public void parentSkillEnd()
    {
        parent.GetComponent<Tower_3D>().skillEnd();
    }
}