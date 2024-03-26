using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum Tower_State
    {
        Idle,
        Attack,
        Die
    }
    Tower_State tower_State;
    public GameObject attack_Core;

    public bool isMelea;        //근접타워인지 판별하기위한변수
    public bool isWall;         //근접타워가 현재 적을 막을수 있는 상태인지 확인하는 변수
    [SerializeField]
    private float AttackRange = 5f;
    [SerializeField]
    public float AttackDel = 3f;
    [SerializeField]
    private float SkillCost = 0;
    [SerializeField]
    private int Damage = 10;

    private GameObject shortOb;

    Vector3 CurPosiotion;

    public SphereCollider sphere;

    public List<GameObject> enemys = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        tower_State = Tower_State.Idle;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeState(Tower_State _Tstate)
    {
        
    }
    IEnumerator SearchEnemy()
    {
        
        //가장 가까운적 추척
        foreach (GameObject enemy in enemys)
        {

        }
       
        yield return null;
    }
    IEnumerator Attack(GameObject g)
    {
        //Instantiate(attack_Core, transform.position, transform.rotation);
        g.gameObject.GetComponent<TestEnemy>().Dameged(Damage);

        Debug.Log("!");
        
        yield return new WaitForSeconds(AttackDel);

    }
    void attackwait()
    {
        StartCoroutine(SearchEnemy());
        StartCoroutine(Attack(shortOb));
    }
    private void OnTriggerEnter(Collider other)
    {
        //리스트에 저장
        if (other.CompareTag("Enemy"))
        {
            enemys.Add(other.gameObject);
            tower_State = Tower_State.Attack;
            shortOb = other.gameObject;
        }
        else if(other.CompareTag("Ui"))
        {

        }
      
    }
    private void OnTriggerStay(Collider other)
    {
        

        
    }
    private void OnTriggerExit(Collider other)
    {
        //리스트에서 제거
        enemys.Remove(other.gameObject);
    }
}
