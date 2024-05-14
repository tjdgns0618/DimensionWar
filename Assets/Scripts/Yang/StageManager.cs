using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public GameObject Stage2;
    public GameObject Stage2Image;
    public GameObject Stage3;
    public GameObject Stage3Image;
    public GameObject Stage4;
    public GameObject Stage4Image;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Stage1Clear") == 1)
        {
            Stage2.SetActive(false);
            Stage2Image.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Stage2Clear") == 1)
        {
            Stage3.SetActive(false);
            Stage3Image.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Stage3Clear") == 1)
        {
            Stage4.SetActive(false);
            Stage4Image.SetActive(true);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
