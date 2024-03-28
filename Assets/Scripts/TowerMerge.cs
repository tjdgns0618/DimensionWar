using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMerge : MonoBehaviour
{
    Camera mainCamera; // 카메라    
    public float surfaceHeight = 0.7f; // 카메라가 바라보는 지면의 높이를 결정합니다.
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

        // 마우스 입력을 카메라 시야 방향으로 변환합니다.
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) && hit.transform.CompareTag("Buildable") && hit.transform.childCount == 1)
            {
                hit.transform.GetChild(0).gameObject.GetComponent<TowerMerge>().doMerge = true;
                hit.transform.GetChild(0).gameObject.layer = 2;
                
                Click = 1; // 클릭됐을때 공격안하는 state로 변경
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

            //오브젝트를 해당 위치로 이동시킵니다.
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
