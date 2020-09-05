using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatBlazor;


namespace Hsf.ApplicationProcess.August2020.Blazor
{
    public class ToastGenerator
    {
        private readonly IMatToaster _toaster;
        private readonly string _errorMessage = "msg";
        private readonly string _errorHeader = "err";

        public ToastGenerator(IMatToaster toaster)
        {
            _toaster = toaster;
        }

        public void DisplayInfo(string title, string message)
        {
            _toaster.Add(message, MatToastType.Info, title, null, config => config.RequireInteraction = false);
        }

        public void DisplayConnectionError(Action retryAction = null, string alternativeMessage = "")
        {
            _toaster.Add(string.IsNullOrWhiteSpace(alternativeMessage) ? _errorMessage : alternativeMessage,
                MatToastType.Danger, _errorHeader, null, options =>
                {
                    if (retryAction != null)
                    {
                        options.Onclick = toast =>
                        {
                            retryAction.Invoke();
                            return Task.CompletedTask;
                        };
                    }
                });
        }
    }
}
