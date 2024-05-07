using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Skill : Bullet
{
    Rigidbody rigid;
    public GameObject g;
    GameObject ps;
    public bool isEnemyTrans;
    //public int id;
    public float skillDamage;
    Vector3 dir;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ps = Instantiate(g, other.transform.position, other.transform.rotation);
            ps.GetComponent<Skill>().init(damage,id);
            if(!isEnemyTrans)
            {
                ps.transform.parent = other.transform;
                other.GetComponent<EnemyController>().OnDamage(damage);
                Destroy(gameObject);
            }
            else
            {

                Destroy(gameObject);
            }

        }
    }

}
