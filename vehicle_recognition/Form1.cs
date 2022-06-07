using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace vehicle_recognition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.btnClickThis.Click += new System.EventHandler(this.btnClickThis_Click);
            this.loadbtn.Click += new System.EventHandler(this.loadbtn_Click);
        }

        private void btnClickThis_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("click!!");

            //Bitmap img = new Bitmap(LoadBitmap(@"C:\Users\User\source\ex\kor.JPG"));
            Bitmap img = new Bitmap(pictureBox1.Image);
            var ocr = new TesseractEngine("./tessdata", "kor", EngineMode.Default);
            var texts = ocr.Process(img);
            MessageBox.Show(texts.GetText());
        }

        private void loadbtn_Click(object sender, EventArgs e)
        {
            string image_file = string.Empty;

            //파일 오픈창 생성 및 설정
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "이미지 파일 불러오기";
            dialog.Filter = "그림 파일 (*.jpg, *.gif, *.bmp) | *.jpg; *.gif; *.bmp; | 모든 파일 (*.*) | *.*";
            
            //파일 오픈창 로드 및 ok 버튼 클릭
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image_file = dialog.FileName; //File경로와 File명을 모두 가지고 옴

            }
            else if(dialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            //pictureBox1.Image = Bitmap.FromFile(image_file);
            Bitmap img = new Bitmap(LoadBitmap(image_file));
            pictureBox1.Image = img;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public Bitmap LoadBitmap(string file_name)
        {
            if (System.IO.File.Exists(file_name))
            {
                //파일을 읽기 전용으로 오픈
                using (System.IO.FileStream stream= new System.IO.FileStream(file_name, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    //FileStream으로부터 BinaryReader를 구한다
                    using (System.IO.BinaryReader reader = new System.IO.BinaryReader(stream))
                    {
                        //파일의 내용을 MemoryStream으로 복사
                        var memoryStream = new System.IO.MemoryStream(reader.ReadBytes((int)stream.Length));
                        //MemoryStream을 Bitmap으로 만들어 반환
                        return new Bitmap(memoryStream);
                    }
                }
            }
            else
            {
                MessageBox.Show("이미지를 찾을 수 없습니다", "이미지 로딩", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }
    }
}
