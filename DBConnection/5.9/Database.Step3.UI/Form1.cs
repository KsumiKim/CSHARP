using Database.Step3.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database.Step3.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int artistId = 10;
            string title = txtTitle.Text;

            List<Album> albums = DataRepository.Album.Search(artistId, title);

            int count = 0;
            int sum = 0;

            foreach (Album album in albums)
            {
                count++;
                sum += album.AlbumId;
            }

            int average = sum / count;

            lblAverage.Text = average.ToString();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            int artistId = 10;
            string title = txtTitle.Text;

            List<Album> albums = DataRepository.Album.Search(artistId, title);

            bdsAlbum.DataSource = albums;
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            //albumData.Insert("new title", 1);

            Album album = new Album();
            album.Title = txtTitle.Text;
            album.ArtistId = 1;

            DataRepository.Album.Insert(album);
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            Album album = DataRepository.Album.GetByPK(AlbumId);
            album.Title = txtTitle.Text;

            //albumData.Update(10, "updated title", 2);
            DataRepository.Album.Update(album);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DataRepository.Album.DeleteByPK(AlbumId);
        }

        private int AlbumId
        {
            get
            {
                return int.Parse(txtAlbumId.Text);
            }
        }
    }
}
