using Database.Group5.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Database.Group5.Winform
{
    public partial class UpdateTable : Form
    {
        ChinookTables table;
        public UpdateTable()
        {
            InitializeComponent();
            List<ChinookTables> tablesList = new List<ChinookTables>
            {
                ChinookTables.Album,ChinookTables.Artist,ChinookTables.Track
            };
            comboBox1.DataSource = tablesList;
        }
        private void FillDataGridView(int firstIdxOfNavCol)
        {
            dataGridView2.RowCount = 0;
            dataGridView2.ColumnCount = dataGridView1.ColumnCount;
            HideNavCol(dataGridView2, firstIdxOfNavCol, dataGridView2.ColumnCount);

            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            for (int i = 0; i < firstIdxOfNavCol; ++i)
            {
                dataGridView2.Columns[i].Name = dataGridView1.Columns[i].Name;
                row.Cells[i].Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[i].Value;
            }
            dataGridView2.Rows.Add(row);
            foreach (DataGridViewColumn c in dataGridView2.Columns) c.ReadOnly = true;
            dataGridView2.Columns[1].ReadOnly = false;
        }

        private void HideNavCol(DataGridView dataGridView, int firstIdxOfNavCol, int columnCount)
        {
            for (int i = firstIdxOfNavCol; i < dataGridView.ColumnCount; ++i)
                dataGridView.Columns[i].Visible = false;
        }

        //private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (table == ChinookTables.Album)
        //        FillDataGridView(Enum.GetValues(typeof(AlbumColumns)).Length);
        //    else if (table == ChinookTables.Artist)
        //        FillDataGridView(Enum.GetValues(typeof(ArtistColumns)).Length);
        //    else if (table == ChinookTables.Track)
        //        FillDataGridView(Enum.GetValues(typeof(TrackColumns)).Length);
        //}

        // 수정 버튼 
        //private void Button1_Click(object sender, EventArgs e)
        //{
        //    //int pk = Convert.ToInt32(dataGridView2.Rows[0].Cells[0].Value.ToString());

        //    //if (table == ChinookTables.Album)
        //    //{
        //    //    Album album = DataRepository.Album.GetByPK(pk);
        //    //    album.Title = dataGridView2.Rows[0].Cells[AlbumColumns.Title.ToString()].Value.ToString();
        //    //    DataRepository.Album.Update(album);
        //    //}
        //    //else if (table == ChinookTables.Artist)
        //    //{
        //    //    Artist artist = DataRepository.Artist.GetByPK(pk);
        //    //    artist.Name = dataGridView2.Rows[0].Cells[ArtistColumns.Name.ToString()].Value.ToString();
        //    //    DataRepository.Artist.Update(artist);
        //    //}
        //    //else if (table == ChinookTables.Track)
        //    //{
        //    //    Track track = DataRepository.Track.GetByPK(pk);
        //    //    track.Name = dataGridView2.Rows[0].Cells[TrackColumns.Name.ToString()].Value.ToString();
        //    //    DataRepository.Track.Update(track);
        //    //}

        //    //dataGridView2.Columns.Clear();
        //    //ComboBox1_SelectionChangeCommitted_1(null, null);
        //}



        private void UpdateTable_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'chinookDataSet.Album' table. You can move, or remove it, as needed.
            this.albumTableAdapter.Fill(this.chinookDataSet.Album);

        }

        private void ComboBox1_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            table = (ChinookTables)comboBox1.SelectedValue;
            int firstIdxOfNavCol = 0;

            if (table == ChinookTables.Album)
            {
                dataGridView1.DataSource = DataRepository.Album.GetAll();
                firstIdxOfNavCol = Enum.GetValues(typeof(AlbumColumns)).Length;
            }
            else if (table == ChinookTables.Artist)
            {
                dataGridView1.DataSource = DataRepository.Artist.GetAll();
                firstIdxOfNavCol = Enum.GetValues(typeof(ArtistColumns)).Length;

            }
            else if (table == ChinookTables.Track)
            {
                dataGridView1.DataSource = DataRepository.Track.GetAll();
                firstIdxOfNavCol = Enum.GetValues(typeof(TrackColumns)).Length;
            }

            HideNavCol(dataGridView1, firstIdxOfNavCol, dataGridView1.ColumnCount);
        }

        private void DataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (table == ChinookTables.Album)
                FillDataGridView(Enum.GetValues(typeof(AlbumColumns)).Length);
            else if (table == ChinookTables.Artist)
                FillDataGridView(Enum.GetValues(typeof(ArtistColumns)).Length);
            else if (table == ChinookTables.Track)
                FillDataGridView(Enum.GetValues(typeof(TrackColumns)).Length);
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            int pk = Convert.ToInt32(dataGridView2.Rows[0].Cells[0].Value.ToString());

            if (table == ChinookTables.Album)
            {
                Album album = DataRepository.Album.GetByPK(pk);
                album.Title = dataGridView2.Rows[0].Cells[AlbumColumns.Title.ToString()].Value.ToString();
                DataRepository.Album.Update(album);
            }
            else if (table == ChinookTables.Artist)
            {
                Artist artist = DataRepository.Artist.GetByPK(pk);
                artist.Name = dataGridView2.Rows[0].Cells[ArtistColumns.Name.ToString()].Value.ToString();
                DataRepository.Artist.Update(artist);
            }
            else if (table == ChinookTables.Track)
            {
                Track track = DataRepository.Track.GetByPK(pk);
                track.Name = dataGridView2.Rows[0].Cells[TrackColumns.Name.ToString()].Value.ToString();
                DataRepository.Track.Update(track);
            }

            dataGridView2.Columns.Clear();
            ComboBox1_SelectionChangeCommitted_1(null, null);
        }
    }
}
