using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Skeleton_Play : MonoBehaviour
{
    public ParticleSystem Skillmotion;

    // Start is called before the first frame update
    void Start()
    {
        Skill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Skill()
    {
        Skillmotion.Play();
    }
}
