using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMerge : MonoBehaviour
{
    Camera mainCamera; // ī�޶�    
    public float surfaceHeight = 0.7f; // ī�޶� �ٶ󺸴� ������ ���̸� �����մϴ�.
    RaycastHit hit;
    int Click = 1;
    public bool doMerge = false;
    public int id = 1;
    public bool canClick = true;
    public GameObject nextTower;

    private void Start()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        GameManager.Instance.towers.Add(this.gameObject);
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        // ���콺 �Է��� ī�޶� �þ� �������� ��ȯ�մϴ�.
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            // Ÿ���� �ִ� ����� Ŭ��������� Ÿ���� �ִٸ� Ŭ���� �Ǵ� �ڵ�
            if (Input.GetMouseButtonDown(0) && hit.transform.CompareTag("Buildable") 
                && hit.transform.childCount == 1 && canClick)
            {
                hit.transform.GetChild(0).gameObject.GetComponent<TowerMerge>().doMerge = true;
                hit.transform.GetChild(0).gameObject.layer = 2;
                canClick = false;
                Debug.Log(canClick);
                Click = 1; // Ŭ�������� ���ݾ��ϴ� state�� ����
            }
            else if (Input.GetMouseButtonDown(0) && doMerge && 
                hit.transform.GetChild(0).gameObject.GetComponent<TowerMerge>().id == this.id
                && hit.transform.GetChild(0).gameObject.layer != 2)
            { 
                Debug.Log(hit.transform.GetChild(0).gameObject.GetComponent<TowerMerge>().id);
                Debug.Log(canClick);
                if (doMerge)
                    Destroy(this.gameObject);
                Destroy(hit.transform.GetChild(0).gameObject);                
                GameObject instance = Instantiate(nextTower, hit.transform.position + new Vector3(0,surfaceHeight,0), Quaternion.Euler(15, 0, 0));
                instance.transform.SetParent(hit.transform);

                for (int i = 0; i < GameManager.Instance.towers.Count; i++)
                {
                    GameManager.Instance.towers[i].GetComponent<TowerMerge>().canClick = true;
                }
            }
        }
        // Ŭ���� Ÿ�� ���콺 ������� �ڵ� / ��ġ �Ұ����� Ŭ���� ���ư��� �ڵ��ʿ���
        if (doMerge)
        {
            //������Ʈ�� �ش� ��ġ�� �̵���ŵ�ϴ�.
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 targetPosition = new Vector3(hit.point.x, surfaceHeight, hit.point.z);
                transform.position = targetPosition;

                if (Input.GetMouseButtonDown(0) && !hit.transform.CompareTag("Buildable"))
                {
                    hit.transform.GetChild(0).gameObject.GetComponent<TowerMerge>().doMerge = false;
                    transform.localPosition = new Vector3(0, 0, 0);
                    canClick = true;
                }
            }
        }
    }
}
