using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadScene(string name)
    {
        PlayerPrefs.SetString("StageName", name);
        SceneManager.LoadScene("LoadingScene");
    }
}
