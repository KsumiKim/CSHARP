using Database.Group5.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database_Group5_Winform
{
    public partial class Senario2 : Form
    {
        public Senario2()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            int value = DataRepository.Artist.GetPlaylistTotalByArtist(textBox1.Text);

            textBox2.Text = value.ToString();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
