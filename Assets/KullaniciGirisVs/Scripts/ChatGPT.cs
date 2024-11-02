using UnityEngine; // UnityEngine kütüphanesini dahil eder.
using UnityEngine.UI; // UnityEngine.UI kütüphanesini dahil eder.
using System.Collections.Generic; // System.Collections.Generic kütüphanesini dahil eder.
using Unity.VisualScripting;
using TMPro;

namespace OpenAI // OpenAI isim alaný tanýmlanýr.
{
    public class ChatGPT : MonoBehaviour // ChatGPT sýnýfý MonoBehaviour'den türetilir.
    {
        //[SerializeField] private InputField inputField; // Kullanýcýdan metin giriþi almak için InputField bileþeni.
        [SerializeField] private Button button1;
        [SerializeField] private Button button2;
        [SerializeField] private Button button3;// Mesaj göndermek için buton bileþeni.
        [SerializeField] private ScrollRect scroll; // Mesajlarýn kaydýrma görünümünde gösterileceði ScrollRect bileþeni.

        [SerializeField] private RectTransform sent; // Kullanýcý tarafýndan gönderilen mesajlarýn UI öðesi.
        [SerializeField] private RectTransform received; // Alýnan mesajlarýn UI öðesi.

        private float height; // Mesajlar eklenirken toplam yükseklik.
        private OpenAIApi openai = new OpenAIApi(); // OpenAIApi nesnesi oluþturulur.

        private List<ChatMessage> messages = new List<ChatMessage>(); // Gönderilen ve alýnan mesajlarý tutan liste.
        private string prompt = "Name an artificial intelligence model prepared by the game developers in a game. Give answers in line with the data given to you."; // Ýlk prompt metni.

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
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0); // Scroll içeriðinin yüksekliðini sýfýrlar.

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content); // Mesajýn rolüne göre doðru prefab'ý oluþturur.
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content; // Mesaj metnini prefab'a ekler.
            item.anchoredPosition = new Vector2(0, -height); // Mesajýn konumunu ayarlar.
            LayoutRebuilder.ForceRebuildLayoutImmediate(item); // Layout'un anýnda güncellenmesini saðlar.
            height += item.sizeDelta.y; // Toplam yüksekliðe yeni mesajýn yüksekliðini ekler.
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height); // Scroll içeriðinin yüksekliðini günceller.
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
                        Content = ChatGPTGonderilecekMesaj // Kullanýcýdan alýnan metni yeni mesaj olarak oluþturur.
                    };
                    //ifle mesajý deðiþtiricem unutma burayý
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

                        //Hata Çýkarsa Return ekle dene
                    }
                    

                    // Yeni mesajý ekrana ekler.

                    if (messages.Count == 0) newMessage.Content = prompt + "\n" + ChatGPTGonderilecekMesaj; // Ýlk mesajsa prompt'u mesaja ekler.

                    messages.Add(newMessage); // Yeni mesajý mesajlar listesine ekler.

                    button1.enabled = false; // Butonu devre dýþý býrakýr.
                    button2.enabled = false;
                    button3.enabled = false;

                    // AI modeli ile cevap oluþturma iþlemi
                    var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
                    {
                        Model = "gpt-3.5-turbo-0613", // Kullanýlan AI modelini belirtir.
                        Messages = messages // Mesajlar listesini gönderir.
                    });

                    if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
                    {
                        var message = completionResponse.Choices[0].Message; // AI modelinin oluþturduðu cevabý alýr.
                        message.Content = message.Content.Trim(); // Mesajýn içeriðini düzeltir.

                        messages.Add(message); // AI cevabýný mesajlar listesine ekler.
                        AppendMessage(message); // AI cevabýný ekrana ekler.
                    }
                    else
                    {
                        Debug.LogWarning("No text was generated from this prompt."); // Uyarý mesajý eðer AI modelinden bir cevap gelmezse.
                    }

                    button1.enabled = true;
                    button2.enabled = true;
                    button3.enabled = true;// Butonu tekrar etkinleþtirir.
                    
                }
                else 
                {
                    DurumKontrolPanosu.SetActive(true);
                    HataText.text = "Yapay Zeka yeterli oyun bilgisi olmadýðý için cevap verememektedir.Oyunlarý oynamalýsýnýz ve daha sonra tekrar denemelisiniz.";
                    button1.enabled = false;
                    button2.enabled = false;
                    button3.enabled = false;
                }
            }
            else
            {
                DurumKontrolPanosu.SetActive(true);
                HataText.text = "Ýnternet baðlantýnýzda bir problem bulunmamaktadýr. Ýnternet baðlantýnýzý kontrol edin ve tekrar deneyiniz.";
                button1.enabled = false;
                button2.enabled = false;
                button3.enabled = false;
            }
            

        }

        
        public void SoruBirMesaj()
        {
            
            ChatGPTGonderilecekMesaj = "";
            Durum = 1;
            EkranaGosterilecekMesaj.Content = "Çoçuðumun Durumu Hakkýnda BÝlgi Ver.";
            SendReply();
        }
        public void SoruIkiMesaj()
        {
            ChatGPTGonderilecekMesaj = "";
            Durum = 2;
            EkranaGosterilecekMesaj.Content = "Çoçuðum Ýçin Ne Önerirsin?";
            SendReply();
        }
        public void SoruUcMesaj()
        {
            ChatGPTGonderilecekMesaj = "";
            Durum = 3;
            EkranaGosterilecekMesaj.Content = "Çocuðumun yatkýnlýðý hakkýnda bilgi verir misin?";
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