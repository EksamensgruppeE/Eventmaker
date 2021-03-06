﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Eventmaker.ViewModel;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Eventmaker.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventPage : Page
    {

        

        public EventPage()
        {
            this.InitializeComponent();
            
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateEventPage));
        }

        private void Listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void Loop()
        {
            
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateEventPage));
            //popup.IsOpen = !popup.IsOpen;
        }

        private void OnClick_EditEvent(object sender, RoutedEventArgs e)
        {
            if (EditEvent.Visibility == Visibility.Collapsed)
            {
                EditEvent.Visibility = Visibility.Visible;
            }
            else
            {
                EditEvent.Visibility = Visibility.Collapsed;
            }
        }
    }
}
