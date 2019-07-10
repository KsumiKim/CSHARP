#region
using System;
using System.Windows.Forms;

#endregion

namespace DevexpressKiller
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode)
                return;

            Killer.Instance.ShutDown += Killer_ShutDown;
            Killer.Instance.Start(interval:300);
        }

        private void Killer_ShutDown(object sender, Killer.ShutDownEventArgs e)
        {
            Text = e.Count.ToString("N0");
        }
    }
}