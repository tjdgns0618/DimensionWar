using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestScript : MonoBehaviour
{
    public Vector3 temp;
    public GameObject nextTower;
    RaycastHit hit;
    Tower tower;
    private void Start()
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

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

    public void SavePos()
    {
        temp = this.transform.localPosition;
    }
     
    public void OnClick()
    {
        Debug.Log("온클릭");
       //GameManager.Instance.tower = this.tower;
       //GameManager.Instance.uiManager.skillCanvas.transform.position = transform.parent.transform.position + new Vector3(0, 5f, -0.5f);
       //GameManager.Instance.uiManager.skillCanvas.gameObject.SetActive(true);
       //GameManager.Instance.clicked = true;
    }

    void OnDrag(PointerEventData data)
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        objPos.y = 0.7f;

        transform.position = objPos;
    }

    void OnEndDrag(PointerEventData data)
    {
        this.gameObject.layer = 2;
        Vector3 mousePosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.childCount > 0 && this.transform.parent != hit.transform)
            {
                gameObject.layer = 0;
                gameObject.transform.parent.gameObject.layer = 0;
                Destroy(gameObject);
                Destroy(hit.transform.gameObject);
                GameObject instance = Instantiate(nextTower);
                instance.transform.SetParent(hit.transform.parent);
                instance.transform.localPosition = new Vector3(0,0.5f,0);
                // instance.transform.rotation = Quaternion.Euler(0,90,0);
                GameManager.Instance.towers.Add(instance);
                Debug.Log("합체성공");
            }
            else
            {
                gameObject.layer = 0;
                if(tower.tower_class == Tower.Tower_Class.Pixel)
                    this.gameObject.transform.localPosition = new Vector3(0, 0.5f, -0.5f);
                else if(tower.tower_class == Tower.Tower_Class.RowPoly)
                    this.gameObject.transform.localPosition = new Vector3(0, 0.5f, 0);
                Debug.Log("합체불가, 설치불가지역");
            }
        }
    }

}
