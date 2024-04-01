using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // ī�޶�
    Camera mainCamera;
    // ī�޶� �ٶ󺸴� ������ ���̸� �����մϴ�.
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
        // ���콺 �Է��� �޽��ϴ�.
        Vector3 mousePosition = Input.mousePosition;
        
        // ���콺 �Է��� ī�޶� �þ� �������� ��ȯ�մϴ�.
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        
        if (Physics.Raycast(ray, out hit) && !Build)
        {
            // ���Ÿ� ��ġ���� || �ٰŸ���� ��ġ �Ұ�
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

                    // Ÿ�� ���� �� ������Ʈ ����
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
                    Debug.Log(this.gameObject.name + "��ġ �Ϸ�");
                }
                else if(Input.GetMouseButtonDown(0) && hit.transform.childCount == 1)
                {
                    Destroy(this.gameObject);
                    Debug.Log("�̹� Ÿ���� ��ġ�Ǿ����ϴ�.");
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(this.gameObject);
                    Debug.Log("��ġ �Ұ����� ����Դϴ�.");
                }

                Vector3 targetPosition;
                // ī�޶� ���ߴ� ���鿡 ���� ��ġ�� ����ϴ�.
                if (transform.position.z > 4)
                    targetPosition = new Vector3(hit.point.x, surfaceHeight, 4);
                else
                    targetPosition = new Vector3(hit.point.x, surfaceHeight, hit.point.z);

                if(gameObject.CompareTag("2D_Tower"))
                    transform.rotation = Quaternion.Euler(30, 0, 0);
                else if(gameObject.CompareTag("3D_Tower"))
                    transform.rotation = Quaternion.Euler(0, 90, 0);

                // ������Ʈ�� �ش� ��ġ�� �̵���ŵ�ϴ�.
                transform.position = targetPosition;

            }
        }
    }
}
