using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PanoControl : MonoBehaviour
{
    string sceneName;
    public GameObject pano,play,home;
    int age;
   

    bool panoSetActive =false;

    public void MenuOnClick()
    {
        if (panoSetActive == false)
        {
            pano.SetActive(true);
        }
        else
        {
            pano.SetActive(false);
        }
        
    }
    public void Gecis()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void HomeOnClick()
    {
        SceneManager.LoadScene("KarsilamaEkrani");
    }
    public void YasSahneGeriDonus()
    {
        
        switch (age)
        {
            case 4:
                SceneManager.LoadScene("DortYasSahne");
                break;
            case 5:
                SceneManager.LoadScene("BesYasOyunlar");
                break;
            case 6:
                SceneManager.LoadScene("AltiYasOyunlar");
                break;
            case 7:
                SceneManager.LoadScene("YediYasOyunlar");
                break;
            case 8:
                SceneManager.LoadScene("SekizYasOyunlar");
                break;
            case 9:
                SceneManager.LoadScene("DokuzYasOyunlar");
                break;
        }
    }

    public void ContinueOnClick()
    {
        pano.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        age = PlayerPrefs.GetInt("Age");
    }

    // Update is called once per frame
    void Update()
    {
        panoSetActive = pano.activeSelf;
    }
}
