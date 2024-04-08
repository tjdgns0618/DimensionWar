using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestScript : MonoBehaviour, IBeginDragHandler
{
    public Vector3 temp;
    public GameObject nextTower;
    bool canMerge = false;
    GameObject mergeTower;
    RaycastHit hit;
    Tower tower;
    bool isDraging = false;
    private void Start()
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry_PointerClick = new EventTrigger.Entry();
        entry_PointerClick.eventID = EventTriggerType.PointerClick;
        entry_PointerClick.callback.AddListener((data) => { OnClick((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerClick);

        EventTrigger.Entry entry_Drag = new EventTrigger.Entry();
        entry_Drag.eventID = EventTriggerType.Drag;
        entry_Drag.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_Drag);        

        EventTrigger.Entry entry_EndDrag = new EventTrigger.Entry();
        entry_EndDrag.eventID = EventTriggerType.EndDrag;
        entry_EndDrag.callback.AddListener((data) => { OnEndDrag((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_EndDrag);

        tower = GetComponent<Tower>();
    }

    private void Update()
    {

    }

    void OnClick(PointerEventData data)
    {
        Debug.Log("온클릭");
        GameManager.Instance.tower = tower;        
        GameManager.Instance.clicked = true;        
        if (GameManager.Instance.clicked && !isDraging)
        {
            Time.timeScale = 0;
            GameManager.Instance.uiManager.skillCanvas.transform.position = transform.parent.transform.position + new Vector3(0, 3.5f, -0.5f);
            GameManager.Instance.uiManager.skillCanvas.transform.LookAt(Camera.main.transform);
            GameManager.Instance.uiManager.skillCanvas.gameObject.SetActive(true);
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        temp = transform.position;
    }

    void OnDrag(PointerEventData data)
    {
        isDraging = true;
        GameManager.Instance.clicked = false;

        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        objPos.y = temp.y;

        transform.position = objPos;
    }

    void OnEndDrag(PointerEventData data)
    {        
        GameManager.Instance.clicked = false;

        #region 트리거엔터사용
        //if (canMerge)
        //{
        //    Destroy(gameObject);
        //    Destroy(mergeTower.transform.gameObject);
        //    GameObject instance = Instantiate(mergeTower.GetComponent<TestScript>().nextTower);
        //    instance.transform.SetParent(mergeTower.transform.parent);
        //    instance.transform.localPosition = new Vector3(0, temp.y, 0);
        //    GameManager.Instance.towers.Add(instance);
        //    Debug.Log("합체성공");
        //}
        //else
        //{
        //    gameObject.transform.localPosition = new Vector3(0, temp.y, 0);
        //    Debug.Log("합체불가, 설치불가지역");
        //}
        #endregion

        #region 레이캐스트이용
        gameObject.layer = 2;                                  // 현재 들고있는 오브젝트 ignore layer 레이파이어를 무시하는 레이어로 변경        
        Vector3 mousePosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hit))                              // 현재 들고있는 오브젝트를 뒤에있는 오브젝트에 레이 발사
        {
            if (hit.transform.gameObject.GetComponent<Tower>() &&
                tower.tower_type == hit.transform.gameObject.GetComponent<Tower>().tower_type
                && hit.transform.gameObject.tag == gameObject.tag)
            {
                gameObject.layer = 0;                                  // 레이가 맞았다면 현재 들고있는 오브젝트 레이어를 default로 다시 변경
                gameObject.transform.parent.gameObject.layer = 0;
                Destroy(gameObject);
                Destroy(hit.transform.gameObject);
                GameObject instance = Instantiate(nextTower);
                instance.transform.SetParent(hit.transform.parent);
                instance.transform.localPosition = new Vector3(0, temp.y, 0);
                // instance.transform.rotation = Quaternion.Euler(0,90,0);
                GameManager.Instance.towers.Add(instance);
                Debug.Log("합체성공");
            }
            else
            {
                gameObject.layer = 0;
                if (tower.tower_class == Tower.Tower_Class.Pixel)
                    gameObject.transform.localPosition = new Vector3(0, temp.y, 0);
                else if (tower.tower_class == Tower.Tower_Class.RowPoly)
                    gameObject.transform.localPosition = new Vector3(0, temp.y, 0);
                Debug.Log("합체불가, 설치불가지역");
            }
        }
        #endregion

        isDraging = false;
    }

    #region 트리거엔터사용
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.childCount > 0 && transform.parent != other.transform &&
    //            tower.tower_type == other.transform.gameObject.GetComponent<Tower>().tower_type
    //            && other.transform.gameObject.tag == gameObject.tag)
    //    {
    //        canMerge = true;
    //        mergeTower = other.gameObject;
    //    }
    //    else
    //    {
    //        canMerge = false;
    //        mergeTower = null;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    canMerge = false;
    //    mergeTower = null;
    //}
    #endregion
}
