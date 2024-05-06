using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnim : MonoBehaviour
{
    public GameObject parent;
    public int parentid;
    public int pos;
    private void Start()
    {
        parentid = parent.GetComponent<Tower_3D>().Tower_id;
    }
    public void parentAttack()
    {
        parent.GetComponent<Tower_3D>().test();
    }
    public void parentDoubleAttack()
    {
        parent.GetComponent<Tower_3D>().attdouble(pos);
    }
    public void parentSkillcountUp()
    {
        parent.GetComponent<Tower_3D>().SkillCountUp();
    }
    public void parentSkill()
    {
        parent.GetComponent<Tower_Skill>().skill(parentid);
    }
    public void parentDoubleSkill()
    {
        parent.GetComponent<Tower_Skill>().posNum = pos;
        parent.GetComponent<Tower_Skill>().skill(parentid);
    }
    public void parentSkillEnd()
    {
        parent.GetComponent<Tower_3D>().skillEnd();
    }
}