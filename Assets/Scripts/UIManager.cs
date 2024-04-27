using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Sprite[] soundImage;
    public Slider BgmSlider;
    public Canvas uiCanvas;
    public Canvas skillCanvas;
    public GameObject BuyPaenl;
    public Text RoundTime;
    public Text GoldText;

    // Start is called before the first frame update
    void Start()
    {
        if(BgmSlider)
            BgmSlider.value = PlayerPrefs.GetFloat("BgmValue");
    }

    public void SetStage(string name)
    {
        PlayerPrefs.SetString("StageName", name);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void SetBgmValue(Slider slider)
    {   
        PlayerPrefs.SetFloat("BgmValue", slider.value);
        Debug.Log((PlayerPrefs.GetFloat("BgmValue")*100).ToString("#"));
    }

    public void SetSoundImage(Image image)
    {
        if (PlayerPrefs.GetFloat("BgmValue") == 0)
            image.sprite = soundImage[0];
        else
            image.sprite = soundImage[1];
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
        SceneManager.LoadScene("StageSelect");
    }

    IEnumerator _QuitGame()
    {
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }
}
