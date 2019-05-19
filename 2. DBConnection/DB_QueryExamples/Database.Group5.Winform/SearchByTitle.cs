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

namespace Database.Group5.Winform
{
    public partial class SearchByTitle : Form
    {
        private const double TotalPrice = 0.99;

        public SearchByTitle()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            List<string> playlists = DataRepository.Track.GetTrack(txtKeyword.Text);

            foreach (string playlist in playlists)
            {
                checkedListBox1.Items.Add(playlist);
            }

            int totalNoOfTracks = DataRepository.Track.GetTotalNoOfTracks(txtKeyword.Text);
            
            if (totalNoOfTracks > 0)
            {
                MessageBox.Show("총 " + totalNoOfTracks + "개의 곡이 검색되었습니다.",
                "검색된 곡을 확인하세요", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            // 검색어가 이름에 포함된 고객(들)이 구매한 곡의 총합 + 내역 
            //Tuple<int, List<Track>> countNTracks = DataRepository.Album.
            //getTracksByCustomerName(txtboxKeyword.Text);

            //lblCustomerCount.Text = "" + countNTracks.Item1;          // 키워드에 해당하는 고객의 수 
            //lblTrackCount.Text = "" + countNTracks.Item2.Count;       // 해당 고객이 구매한 곡의 총 합
            //trackBindingSource2.DataSource = countNTracks.Item2;      // 트랙 (중에서도 원하는 테이블만 datagridview에 가져옴)


        }

        private void CheckedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // 어떤 것들이 체크된지 확인한다. 
            decimal totalPrice = (decimal)TotalPrice;
            // int totalPlaytime = 0;

            for (int i = 0; i < checkedListBox1.CheckedItems.Count; ++i)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    // 체크된 곡들의 합계 구하기
                    string checkedTracks = (string)checkedListBox1.Items[i];
                    decimal price = DataRepository.Track.GetTotalPrice(checkedTracks);
                    totalPrice += price;

                    // 체크된 곡들의 총 재생시간 구하기 - for문 별도로 만들기
                    //int playtime = DataRepository.Track.GetTrackTime(checkedTracks);
                    //totalPlaytime +=
                    //int.Parse(TimeSpan.FromMilliseconds(playtime).TotalMinutes.ToString("F0"));

                }
                else break;                
            }
            txtTotal.Text = totalPrice.ToString();

            //txtTotalPlayTime.Text = totalPlaytime.ToString();
        }
    }
}
