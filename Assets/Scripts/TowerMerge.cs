using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMerge : MonoBehaviour
{
    Camera mainCamera; // ī�޶�    
    public float surfaceHeight = 0.7f; // ī�޶� �ٶ󺸴� ������ ���̸� �����մϴ�.
    RaycastHit hit;
    int Click = 1;
    bool doMerge = false;
    public int id = 1;

    private void Start()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();        
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        // ���콺 �Է��� ī�޶� �þ� �������� ��ȯ�մϴ�.
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) && hit.transform.CompareTag("Buildable") && hit.transform.childCount == 1)
            {
                hit.transform.GetChild(0).gameObject.GetComponent<TowerMerge>().doMerge = true;
                hit.transform.GetChild(0).gameObject.layer = 2;
                
                Click = 1; // Ŭ�������� ���ݾ��ϴ� state�� ����
            }
            else if (Input.GetMouseButtonDown(0) && hit.transform.GetChild(0).gameObject.GetComponent<TowerMerge>().id == this.id)
            {
                Debug.Log(hit.transform.GetChild(0).gameObject.GetComponent<TowerMerge>().id);
                Destroy(this.gameObject);
                Destroy(hit.transform.GetChild(0).gameObject);
            }            
        }
        if (doMerge)
        {
            Vector3 targetPosition = new Vector3(hit.point.x, surfaceHeight, hit.point.z);

            //������Ʈ�� �ش� ��ġ�� �̵���ŵ�ϴ�.
            transform.position = targetPosition;
            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetMouseButtonDown(0) && !hit.transform.CompareTag("Buildable"))
                {
                    hit.transform.GetChild(0).gameObject.GetComponent<TowerMerge>().doMerge = false;
                    transform.localPosition = new Vector3(0, 0, 0);
                }
            }
        }
    }
}
