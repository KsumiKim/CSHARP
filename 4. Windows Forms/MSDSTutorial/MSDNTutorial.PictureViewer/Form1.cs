using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSDNTutorial.PictureViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ChbStretch_CheckedChanged(object sender, EventArgs e)
        {
            if (chbStretch.Checked)
            ptbImage.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                ptbImage.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close(); // Form 클래스 안에 close가 정의되어 있다. 
        }

        private void BtnBackground_Click(object sender, EventArgs e)
        {
            if (cldBackColor.ShowDialog() != DialogResult.OK)
                return;

            ptbImage.BackColor = cldBackColor.Color;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ptbImage.Image = null;
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {   // if 문 안에서 함수를 호출하는 코드는 권장되지 않는다. 
            if (ofdOpen.ShowDialog() != DialogResult.OK)
                return;

            //// 하드 디스크에서 직접 원하는 사진을 선택한다. 
            //DialogResult result = ofdOpen.ShowDialog();
            //// 창을 닫아야 실행됨. 어떤 버튼을 눌렀는지는 result 변수에 담겨있다. 

            //if (result != DialogResult.OK)
            //    return;

            ptbImage.Load(ofdOpen.FileName);            
        }
    }
}
