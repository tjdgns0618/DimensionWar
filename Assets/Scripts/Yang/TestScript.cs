using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestScript : MonoBehaviour, IBeginDragHandler, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    public GameObject nextTower;    // 합성시 나오는 다음 타워
    bool canMerge = false;          // 합성 가능 상태 확인용
    Vector3 temp;                   // 드래그 시작시 현재 위치 저장용
    RaycastHit hit;
    Tower tower;
    public bool isDraging = false;         // 드래그 중인지 체크용

    private void Start()
    {
        tower = GetComponent<Tower>();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {   // 오브젝트 클릭시
        GameManager.Instance.tower = tower;
        GameManager.Instance.clicked = true;
        Camera.main.GetComponent<CinemachineVirtualCamera>().LookAt = this.transform;
        if (GameManager.Instance.clicked && !isDraging)
        {
            Time.timeScale = 0;
            GameManager.Instance.uiManager.skillCanvas.transform.position = transform.parent.transform.position + new Vector3(0, 3.5f, -0.5f);
            GameManager.Instance.uiManager.skillCanvas.transform.LookAt(Camera.main.transform);
            GameManager.Instance.uiManager.skillCanvas.gameObject.SetActive(true);
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {   // 드래그 시작할때
        temp = transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {   // 드래그 중일때
        if (transform.CompareTag("Tower"))
            return;

        isDraging = true;                       // 드래그와 클릭 구분용
        GameManager.Instance.clicked = false;   // 드래그와 클릭 구분용

        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        objPos.y = temp.y;

        transform.position = objPos;    // 드래그 중일때 타워가 마우스를 따라오게 하는 코드
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {   // 드래그가 끝날때
        if (transform.CompareTag("Tower"))
            return;

        GameManager.Instance.clicked = false;

        #region 레이캐스트이용
        gameObject.layer = 2;                                  // 현재 들고있는 오브젝트 ignore layer 레이파이어를 무시하는 레이어로 변경        
        Vector3 mousePosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hit))                              // 현재 들고있는 오브젝트를 뒤에있는 오브젝트에 레이 발사
        {
            if (hit.transform.gameObject.GetComponent<Tower>() && 
                hit.transform.gameObject.tag == gameObject.tag)
            {
                gameObject.layer = 0;                                  // 레이가 맞았다면 현재 들고있는 오브젝트 레이어를 default로 다시 변경
                gameObject.transform.parent.gameObject.layer = 0;
                Destroy(gameObject);
                Destroy(hit.transform.gameObject);
                GameObject instance = Instantiate(hit.transform.gameObject.GetComponent<TestScript>().nextTower);
                instance.transform.SetParent(hit.transform.parent);
                instance.transform.localPosition = new Vector3(0, hit.transform.position.y, 0);
                // instance.transform.rotation = Quaternion.Euler(0,90,0);
                GameManager.Instance.towers.Add(instance);
                transform.parent.GetComponent<Blocks>().isBuild = false;
                Debug.Log("합체성공");
            }
            else
            {
                gameObject.layer = 0;
                gameObject.transform.localPosition = new Vector3(0, temp.y, 0);
                Camera.main.GetComponent<CinemachineVirtualCamera>().LookAt = null;
                Debug.Log("합체불가, 설치불가지역");
            }
        }
        #endregion

        isDraging = false;
    }
}
