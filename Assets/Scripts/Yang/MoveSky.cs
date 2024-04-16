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
    }

    // Update is called once per frame
    void Update()
    {
        degree += Time.deltaTime;
        if (degree >= 360)
            degree = 0;

        RenderSettings.skybox.SetFloat("_Rotation", degree);

        if (start)
        {
            boost += 0.2f * Time.deltaTime;
            tunnel.material.SetFloat("_Boost", boost);
            tunnel.material.SetFloat("_Exp", boost);
        }
    }

    public void ClickStart()
    {
        start = true;
    }
}
