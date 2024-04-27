using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Slider BgmSlider;
    public Canvas uiCanvas;
    public GameObject BuyPaenl;
    public Canvas skillCanvas;
    public Text RoundTime;
    public Text GoldText;

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

    public void SetBgmValue(Slider slider)
    {        
        PlayerPrefs.SetFloat("BgmValue", slider.value);
        Debug.Log((PlayerPrefs.GetFloat("BgmValue")*100).ToString("#"));
    }
    
    public void StartGame()
    {
        StartCoroutine(_StartGame());
    }

    public void QuitGame()
    {
        StartCoroutine(_QuitGame());
    }

    IEnumerator _StartGame()
    {
        yield return new WaitForSeconds(3f);
        LoadScene("StageSelect");
    }

    IEnumerator _QuitGame()
    {
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }
}
