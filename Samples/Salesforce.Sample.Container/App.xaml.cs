﻿using System;
using Windows.UI.Xaml.Navigation;
using Salesforce.Sample.Container.Settings;
using Salesforce.SDK.Auth;
using Salesforce.SDK.Security;
using Salesforce.SDK.Settings;
using Salesforce.SDK.Strings;
using Salesforce.SDK.App;
using Salesforce.SDK.Core;
using Salesforce.SDK.Logging;
using Salesforce.SDK.Hybrid.Logging;
using System.Threading.Tasks;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=402347&clcid=0x409

namespace Salesforce.Sample.Container
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : SalesforceApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();

            // Inject our own resources into SDK
            LocalizedStrings.SetResourceLocation("MobileSDK");
        }

        /// <summary>
        ///     Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        protected override Task InitializeConfig()
        {
            SDKServiceLocator.RegisterService<IEncryptionService, Encryptor>();
            SDKServiceLocator.RegisterService<ILoggingService, Logger>();
            Encryptor.init(new EncryptionSettings(new HmacSHA256KeyGenerator()));
            var config = SDKManager.InitializeConfigAsync<Config>().Result;
            return config.SaveConfigAsync();
        }

        protected override Type SetRootApplicationPage()
        {
            return typeof(MainPage);
        }
    }
}
