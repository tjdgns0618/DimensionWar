using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] audios;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (audios != null)
        {
            for (int i = 0; i < audios.Length; i++)
            {
                // 매니저 안에 들어있는 오디오 소스의 볼륨을 변경해준다.
                audios[i].volume = PlayerPrefs.GetFloat("BgmValue"); 
            }
        }
    }
}
