using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class TestScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject nextTower;    // 합성시 나오는 다음 타워
    public GameObject ClickEffect;    // 클릭된 블럭 구별용 이펙트

    Vector3 temp;                   // 드래그 시작시 현재 위치 저장용
    Tower tower;                    // 본인의 타워 스크립트 정보 저장용


    #region 레이캐스트용
    // bool canMerge = false;          // 합성 가능 상태 확인용
    // RaycastHit hit;
    // public bool isDraging = false;         // 드래그 중인지 체크용
    #endregion

    private void Start()
    {
        tower = GetComponent<Tower>();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {   // 블럭이 클릭된 상태
        if (GameManager.Instance.SelectBlock)
        {
            Destroy(GameManager.Instance.SelectBlock.GetComponent<Blocks>().tempBuyEffect.gameObject);   // 블럭 선택 이펙트를 지워준다.
            GameManager.Instance.SelectBlock.GetComponent<Blocks>().isBuild = false;
            GameManager.Instance.SelectBlock = null;
            TowerClick();
        }
        // 다른 타워가 클릭된 상태였다면
        else if (GameManager.Instance.tower)
        {
            GameManager.Instance.tower.GetComponent<TestScript>().ClickEffect.SetActive(false);
            TowerClick();
        }
        // 블럭이 클릭 안된상태
        else
        {
            TowerClick();
        }
    }

    void TowerClick()
    {
        GameManager.Instance.tower = tower;         // 게임메니저에 현재 타워에 대한 정보 저장
        GameManager.Instance.towerClicked = true;   // 현재 클릭중임
        GameManager.Instance.uiManager.BuyButton.image.color = Color.gray;
        GameManager.Instance.uiManager.SellButton.image.color = Color.white;
        GameManager.Instance.uiManager.towerUpgradeButton.image.color = Color.white;
        GameManager.Instance.uiManager.argumentButton.image.color = Color.white;
        GameManager.Instance.uiManager.BuyButton.GetComponent<Button>().enabled = false;
        GameManager.Instance.uiManager.SellButton.GetComponent<Button>().enabled = true;
        GameManager.Instance.uiManager.towerUpgradeButton.GetComponent<Button>().enabled = true;
        GameManager.Instance.uiManager.argumentButton.GetComponent<Button>().enabled = true;
        ClickEffect.SetActive(true);
        if (GameManager.Instance.towerClicked)      // 타워가 클릭되었을때
        {
            GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORestart();
            // GameManager.Instance.uiManager.skillCanvas.gameObject.SetActive(true);  // 스킬업그레이드 캔버스 활성화
            // GameManager.Instance.uiManager.uiCanvas.gameObject.SetActive(false);    // 게임 ui를 모두 비활성화
        }
    }

    #region 레이캐스트 사용, 드래그 합성 사용시
    //void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    //{   // 드래그 시작할때
    //    temp = transform.position;
    //}

    //void IDragHandler.OnDrag(PointerEventData eventData)
    //{   // 드래그 중일때
    //    if (transform.CompareTag("Tower"))
    //        return;

    //    isDraging = true;                       // 드래그와 클릭 구분용
    //    GameManager.Instance.clicked = false;   // 드래그와 클릭 구분용

    //    float distance = Camera.main.WorldToScreenPoint(transform.position).z;

    //    Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
    //    Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
    //    objPos.y = temp.y;

    //    transform.position = objPos;    // 드래그 중일때 타워가 마우스를 따라오게 하는 코드
    //}

    //void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    //{   // 드래그가 끝날때
    //    if (transform.CompareTag("Tower"))
    //        return;

    //    GameManager.Instance.clicked = false;

    //    #region 레이캐스트이용
    //    gameObject.layer = 2;                                  // 현재 들고있는 오브젝트 ignore layer 레이파이어를 무시하는 레이어로 변경        
    //    Vector3 mousePosition = Input.mousePosition;

    //    Ray ray = Camera.main.ScreenPointToRay(mousePosition);
    //    if (Physics.Raycast(ray, out hit))                              // 현재 들고있는 오브젝트를 뒤에있는 오브젝트에 레이 발사
    //    {
    //        if (hit.transform.gameObject.GetComponent<Tower>() && 
    //            hit.transform.gameObject.tag == gameObject.tag)
    //        {
    //            gameObject.layer = 0;                                  // 레이가 맞았다면 현재 들고있는 오브젝트 레이어를 default로 다시 변경
    //            gameObject.transform.parent.gameObject.layer = 0;
    //            Destroy(gameObject);
    //            Destroy(hit.transform.gameObject);
    //            GameObject instance = Instantiate(hit.transform.gameObject.GetComponent<TestScript>().nextTower);
    //            instance.transform.SetParent(hit.transform.parent);
    //            instance.transform.localPosition = new Vector3(0, hit.transform.position.y, 0);
    //            // instance.transform.rotation = Quaternion.Euler(0,90,0);
    //            GameManager.Instance.towers.Add(instance);
    //            if(transform.parent.GetComponent<Blocks>())
    //                transform.parent.GetComponent<Blocks>().isBuild = false;
    //            Debug.Log("합체성공");
    //        }
    //        else
    //        {
    //            gameObject.layer = 0;
    //            gameObject.transform.localPosition = new Vector3(0, temp.y, 0);
    //            Debug.Log("합체불가, 설치불가지역");
    //        }
    //    }
    //    #endregion

    //    isDraging = false;
    //}
    #endregion
}
