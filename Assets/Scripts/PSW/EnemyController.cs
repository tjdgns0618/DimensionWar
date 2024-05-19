using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType // 적의 타입을 정의하는 열거형
    {
        Ground,
        Air
    }

    public EnemyType enemyType; // 적의 유형
    public float DamageToPlayer = 20f; // 플레이어에게 가하는 데미지

    public bool isBoss = false;
    private bool isLinking;

    // 이동에 사용되는 변수들
    private GameObject player; // 플레이어 오브젝트
    private NavMeshAgent navMeshAgent; // NavMesh 에이전트
    private Transform[] pathPoints; // 경로를 이루는 지점들
    private int currentPathIndex = 0; // 현재 경로 인덱스
    public float movementSpeed; // 적의 이동속도
    private float originalSpeed; // 원래 이동속도
    public float increasedSpeedMultiplier = 1.5f; // 속도 증가 배수

    // 타워에 대한 참조
    [SerializeField]
    private Tower currentTower; // 현재 충돌한 타워

    // 공격에 사용되는 변수들
    public float attackDamage = 10f; // 공격력
    public float attackCooldown = 1f; // 공격 쿨다운 시간
    private bool isAttacking = false; // 현재 타워를 공격 중인지 여부

    [SerializeField] private Slider hpBar; // 체력바 슬라이더
    public float health = 100f; // 적의 체력
    public float tempHealth; // 체력 임시 저장 변수

    public int goldDropAmount = 10; // 골드 드랍량

    private Animator WeaponAnimator; // ���� �ִϸ�����

    private Animator animator; // 상태 애니메이터
    public List<string> attackAnimationNames = new List<string> { "Attack01", "Attack02", "Attack03", "Attack04", "Attack05" }; // 공격 애니메이션 리스트
    private bool isWalking = false; // 걷기 상태 여부

    private bool isDead = false; // 사망 상태 여부
    public bool isStun = false; // 기절 상태 여부
    float Waittime;

    void Start()
    {
        // 체력바 슬라이더의 최댓값과 초기값 설정
        if (hpBar != null)
        {
            hpBar.maxValue = health;
            hpBar.value = health;
        }

        // Animator 컴포넌트를 가져옴
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

        isLinking = false;
    }

    void Update()
    {
        // 체력바 갱신
        UpdateHealthBar();

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

            // 기절 상태인 경우 아무것도 하지 않음
            if (isStun)
            {
                return;
            }
            // 목적지에 도착한 경우 다음 지점으로 이동
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
            {
                animator.SetBool("IsWalking", false);
                SetDestinationToNextPathPoint(); // 다음 지점으로 이동
            }

            // 공격 중이며 현재 타워가 존재하는 경우 타워 공격
            if (isAttacking && currentTower != null && currentTower.gameObject != null)
            {
                // 적의 유형이 지상 유닛인 경우에만 타워 공격
                if (enemyType == EnemyType.Ground)
                {
                    AttackTower(currentTower);
                }
            }

            // 걷는 상태 여부 갱신
            isWalking = navMeshAgent.velocity.magnitude > 0.1f;

            animator.SetBool("IsWalking", isWalking);

            // 애니메이션 속도 조절
            AdjustAnimationSpeed();
        }

        void UpdateHealthBar()
        {
            // 체력바가 비어있는 경우 아무것도 하지 않음
            if (hpBar == null)
            {
                return;
            }

            // 체력바 값을 체력에 맞춤
            hpBar.value = health;
        }
    }

    private void FixedUpdate()
    {
        if (navMeshAgent.isOnOffMeshLink && isLinking == false)
        {
            float tempspeed = navMeshAgent.speed;
            StartCoroutine(nameof(MoveAcrossOffLink));
            navMeshAgent.speed = tempspeed;
            navMeshAgent.updateRotation = true;
            isLinking = true;
            navMeshAgent.CompleteOffMeshLink();
            isLinking = false;
        }
    }

    IEnumerator MoveAcrossOffLink()
    {
        OffMeshLinkData data = navMeshAgent.currentOffMeshLinkData;
        navMeshAgent.updateRotation = false;
        navMeshAgent.speed = 500f;
        yield return null;
    }

    // 타워 설정 함수
    public void SetTower(Tower tower)
    {
        currentTower = tower;
    }

    // 타워 제거 함수
    public void RemoveTower()
    {
        currentTower = null;
    }

    // 데미지를 받는 함수
    void TakeDamage(float damage)
    {
        // 체력바 값을 먼저 갱신
        if (hpBar != null)
        {
            hpBar.value -= damage;
        }

        // 적의 체력을 감소
        health -= damage;

        // 체력이 0 이하로 떨어졌을 때 처리
        if (health <= 0f)
        {
            health = 0f; // 체력을 0으로 설정
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((enemyType == EnemyType.Ground) && other.GetComponent<Tower>()) // 지상 유닛이고 타워와 충돌한 경우
        {
            currentTower = other.GetComponent<Tower>(); // 현재 타워 설정

            if (currentTower.currentEnemyCount < currentTower.maxEnemiesPerTower)
            {
                currentTower.AddEnemy(); // 타워에 적 추가
                currentTower.enemiesInRange.Add(this); // 타워의 범위 내에 적 추가
                navMeshAgent.isStopped = true; // 이동 중지
                isAttacking = true; // 공격 상태 설정
            }
            else // 최대 적 수에 도달한 경우 이동 중지
            {
                currentTower = null; // 타워 초기화
            }
        }
        else if (other.CompareTag("Player")) // 플레이어와 충돌한 경우
        {
            PlayerController playerController = other.GetComponent<PlayerController>(); // 플레이어 컨트롤러 가져옴
            if(this.isBoss == true && playerController != null)
                playerController.TakeDamage(20);
            if (playerController != null)
            {
                playerController.TakeDamage(1); // 플레이어 데미지 입힘
            }
            gameObject.SetActive(false); // 게임 오브젝트 비활성화
        }
    }

    void OnTriggerExit(Collider other) // 적이 타워의 범위를 벗어난 경우
    {
        // 공중 유닛인 경우
        if (enemyType == EnemyType.Air)
        {
            return;
        }

        // 타워와 충돌한 경우
        if (other.CompareTag("Tower")) 
        {
            if (currentTower != null)
            {
                currentTower.RemoveEnemy(); // 타워에서 적 제거
                currentTower = null; // 타워 초기화
            }
        }
    }

    // 이동 시작 함수
    public void StartMoving()
    {
        // 사망 상태인 경우 이동하지 않음
        if (isDead)
        {
            return;
        }

        SetDestinationToNextPathPoint(); // 다음 지점으로 이동
        navMeshAgent.isStopped = false; // 이동 시작
    }

    void AttackTower(Tower tower)
    {
        // 공격 쿨다운 감소
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0f)
        {
            // 랜덤한 공격 애니메이션 선택
            string randomAttackAnimation = GetRandomAttackAnimation();

            // 해당 애니메이션을 트리거로 설정
            animator.SetTrigger(randomAttackAnimation);

            // 공격 애니메이션을 가진 무기 오브젝트들에 대해 애니메이션을 트리거로 설정
            GameObject[] weaponObjects = GameObject.FindGameObjectsWithTag("Weapon");
            foreach (GameObject weaponObject in weaponObjects)
            {
                Animator weaponAnimator = weaponObject.GetComponent<Animator>();
                if (weaponAnimator != null)
                {
                    weaponAnimator.SetTrigger(randomAttackAnimation);
                }
            }

            tower.TakeDamage(attackDamage); // 타워에 데미지 입힘
            attackCooldown = 1f; // 쿨다운 초기화
        }
    }

    // 애니메이션 속도 조절 함수
    void AdjustAnimationSpeed()
    {
        // 걷는 애니메이션 속도 조절
        animator.SetFloat("WalkSpeed", navMeshAgent.speed / originalSpeed);

        // 공격 애니메이션 속도 조절
        float attackSpeedMultiplier = 1f / attackCooldown; 
        animator.SetFloat("AttackSpeed", attackSpeedMultiplier);
    }

    // 기절 코루틴
    public IEnumerator OnStun(float time)
    {
        isStun = true;
       yield return YieldCache.WaitForSeconds(time);
        isStun = false;
    }
    // 이동 정지 코루틴
    public IEnumerator OnStop(float time)
    {
        float temp = movementSpeed;
        movementSpeed = 0;
        yield return YieldCache.WaitForSeconds(time);
        movementSpeed = temp;
    }
    // 데미지 감소 코루틴
    public IEnumerator OnDamageDown(float DamageDown,float time)
    {
        attackDamage *= (1 - DamageDown);
        yield return YieldCache.WaitForSeconds(time);
        attackDamage /= (1 - DamageDown);
    }
    // 이동 속도 감소 코루틴
    public IEnumerator OnSpeedDown(float SpeedDown, float time)
    {
        movementSpeed *= (1 - SpeedDown);
        yield return YieldCache.WaitForSeconds(time);
        movementSpeed /= (1 - SpeedDown);
    }
    // 데미지를 입는 함수
    public void OnDamage(float damage)
    {
        health -= (damage+GameManager.Instance.BonusDamage);
    }

    // 사망 함수
    void Die()
    {
        if (isDead) // 이미 사망한 경우 아무것도 하지 않음
        {
            return;
        }
        isDead = true; // 사망 상태로 변경

        GameManager.Instance.gold += 10; // 골드 획득
        GameManager.Instance.Killcount++; // 킬 카운트 증가

        // 죽는 애니메이션 실행
        animator.SetTrigger("Die");

        // 적이 지상 유닛이고 현재 타워가 존재하는 경우
        if ((enemyType == EnemyType.Ground) && currentTower != null)
        {
            currentTower.RemoveEnemy(); // 타워에서 적 제거
        }

        // NavMesh 에이전트 비활성화
        navMeshAgent.enabled = false;

        // 콜라이더 비활성화
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // 공중 유닛인 경우 일정 시간 뒤에 사라짐
        if (enemyType == EnemyType.Air)
        {
            StartCoroutine(FallDownCoroutine());
        }
        else // 지상 유닛인 경우 일정 시간 뒤에 사라짐
        {
            StartCoroutine(DestroyAfterDelay(3f));
        }
    }

    // 공중 유닛 사망시 추락 구현 코루틴
    IEnumerator FallDownCoroutine()
    {
        float totalTime = 2f; // 전체 시간
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition - Vector3.up * 2f; // 목표 지점 설정

        while (elapsedTime < totalTime)
        {
            // 시간에 따라 현재 위치 변경
            float t = elapsedTime / totalTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            // 경과 시간 증가
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 최종 위치 설정
        gameObject.SetActive(false);
    }

    // 지정된 지연 후에 제거되는 코루틴
    IEnumerator DestroyAfterDelay(float delay)
    {
        // 지연
        yield return new WaitForSeconds(delay);

        // 오브젝트 비활성화
        // Destroy(gameObject);
        gameObject.SetActive(false);
    }

    // 랜덤한 공격 애니메이션 반환 함수
    string GetRandomAttackAnimation()
    {
        // 랜덤한 인덱스 선택
        int randomIndex = Random.Range(0, attackAnimationNames.Count);
        return attackAnimationNames[randomIndex];
    }

    // 다음 목적지 설정 함수
    void SetDestinationToNextPathPoint()
    {
        if (currentPathIndex < pathPoints.Length - 1) // 아직 경로가 남아있는 경우
        {
            currentPathIndex++;
            Vector3 destination = pathPoints[currentPathIndex].position;
            navMeshAgent.SetDestination(destination);
            // 걷기 애니메이션 설정
            animator.SetBool("IsWalking", true);
        }
        else // 경로가 없는 경우 플레이어 쪽으로 이동
        {
            navMeshAgent.SetDestination(player.transform.position);
            // 걷기 애니메이션 설정
            animator.SetBool("IsWalking", true);
        }
    }

    // 웨이브에 따른 속도 증가 함수
    public void IncreaseSpeedForWave(float multiplier)
    {
        // 이동 속도 증가
        navMeshAgent.speed = originalSpeed * multiplier;
    }

    private void OnEnable()
    {
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        SetDestinationToNextPathPoint();
        isDead = false;
        health = tempHealth;
    }

    private void OnDisable()
    {
        //foreach (GameObject obj in GameManager.Instance.enemys)
        //{
        //    if (obj.activeSelf)
        //    {
        //        return;
        //    }
        //}        

        //GameManager.Instance.diamond += 3;  // ���̺� ������ ���̾� ŉ��
        //GameManager.Instance.meleeRespawn();
        transform.SetAsLastSibling();

        if (isBoss && transform.parent.childCount == 1)
        {
            GameManager.Instance.ClearGame();
        }

        foreach (EnemyController e in transform.parent.GetComponentsInChildren<EnemyController>())
        {
            if (e.isDead == false)
            {
                return;
            }
        }
        if (GameManager.Instance.enemySpawner.lastSpawn && !GameManager.Instance.enemySpawner.rewardGiven)
        {
            GameManager.Instance.enemySpawner.rewardGiven = true;
            GameManager.Instance.diamond += 2;
            GameManager.Instance.meleeRespawn();
        }
    }
}