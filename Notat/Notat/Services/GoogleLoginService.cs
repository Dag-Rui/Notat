using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Notat.Util;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Notat.Services
{
    public class GoogleLoginService
    {
        DriveService driveService;
        public async void initDriveAsync(Task<string> authCodeTask)
        {

            try
            {
                UserCredential credential;

                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("Notat.Resources.credentials.json"))
                {

                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.

                    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "token.json");

                    Debug.WriteLine("filename " + fileName);

                    ICodeReceiver codeReceiver = new Util.PromptCodeReceiver(authCodeTask);

                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        new string[] { DriveService.Scope.Drive },
                        "user",
                        CancellationToken.None,
                        new FileDataStore(fileName, true),
                        codeReceiver);


                    Debug.WriteLine("Credential file saved to: " + fileName);
                }

                // Create Drive API service.

                driveService = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = Constants.applicationName
                });

            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }


        }

        public bool isAuthenticated()
        {
            return driveService != null;
        }
    }


}
