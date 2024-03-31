using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject StagePanel;
    public GameObject SettingPanel;
    public Slider BgmSlider;

    // Start is called before the first frame update
    void Start()
    {
        if(BgmSlider)
            BgmSlider.value = PlayerPrefs.GetFloat("BgmValue");
    }

    public void LoadScene(string name)
    {
        PlayerPrefs.SetString("StageName", name);
        SceneManager.LoadScene("LoadingScene");
    }

    public void SettingBtn()
    {
        StagePanel.SetActive(false);
        SettingPanel.SetActive(true);        
    }

    public void SettingQuit()
    {
        SettingPanel.SetActive(false);
        StagePanel.SetActive(true);
    }

    public void SetBgmValue(Slider slider)
    {        
        PlayerPrefs.SetFloat("BgmValue", slider.value);
        Debug.Log((PlayerPrefs.GetFloat("BgmValue")*100).ToString("#"));
    }
}
