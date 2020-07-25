using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Notat.Util
{
    class PromptCodeReceiver : ICodeReceiver
    {

        Task<string> authCodeTask;

        public PromptCodeReceiver(Task<string> authCodeTask)
        {
            this.authCodeTask = authCodeTask;
        }

        /// <inheritdoc/>
        public string RedirectUri
        {
            get { return GoogleAuthConsts.InstalledAppRedirectUri; }
        }

        /// <inheritdoc/>
        public async Task<AuthorizationCodeResponseUrl> ReceiveCodeAsync(AuthorizationCodeRequestUrl url,
            CancellationToken taskCancellationToken)
        {
            var authorizationUrl = url.Build().AbsoluteUri;

            await Browser.OpenAsync(authorizationUrl, BrowserLaunchMode.SystemPreferred);

            string code = await authCodeTask;

            return new AuthorizationCodeResponseUrl { Code = code };
        }
    }
}
