using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class FollowMouse : MonoBehaviour
{
    // 타워 설치 방법 변경으로 이제 사용 안하는 스크립트

    #region 타워 미리보기 오브젝트가 마우스를 따라오는 스크립트
    // 카메라
    Camera mainCamera;
    // 카메라가 바라보는 지면의 높이를 결정합니다.
    public float surfaceHeight = 0f;    // 생성된 타워 프리뷰의 y값을 고정하는 값
    public GameObject Towerprefab;      // 프리뷰 오브젝트에서 생성할 타워 오브젝트
    Tower tower;                        // 타워 스크립트를 받아오기위한 Tower형 변수
    RaycastHit hit;
    bool Build = false;                 // 설치했는 확인용

    private void Start()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        tower = GetComponent<Tower>();
    }

    void Update()
    {
        // 마우스 입력을 받습니다.
        Vector3 mousePosition = Input.mousePosition;

        // 마우스 입력을 카메라 시야 방향으로 변환합니다.
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out hit) && !Build)
        {
            // 원거리 설치가능 || 근거리블록 설치 불가
            if (hit.transform.gameObject.CompareTag("Buildable"))
            {
                transform.position = hit.transform.position + new Vector3(0, 1, 0);
                // 마우스 왼쪽클릭할 경우, 클릭한 오브젝트에 타워가 없다면
                if (Input.GetMouseButtonDown(0) && hit.transform.childCount == 0)
                {
                    Build = true;
                    hit.transform.gameObject.GetComponent<Blocks>().isBuild = true;

                    // 타워 제작 후 오브젝트 제거
                    GameObject instance = Instantiate(Towerprefab);
                    instance.transform.SetParent(hit.transform);

                    // 각 진영별로 위치값 초기화
                    if (tower.tower_class == Tower.Tower_Class.Pixel)
                    {
                        instance.transform.localPosition = new Vector3(0, 2.1f, 0);
                        instance.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else if (tower.tower_class == Tower.Tower_Class.RowPoly)
                    {
                        instance.transform.localPosition = new Vector3(0, 0.5f, 0);
                        instance.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else if(tower.tower_class == Tower.Tower_Class._3D)
                    {
                        instance.transform.localPosition = new Vector3(0, 0.5f, 0);
                        instance.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    // 게임매니저에 타워 저장
                    GameManager.Instance.towers.Add(instance);
                    Destroy(this.gameObject);
                    // 블럭 클릭 불가능하게 변경
                    hit.transform.gameObject.layer = 2; 
                    
                }
                // 이미 타워가 설치되있으면 설치 불가능
                else if (Input.GetMouseButtonDown(0) && hit.transform.childCount == 1)
                {
                    Destroy(this.gameObject);
                }
            }
            // 근접타워 설치 블럭일 경우
            else if (hit.transform.gameObject.CompareTag("MeleeBuildable") && tower.isMelea == true)
            {
                // 근접타워 미리보기 높이 고정
                transform.position = hit.transform.position + new Vector3(0, surfaceHeight, 0);
                if (tower.tower_class == Tower.Tower_Class.Pixel)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                // 왼쪽마우스 클릭, 클릭한 오브젝트에 타워가 없다면
                if (Input.GetMouseButtonDown(0) && hit.transform.childCount == 0)
                {
                    Build = true;

                    // 타워 제작 후 오브젝트 제거
                    GameObject instance = Instantiate(Towerprefab);
                    instance.transform.SetParent(hit.transform);
                    // 각 진영별 위치 초기화
                    if (tower.tower_class == Tower.Tower_Class.Pixel)
                    {
                        instance.transform.localPosition = new Vector3(0, 2.1f, 0);
                        instance.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else if (tower.tower_class == Tower.Tower_Class.RowPoly)
                    {
                        instance.transform.localPosition = new Vector3(0, 0.5f, 0);
                        instance.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else if (tower.tower_class == Tower.Tower_Class._3D)
                    {
                        instance.transform.localPosition = new Vector3(0, 0.5f, 0);
                        instance.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    // 게임매니저에 타워 저장
                    GameManager.Instance.towers.Add(instance);
                    Destroy(this.gameObject);
                    // 블럭 클릭 불가능하게 변경
                    hit.transform.gameObject.layer = 2;
                }
                // 이미 타워가 설치되있으면 설치 불가능
                else if (Input.GetMouseButtonDown(0) && hit.transform.childCount == 1)
                {
                    Destroy(this.gameObject);
                }
            }
            // 타워 설치 불가능 구역에 마우스가 있을경우
            else
            {
                // 설치 불가능 출력
                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(this.gameObject);
                }
                
                Vector3 targetPosition;
                // 카메라가 비추는 지면에 대한 위치를 얻습니다.
                if (transform.position.z > 8)
                    targetPosition = new Vector3(hit.point.x, surfaceHeight, 8);
                else
                    targetPosition = new Vector3(hit.point.x, surfaceHeight, hit.point.z);

                if (tower.tower_class == Tower.Tower_Class.Pixel)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                else if (tower.tower_class == Tower.Tower_Class.RowPoly)
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                else if (tower.tower_class == Tower.Tower_Class._3D)
                    transform.rotation = Quaternion.Euler(0, 0, 0);

                // 오브젝트를 마우스 위치로 이동시킵니다.
                transform.position = targetPosition;

            }
        }
    }
    #endregion
}