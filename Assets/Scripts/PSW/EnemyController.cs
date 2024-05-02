using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType // 적의 타입
    {
        Ground,
        Air
    }

    public EnemyType enemyType; // 적의 유형
    public float DamageToPlayer = 20f; // 적이 플레이어에게 가하는 데미지

    // 이동에 사용되는 변수들
    private GameObject player; // 플레이어 오브젝트
    private NavMeshAgent navMeshAgent; // NavMesh 에이전트
    private Transform[] pathPoints; // 경로를 이루는 지점들
    private int currentPathIndex = 0; // 현재 경로 인덱스
    public float movementSpeed; // 적의 이동속도 (NavMesh 에이전트의 스피드 사용함)
    private float originalSpeed;
    public float increasedSpeedMultiplier = 1.5f; // 속도 증가 배수

    // 타워에 대한 참조
    [SerializeField]
    private Tower currentTower; // 현재 충돌한 타워

    // 공격에 사용되는 변수들
    public float attackDamage = 10f; // 적의 공격력
    public float attackCooldown = 1f; // 적의 공격 쿨다운 시간
    private bool isAttacking = false; // 현재 타워를 공격 중인지 여부

    public float health = 100f; // 적의 체력

    private Animator WeaponAnimator; // 무기 애니메이터

    private Animator animator; // 상태 애니메이터
    public List<string> attackAnimationNames = new List<string> { "Attack01", "Attack02", "Attack03", "Attack04", "Attack05" }; // 공격 애니메이션 리스트
    private bool isWalking = false; // 걷기 상태 여부

    private bool isDead = false; // 사망 상태 여부

    void Start()
    {
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();

        // 플레이어 게임 오브젝트를 태그로 찾음
        player = GameObject.FindGameObjectWithTag("Player");
        // NavMesh 에이전트를 가져옴
        navMeshAgent = GetComponent<NavMeshAgent>();
        // NavMesh 에이전트의 스피드 사용
        movementSpeed = navMeshAgent.speed;
        // 경로를 이루는 지점들을 가져옴
        GameObject pathParent = GameObject.FindGameObjectWithTag("PathParent");
        pathPoints = new Transform[pathParent.transform.childCount];
        for (int i = 0; i < pathParent.transform.childCount; i++)
        {
            pathPoints[i] = pathParent.transform.GetChild(i);
        }

        // 원래 속도 기록
        originalSpeed = navMeshAgent.speed;

        // 초기 목적지 설정
        SetDestinationToNextPathPoint();
    }

    void Update()
    {
        // 사망 상태인 경우 아무것도 하지 않음
        if (isDead)
        {
            return;
        }
        else
        {
            // 체력이 0 이하인 경우 Die()함수 호출
            if (health <= 0f)
            {
                Die();
                return; // 사망한 경우 이후 코드를 실행하지 않음
            }

            // 목적지에 도착했는지 확인
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
            {
                // 걷기 애니메이션 종료
                animator.SetBool("IsWalking", false);
                SetDestinationToNextPathPoint(); // 다음 목적지로 설정
            }

            // 타워를 공격할 수 있는 상태이면 공격
            if (isAttacking && currentTower != null && currentTower.gameObject != null)
            {
                // 적 타입이 Ground인 경우에만 타워를 공격
                if (enemyType == EnemyType.Ground)
                {
                    AttackTower(currentTower);
                }
            }

            // 걷기 상태 갱신
            isWalking = navMeshAgent.velocity.magnitude > 0.1f;

            // 걷기 상태에 따라 애니메이션 설정
            animator.SetBool("IsWalking", isWalking);
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
        if ((enemyType == EnemyType.Ground) && other.GetComponent<Tower>()) // 적 타입이 Ground이고 충돌한 오브젝트가 "Tower" 태그를 가진 타워인 경우
        {
            currentTower = other.GetComponent<Tower>(); // 충돌한 타워 가져오기

            // 타워의 적 수가 최대치보다 작은 경우에만 적 추가 및 이동 멈춤
            if (currentTower.currentEnemyCount < currentTower.maxEnemiesPerTower)
            {
                currentTower.AddEnemy(); // 타워의 적 수 증가
                currentTower.enemiesInRange.Add(this); // 적을 타워의 적 리스트에 추가
                navMeshAgent.isStopped = true; // 이동을 멈춤
                isAttacking = true; // 공격 상태로 변경
            }
            else // 최대 적 수 초과시 현재 타워를 무시하고 다시 이동
            {
                currentTower = null; // 현재 타워 초기화
            }
        }
        else if (other.CompareTag("Player")) // 충돌한 오브젝트가 "Player" 태그를 가진 플레이어인 경우
        {
            PlayerController playerController = other.GetComponent<PlayerController>(); // 플레이어 컴포넌트 가져오기
            if (playerController != null)
            {
                float damageToPlayer = DamageToPlayer;
                playerController.TakeDamage(damageToPlayer);
            }
            Destroy(gameObject); // 적 오브젝트 파괴
        }
    }

    void OnTriggerExit(Collider other) // 타워를 통과할 때
    {
        // 적 타입이 Air일 경우 리턴
        if (enemyType == EnemyType.Air)
        {
            return;
        }

        if (other.CompareTag("Tower"))
        {
            if (currentTower != null)
            {
                currentTower.RemoveEnemy(); // 타워의 적 수 감소
                currentTower = null; // 현재 타워 초기화
            }
        }
    }
        
    // 적의 이동 시작
    public void StartMoving()
    {
        // 사망 상태인 경우 이동을 시작하지 않음
        if (isDead)
        {
            return;
        }

        SetDestinationToNextPathPoint(); // 웨이포인트로 이동
        navMeshAgent.isStopped = false; // 이동 시작
    }

    void AttackTower(Tower tower)
    {
        // 공격 쿨다운 시간이 지날 때마다 타워 공격
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0f)
        {
            // 무작위로 공격 애니메이션 선택
            string randomAttackAnimation = GetRandomAttackAnimation();

            // 선택된 공격 애니메이션의 트리거 재생
            animator.SetTrigger(randomAttackAnimation);

            tower.TakeDamage(attackDamage); // 타워 체력 감소 
            attackCooldown = 1f; // 쿨다운 초기화
        }
    }


    public void OnDamage(float damage)
    {
        health -= damage;
    }

    void Die()
    {
        if (isDead) // 이미 사망한 경우에는 더 이상 실행하지 않음
        {
            return;
        }
        isDead = true; // 사망 상태로 변경

        GameManager.Instance.gold += 10; // 사망시 골드 흭득
        GameManager.Instance.Killcount++; // 사망시 킬카운트 1증가

        // Die 애니메이션을 재생
        animator.SetTrigger("Die");

        // 적 타워의 적 수 감소
        if ((enemyType == EnemyType.Ground) && currentTower != null)
        {
            currentTower.RemoveEnemy();
        }

        // 네비게이션 메쉬 에이전트 비활성화
        navMeshAgent.enabled = false;

        // 콜라이더 비활성화
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Air 타입인 경우 점진적으로 추락하는 효과 부여
        if (enemyType == EnemyType.Air)
        {
            StartCoroutine(FallDownCoroutine());
        }
        else // Ground 타입인 경우는 즉시 파괴
        {
            StartCoroutine(DestroyAfterDelay(2f));
        }

    }

    IEnumerator FallDownCoroutine()
    {
        float totalTime = 2f; // 추락하는데 걸리는 전체 시간
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition - Vector3.up * 2f; // 추락 거리 조정

        while (elapsedTime < totalTime)
        {
            // 현재 시간에 따라 적당한 비율로 위치를 보간
            float t = elapsedTime / totalTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            // 시간 업데이트
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 끝까지 추락한 후 파괴
        Destroy(gameObject);
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        // delay 시간 대기
        yield return new WaitForSeconds(delay);

        // 적 오브젝트 파괴
        Destroy(gameObject);
    }

    string GetRandomAttackAnimation()
    {
        // 공격 애니메이션들 중 하나를 무작위로 선택하여 반환
        int randomIndex = Random.Range(0, attackAnimationNames.Count);
        return attackAnimationNames[randomIndex];
    }

    void SetDestinationToNextPathPoint()
    {
        if (currentPathIndex < pathPoints.Length - 1) // 다음 지점으로 이동
        {
            currentPathIndex++;
            Vector3 destination = pathPoints[currentPathIndex].position;
            navMeshAgent.SetDestination(destination);
            // 걷기 애니메이션 재생
            animator.SetBool("IsWalking", true);
        }
        else // 경로의 끝에 도달한 경우 플레이어 쪽으로 이동
        {
            navMeshAgent.SetDestination(player.transform.position);
            // 걷기 애니메이션 재생
            animator.SetBool("IsWalking", true);
        }
    }

    // 웨이브가 변경될 때 호출되는 함수
    public void IncreaseSpeedForWave(float multiplier)
    {
        // 속도를 증가된 속도로 설정
        navMeshAgent.speed = originalSpeed * multiplier;
    }
}