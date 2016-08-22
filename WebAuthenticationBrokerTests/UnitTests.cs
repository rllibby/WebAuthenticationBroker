/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sage.WebAuthenticationBroker;
using System.Drawing;

namespace WebAuthenticationBrokerTests
{
    [TestClass]
    public class WebAuthenticationBrokerTests
    {
        #region Private fields

        private const string AzureUri = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize?redirect_uri=urn:ietf:wg:oauth:2.0:oob&client_id=2585caa5-8695-4945-9da7-6d6a73c5b172&response_type=code&scope=offline_access+https%3A%2F%2Fgraph.microsoft.com%2FFiles.ReadWrite+https%3A%2F%2Fgraph.microsoft.com%2FGroup.ReadWrite.All+https%3A%2F%2Fgraph.microsoft.com%2FDirectory.ReadWrite.All+https%3A%2F%2Fgraph.microsoft.com%2FDirectory.AccessAsUser.All";
        private const string AzureRedirectUri = "urn:ietf:wg:oauth:2.0:oob";
        private const string LiveUri = "https://login.live.com/oauth20_authorize.srf?client_id=0000000044199A46&scope=offline_access onedrive.readwrite&response_type=code&redirect_uri=https://login.live.com/oauth20_desktop.srf";
        private const string LiveRedirectUri = "https://login.live.com/oauth20_desktop.srf";
        private const string PayCenterUri = "https://www.sageexchange.com/WebServices/OAuth/Login#/PaymentCenter?client_id=SAGEERPMV4.5000ACCTAAG5USEN&redirect_uri=https:%2F%2Fwww.sageexchange.com%2Fwebservices%2Foauth%2Fauthorize%2F1CBE5AB2-2B4E-4EA5-AD9C-115F82CF9F1C&state=login&gatewayId=619156185137";
        private const string PayCenterRedirectUri = "https://www.sageexchange.com/webservices/oauth/authorize/1CBE5AB2-2B4E-4EA5-AD9C-115F82CF9F1C";

        #endregion

        #region Public methods

        [TestMethod]
        public void TestCustom()
        {
            WebAuthenticationBroker.SetTheme(Color.White, Color.CadetBlue);
            WebAuthenticationBroker.Authenticate(LiveUri, LiveRedirectUri);
        }

        [TestMethod]
        public void TestResetTheme()
        {
            WebAuthenticationBroker.SetDefaults();

            Assert.IsTrue(WebAuthenticationBroker.BackColor == Color.White);
            Assert.IsTrue(WebAuthenticationBroker.ForeColor == Color.Black);
        }

        [TestMethod]
        public void TestLiveAccount()
        {
            WebAuthenticationBroker.SetDefaults();
            WebAuthenticationBroker.Authenticate(WebAuthenticationOptions.None, LiveUri, LiveRedirectUri);
        }

        [TestMethod]
        public void TestPayCenterAccount()
        {
            WebAuthenticationBroker.SetDefaults();
            WebAuthenticationBroker.Height = 630;
            WebAuthenticationBroker.SetTheme(Color.White, Color.Green);

            var auth = WebAuthenticationBroker.Authenticate(WebAuthenticationOptions.UseTitle, PayCenterUri, PayCenterRedirectUri);

            if (auth.ResponseStatus == WebAuthenticationStatus.Success)
            {
                var values = auth.ParseResponse();
            }
        }

        [TestMethod]
        public void TestAzureAd()
        {
            WebAuthenticationBroker.SetDefaults();
            WebAuthenticationBroker.Authenticate(WebAuthenticationOptions.UseTitle, AzureUri, AzureRedirectUri);
        }

        #endregion
    }
}
