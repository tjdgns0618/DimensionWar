using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class FollowMouse : MonoBehaviour
{
    // Ÿ�� ��ġ ��� �������� ���� ��� ���ϴ� ��ũ��Ʈ

    #region Ÿ�� �̸����� ������Ʈ�� ���콺�� ������� ��ũ��Ʈ
    // ī�޶�
    Camera mainCamera;
    // ī�޶� �ٶ󺸴� ������ ���̸� �����մϴ�.
    public float surfaceHeight = 0f;    // ������ Ÿ�� �������� y���� �����ϴ� ��
    public GameObject Towerprefab;      // ������ ������Ʈ���� ������ Ÿ�� ������Ʈ
    Tower tower;                        // Ÿ�� ��ũ��Ʈ�� �޾ƿ������� Tower�� ����
    RaycastHit hit;
    bool Build = false;                 // ��ġ�ߴ� Ȯ�ο�

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
                transform.position = hit.transform.position + new Vector3(0, 1, 0);
                // ���콺 ����Ŭ���� ���, Ŭ���� ������Ʈ�� Ÿ���� ���ٸ�
                if (Input.GetMouseButtonDown(0) && hit.transform.childCount == 0)
                {
                    Build = true;
                    hit.transform.gameObject.GetComponent<Blocks>().isBuild = true;

                    // Ÿ�� ���� �� ������Ʈ ����
                    GameObject instance = Instantiate(Towerprefab);
                    instance.transform.SetParent(hit.transform);

                    // �� �������� ��ġ�� �ʱ�ȭ
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
                    // ���ӸŴ����� Ÿ�� ����
                    GameManager.Instance.towers.Add(instance);
                    Destroy(this.gameObject);
                    // �� Ŭ�� �Ұ����ϰ� ����
                    hit.transform.gameObject.layer = 2; 
                    
                }
                // �̹� Ÿ���� ��ġ�������� ��ġ �Ұ���
                else if (Input.GetMouseButtonDown(0) && hit.transform.childCount == 1)
                {
                    Destroy(this.gameObject);
                }
            }
            // ����Ÿ�� ��ġ ���� ���
            else if (hit.transform.gameObject.CompareTag("MeleeBuildable") && tower.isMelea == true)
            {
                // ����Ÿ�� �̸����� ���� ����
                transform.position = hit.transform.position + new Vector3(0, surfaceHeight, 0);
                if (tower.tower_class == Tower.Tower_Class.Pixel)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                // ���ʸ��콺 Ŭ��, Ŭ���� ������Ʈ�� Ÿ���� ���ٸ�
                if (Input.GetMouseButtonDown(0) && hit.transform.childCount == 0)
                {
                    Build = true;

                    // Ÿ�� ���� �� ������Ʈ ����
                    GameObject instance = Instantiate(Towerprefab);
                    instance.transform.SetParent(hit.transform);
                    // �� ������ ��ġ �ʱ�ȭ
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
                    // ���ӸŴ����� Ÿ�� ����
                    GameManager.Instance.towers.Add(instance);
                    Destroy(this.gameObject);
                    // �� Ŭ�� �Ұ����ϰ� ����
                    hit.transform.gameObject.layer = 2;
                }
                // �̹� Ÿ���� ��ġ�������� ��ġ �Ұ���
                else if (Input.GetMouseButtonDown(0) && hit.transform.childCount == 1)
                {
                    Destroy(this.gameObject);
                }
            }
            // Ÿ�� ��ġ �Ұ��� ������ ���콺�� �������
            else
            {
                // ��ġ �Ұ��� ���
                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(this.gameObject);
                }
                
                Vector3 targetPosition;
                // ī�޶� ���ߴ� ���鿡 ���� ��ġ�� ����ϴ�.
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

                // ������Ʈ�� ���콺 ��ġ�� �̵���ŵ�ϴ�.
                transform.position = targetPosition;

            }
        }
    }
    #endregion
}