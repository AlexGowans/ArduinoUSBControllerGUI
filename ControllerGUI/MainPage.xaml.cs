﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;   //fuck yeah tasks are cool
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ControllerGUI {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {

        private SerialController serialController = new SerialController();    

        public MainPage() {
            this.InitializeComponent();

            RefreshDeviceList();           
        }

        private void RefreshDeviceList() {
            serialController.ListAvailablePorts();   //Get a port
            lstSerialDevices.ItemsSource = serialController.listOfDevices; //Show list in XAML
            lstSerialDevices.SelectedIndex = -1;
        }

        

        #region Buttons
        //Click to initiate
        private void btnConnectToDevice_Click(object sender, RoutedEventArgs e) {
            serialController.SerialPortConfiguration(lstSerialDevices.SelectedItems);
        }

        //Send
        private void btnWrite_Click(object sender, RoutedEventArgs e) {
            serialController.PreparePacketSend(txtSend.Text.ToString());
        }
        #endregion


    }
}
