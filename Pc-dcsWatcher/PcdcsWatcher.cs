using DxpSimpleAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pc_dcsWatcher
{
    public partial class PcdcsWatcher : Form
    {
        DxpSimpleClass opc = new DxpSimpleClass();
        int pCheckCounter = 0;

        public PcdcsWatcher()
        {
            InitializeComponent();
            notifyIcon.Icon = System.Drawing.SystemIcons.Application;
        }

        private void PcdcsWatcher_Load(object sender, EventArgs e)
        {
            notifyIcon.Visible = true;

            bool connected = false; 
            do {
                connected = opc.Connect("localhost", "Takebishi.Dxp");
                //if ( connected == false) {
                //    MessageBox.Show("The connection to OPC server failed.", "OPC Connection Error", 
                //            MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                Thread.Sleep(10000);    // wait for a while;
            } while (connected == false);
        }

        private void PcdcsWatcher_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon.Visible = false;
            opc.Disconnect();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Is it okay to stop monitoring PC-DCS?", "Ending Program", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == System.Windows.Forms.DialogResult.Yes) {
                Close();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("hke").Count() > 0)
            {
                pCheckCounter = (pCheckCounter >= 0) ? pCheckCounter + 1 : 0;
            }
            else
            {
                pCheckCounter = (pCheckCounter <= 0) ? pCheckCounter - 1 : 0;
            }

            if (pCheckCounter > 1)
            {
                string[] target = new string[] { "DEV1.B10A1", };
                object[] val = new object[] { 1 };
                int[] nErrorArray;

                opc.Write(target, val, out nErrorArray);
            }
            if (pCheckCounter < -1) 
            {
                string[] target = new string[] { "DEV1.B10A1", };
                object[] val = new object[] { 0 };
                int[] nErrorArray;

                opc.Write(target, val, out nErrorArray);
            }
        }
    }
}
