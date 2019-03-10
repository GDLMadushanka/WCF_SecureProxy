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

namespace CallProxyApplication
{
    public partial class CallWebServiceForm : Form
    {
        public CallWebServiceForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            echoService.echoPortTypeClient client = new echoService.echoPortTypeClient("echoHttpSoap11Endpoint");
            CallProxyApplication.echoService.echoStringRequest echoStringRequest = new echoService.echoStringRequest();
            CallProxyApplication.echoService.echoStringResponse echoStringResponse = new echoService.echoStringResponse();

            echoStringRequest.@in = textBox1.Text;
            try
            {
                String response = client.echoString(echoStringRequest.@in);
                richTextBox1.Text = response;
            }
            catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            echoSecured.echoSecuredPortTypeClient client = new echoSecured.echoSecuredPortTypeClient("echoSecuredHttpsSoap11Endpoint");
            CallProxyApplication.echoSecured.echoStringRequest echoStringRequest = new echoSecured.echoStringRequest();
            CallProxyApplication.echoSecured.echoStringResponse echoStringResponse = new echoSecured.echoStringResponse();

            client.ClientCredentials.UserName.UserName = "admin";
            client.ClientCredentials.UserName.Password = "admin";

            echoStringRequest.@in = textBox2.Text;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                String response = client.echoString(echoStringRequest.@in);
                richTextBox2.Text = response;
            }
            catch (Exception ex)
            {
                richTextBox2.Text = ex.Message;
            }
        }
    }
}
