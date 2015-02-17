using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Timers;

namespace NetTools
{
    public class ToolsBase
    {
        public FormMain form = null;

        public void SetVisualControls( FormMain form ){
            this.form = form;
        }
    }



    public class CallSite  : ToolsBase
    {
        private static Timer aTimer;


        public string Uri { get; set; }
        public double Delay { get; set; }
        public HttpWebRequest Request { get; set; }

        /// <summary>
        /// save history settigns for controls
        /// </summary>
        public void SaveHistorySettings()
        {

        }


        public CallSite()
        {
            this.Uri = null;
            this.Delay = 1000000;
        }

        public CallSite(string Uri, double Delay)
        {
            this.Uri = Uri;
            this.Delay = Delay;
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartCalling()
        {
            if (String.IsNullOrEmpty(Uri))
            {
                throw new Exception("Uri is empty");
            }
            if (Delay <= 0)
            {
                throw new Exception("delay is empty ");
            }

            ActionCalling();

            aTimer = new System.Timers.Timer( Delay * 1000 );
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;

            var test2 = 0;
            return;
        }


        public void ActionCalling()
        {
            WebRequest request = WebRequest.Create(Uri);
            HttpWebResponse response = default(HttpWebResponse);
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                response.Close();

                form.LogAdd( Uri + " : " + response.StatusCode + " : " + response.StatusDescription );

            }
            catch (Exception ex)
            {
                var test = 0;
            }
            finally
            {
                response.Close();
            }
        }


        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            // Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
            ActionCalling();
        }


        public void ActionStop(){
            aTimer.Enabled = false;
            return;
        }



    }
}
