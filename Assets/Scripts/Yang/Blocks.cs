using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class Blocks : MonoBehaviour, IPointerClickHandler
{
    float speed = 2f;               // 이동 속도

    [HideInInspector]
    public AudioSource audiosource;
    public AudioClip[] audio;
    public bool isBuild = false;    // 타워가 설치된 블럭 구별용
    public GameObject BuyEffect;    // 클릭된 블럭 구별용 이펙트
    public int dimension;           // 현재 스테이지에 따라 블럭움직임을 줄지 정하는 변수
    [HideInInspector]
    public GameObject instance;     // BuyEffect를 생성해줄 GameObject 변수

    private void Awake()
    {
        dimension = PlayerPrefs.GetInt("dimension");    // 현재 스테이지의 Dimension을 받아온다.
        audiosource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (gameObject.CompareTag("MeleeBuildable") || dimension == 2)    // 근접타워이거나 스테이지가 2D스테이지라면 블럭이 움직이지 않음
            return;

        if (isBuild)
            BlockUp();      // 블럭이 선택되었을때 블럭이 위로 올라오는 함수
        else
            BlockDown();    // 블럭 선택이 해제되었을때 블럭이 내려가는 함수
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // 이미 선택된 블럭이 존재하는 경우 반환, 타워가 선택중일경우
        if (GameManager.Instance.tower)
        {
            GameManager.Instance.tower = null;
            BlockClick();
        }
        else if (GameManager.Instance.SelectBlock)
        {
            Destroy(GameManager.Instance.SelectBlock.GetComponent<Blocks>().instance.gameObject);
            GameManager.Instance.SelectBlock.GetComponent<Blocks>().isBuild = false;
            BlockClick();
        }
        else
        {
            BlockClick();
        }
    }

    void BlockClick()
    {
        GameManager.Instance.uiManager.SellButton.image.color = Color.gray;
        GameManager.Instance.uiManager.BuyButton.image.color = Color.white;
        GameManager.Instance.uiManager.UpgradeButton.image.color = Color.gray;
        GameManager.Instance.uiManager.SellButton.GetComponent<Button>().enabled = false;
        GameManager.Instance.uiManager.BuyButton.GetComponent<Button>().enabled = true;
        GameManager.Instance.uiManager.UpgradeButton.GetComponent<Button>().enabled = false;
        audiosource.clip = audio[0];
        audiosource.volume = PlayerPrefs.GetFloat("BgmValue");
        audiosource.Play();

        GameManager.Instance.blockClicked = true;               // 블럭이 선택됨
        GameManager.Instance.SelectBlock = this.gameObject;     // 게임매니저에 현재 선택된 블럭에 이 블럭을 저장
        isBuild = true;
        GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORestart();   // 구매 패널이 올라오게하는 함수
        instance = Instantiate(BuyEffect);      // 블럭이 선택되었는지 알려주는 이펙트 생성
        instance.transform.parent = transform;  // 이펙트를 이 블럭의 자식으로 생성
        instance.transform.localPosition = new Vector3(0, 0.8f, 0); // 이펙트의 위치 변경
    }

    void BlockUp()
    {
        Vector3 target = new Vector3(transform.position.x, 0.2f, transform.position.z); // 목표 위치
        Vector3 direction = (target - transform.position).normalized;   // 목표 위치로의 방향 벡터
        float distance = Vector3.Distance(transform.position, target);  // 목표 위치로의 거리

        // 거리가 일정 이상 멀 때만 이동
        if (distance > 0.1f)
        {
            // 부드러운 이동을 위해 Lerp 사용
            transform.localPosition += direction * speed * Time.fixedDeltaTime;
        }
    }

    void BlockDown()
    {
        Vector3 target = new Vector3(transform.position.x, 0.4f - 1.25f, transform.position.z); // 목표 위치
        Vector3 direction = (target - transform.position).normalized;   // 목표 위치로의 방향 벡터
        float distance = Vector3.Distance(transform.position, target);  // 목표 위치로의 거리

        // 거리가 일정 이상 멀 때만 이동
        if (distance > 0.1f)
        {
            // 부드러운 이동을 위해 Lerp 사용
            transform.localPosition += direction * speed * Time.fixedDeltaTime;
        }
    }
}
