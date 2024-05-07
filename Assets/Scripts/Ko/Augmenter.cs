using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Augmenter : MonoBehaviour
{
    public int number;
    public Text Name;
    public Text Effet;
    public Image Image;
    public GameObject button;
    public Augmenter_Data[] augmenter_Datas;
    public int r;

    // Start is called before the first frame update
    public void AugmentUpdate()
    {
        Name.text = augmenter_Datas[r].Augmenter_Name;
        Effet.text = augmenter_Datas[r].Augmenter_Desc;
        Image.sprite = augmenter_Datas[r].image;
        number = augmenter_Datas[r].number;
    }
    public void Button()
    {
        button.SetActive(false);
        Time.timeScale = PlayerPrefs.GetInt("Timescale");
        switch (number)
        {
            case 0:
                HealthUP();
                break;
            case 1:
                AttackUp();
                break;
            case 2:
                SpeedUp();
                break;
            case 3:
                BonusDamage();
                break;
            case 4:
                Skill_DamageUp();
                break;
            case 5:
                Pixel_HealthUP();
                break;
            case 6:
                Pixel_AttackUp();
                break;
            case 7:
                LowPoly_HealthUP();
                break;
            case 8:
                LowPoly_AttackUp();
                break;
            case 9:
                _3D_HealthUP();
                break;
            case 10:
                _3D_AttackUp();
                break;
        }
    }
    public void HealthUP()
    {
        GameManager.Instance.towerHp += 100;

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower>().health += 100;
        }
    }

    public void AttackUp()
    {
        GameManager.Instance.towerDamage += 100;

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower>().Damage += 100;
        }
    }
    public void SpeedUp()
    {
        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower>().AttackDel /= 1.5f;

        }
    }

    public void BonusDamage()
    {
        GameManager.Instance.BonusDamage += 10;
    }
    public void Skill_DamageUp()
    {
        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower_Skill>().SkillDamage += 100;
        }
    }
    public void Pixel_HealthUP()
    {

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.Pixel)
                GameManager.Instance.towers[i].GetComponent<Tower>().health += 100;
        }
    }

    public void Pixel_AttackUp()
    {

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.Pixel)
                GameManager.Instance.towers[i].GetComponent<Tower>().Damage += 100;
        }
    }
    public void LowPoly_HealthUP()
    {

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.RowPoly)
                GameManager.Instance.towers[i].GetComponent<Tower>().health += 100;
        }
    }

    public void LowPoly_AttackUp()
    {

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.RowPoly)
                GameManager.Instance.towers[i].GetComponent<Tower>().Damage += 100;
        }
    }
    public void _3D_HealthUP()
    {

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.RowPoly)
                GameManager.Instance.towers[i].GetComponent<Tower>().health += 100;
        }
    }
    public void _3D_AttackUp()
    {

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.RowPoly)
                GameManager.Instance.towers[i].GetComponent<Tower>().Damage += 100;
        }
    }
}





    


