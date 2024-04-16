using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // 카메라
    Camera mainCamera;
    // 카메라가 바라보는 지면의 높이를 결정합니다.
    public float surfaceHeight = 0f;
    public GameObject Towerprefab;
    Tower tower;
    RaycastHit hit;
    bool Build = false;

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
                if (tower.tower_class == Tower.Tower_Class.Pixel)
                {
                    transform.position = hit.transform.position + new Vector3(0, surfaceHeight, 0);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (tower.tower_class == Tower.Tower_Class.RowPoly)
                {
                    transform.position = hit.transform.position + new Vector3(0, surfaceHeight, 0);

                }
                if (Input.GetMouseButtonDown(0) && hit.transform.childCount == 0)
                {
                    Build = true;

                    // 타워 제작 후 오브젝트 제거
                    GameObject instance = Instantiate(Towerprefab);
                    instance.transform.SetParent(hit.transform);
                    if (tower.tower_class == Tower.Tower_Class.Pixel)
                    {
                        instance.transform.localPosition = new Vector3(0, surfaceHeight, 0);
                        instance.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else if(tower.tower_class == Tower.Tower_Class.RowPoly)
                    {
                        instance.transform.localPosition = new Vector3(0, 0.5f, 0);
                        instance.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    GameManager.Instance.towers.Add(instance);
                    Destroy(this.gameObject);
                    hit.transform.gameObject.layer = 2;
                    Debug.Log(this.gameObject.name + "설치 완료");
                }
                else if(Input.GetMouseButtonDown(0) && hit.transform.childCount == 1)
                {
                    Destroy(this.gameObject);
                    Debug.Log("이미 타워가 설치되었습니다.");
                }
            }
            else if (hit.transform.gameObject.CompareTag("MeleeBuildable") && tower.tower_type == Tower.Tower_Type.Meele)
            {
                if (tower.tower_class == Tower.Tower_Class.Pixel)
                {
                    transform.position = hit.transform.position + new Vector3(0, surfaceHeight, 0);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (tower.tower_class == Tower.Tower_Class.RowPoly)
                {
                    transform.position = hit.transform.position + new Vector3(0, surfaceHeight, 0);

                }
                if (Input.GetMouseButtonDown(0) && hit.transform.childCount == 0)
                {
                    Build = true;

                    // 타워 제작 후 오브젝트 제거
                    GameObject instance = Instantiate(Towerprefab);
                    instance.transform.SetParent(hit.transform);
                    if (tower.tower_class == Tower.Tower_Class.Pixel)
                    {
                        instance.transform.localPosition = new Vector3(0, surfaceHeight, 0);
                        instance.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else if (tower.tower_class == Tower.Tower_Class.RowPoly)
                    {
                        instance.transform.localPosition = new Vector3(0, 0.5f, 0);
                        instance.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    GameManager.Instance.towers.Add(instance);
                    Destroy(this.gameObject);
                    hit.transform.gameObject.layer = 2;
                    Debug.Log(this.gameObject.name + "설치 완료");
                }
                else if (Input.GetMouseButtonDown(0) && hit.transform.childCount == 1)
                {
                    Destroy(this.gameObject);
                    Debug.Log("이미 타워가 설치되었습니다.");
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(this.gameObject);
                    Debug.Log("설치 불가능한 블록입니다.");
                }

                Vector3 targetPosition;
                // 카메라가 비추는 지면에 대한 위치를 얻습니다.
                if (transform.position.z > 8)
                    targetPosition = new Vector3(hit.point.x, surfaceHeight, 8);
                else
                    targetPosition = new Vector3(hit.point.x, surfaceHeight, hit.point.z);

                if(tower.tower_class == Tower.Tower_Class.Pixel)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                else if(tower.tower_class == Tower.Tower_Class.RowPoly)
                    transform.rotation = Quaternion.Euler(0, 90, 0);

                // 오브젝트를 해당 위치로 이동시킵니다.
                transform.position = targetPosition;

            }
        }
    }
}
