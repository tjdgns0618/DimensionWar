using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;

public class BTManager : MonoBehaviour
{
    [Header("타워")]
    public GameObject[] Tower;      // 타워들
    [Header("게임속도표시UI")]
    public GameObject[] SpeedUI;    // 게임속도 표시 UI 이미지

    int gameSpeed = 1;              // 현재 게임 속도

    public void BuyTower()      // 타워 구매 함수
    {
        if(GameManager.Instance.diamond >= 1) // 20골드가 있어야만 구매 가능 조건
            StartCoroutine(_BuyTower());
    }

    public void SellTower()
    {
        StartCoroutine(_SellTower());
    }

    public void UpgradeTower()
    {
        if (GameManager.Instance.gold >= 50)    // 50골드가 있어야 업그레이드 가능
            StartCoroutine(_UpgradeTower());
        else
        {
            StartTime();                        // timescale을 원래대로 돌려주는 함수
        }
    }

    public void MeleeTowerSpawn()               // 근접 타워를 생성해주는 함수
    {
        if (GameManager.Instance.SelectBlock.transform.childCount >= 2) // 타워가 존재하지 않는 블럭이라면
            return;

        int i = Random.Range(0, 3);
        GameObject instance = Instantiate(Tower[i]);    // 근접타워중에서 랜덤으로 생성
        GameManager.Instance.towers.Add(instance);      // 생성된 타워를 게임매니저에 저장해준다.
        instance.transform.SetParent(GameManager.Instance.SelectBlock.transform);   // 클릭한 블럭 자식으로 타월르 생성한다.
        if (i == 0) {
            instance.transform.localPosition = new Vector3(0, 1.519f, 0);   // 픽셀 근접 타워 위치 초기화
            instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (i == 1)
        {
            instance.transform.localPosition = new Vector3(0, 0.5f, 0);     // 로우폴리 근접 타워 위치 초기화
            instance.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (i == 2)
        {
            instance.transform.localPosition = new Vector3(0, 0.5f, 0);     // 3D 근접 타워 위치 초기화
            instance.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void RangerTowerSpawn()
    {
        if (GameManager.Instance.SelectBlock.transform.childCount >= 2) // 타워가 존재하지 않는 블럭이라면
            return;

        int i = Random.Range(3, 9);
        GameObject instance = Instantiate(Tower[i]);    // 원거리타워중에서 랜덤으로 생성
        GameManager.Instance.towers.Add(instance);      // 생성된 타워를 게임매니저에 저장해준다.
        instance.transform.SetParent(GameManager.Instance.SelectBlock.transform);   // 클릭한 블럭 자식으로 타월르 생성한다.
        if (i == 3 || i == 4)
        {
            instance.transform.localPosition = new Vector3(0, 1.418f, 0);   // 픽셀 원거리 타워 위치 초기화
            instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (i == 5 || i == 6)
        {
            instance.transform.localPosition = new Vector3(0, 0.5f, 0);     // 로우폴리 원거리 타워 위치 초기화
            instance.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (i == 7 || i == 8)
        {
            instance.transform.localPosition = new Vector3(0, 0.5f, 0);     // 3D 원거리 타워 위치 초기화
            instance.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void UpgradeBtnClick()
    {
        GameManager.Instance.uiManager.UpgradeCanvas.gameObject.SetActive(true);

        if (!GameManager.Instance.tower.GetComponent<Tower>().upgrade[0])
            GameManager.Instance.uiManager.stat1Button.image.color = Color.white;
        else
            GameManager.Instance.uiManager.stat1Button.image.color = Color.green;

        if (!GameManager.Instance.tower.GetComponent<Tower>().upgrade[1])
            GameManager.Instance.uiManager.stat2Button.image.color = Color.white;
        else
            GameManager.Instance.uiManager.stat2Button.image.color = Color.green;


        GameManager.Instance.uiManager.stat1Button.enabled = !GameManager.Instance.tower.GetComponent<Tower>().upgrade[0];
        GameManager.Instance.uiManager.stat2Button.enabled = !GameManager.Instance.tower.GetComponent<Tower>().upgrade[1];
        GameManager.Instance.uiManager.UpgradeButton.enabled = (GameManager.Instance.tower.GetComponent<Tower>().upgrade[0] && GameManager.Instance.tower.GetComponent<Tower>().upgrade[1]);
    }

    public void Stat1Upgrade()
    {
        GameManager.Instance.uiManager.stat1Button.enabled = false;
        GameManager.Instance.uiManager.stat1Button.image.color = Color.green;
        GameManager.Instance.tower.GetComponent<Tower>().upgrade[0] = true;
        if(GameManager.Instance.tower.GetComponent<Tower>().upgrade[0] && 
            GameManager.Instance.tower.GetComponent<Tower>().upgrade[1])
        {
            GameManager.Instance.uiManager.UpgradeButton.enabled = true;
        }
        StartTime();
    }

    public void Stat2Upgrade()
    {
        GameManager.Instance.uiManager.stat2Button.enabled = false;
        GameManager.Instance.uiManager.stat2Button.image.color = Color.green;
        GameManager.Instance.tower.GetComponent<Tower>().upgrade[1] = true;
        if (GameManager.Instance.tower.GetComponent<Tower>().upgrade[0] &&
            GameManager.Instance.tower.GetComponent<Tower>().upgrade[1])
        {
            GameManager.Instance.uiManager.UpgradeButton.enabled = true;
        }
        StartTime();
    }

    public void DamageUpgrade()
    {
        GameManager.Instance.tower.GetComponent<Tower>().Damage *= 1.2f;
    }

    public void SpeedUpgrade()
    {
        GameManager.Instance.tower.GetComponent<Tower>().AttackDel *= 0.9f;
    }

    public void SkillCoolUpgrade()
    {
        GameManager.Instance.tower.GetComponent<Tower>().SkillCost--;
    }

    public void HealthUpgrade()
    {
        GameManager.Instance.tower.GetComponent<Tower>().health *= 2f;
        GameManager.Instance.tower.GetComponent<Tower>().tempHealth *= 2f;
    }

    IEnumerator _BuyTower()
    {
        if (GameManager.Instance.SelectBlock.CompareTag("MeleeBuildable"))  // 근접 설치 가능 블럭이라면
            MeleeTowerSpawn();
        else
            RangerTowerSpawn();
                
        GameManager.Instance.diamond--;            // 20골드가 소모된다.
        GameManager.Instance.blockClicked = false;  // 블럭 클릭상태를 초기화해준다.
        GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORewind();    // 구매 패널을 돌려보낸다.
        GameManager.Instance.SelectBlock.layer = 2; // 블럭을 클릭 불가능하게 만든다.
        Destroy(GameManager.Instance.SelectBlock.GetComponent<Blocks>().tempBuyEffect.gameObject);   // 블럭 선택 이펙트를 지워준다.
        GameManager.Instance.SelectBlock = null;

        yield return null;
    }

    IEnumerator _SellTower()
    {
        GameManager.Instance.gold += 20;    // 20골드를 돌려받는다.
        Destroy(GameManager.Instance.tower.gameObject); // 타워를 제거한다.
        GameManager.Instance.tower.transform.parent.GetComponent<Blocks>().isBuild = false;
        GameManager.Instance.blockClicked = false;  // 블럭 클릭상태를 초기화해준다.
        GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORewind();    // 구매 패널을 돌려보낸다.
        GameManager.Instance.tower.transform.parent.gameObject.layer = 0;   // 블럭을 클릭 가능하게 만든다.

        yield return null;
    }

    IEnumerator _UpgradeTower()
    {
        // 다음 타워가 존재하지 않다면 예외처리
        if (GameManager.Instance.tower.GetComponent<TestScript>().nextTower == null)
        {
            Time.timeScale = gameSpeed;
            yield return null;
        }

        GameManager.Instance.gold -= 50;    // 50골드를 소비한다.
        // 현재 선택된 타워의 다음 타워를 생성한다.
        GameObject instance = Instantiate(GameManager.Instance.tower.GetComponent<TestScript>().nextTower);
        instance.transform.SetParent(GameManager.Instance.tower.transform.parent.transform);
        instance.transform.position = GameManager.Instance.tower.transform.position;
        if(instance.GetComponent<Tower>().tower_class != global::Tower.Tower_Class.Pixel)
            instance.transform.rotation = Quaternion.Euler(0, 180, 0);
        if (GameManager.Instance.tower.tower_class == global::Tower.Tower_Class.Pixel && GameManager.Instance.tower.tag == "Level2")
            instance.transform.position += new Vector3(0, 0.4f, 0); // 픽셀 3성 위치값 예외처리
        Destroy(GameManager.Instance.tower.gameObject); // 현재 타워를 제거한다.
        Time.timeScale = gameSpeed;

        yield return null;
    }

    // 블럭 선택을 취소하고 구매 패널을 돌려보내는 함수
    public void EndBuyTower()
    {
        if (GameManager.Instance.tower != null)
        {
            GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORewind();
            GameManager.Instance.tower.GetComponent<TestScript>().ClickEffect.SetActive(false);
            return;
        }
        GameManager.Instance.blockClicked = false;
        GameManager.Instance.SelectBlock.GetComponent<Blocks>().isBuild = false;
        GameManager.Instance.SelectBlock.GetComponent<Blocks>().audiosource.clip =
            GameManager.Instance.SelectBlock.GetComponent<Blocks>().audio[1];
        GameManager.Instance.SelectBlock.GetComponent<Blocks>().audiosource.Play();
        Destroy(GameManager.Instance.SelectBlock.GetComponent<Blocks>().tempBuyEffect.gameObject);

        GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORewind();
        GameManager.Instance.SelectBlock = null;
        GameManager.Instance.tower = null;
    }

    // 스킬 캔버스를 종료하고 게임속도를 원래대로 하는 함수
    public void EndSkillUp()
    {
        Time.timeScale = gameSpeed;
        GameManager.Instance.towerClicked = false;
    }

    // timescale을 원래 속도로 만드는 함수
    public void StartTime()
    {
        Time.timeScale = gameSpeed;
    }

    public void MakeTime1()
    {
        Time.timeScale = 1;
    }

    // timescale을 0으로 만드는 함수
    public void StopTime()
    {
        Time.timeScale = 0;
    }

    // timescale을 1,2,3으로 만들어주고 gameSpeed 초기화해주는 함수
    public void SpeedUp()
    {
        if (Time.timeScale == 1)
        {
            gameSpeed = 2;
            PlayerPrefs.SetInt("Timescale", gameSpeed);
            SpeedUI[0].SetActive(false);
            SpeedUI[1].SetActive(true);
            Time.timeScale = gameSpeed;
        }
        else if (Time.timeScale == 2)
        {
            gameSpeed = 3;
            PlayerPrefs.SetInt("Timescale", gameSpeed);
            SpeedUI[1].SetActive(false);
            SpeedUI[2].SetActive(true);
            Time.timeScale = gameSpeed;
        }
        else if (Time.timeScale == 3)
        {
            gameSpeed = 1;
            PlayerPrefs.SetInt("Timescale", gameSpeed);
            SpeedUI[2].SetActive(false);
            SpeedUI[0].SetActive(true);
            Time.timeScale = gameSpeed;
        }
    }
}
