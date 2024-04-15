using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Skill : Bullet
{
    Rigidbody rigid;
    public GameObject g;
    GameObject ps;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().OnDamage(damage);
            ps = Instantiate(g, other.transform.position, other.transform.rotation);
            ps.transform.parent = other.transform;
            Destroy(gameObject);

        }
    }

}
