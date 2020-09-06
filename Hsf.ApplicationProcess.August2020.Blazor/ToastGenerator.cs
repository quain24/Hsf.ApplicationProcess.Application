using MatBlazor;
using System;
using System.Linq;
using System.Threading.Tasks;
using Hsf.ApplicationProcess.August2020.Blazor.ApiServices;

namespace Hsf.ApplicationProcess.August2020.Blazor
{
    public class ToastGenerator
    {
        private readonly IMatToaster _toaster;

        public ToastGenerator(IMatToaster toaster)
        {
            _toaster = toaster;
        }

        public void DisplayInfo(string title, string message = "")
        {
            _toaster.Add(message, MatToastType.Success, title, null, config => config.RequireInteraction = false);
        }

        public void DisplayConnectionError(string title, string message = "", Action retryAction = null)
        {
            _toaster.Add(message, MatToastType.Danger, title, null, options =>
                {
                    if (retryAction != null)
                    {
                        options.ShowCloseButton = false;
                        options.Onclick = toast =>
                        {
                            retryAction.Invoke();
                            return Task.CompletedTask;
                        };
                    }
                });
        }

        public void DisplayPostInfoErrors(string title, string messageHeader, ApiInfo info)
        {
            var type = MatToastType.Danger;
            var message = string.Empty;

            var infos = info.ResponseCodes.GetCodes();

            foreach (var infoList in infos)
            {
                var tmp = infoList.Aggregate((s, next) => s + ", " + next);
                message += tmp + " ";
            }

            _toaster.Add(title + " " + message, type, messageHeader, null, configure => configure.ShowCloseButton = false);

        }
    }
}