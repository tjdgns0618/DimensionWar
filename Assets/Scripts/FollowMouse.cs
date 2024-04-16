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
        // ���콺 �Է��� �޽��ϴ�.
        Vector3 mousePosition = Input.mousePosition;
        
        // ���콺 �Է��� ī�޶� �þ� �������� ��ȯ�մϴ�.
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        
        if (Physics.Raycast(ray, out hit) && !Build)
        {
            // ���Ÿ� ��ġ���� || �ٰŸ���� ��ġ �Ұ�
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

                    // Ÿ�� ���� �� ������Ʈ ����
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
                    Debug.Log(this.gameObject.name + "��ġ �Ϸ�");
                }
                else if(Input.GetMouseButtonDown(0) && hit.transform.childCount == 1)
                {
                    Destroy(this.gameObject);
                    Debug.Log("�̹� Ÿ���� ��ġ�Ǿ����ϴ�.");
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

                    // Ÿ�� ���� �� ������Ʈ ����
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
                    Debug.Log(this.gameObject.name + "��ġ �Ϸ�");
                }
                else if (Input.GetMouseButtonDown(0) && hit.transform.childCount == 1)
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
                if (transform.position.z > 8)
                    targetPosition = new Vector3(hit.point.x, surfaceHeight, 8);
                else
                    targetPosition = new Vector3(hit.point.x, surfaceHeight, hit.point.z);

                if(tower.tower_class == Tower.Tower_Class.Pixel)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                else if(tower.tower_class == Tower.Tower_Class.RowPoly)
                    transform.rotation = Quaternion.Euler(0, 90, 0);

                // ������Ʈ�� �ش� ��ġ�� �̵���ŵ�ϴ�.
                transform.position = targetPosition;

            }
        }
    }
}
