using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using IMAD = Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace WindowsFormsSample
{
    public partial class Form1 : Form
    {
        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        // The Authority is the sign-in URL of the tenant.

        private string clientId = ConfigurationSettings.AppSettings["ClientId"];
        private string authority = "https://login.windows.net/common/oauth2/authorize";

        // To authenticate to the service, the client needs to know the service's App ID URI.
        // To contact the list service we need it's URL as well.

        private string resourceId = ConfigurationSettings.AppSettings["ResourceId"];
        private string listBaseAddress = ConfigurationSettings.AppSettings["BaseAddress"];

        private HttpClient httpClient = new HttpClient();
        private IMAD.AuthenticationContext authContext = null;
        private UserCredential uc = new UserPasswordCredential("powerbi@markowenjoneshotmail.onmicrosoft.com", "PasswordForAccount");

        private string AccessToken { get; set; }

        //Power BI Reports used to deserialize the Get Reports response.
        public class PBIReports
        {
            public PBIReport[] value { get; set; }
        }
        public class PBIReport
        {
            public string id { get; set; }
            public string name { get; set; }
            public string webUrl { get; set; }
            public string embedUrl { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
            authContext = new IMAD.AuthenticationContext(authority, new FileCache());
        }

        private void getReportsWithPowerBiApi(string aadToken, string baseUri)
        {
            var credentials = new TokenCredentials(aadToken, "Bearer");

            try
            {
                using (var client = new PowerBIClient(new Uri(baseUri), credentials))
                {
                    var reports = client.Reports.GetReports();
                    var powerBiReports = reports.Value.ToList();
                }
            }
            catch(Exception ex)
            {
                //Operation returned an invalid status code 'Forbidden' 
            }
        }

        private string generateEmbedToken(string aadToken, string reportId, string baseUri, string accessLevel = "View", string datasetId = "")
        {
            var credentials = new TokenCredentials(aadToken);

            try
            {
                using (var powerBiClient = new PowerBIClient(credentials))
                {
                    powerBiClient.BaseUri = new Uri(baseUri);

                    var requestParameters = new GenerateTokenRequest(accessLevel, datasetId);

                    
                    EmbedToken token = powerBiClient.Reports.GenerateToken(reportId, requestParameters);

                    return token.Token;
                }
            }
            catch(Exception ex)
            {
                //Operation returned an invalid status code 'Forbidden'
                return string.Empty;
            }
        }

        private void LoadReports_Click(object sender, EventArgs e)
        {
            var baseUri = "https://api.powerbi.com/v1.0/myorg/reports";

            var aadToken = newGetToken();

            //example report ID
            AccessToken = generateEmbedToken(aadToken, "aa0fabeb-f4d0-4d9b-a109-d1db031f455e", baseUri); //forbidden

            System.Net.WebRequest request = System.Net.WebRequest.Create(baseUri) as System.Net.HttpWebRequest;

            request.Method = "GET";
            request.ContentType = "application/json";
            request.ContentLength = 0;

            request.Headers.Add("Authorization", String.Format("Bearer {0}", AccessToken));
            try
            {

                //Get Reports response from request.GetResponse()
                using (var response = request.GetResponse() as System.Net.HttpWebResponse) //403 error
                {
                    //Get reader from response stream
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        //Deserialize JSON string
                        PBIReports Reports = JsonConvert.DeserializeObject<PBIReports>(reader.ReadToEnd());

                        cboxReports.DataSource = null;
                        cboxReports.Items.Clear();

                        List<KeyValuePair<string, string>> menuItems = new List<KeyValuePair<string, string>>();
                        BindingSource bs = new BindingSource(menuItems, null);

                        cboxReports.DataSource = bs;
                        cboxReports.DisplayMember = "Value";
                        cboxReports.ValueMember = "Key";

                        for (int i = 0; i < Reports.value.Length; i++)
                        {
                            menuItems.Add(new KeyValuePair<string, string>(Reports.value[i].embedUrl, Reports.value[i].name));
                            bs.ResetBindings(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Getting 403 forbidden
            }

        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (AccessToken != null)
            {
                var message = "{\"action\":\"loadReport\",\"accessToken\":\"" + AccessToken + "\"}";
                webBrowser1.Document.InvokeScript("postMessage", new object[] { message, "*" });
            }
            else
            {
                MessageBox.Show("AccessToken is not defined.");
            }
        }

        private string newGetToken()
        {
            string token = string.Empty;

            try
            {
                IMAD.AuthenticationResult result = authContext.AcquireTokenAsync(resourceId, clientId, uc).Result;
                token = result.AccessToken;
            }
            catch (AdalException ex)
            {
                // An unexpected error occurred.
                string message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "Error Code: " + ex.ErrorCode + "Inner Exception : " + ex.InnerException.Message;
                }
                MessageBox.Show(message);
            }
            return token;
        }

        private void showReport_Click(object sender, EventArgs e)
        {
            var report = cboxReports.SelectedValue.ToString();
            webBrowser1.DocumentCompleted -= WebBrowser1_DocumentCompleted;
            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            webBrowser1.Navigate(report + "&filterPaneEnabled=false");
        }

    }
}
