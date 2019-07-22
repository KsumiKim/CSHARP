using DevExpress.XtraEditors;
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

namespace MovieAnalyzer.WinformUI
{
    public partial class MovieList : XtraForm
    {
        public MovieList()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode)
                return;

            movieModelBindingSource.DataSource = DataRepository.Movie.GetModels();
        }
    }
}
