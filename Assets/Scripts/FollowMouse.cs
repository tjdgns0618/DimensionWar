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

    RaycastHit hit;
    bool Build = false;

    private void Start()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
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
                if (gameObject.CompareTag("2D_Tower"))
                {
                    transform.position = hit.transform.position + new Vector3(0, 0.5f, -0.5f);
                    transform.rotation = Quaternion.Euler(30, 0, 0);
                }
                else if (gameObject.CompareTag("3D_Tower"))
                {
                    transform.position = hit.transform.position + new Vector3(0, surfaceHeight, 0);

                }
                if (Input.GetMouseButtonDown(0) && hit.transform.childCount == 0)
                {
                    Build = true;

                    // 타워 제작 후 오브젝트 제거
                    GameObject instance = Instantiate(Towerprefab);
                    instance.transform.SetParent(hit.transform);
                    if (gameObject.CompareTag("2D_Tower"))
                    {
                        instance.transform.localPosition = new Vector3(0, 0.5f, -0.5f);
                        instance.transform.rotation = Quaternion.Euler(30, 0, 0);
                    }
                    else if(gameObject.CompareTag("3D_Tower"))
                    {
                        instance.transform.localPosition = new Vector3(0, 0.5f, 0);
                        instance.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
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
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(this.gameObject);
                    Debug.Log("설치 불가능한 블록입니다.");
                }

                Vector3 targetPosition;
                // 카메라가 비추는 지면에 대한 위치를 얻습니다.
                if (transform.position.z > 4)
                    targetPosition = new Vector3(hit.point.x, surfaceHeight, 4);
                else
                    targetPosition = new Vector3(hit.point.x, surfaceHeight, hit.point.z);

                if(gameObject.CompareTag("2D_Tower"))
                    transform.rotation = Quaternion.Euler(30, 0, 0);
                else if(gameObject.CompareTag("3D_Tower"))
                    transform.rotation = Quaternion.Euler(0, 90, 0);

                // 오브젝트를 해당 위치로 이동시킵니다.
                transform.position = targetPosition;

            }
        }
    }
}
