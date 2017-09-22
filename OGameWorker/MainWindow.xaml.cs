﻿using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HtmlAgilityPack;
using Worker.HttpModule.Clients;
using Worker.HttpModule.RequestBuilder;

namespace OGameWorker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new OGameHttpClient("s147-pl.ogame.gameforge.com");
            var requestBuilder = new OGameWorkerRequestBuilder(client);
            var login = client.SendHttpRequest(requestBuilder.BuildLoginRequest("mail.rafixwpt@gmail.com", "raf109aello"));
            if (login.StatusCode == HttpStatusCode.OK)
            {
                var overviewHtml = client.SendHttpRequest(requestBuilder.BuildOverviewRequest()).ResponseHtmlDocument.Value;
            }
        }
    }
}
