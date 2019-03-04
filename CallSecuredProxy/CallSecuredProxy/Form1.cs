using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CallSecuredProxy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EchoServiceReference.echoSecuredPortTypeClient client = new EchoServiceReference.echoSecuredPortTypeClient("echoSecuredHttpsSoap11Endpoint");
            client.ClientCredentials.UserName.UserName = "admin";
            client.ClientCredentials.UserName.Password = "admin";

            CallSecuredProxy.EchoServiceReference.echoIntRequest echoIntRequest = new EchoServiceReference.echoIntRequest();
            CallSecuredProxy.EchoServiceReference.echoIntResponse echoIntResponse = new EchoServiceReference.echoIntResponse();
            echoIntRequest.@in = Convert.ToInt32(textBox1.Text);
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                int response = client.echoInt(echoIntRequest.@in);
                richTextBox1.Text = response.ToString();
            }
            catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }
        }
    }
}
