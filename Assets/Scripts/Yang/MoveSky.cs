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

        //StartCoroutine(_start()); // 영상제작용
    }

    // Update is called once per frame
    void Update()
    {
        degree += Time.deltaTime;
        if (degree >= 360)
            degree = 0;
        // Material에 있는 변수의 값을 변경시키는 함수
        RenderSettings.skybox.SetFloat("_Rotation", degree);

        // 게임이 시작되면 Material에 있는 변수 값을 변경시키는 함수
        if (start)
        {
            boost += 0.1f * Time.deltaTime;
            tunnel.material.SetFloat("_Boost", boost);
            tunnel.material.SetFloat("_Exp", boost);
        }
    }

    // 게임 시작을 확인하는 함수
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
