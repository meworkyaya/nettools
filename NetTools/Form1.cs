using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using NetTools.Properties;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace NetTools
{
    public partial class FormMain : Form
    {
        public string SettingsFile = "data.txt";

        public NetToolsApplication app = null;
        delegate void SetTextCallback(string text);

        public FormMain()
        {
            InitializeComponent();

            app = new NetToolsApplication();
            app.form = this;

            LoadHistory();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnStartUriCall_Click(object sender, EventArgs e)
        {
            string Uri = cbUrl.Text;
            if (String.IsNullOrEmpty(Uri))
            {
                MessageBox.Show("Please fill uri");
                return;
            }
            double Delay = Double.Parse( tbDelay.Text );


            AddUrlToHistoryCallSite( Uri );

            app.CallSite(Uri, Delay);

        }



        public void AddUrlToHistoryCallSite(string Uri)
        {
            int finded = cbUrl.Items.IndexOf(Uri);
            if ( -1 == finded )
            {
                cbUrl.Items.Add(Uri);
            }
        }



        public void LogAdd( string message ){
            if (this.lbLog.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(LogAdd);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                DateTime time = DateTime.Now;
                string date = time.ToString();
                string result = date + " : " + message;

                this.lbLog.Items.Add(result);
                lbLog.TopIndex = lbLog.Items.Count - 1;
            }

            //lbLog.Items.Add( date + " : " + message );
            //lbLog.TopIndex = lbLog.Items.Count - 1;

            //this.Refresh();
            // Application
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            app.CallSiteStop();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveHistory();
        }


        public void SaveHistory()
        {
            string[] items = new string[ cbUrl.Items.Count ];
            var count = 0;
            foreach( string item in cbUrl.Items) {
                items[count++] = item;
            }
            System.IO.File.WriteAllLines(SettingsFile, items );
        }


        public void LoadHistory()
        {
            if (File.Exists(SettingsFile))
            {
                string[] items = System.IO.File.ReadAllLines(SettingsFile);
                foreach (string item in items)
                {
                    cbUrl.Items.Add(item);
                }
            }
        }
    }
}
