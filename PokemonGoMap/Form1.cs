using PokemonGoMap.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonGoMap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timeTimer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Minimized:
                    Visible = false;
                    notifyIcon.Visible = true;
                    break;

                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                default:
                    Visible = true;
                    notifyIcon.Visible = false;
                    break;
            }
        }

        private void tickTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now >= NextDownloadTime)
            {
                NextDownloadTime = DateTime.Now + TimeSpan.FromMinutes(3);
                Task.Run(() =>
                {
                    try
                    {
                        DownloadData.Run();
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex.ToString());
                    }
                });
            }

            if (DateTime.Now >= NextReportTime)
            {
                NextReportTime = DateTime.Now + TimeSpan.FromHours(6);
                Task.Run(() =>
                {
                    try
                    {
                        CreateReport.Run();
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex.ToString());
                    }
                });
            }
        }
        private DateTime NextDownloadTime = DateTime.Now;
        private DateTime NextReportTime = DateTime.Now;
    }
}
