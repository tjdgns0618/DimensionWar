using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void init(float Damage,int id)
    {
        this.Damage = Damage;
        this.id = id;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<TestEnemy>().Dameged(Damage);
            Debug.Log("skill");
            Destroy(gameObject);
        }

    }
    
}
