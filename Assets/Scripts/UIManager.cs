using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Sprite[] soundImage;     // ���� �̹��� �����
    public Slider BgmSlider;        // ������� ���� ���� �����̴�
    public Canvas uiCanvas;         // ����ui�� ĵ����
    public Canvas skillCanvas;      // ��ų���׷��̵�ui�� ĵ����
    public GameObject BuyPaenl;     // Ÿ�� ���� �г�
    public Text RoundTime;          // ���� ���۱��� ���� �ð�
    public Text GoldText;           // ���� �������� ���
    public Button BuyButton;
    public Button SellButton;
    public Button UpgradeButton;

    // Start is called before the first frame update
    void Start()
    {
        if(BgmSlider != null)
            BgmSlider.value = PlayerPrefs.GetFloat("BgmValue"); // PlayerPref�� ������ ���� �ҷ�����
        //StartGame();  // �������ۿ�
    }

    public void SetDimension(int dimension)
    {
        PlayerPrefs.SetInt("dimension", dimension); // 3,2���� �������� ������
    }

    public void SetStage(string name)
    {
        PlayerPrefs.SetString("StageName", name);   // �ҷ��� �� �̸� �����
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("LoadingScene");     // �ε��� ������ SetStage�� ������ �� �ҷ���
    }

    public void SetBgmValue(Slider slider)
    {   
        PlayerPrefs.SetFloat("BgmValue", slider.value); // �����̴��� ������ ������ PlayerPrefs�� ����
    }

    public void SetSoundImage(Image image)
    {
        if (PlayerPrefs.GetFloat("BgmValue") == 0)  // ������ 0�̸� �̹��� ����
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

    IEnumerator _StartGame()                    // ������ �����ϴ� �Լ�
    {
        yield return new WaitForSeconds(3f);
        //yield return new WaitForSeconds(10f);   // ��������
        SceneManager.LoadScene("StageSelect");    
    }

    IEnumerator _QuitGame()                     // ������ �����ϴ� �Լ�
    {
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }
}
