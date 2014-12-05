using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pc_dcsWatcher
{
    public partial class PcdcsWatcher : Form
    {
        public PcdcsWatcher()
        {
            InitializeComponent();
            notifyIcon.Icon = System.Drawing.SystemIcons.Application;
        }

        private void PcdcsWatcher_Load(object sender, EventArgs e)
        {
            notifyIcon.Visible = true;
        }

        private void PcdcsWatcher_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon.Visible = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
