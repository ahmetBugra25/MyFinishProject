using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KarsilamaEkraniMenuController : MonoBehaviour
{
    public InternetControl Fonksiyon;
    public InternetControl NetCheck;
    public KarsilamaEkraniBilgiCekme BoolKayitDurumu;
    public KarsilamaEkraniBilgiCekme KullaniciBilgiDizisi;
    public KayýtOlmaIslemleri IsNumeric;
    public TMP_InputField username;
    public TMP_InputField age;
    public TMP_InputField GecirilecekSure;
    public TextMeshProUGUI mathZeka;
    public TextMeshProUGUI gorselZeka;
    public TextMeshProUGUI isitselZeka;
    public GameObject DurumBilgilendirmeKarti;
    public TextMeshProUGUI HataMesaji;
    private int DataLoadAndUpdateControl=0;
   

    public void YeniKayitaGecis()
    {
        bool KayitDurumu = BoolKayitDurumu.KayitDurumu;

        if (KayitDurumu == false)
        {
            SceneManager.LoadScene("KayitEkleSahne");
        }
        else
        {
            DurumBilgilendirmeKarti.SetActive(true);
            HataMesaji.text = "Mevcut Hesap bulunmaktadýr.Yeni bir hesap oluþturmak istiyorsanýz lütfen mevcut hesap bilgilerini siliniz.";
            Debug.Log("Mevcut Hesabý Sil  sonra kayit yap");
        }

    }

    public void DurumKontrolKartiKapatma()
    {
        if (HataMesaji.text == "Kullanýcý bilgileri baþarýyla silindi")
        {
            SceneManager.LoadScene("KarsilamaEkrani");
        }
        else
        {
            DurumBilgilendirmeKarti.SetActive(false);
        }
        
    }

    public void SosyalMedyaIcon()
    {
        DurumBilgilendirmeKarti.SetActive(true);
        HataMesaji.text = "Uygulama Tamamlanmadýðý için henüz sosyal medya hesaplarý bulunmamaktadýr.";
    }
    public void AyarlaraGit()
    {
        
        if (BoolKayitDurumu.KayitDurumu == true)
        {
            SceneManager.LoadScene("Ayarlar");
            
        }
        else
        {
            DurumBilgilendirmeKarti.SetActive(true);
            HataMesaji.text = "Mevcut bir hesap bulunamadý.Lütfen bir hesap oluþturun daha sonra bu hesap için ayarlamalar yapabilirsiniz.";
            Debug.Log("AyarlarKontrolü");
        }
        
    }
    public void DurumAIGiris()
    {
        Fonksiyon.KontrolYap();
        bool isChecking = NetCheck.isChecking;
        bool KayitDurumu = BoolKayitDurumu.KayitDurumu;
        if (isChecking == true && KayitDurumu == true)
        {
            SceneManager.LoadScene("ChatGPTSample");
        }
        else
        {
            DurumBilgilendirmeKarti.SetActive(true);
            HataMesaji.text = "Lütfen mevcut hesabýnýz var olduðunu ve internet baðlantýnýz olduðuna emin olunuz.";
        }
    }
    public void ClearUserData()
    {
        string[] SilinecekBilgi = new string[] { "Username", "Password", "Age", "GecirilecekSure", "MathZeka", "GorselZeka", "IsitselZeka" };
        for (int i = 0; i < SilinecekBilgi.Length; i++)
        {
            PlayerPrefs.DeleteKey(SilinecekBilgi[i].ToString());
            
        }
        Debug.Log("Kullanýcý bilgileri baþarýyla silindi.");
        
        DurumBilgilendirmeKarti.SetActive(true);
        HataMesaji.text= "Kullanýcý bilgileri baþarýyla silindi";

    }
    public void MevcutHesapGiris()
    {
        int age = PlayerPrefs.GetInt("Age");
        if (age != 0)
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
        else
        {
            DurumBilgilendirmeKarti.SetActive(true);
            HataMesaji.text = "Kayýtlý hesap bilgisi bulunamadý.Lütfen ilk önce kayýt yapýnýz.";
        }
        
    }
    public void DataUptade()
    {
        
        bool NumericAge = IsNumeric.IsNumeric(age.text);
        bool NumericSure = IsNumeric.IsNumeric(GecirilecekSure.text);

        if (int.Parse(age.text) <= 9 && int.Parse(age.text) >= 4 && username.text != "" && GecirilecekSure.text != "" && age.text != "" && NumericAge == true && NumericSure == true)
        {
            PlayerPrefs.SetString("Username", username.text.ToString());
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Age", int.Parse(age.text));
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("GecirilecekSure", int.Parse(GecirilecekSure.text));
            PlayerPrefs.Save();
            KullaniciBilgiDizisi.KullaniciDizi[0] = username.text.ToString();
            KullaniciBilgiDizisi.KullaniciDizi[2] = age.text.ToString();
            KullaniciBilgiDizisi.KullaniciDizi[3] = GecirilecekSure.text.ToString();
            DataLoadAndUpdateControl = 0;
            DurumBilgilendirmeKarti.SetActive(true);
            HataMesaji.text = "Bilgiler Güncellendi";
        }
        else
        {
            DurumBilgilendirmeKarti.SetActive(true);
            HataMesaji.text = " Bilgiler Güncellenirken, bilgi eksikliði veya yaþ bilgisi için sayýsal deðer yazýlmadýðý için bilgiler güncellenememiþtir";
        }


    }
    public void DataLoading()
    {
        object[] kullaniciInfoArray = KullaniciBilgiDizisi.KullaniciDizi;

        username.text = kullaniciInfoArray[0].ToString();
        age.text = kullaniciInfoArray[2].ToString();
        GecirilecekSure.text = kullaniciInfoArray[3].ToString();
        mathZeka.text = kullaniciInfoArray[4].ToString();
        gorselZeka.text = kullaniciInfoArray[5].ToString();
        isitselZeka.text = kullaniciInfoArray[6].ToString();
        
    }
    private void Update()
    {
        string sahneAdi = SceneManager.GetActiveScene().name;
        if (sahneAdi == "Ayarlar" && DataLoadAndUpdateControl == 0)
        {
            DataLoading();
            DataLoadAndUpdateControl = 1;
        }

    }
    
}
