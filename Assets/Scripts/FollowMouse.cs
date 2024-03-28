using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // ī�޶�
    Camera mainCamera;
    // ī�޶� �ٶ󺸴� ������ ���̸� �����մϴ�.
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
        // ���콺 �Է��� �޽��ϴ�.
        Vector3 mousePosition = Input.mousePosition;
        
        // ���콺 �Է��� ī�޶� �þ� �������� ��ȯ�մϴ�.
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        
        if (Physics.Raycast(ray, out hit) && !Build)
        {
            // ���Ÿ� ��ġ���� || �ٰŸ���� ��ġ �Ұ�
            if (hit.transform.gameObject.CompareTag("Buildable"))
            {
                transform.position = hit.transform.position + new Vector3(0, 1f, 0);
                transform.rotation = Quaternion.Euler(15, 0, 0);

                if (Input.GetMouseButtonDown(0) && hit.transform.childCount == 0)
                {
                    Build = true;

                    // Ÿ�� ���� �� ������Ʈ ����
                    GameObject instance = Instantiate(Towerprefab, hit.transform.position + new Vector3(0, 0, 0), Quaternion.Euler(15,0,0));
                    instance.transform.SetParent(hit.transform);
                    Destroy(this.gameObject);
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

                // ī�޶� ���ߴ� ���鿡 ���� ��ġ�� ����ϴ�.
                Vector3 targetPosition = new Vector3(hit.point.x, surfaceHeight, hit.point.z);

                // ������Ʈ�� �ش� ��ġ�� �̵���ŵ�ϴ�.
                transform.position = targetPosition;

            }
        }
    }
}
