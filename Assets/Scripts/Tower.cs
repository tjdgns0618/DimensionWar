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

    public bool isMelea;        //����Ÿ������ �Ǻ��ϱ����Ѻ���
    public bool isWall;         //����Ÿ���� ���� ���� ������ �ִ� �������� Ȯ���ϴ� ����
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
        
        //���� ������� ��ô
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
        //����Ʈ�� ����
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
        //����Ʈ���� ����
        enemys.Remove(other.gameObject);
    }
}
