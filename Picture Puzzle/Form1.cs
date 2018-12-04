using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Picture_Puzzle
{
    public partial class Form1 : Form
    {
        int tık = 0;
        Point boş_konum;//Belirten bir değer alır ve  olup olmadığını bu Point boştur.
        ArrayList images = new ArrayList();//Resmi arraylist(dinamik boyutunu kendini ayrlayabilen dizi) olarak tanımlar
        public Form1()
        {
            boş_konum.X = 180;//Bu x koordinatını alır veya konumu ayarlar.
            boş_konum.Y = 180;//Bu y koordinatını alır veya konumu ayarlar.
            InitializeComponent();
        }

        

        private void button9_Click(object sender, EventArgs e)//Oyunu başlatır
        {
            tık = 0;
            label1.Text = "0";
            //Butonların tümüne aynı işlemleri uygulamak için foreach döngüsünü kullandık
            foreach (Button b in panel1.Controls)
                b.Enabled = true;

            Image orginal = Image.FromFile(@"C:\Users\ORHAN METİN\Desktop\Picture Puzzle\Picture Puzzle\img\araba.jpg");//resmin adresine ulaşılır

            ResimParcala(orginal, 270, 270);//Resmi parçalar

            Butonlara_resim_ekle(images);
            
                
        }

        private void Butonlara_resim_ekle(ArrayList images)//Butonların üstüne rastgele resim parçarının yerleştirilmesi 
        {
            int i = 0;
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7 };

            arr = Diziyi_karıştır(arr);

            foreach(Button b in panel1.Controls)
            {
                if(i<arr.Length)
                {
                    b.Image = (Image)images[arr[i]];//Butonların üzerine resimleri yapıştırır.
                    i++;
                }
            }
        }

        private int[] Diziyi_karıştır(int[] arr)//Dizinin elemanlarını karışık sıralar
        {
            Random rand = new Random();
            arr = arr.OrderBy(x => rand.Next()).ToArray();//Verilen dizinin elemalarını karışık olarak random sıralar

            return arr;
        }

        private void ResimParcala(Image orginal, int w, int h)//Resimlerin parçalanması
        {
            Bitmap bmp = new Bitmap(w, h);//Görüntü piksellerini bit-bit tanımlayan sıkıştırılmamış bir grafik biçimidir.
            // bmp, herhangi bir sıkıştırma yapmadan resmin özelliklerini tutan, Microsoft firmasına ait bir resim dosyası biçimidir.
            Graphics graphic = Graphics.FromImage(bmp);//Değişiklik için grafik nesnesi oluşturuldu.
            

           graphic.DrawImage(orginal, 0, 0, w, h);//Belirtilen Görüntüyü, orijinal fiziksel boyutunu kullanarak belirtilen konumda çizer.
            


            graphic.Dispose();//Oluşan grafik nesnesini imha eder

            int movr = 0, movd = 0;

            for(int x=0;x<8;x++)
            {
                Bitmap piece = new Bitmap(90, 90); //Yeni bitmap nesnesi oluşturur.

                for (int i = 0; i < 90; i++)
                    for (int j = 0; j < 90; j++)
                        piece.SetPixel(i, j,bmp.GetPixel(i + movr, j + movd));//Setpixel belirtilen piksel rengi ayarlar
                //Getpixel bu konuda belirtilen piksel rengini alır

                images.Add(piece);//Bölünen parçayı ekle

                movr += 90;

                if(movr==270)
                {
                    movr = 0;
                    movd += 90;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)//Panel üzerindeki tüm bütonların seçilmesi olayı
        {
            MoveButton((Button)sender);
        }

        private void MoveButton(Button btn)
        {
           //Seçilen butonların panel üzerinde nerelere hareket edebileceğinin tanımı
            if(((btn.Location.X==boş_konum.X-90||btn.Location.X==boş_konum.X+90)
                &&btn.Location.Y==boş_konum.Y)
                ||(btn.Location.Y==boş_konum.Y-90||btn.Location.Y==boş_konum.Y+90)
                &&btn.Location.X==boş_konum.X)
            {
                Point swap = btn.Location;
                btn.Location = boş_konum;
                boş_konum = swap;
                //Buton konumlarını yer değiştir 
                tık++;
                label1.Text = tık.ToString();
            }
            else
            {
                MessageBox.Show("Yanlış hamle yaptınız.\nBoşluğa cephesi olan parçaları seçebilirsiniz.");
            }

            if(boş_konum.X==180&&boş_konum.Y==180) //Parçalar doğru mu yerşertirilmiş mi diye,Oyunun başlangıçtaki korrdinatlardaki konumu kontrol eder.
                Kontrol_et();
            
        }

        private void Kontrol_et() //Parçalar eşleşiyor mu kontrol
        {
            int count = 0, index;
            foreach(Button btn in panel1.Controls)//Foreach döngüsü ile, her bir öğeyi ayrı ayrı değerlendiriyoruz.
           //Foreach hiçbir tamsayı dizini kullanmaz. Bunun yerine, bir koleksiyonda kullanılır-sırayla her öğeyi döndürür.
            {
                index = (btn.Location.Y / 90) * 3 + btn.Location.X / 90;//
                if (images[index] == btn.Image)//Butonların konumlarına göre resimlerin karşılaştırılması
                    count++;
            }
            if (count == 8)//Eğer 8 parçada doğru yerleşmiş ise;
            {
                //Oyun puanlama
                if (tık <= 40)
                {
                    MessageBox.Show( tık.ToString()+ " ADIMDA KAZANDINIZ "+"PUANINIZ:*****");
                }
                if (tık == 41 && tık == 80)
                {
                    MessageBox.Show(tık.ToString() + " ADIMDA KAZANDINIZ " + "PUANINIZ:***");
                }
                if (tık > 80)
                {
                    MessageBox.Show(tık.ToString() + " ADIMDA KAZANDINIZ " + "PUANINIZ:*");
                }
            }
                
               
        }

        

        private void BTN_ÇIKIŞ_Click(object sender, EventArgs e)
        {
            Application.Exit();//Çıkış yapar
        }

        private void btn_hakkında_Click(object sender, EventArgs e)
        {
            hakkında h = new hakkında();//Hakkında formuna geçiş yapar.
            h.Show();
            
        }
    }
}
