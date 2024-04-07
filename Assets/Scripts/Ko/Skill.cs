using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public int id;
    float Deltime;
    private EnemyController enemy;
    public List<GameObject> hitObject = new List<GameObject>();
    ParticleSystem ps;
    public List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
    bool isDmg = true;
    
        
    // Start is called before the first frame update
    void Start()
    {
        ps= GetComponent<ParticleSystem>();
        StartCoroutine(particleDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        // StartCoroutine(particleDestroy());
        Deltime += Time.deltaTime;

    }

    private IEnumerator particleDestroy()
    {
        yield return new WaitForSeconds(gameObject.GetComponent<ParticleSystem>().duration);
        Destroy(gameObject);
    }
   

    private void OnParticleCollision(GameObject other)
    {
        //enemy = other.GetComponent<EnemyController>();
        //enemy.health -= Damage;
        //if(id == 4)
        //{

        //    StartCoroutine(StopCtl());

        //}
        //Debug.Log(enemy.health);
        
    }
    
    public void init(float Damage,int id)
    {
        this.Damage = Damage;
        this.id = id;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            hitObject.Add(other.gameObject);
        }
        foreach (GameObject obj in hitObject)
        {
            enemy = obj.GetComponent<EnemyController>();
            switch(id)
            {
                case 0://tower_skill에서처리
                    break;
                case 1:
                    StartCoroutine(id_1());
                    break;
                case 2:
                    StartCoroutine(id_2());
                    break;
                case 3:
                    if (isDmg)
                        StartCoroutine(id_3());
                    break;
                case 4:
                    StartCoroutine(id_4());
                    StartCoroutine(StopCtl());
                    break;
                case 5:
                    break;
                case 6:
                    StartCoroutine(id_4());
                    break;
                case 7:
                    break;
                case 8:
                    StartCoroutine(id_8());
                break;

            }
            

                
        }
    }
    void AttCtl()
    {
        float attctr = enemy.attackDamage -= enemy.attackDamage * 0.3f;
        enemy.attackDamage -= attctr;
        
        enemy.attackDamage += attctr;
    }
    void SpeedCtl()
    {
        float attctr = enemy.movementSpeed -= enemy.movementSpeed * 1f;
        enemy.attackDamage -= attctr;

        enemy.attackDamage += attctr;
    }
    IEnumerator StopCtl()
    {
        float speed = enemy.movementSpeed;
        enemy.movementSpeed -= enemy.movementSpeed; 
        yield return new WaitForSeconds(3);
        enemy.movementSpeed += speed;
    }
    IEnumerator id_1()
    {
        
        if (Deltime > 1)
        {
            Deltime = 0;
            enemy.health -= Damage;
            Debug.Log(enemy.health);
        }
       

        yield return new WaitForSeconds(gameObject.GetComponent<ParticleSystem>().duration);

    }
    IEnumerator id_2()//2,
    {
        if (Deltime > 1)
        {
            Deltime = 0;
            enemy.health -= Damage;
            
            Debug.Log(enemy.health);
        }
        yield return new WaitForSeconds(gameObject.GetComponent<ParticleSystem>().duration);

    }
    IEnumerator id_3()
    {
        isDmg = false;
        enemy.health -= Damage;
        Debug.Log(enemy.health);
        yield return new WaitForSeconds(gameObject.GetComponent<ParticleSystem>().duration);
    }
    IEnumerator id_4()
    {
        isDmg = false;
        enemy.health -= Damage;
        Debug.Log(enemy.health);
        yield return new WaitForSeconds(gameObject.GetComponent<ParticleSystem>().duration);
    }
    IEnumerator id_8()
    {
        enemy.health -= Damage;
        Debug.Log(enemy.health);
        yield return new WaitForSeconds(gameObject.GetComponent<ParticleSystem>().duration);
    }
}
