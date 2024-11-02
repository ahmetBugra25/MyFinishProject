using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class KarsilamaEkraniBilgiCekme : MonoBehaviour
{
    public bool KayitDurumu = false;
    public bool YapayZekaUygunluk=false;
    public object[] KullaniciDizi;
    
    void Start()
    {
        UserCanvasKontrol();
        UygunlukDurumu();
    
    }
    public void UygunlukDurumu()
    {//Burada Kaldým Burayý deðþtirmem lazým.
        
        if (PlayerPrefs.GetInt("MathZeka") == 0 && PlayerPrefs.GetInt("GorselZeka")==0 && PlayerPrefs.GetInt("IsitselZeka")==0)
        {
            YapayZekaUygunluk = false;
        }
    }
    
    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("KarsilamaEkrani");
    }
    public void UserCanvasKontrol()
    {
        if (PlayerPrefs.GetInt("Age") != 0)
        {
            KullaniciDizi = new object[7];
            KullaniciDizi[0] = PlayerPrefs.GetString("Username");
            KullaniciDizi[1] = PlayerPrefs.GetString("Password");
            KullaniciDizi[2] = PlayerPrefs.GetInt("Age");
            KullaniciDizi[3] = PlayerPrefs.GetInt("GecirilecekSure");
            KullaniciDizi[4] = PlayerPrefs.GetInt("MathZeka");
            KullaniciDizi[5] = PlayerPrefs.GetInt("GorselZeka");
            KullaniciDizi[6] = PlayerPrefs.GetInt("IsitselZeka");
            KayitDurumu = true;
            
        }
    }
    
}
