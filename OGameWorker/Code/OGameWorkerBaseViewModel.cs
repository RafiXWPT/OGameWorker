using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Exceptions;

namespace OGameWorker.Code
{
    public abstract class OGameWorkerBaseViewModel : ReactiveObject
    {
        protected readonly OGameHttpClient Client;

        protected OGameWorkerBaseViewModel() { }

        protected OGameWorkerBaseViewModel(OGameHttpClient client)
        {
            Client = client;
        }

        public async Task SafeHttpTask(Task action)
        {
            HANDLE:
            try
            {
                await action;
            }
            catch (OgameWorkerLoggedOutException)
            {
                await Client.ReLogIn();
                goto HANDLE;
            }
        }

        public void RunOnDispatcher(Action lambda)
        {
            Application.Current.Dispatcher.Invoke(lambda);
        }
    }
}
