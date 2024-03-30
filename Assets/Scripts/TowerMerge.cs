using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMerge : MonoBehaviour
{
    Camera mainCamera; // 카메라    
    public float surfaceHeight = 0.7f; // 카메라가 바라보는 지면의 높이를 결정합니다.
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

        // 마우스 입력을 카메라 시야 방향으로 변환합니다.
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            // 타워가 있는 블록을 클릭했을경우 타워가 있다면 클릭이 되는 코드
            if (Input.GetMouseButtonDown(0) && hit.transform.CompareTag("Buildable") 
                && hit.transform.childCount == 1 && canClick)
            {
                hit.transform.GetChild(0).gameObject.GetComponent<TowerMerge>().doMerge = true;
                hit.transform.GetChild(0).gameObject.layer = 2;
                canClick = false;
                Debug.Log(canClick);
                Click = 1; // 클릭됐을때 공격안하는 state로 변경
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
        // 클릭된 타워 마우스 따라오는 코드 / 설치 불가구역 클릭시 돌아가는 코드필요함
        if (doMerge)
        {
            //오브젝트를 해당 위치로 이동시킵니다.
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
