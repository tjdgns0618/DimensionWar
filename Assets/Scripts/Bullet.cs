using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int Speed;
    Rigidbody rigid;
    public bool isBuffbullet = false;
    public bool isDestroy = false;
    // Start is called before the first frame update
    void Awake()
    {
        if(!isBuffbullet)
        rigid = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(float damage, int Speed, Vector3 dir)
    {
        this.damage = damage;
        this.Speed = Speed;
        if (!isBuffbullet)
        {
            rigid.velocity = dir * Speed;
            if (dir.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().OnDamage(damage);
            if(isDestroy)
            Destroy(gameObject);
        }
    }

}
