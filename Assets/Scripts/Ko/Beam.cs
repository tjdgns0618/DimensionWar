using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public BoxCollider beam;
    public ParticleSystem ps;
    float starttime;
    // Start is called before the first frame update
    void Start()
    {
        beam.size = new Vector3(0.5f, 0.5f, 0.5f);
        beam.center = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        starttime += Time.deltaTime;
        if(starttime>ps.startDelay)
        {

            beam.size += new Vector3(0, 0, Time.deltaTime * 20);
            beam.center += new Vector3(0, 0, Time.deltaTime * 10);

        }
    }
}
