using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_3D : Tower
{
    public Animator[] Attanim;
    public Animator[] Skillanim;
    public int attcount=0;
    public bool doubleattack = false;
    public bool doubleSkillattack = false;
    // Start is called before the first frame update

    protected override void Attack()
    {
      
        attTime += Time.deltaTime;
        
            if (attTime >= AttackDel)
            {
                 attTime = 0;
            if (doubleattack)
            {

                Attanim[0].SetTrigger("hit_1");
                Attanim[1].SetTrigger("hit_1");
            }
            else
            {
                if (Attanim.Length >= 2)
                {
                    if ((attcount %= 2) == 0 && attcount != 0)
                    {
                        Attanim[0].SetTrigger("hit_1");
                    }
                    else
                    {
                        Attanim[1].SetTrigger("hit_1");
                        attcount++;
                    }
                }
               
            
            }
        }
        
        
    }
    public override void Skill()
    {
        if (tower_state == Tower_State.Skill && gameObject.tag != "Preview")
        {
            isSkill = true; 
            if(doubleSkillattack)
            {

                Skillanim[0].SetTrigger("skill");
                Skillanim[1].SetTrigger("skill");
                
            }
           else
            {
                if (Skillanim.Length > 2)
                {
                    if ((attcount /= 2) == 0)
                    {
                        Skillanim[0].SetTrigger("skill");
                    }
                    else
                    {
                        Skillanim[1].SetTrigger("skill");
                    }
                }
                else
                {
                    Skillanim[0].SetTrigger("skill");
                }
            }
        }
    }
}
