using UnityEngine; // UnityEngine k�t�phanesini dahil eder.
using UnityEngine.UI; // UnityEngine.UI k�t�phanesini dahil eder.
using System.Collections.Generic; // System.Collections.Generic k�t�phanesini dahil eder.
using Unity.VisualScripting;
using TMPro;

namespace OpenAI // OpenAI isim alan� tan�mlan�r.
{
    public class ChatGPT : MonoBehaviour // ChatGPT s�n�f� MonoBehaviour'den t�retilir.
    {
        //[SerializeField] private InputField inputField; // Kullan�c�dan metin giri�i almak i�in InputField bile�eni.
        [SerializeField] private Button button1;
        [SerializeField] private Button button2;
        [SerializeField] private Button button3;// Mesaj g�ndermek i�in buton bile�eni.
        [SerializeField] private ScrollRect scroll; // Mesajlar�n kayd�rma g�r�n�m�nde g�sterilece�i ScrollRect bile�eni.

        [SerializeField] private RectTransform sent; // Kullan�c� taraf�ndan g�nderilen mesajlar�n UI ��esi.
        [SerializeField] private RectTransform received; // Al�nan mesajlar�n UI ��esi.

        private float height; // Mesajlar eklenirken toplam y�kseklik.
        private OpenAIApi openai = new OpenAIApi(); // OpenAIApi nesnesi olu�turulur.

        private List<ChatMessage> messages = new List<ChatMessage>(); // G�nderilen ve al�nan mesajlar� tutan liste.
        private string prompt = "Name an artificial intelligence model prepared by the game developers in a game. Give answers in line with the data given to you."; // �lk prompt metni.

        public KarsilamaEkraniBilgiCekme AiCheck;
        public InternetControl NetCheck;
        public InternetControl NetFonksiyon;
        public GameObject DurumKontrolPanosu;
        public TextMeshProUGUI HataText;
        string ChatGPTGonderilecekMesaj;
        int Durum =0;
        ChatMessage EkranaGosterilecekMesaj;
        //public KarsilamaEkraniBilgiCekme KullaniciBilgiDizisi;
        private void Start()
        {
           
        }

        private void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0); // Scroll i�eri�inin y�ksekli�ini s�f�rlar.

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content); // Mesaj�n rol�ne g�re do�ru prefab'� olu�turur.
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content; // Mesaj metnini prefab'a ekler.
            item.anchoredPosition = new Vector2(0, -height); // Mesaj�n konumunu ayarlar.
            LayoutRebuilder.ForceRebuildLayoutImmediate(item); // Layout'un an�nda g�ncellenmesini sa�lar.
            height += item.sizeDelta.y; // Toplam y�ksekli�e yeni mesaj�n y�ksekli�ini ekler.
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height); // Scroll i�eri�inin y�ksekli�ini g�nceller.
            scroll.verticalNormalizedPosition = 0; // Scroll pozisyonunu en alta ayarlar.
        }

        private async void SendReply()
        {

            NetFonksiyon.KontrolYap();
            bool NetChecking = NetCheck.isChecking;
            if (NetChecking == true)
            {
                bool AiChecking = AiCheck.YapayZekaUygunluk;
                if (AiChecking == true)
                {
                    var newMessage = new ChatMessage()
                    {
                        Role = "user",
                        Content = ChatGPTGonderilecekMesaj // Kullan�c�dan al�nan metni yeni mesaj olarak olu�turur.
                    };
                    //ifle mesaj� de�i�tiricem unutma buray�
                    EkranaGosterilecekMesaj.Role = "user";
                    switch (Durum)
                    {
                        case 1:
                            AppendMessage(EkranaGosterilecekMesaj);
                            break;
                        case 2:
                            AppendMessage(EkranaGosterilecekMesaj);
                            break;
                        case 3: 
                            AppendMessage(EkranaGosterilecekMesaj); 
                            break;

                        //Hata ��karsa Return ekle dene
                    }
                    

                    // Yeni mesaj� ekrana ekler.

                    if (messages.Count == 0) newMessage.Content = prompt + "\n" + ChatGPTGonderilecekMesaj; // �lk mesajsa prompt'u mesaja ekler.

                    messages.Add(newMessage); // Yeni mesaj� mesajlar listesine ekler.

                    button1.enabled = false; // Butonu devre d��� b�rak�r.
                    button2.enabled = false;
                    button3.enabled = false;

                    // AI modeli ile cevap olu�turma i�lemi
                    var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
                    {
                        Model = "gpt-3.5-turbo-0613", // Kullan�lan AI modelini belirtir.
                        Messages = messages // Mesajlar listesini g�nderir.
                    });

                    if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
                    {
                        var message = completionResponse.Choices[0].Message; // AI modelinin olu�turdu�u cevab� al�r.
                        message.Content = message.Content.Trim(); // Mesaj�n i�eri�ini d�zeltir.

                        messages.Add(message); // AI cevab�n� mesajlar listesine ekler.
                        AppendMessage(message); // AI cevab�n� ekrana ekler.
                    }
                    else
                    {
                        Debug.LogWarning("No text was generated from this prompt."); // Uyar� mesaj� e�er AI modelinden bir cevap gelmezse.
                    }

                    button1.enabled = true;
                    button2.enabled = true;
                    button3.enabled = true;// Butonu tekrar etkinle�tirir.
                    
                }
                else 
                {
                    DurumKontrolPanosu.SetActive(true);
                    HataText.text = "Yapay Zeka yeterli oyun bilgisi olmad��� i�in cevap verememektedir.Oyunlar� oynamal�s�n�z ve daha sonra tekrar denemelisiniz.";
                    button1.enabled = false;
                    button2.enabled = false;
                    button3.enabled = false;
                }
            }
            else
            {
                DurumKontrolPanosu.SetActive(true);
                HataText.text = "�nternet ba�lant�n�zda bir problem bulunmamaktad�r. �nternet ba�lant�n�z� kontrol edin ve tekrar deneyiniz.";
                button1.enabled = false;
                button2.enabled = false;
                button3.enabled = false;
            }
            

        }

        
        public void SoruBirMesaj()
        {
            
            ChatGPTGonderilecekMesaj = "";
            Durum = 1;
            EkranaGosterilecekMesaj.Content = "�o�u�umun Durumu Hakk�nda B�lgi Ver.";
            SendReply();
        }
        public void SoruIkiMesaj()
        {
            ChatGPTGonderilecekMesaj = "";
            Durum = 2;
            EkranaGosterilecekMesaj.Content = "�o�u�um ��in Ne �nerirsin?";
            SendReply();
        }
        public void SoruUcMesaj()
        {
            ChatGPTGonderilecekMesaj = "";
            Durum = 3;
            EkranaGosterilecekMesaj.Content = "�ocu�umun yatk�nl��� hakk�nda bilgi verir misin?";
            SendReply();
        }
        public void TamamButton()
        {
            DurumKontrolPanosu.SetActive(false);
            button1.enabled = true;
            button2.enabled = true;
            button3.enabled = true;
        }
    }
}