using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI_QuanLyThuVien
{
    public partial class frmWelcome : Form
    {
        private System.Windows.Forms.Timer timer;

        public frmWelcome()
        {
            InitializeComponent();

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            timer = new System.Windows.Forms.Timer(); // dùng đầy đủ namespace
            timer.Interval = 30;
            timer.Tick += (s, e) =>
            {
                progressBar1.Value += 2;
                if (progressBar1.Value >= progressBar1.Maximum)
                    progressBar1.Value = 0;
            };
            timer.Start();

            Task.Delay(3000).ContinueWith(t =>
            {
                if (!this.IsDisposed)
                {
                    this.Invoke(new Action(() =>
                    {
                        timer.Stop();
                        this.Close();
                    }));
                }
            });
        }
    }
}
