using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTools
{
    public class NetToolsApplication
    {
        public FormMain form = null;

        public CallSite call = null;


        public void CallSite( string uri, double Delay )
        {
            call = new CallSite(uri, Delay);
            call.SetVisualControls( form );

            call.StartCalling();
        }

        public void CallSiteStop()
        {
            call.ActionStop();
        }

    }
}
