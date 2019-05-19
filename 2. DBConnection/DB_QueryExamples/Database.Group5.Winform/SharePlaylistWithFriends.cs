using Database.Group5.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Database.Group5.Winform
{
    public partial class SharePlaylistWithFriends : Form
    {
        public SharePlaylistWithFriends()
        {
            InitializeComponent();
        }

        private void SharePlaylistWithFriends_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'chinookDataSet.Track' table. You can move, or remove it, as needed.
            // this.trackTableAdapter.Fill(this.chinookDataSet.Track);


        }

        private void TrackBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void BtnCustomerSearch_Click(object sender, EventArgs e)
        {
            List<Track> tracksByCustomer = DataRepository.Track.
                GetTop5TracksByCustomer(txtFirstName.Text, txtLastName.Text);

            dataGridView1.DataSource = tracksByCustomer;

            txtInformation.Text = txtFirstName.Text + " " + txtLastName.Text +
                "님이 구매하신 곡은 다음과 같습니다.";

        }
    }
}
