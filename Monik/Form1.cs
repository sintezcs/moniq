using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monik
{
    public partial class Form1 : Form
    {
        protected List<MonitoredDir> MonitoredDirs = new List<MonitoredDir>(); 
        
        public Form1()
        {
            InitializeComponent();
            var tmp = new MonitoredDir() { Path = Path.GetTempPath() };
            MonitoredDirs.Add(tmp);
            RefreshDirs();
            dirsListView.Items.Add(new ListViewItem() { Name = tmp.Path, SubItems = { tmp.Path, tmp.Size.ToString() } });
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }            
        }

        private void settingMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void RefreshDirs()
        {
            foreach (var monitoredDir in MonitoredDirs)
            {
                UpdateDirInfo(monitoredDir);
            }
        }

        private void UpdateDirInfo(MonitoredDir dir)
        {
            dir.Size = Math.Round((double)(GetDirectorySize(dir.Path) / 1000000), 2);
        }

        public static long GetDirectorySize(string parentDirectory)
        {
            return new DirectoryInfo(parentDirectory).GetFiles("*.*", SearchOption.AllDirectories).Sum(file => file.Length);
        }
    }

    public class MonitoredDir
    {
        public string Path { get; set; }

        public MonitoredDir()
        {
            Size = 0;
        }

        public double Size { get; set; }
    }
}
