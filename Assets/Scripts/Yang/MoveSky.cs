using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSky : MonoBehaviour
{
    public Renderer tunnel;
    public bool start = false;
    float degree;
    float boost;

    // Start is called before the first frame update
    void Start()
    {
        degree = 0;
        boost = 3.9f;
        //StartCoroutine(_start()); // �������ۿ�
    }

    // Update is called once per frame
    void Update()
    {
        degree += Time.deltaTime;
        if (degree >= 360)
            degree = 0;
        // Material�� �ִ� ������ ���� �����Ű�� �Լ�
        RenderSettings.skybox.SetFloat("_Rotation", degree);

        // ������ ���۵Ǹ� Material�� �ִ� ���� ���� �����Ű�� �Լ�
        if (start)
        {
            boost += 0.1f * Time.deltaTime;
            tunnel.material.SetFloat("_Boost", boost);
            tunnel.material.SetFloat("_Exp", boost);
        }
    }

    // ���� ������ Ȯ���ϴ� �Լ�
    public void ClickStart()
    {
        start = true;
    }

    IEnumerator _start()
    {
        yield return new WaitForSeconds(3f);
        start = true;
    }
}
