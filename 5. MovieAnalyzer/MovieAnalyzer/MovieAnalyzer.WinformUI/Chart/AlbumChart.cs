using MovieAnalyzer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieAnalyzer.WinformUI.Chart
{
    public partial class AlbumChart : Form
    {
        public AlbumChart()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode)
                return;

            List<int> movieIds = DataRepository.Movie.GetOnlyById();

            Dictionary<string, long> movieSalesTotal = new Dictionary<string, long>();

            for (int i = 0; i < movieIds.Count; i++)
            {
                var movieNames = DataRepository.Movie.GetMovieNames(movieIds[i]);
                var movieSales = DataRepository.Movie.GetAccumulatedSales(movieIds[i]);
                movieSalesTotal.Add(movieNames, movieSales);
            }

            movieModelBindingSource.DataSource =
                (from d in movieSalesTotal orderby d.Value select new { d.Key, d.Value }).ToList();

        }
    }
}
