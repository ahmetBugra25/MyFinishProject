using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KayıtOlmaIslemleri : MonoBehaviour
{
    [System.Serializable]
    public class Player
    {
        public string username;
        public int age;
        public string password;
        public int GecirilecekSure;
        public int MathZeka;
        public int GorselZeka;
        public int IsitselZeka;

        public Player(string _username, int _age, string _password, int _gecirilecekSure, int _mathZeka, int _gorselZeka,int _isitselZeka)
        {
            username = _username;
            age = _age;
            password = _password;
            GecirilecekSure = _gecirilecekSure;
            MathZeka = _mathZeka;
            GorselZeka = _gorselZeka;
            IsitselZeka= _isitselZeka;
        }
    }
    public GameObject KayitKarti;
    public GameObject HataKarti;
    public TextMeshProUGUI HataMesaji;
    public TMP_InputField usernameInputField;
    public TMP_InputField ageInputField;
    public TMP_InputField passwordInputField;
    public TMP_InputField GecirilenSureField;

    public void HataMesajiKapa()
    {
        if (HataMesaji.text == "Kayıt başarılı bir şekilde tamamlanmışıtr")
        {
            SceneManager.LoadScene("KarsilamaEkrani");
        }
        else
        {
            HataKarti.SetActive(false);
            KayitKarti.SetActive(true);
        }
    }
    public void RegisterUser()
    {
        
        if (!IsNumeric(ageInputField.text))
        {
            KayitKarti.SetActive(false);
            HataKarti.SetActive(true);
            HataMesaji.text = "Yaş sadece rakamlardan oluşmalıdır.";
            return;
        }
        
        string username = usernameInputField.text;
        int age = int.Parse(ageInputField.text);
        string password = passwordInputField.text;
        int GecirelecekSure = int.Parse(GecirilenSureField.text);
        int MathZeka=0;
        int GorselZeka=0;
        int IsitselZeka = 0;

        Player player = new Player(username, age, password,GecirelecekSure,MathZeka,GorselZeka,IsitselZeka);

        // Oyuncu bilgilerini kaydetmek için PlayerPrefs veya başka bir yöntem kullanabilirsiniz.
        // PlayerPrefs örneğini kullanalım:
        
        if (username != "" && age >=4 && password != "")
        {
            
            PlayerPrefs.SetString("Username", player.username);
            PlayerPrefs.SetInt("Age", player.age);
            PlayerPrefs.SetString("Password", player.password);
            PlayerPrefs.SetInt("GecirilecekSure", player.GecirilecekSure);
            PlayerPrefs.SetInt("MathZeka", player.MathZeka);
            PlayerPrefs.SetInt("GorselZeka", player.GorselZeka);
            PlayerPrefs.SetInt("IsitselZeka",player.IsitselZeka);
            KayitKarti.SetActive(false);
            HataKarti.SetActive(true);
            HataMesaji.text = "Kayıt başarılı bir şekilde tamamlanmışıtr";
            
        }
        else
        {
            KayitKarti.SetActive(false);
            HataKarti.SetActive(true);
            HataMesaji.text = "Kullanıcı verilerinde eksiklik vardır.Lütfen tamamlayınız.";
            return;
        }
        
        if (age < 4)
        {

            KayitKarti.SetActive(false);
            HataKarti.SetActive(true);
            HataMesaji.text = "kullanıcın yaşı 4 yaşın altında olamaz";
        }
        if (age > 9)
        {

            KayitKarti.SetActive(false);
            HataKarti.SetActive(true);
            HataMesaji.text = "kullanıcın yaşı 9 yaşın üstünde olamaz";
        }
        if (GecirelecekSure <= 0)
        {
            KayitKarti.SetActive(false);
            HataKarti.SetActive(true);
            HataMesaji.text = "Geçirilecek süre 0 veya 0 dan küçük olamaz.Lütfen Süreyi belirleyiniz.";
        }

    }
    public bool IsNumeric(string str)
    {
        foreach (char c in str)
        {
            if (!char.IsDigit(c))
            {
                return false;
            }
        }
        return true;
    }
    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("KarsilamaEkrani");
    }
}
