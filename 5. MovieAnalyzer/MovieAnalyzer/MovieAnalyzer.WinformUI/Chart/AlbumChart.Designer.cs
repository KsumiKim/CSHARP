namespace MovieAnalyzer.WinformUI.Chart
{
    partial class AlbumChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTotalSum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMovieTitles = new DevExpress.XtraGrid.Columns.GridColumn();
            this.movieModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.movieModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.movieModelBindingSource;
            this.gridControl1.Location = new System.Drawing.Point(76, 58);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(400, 200);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTotalSum,
            this.colMovieTitles});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // colTotalSum
            // 
            this.colTotalSum.FieldName = "TotalSum";
            this.colTotalSum.MinWidth = 25;
            this.colTotalSum.Name = "colTotalSum";
            this.colTotalSum.Visible = true;
            this.colTotalSum.VisibleIndex = 0;
            this.colTotalSum.Width = 94;
            // 
            // colMovieTitles
            // 
            this.colMovieTitles.FieldName = "MovieTitles";
            this.colMovieTitles.MinWidth = 25;
            this.colMovieTitles.Name = "colMovieTitles";
            this.colMovieTitles.Visible = true;
            this.colMovieTitles.VisibleIndex = 1;
            this.colMovieTitles.Width = 94;
            // 
            // movieModelBindingSource
            // 
            this.movieModelBindingSource.DataSource = typeof(MovieAnalyzer.Data.Model.MovieModel);
            // 
            // AlbumChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gridControl1);
            this.Name = "AlbumChart";
            this.Text = "AlbumChart";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.movieModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalSum;
        private DevExpress.XtraGrid.Columns.GridColumn colMovieTitles;
        private System.Windows.Forms.BindingSource movieModelBindingSource;
    }
}