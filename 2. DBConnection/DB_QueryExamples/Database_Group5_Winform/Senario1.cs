using Database.Group5.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Database_Group5_Winform
{
    public partial class Senario1 : Form
    {
        public Senario1()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void TxtKeyword_TextChanged(object sender, EventArgs e)
        {
        }

        private void Button1_MouseClick(object sender, MouseEventArgs e)
        {
            List<string> genreNames = DataRepository.Track.GetTrackGenres(txtKeyword.Text);

            foreach (string genreName in genreNames)
            {
                txtResult.Text += genreName + Environment.NewLine;
            }
        }
    }
}
