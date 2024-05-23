    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multi_Skill : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> SkillList;
    public int id;
    public float Damage;

    public void init(float Damage,int id)
    {
        this.Damage=Damage;
        this.id=id;
    }
    private void Start()
    {
        foreach (GameObject child in SkillList)
        {
            child.GetComponent<MultipleObjectsMake>().parent_skillDmg = Damage;
        }
    }
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}
