﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Clients.OGameClient;

namespace OGameWorker.Views.Main.Fleet
{
    public class FleetViewModel : ReactiveObject
    {
        private readonly OGameHttpClient _client;
        public FleetViewModel() { }

        public FleetViewModel(OGameHttpClient client)
        {
            _client = client;
        }
    }
}
