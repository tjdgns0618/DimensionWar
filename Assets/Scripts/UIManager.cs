using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // public Slider BgmSlider;        // 배경음악 볼륨 조절 슬라이더
    public Canvas uiCanvas;         // 게임ui용 캔버스
    public Canvas UpgradeCanvas;      // 스킬업그레이드ui용 캔버스
    public GameObject BuyPaenl;     // 타워 구매 패널
    public GameObject ClearPanel;
    public GameObject FailedPanel;
    public Text RoundTime;          // 라운드 시작까지 남은 시간
    public Text GoldText;           // 현재 소유중인 골드
    public Text DiaText;
    public Button BuyButton;
    public Button SellButton;
    public Button towerUpgradeButton;
    public Button argumentButton;
    public Button rangeStat1Button;      // 1번 강화버튼
    public Button rangeStat2Button;      // 2번 강화버튼
    public Button meleeStat1Button;      // 2번 강화버튼
    public Button meleeStat2Button;      // 2번 강화버튼
    public Button UpgradeButton;    // 1,2 강화한 후 활성화되는 버튼

    // Start is called before the first frame update
    void Start()
    {
        //if(BgmSlider != null)
        //    BgmSlider.value = PlayerPrefs.GetFloat("BgmValue"); // PlayerPref에 저장한 볼륨 불러오기
        //StartGame();  // 영상제작용
        Screen.SetResolution(1920, 1080, true);
    }

    public void SetDimension(int dimension)
    {
        PlayerPrefs.SetInt("dimension", dimension); // 3,2차원 스테이지 구별용
    }

    public void SetStage(string name)
    {
        PlayerPrefs.SetString("StageName", name);   // 불러올 씬 이름 저장용
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("LoadingScene");     // 로딩씬 실행후 SetStage로 저장한 씬 불러옴
    }

    /*public void SetBgmValue(Slider slider)
    {   
        PlayerPrefs.SetFloat("BgmValue", slider.value); // 슬라이더로 조절한 볼륨을 PlayerPrefs에 저장
    }*/

    /*public void SetSoundImage(Image image)
    {
        if (PlayerPrefs.GetFloat("BgmValue") == 0)  // 볼륨이 0이면 이미지 변경
            image.sprite = soundImage[0];
        else
            image.sprite = soundImage[1];
    }*/

    public void StartGame()
    {
        StartCoroutine(_StartGame());
    }

    public void QuitGame()
    {
        StartCoroutine(_QuitGame());
    }

    IEnumerator _StartGame()                    // 게임을 시작하는 함수
    {
        yield return new WaitForSeconds(3f);
        //yield return new WaitForSeconds(10f);   // 영상찍기용
        SceneManager.LoadScene("StageSelect");    
    }

    IEnumerator _QuitGame()                     // 게임을 종료하는 함수
    {
        Application.Quit();
        yield return null;
    }
}
