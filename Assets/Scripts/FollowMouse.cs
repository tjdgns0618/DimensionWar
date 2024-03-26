using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // 카메라
    Camera mainCamera;
    // 카메라가 바라보는 지면의 높이를 결정합니다.
    public float surfaceHeight = 0.7f;
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
            if (hit.transform.gameObject.CompareTag("Buildable"))
            {
                transform.position = hit.transform.position + new Vector3(0, 1f, 0);

                if (Input.GetMouseButtonDown(0))
                {
                    Build = true;

                    // 타워 제작 후 오브젝트 제거
                    GameObject instance = Instantiate(Towerprefab, hit.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
                    instance.transform.SetParent(hit.transform);
                    Destroy(this.gameObject);
                }
            }
            else
            {
                // 카메라가 비추는 지면에 대한 위치를 얻습니다.
                Vector3 targetPosition = new Vector3(hit.point.x, surfaceHeight, hit.point.z);

                // 오브젝트를 해당 위치로 이동시킵니다.
                transform.position = targetPosition;
            }
        }
        //else
        //{
        //    // 카메라가 비추는 지면에 대한 위치를 얻습니다.
        //    Vector3 targetPosition = new Vector3(Input.mousePosition.x, surfaceHeight, Input.mousePosition.z);

        //    // 오브젝트를 해당 위치로 이동시킵니다.
        //    transform.position = targetPosition;
        //}
    }
}
