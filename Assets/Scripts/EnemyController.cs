using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // 이동에 사용되는 변수들
    private GameObject player; // 플레이어 오브젝트
    private NavMeshAgent navMeshAgent; // NavMesh 에이전트
    private Transform[] pathPoints; // 경로를 이루는 지점들
    private int currentPathIndex = 0; // 현재 경로 인덱스

    // 타워에 대한 참조
    private Tower currentTower; // 현재 충돌한 타워

    // 공격에 사용되는 변수들
    public float attackDamage = 10f; // 적의 공격력
    public float attackCooldown = 1f; // 적의 공격 쿨다운 시간
    private bool isAttacking = false; // 현재 타워를 공격 중인지 여부

    public float health = 100f; // 적의 체력

    public float movementSpeed;

    void Start()
    {
        // 플레이어 게임 오브젝트를 태그로 찾음
        player = GameObject.FindGameObjectWithTag("Player");
        // NavMesh 에이전트를 가져옴
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        movementSpeed = navMeshAgent.speed;
        // 경로를 이루는 지점들을 가져옴
        GameObject pathParent = GameObject.FindGameObjectWithTag("PathParent");
        pathPoints = new Transform[pathParent.transform.childCount];
        for (int i = 0; i < pathParent.transform.childCount; i++)
        {
            pathPoints[i] = pathParent.transform.GetChild(i);
        }

        // 초기 목적지 설정
        SetDestinationToNextPathPoint();
    }

    void Update()
    {
        // 체력이 0 이하인 경우 파괴하고 리턴
        if (health <= 0f)
        {
            if (currentTower != null)
            {
                currentTower.RemoveEnemy(); // 타워의 적 수 감소
            }
            Destroy(gameObject); // 적 오브젝트 파괴
            return;
        }

        // 목적지에 도착했는지 확인
        //if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        //{
        //    Debug.Log("목적지 도착. 다음 목적지 설정");
        //    SetDestinationToNextPathPoint(); // 다음 목적지로 설정
        //}

        // 타워를 공격할 수 있는 상태이면 공격
        if (isAttacking && currentTower != null && currentTower.gameObject != null)
        {
            Debug.Log("타워공격중");
            AttackTower(currentTower);
        }
    }

    // 타워 설정
    public void SetTower(Tower tower)
    {
        currentTower = tower;
    }

    // 타워 제거
    public void RemoveTower()
    {
        currentTower = null;
    }

    void TakeDamage(float damage)
    {
        health -= damage; // 적의 체력 감소
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Tower>()) // 충돌한 오브젝트가 "3D_Tower" 태그를 가진 타워인 경우
        {
            currentTower = other.GetComponent<Tower>(); // 충돌한 타워 가져오기

            // 타워의 적 수가 최대치보다 작은 경우에만 적 추가 및 이동 멈춤
            if (currentTower.currentEnemyCount < currentTower.maxEnemiesPerTower)
            {
                currentTower.AddEnemy(); // 타워의 적 수 증가
                currentTower.enemiesInRange.Add(this); // 적을 타워의 적 리스트에 추가
                navMeshAgent.isStopped = true; // 이동을 멈춤
                Debug.Log("isStopped = true");
                isAttacking = true; // 공격 상태로 변경
            }
            else // 최대 적 수 초과시 현재 타워를 무시하고 다시 이동
            {
                currentTower = null; // 현재 타워 초기화
            }
        }
        else if (other.CompareTag("Player")) // 충돌한 오브젝트가 "Player" 태그를 가진 플레이어인 경우
        {
            PlayerController player = other.GetComponent<PlayerController>(); // 플레이어 컴포넌트 가져오기
            if (player != null)
            {
                player.TakeDamage(attackDamage); // 플레이어에게 데미지 주기
            }
            Destroy(gameObject); // 적 오브젝트 파괴
        }
    }

    void OnTriggerExit(Collider other) // 타워를 통과할 때
    {
        Debug.Log("OnTriggerExit");
        if (other.GetComponent<Tower>() && other.GetComponent<Tower>().isMelea)
        {
            if (currentTower != null)
            {
                currentTower.RemoveEnemy(); // 타워의 적 수 감소
                currentTower = null; // 현재 타워 초기화
                StartMoving(); // 이동 재개
            }
        }
    }

    // 적의 이동 시작
    public void StartMoving()
    {
        Debug.Log("StartMoving");
        SetDestinationToNextPathPoint(); // 웨이포인트로 이동
        navMeshAgent.isStopped = false; // 이동 시작
    }

    void AttackTower(Tower tower)
    {
        // 공격 쿨다운 시간이 지날 때마다 타워 공격
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0f)
        {
            tower.TakeDamage(attackDamage); // 타워에 데미지 주기
            attackCooldown = 1f; // 쿨다운 초기화
        }
    }

    void SetDestinationToNextPathPoint()
    {
        Debug.Log("SetDestinationToNextPathPoint");
        if (currentPathIndex < pathPoints.Length - 1) // 다음 지점으로 이동
        {
            currentPathIndex++;
            Vector3 destination = pathPoints[currentPathIndex].position;
            navMeshAgent.SetDestination(destination);
        }
        else // 경로의 끝에 도달한 경우 플레이어 쪽으로 이동
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    public void OnDamage(float Dmg)
    {
        health -= Dmg;
    }
    public void MoveSpeedCtrl(float speed)
    {
        movementSpeed += speed;
    }
    public void AttackDmgCtrl(float attackdamage)
    {
        attackDamage -= attackdamage;
    }
}