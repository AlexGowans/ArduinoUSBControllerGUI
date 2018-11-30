using System;
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
            DataContext = serialController;
        }

        private async void RefreshDeviceList() {
            serialController.ListAvailablePorts();   //Get a port
            lstSerialDevices.ItemsSource = serialController.listOfDevices; //Show list in XAML
            lstSerialDevices.SelectedIndex = -1;
        }

        

        #region Buttons
        //Click to initiate
        private async void btnConnectToDevice_Click(object sender, RoutedEventArgs e) {
            serialController.SerialPortConfiguration(lstSerialDevices.SelectedItems);
        }

        private void btnRefreshList_Click(object sender, RoutedEventArgs e) {
            RefreshDeviceList();
        }

        //Send
        private async void btnWrite_Click(object sender, RoutedEventArgs e) {
            serialController.PreparePacketSend(txtSend.Text.ToString());
        }
        

        private void btnDCDropdown_Click(object sender, RoutedEventArgs e) {
            if (spDC.Visibility == Visibility.Collapsed)
            {
                spDC.Visibility = Visibility.Visible;
                btnDCDropdown.Content = "v";
            }
            else if (spDC.Visibility == Visibility.Visible)
            {
                spDC.Visibility = Visibility.Collapsed;
                btnDCDropdown.Content = ">";
            }
        }

        private void btnGVDropdown_Click(object sender, RoutedEventArgs e) {
            if (spGV.Visibility == Visibility.Collapsed)
            {
                spGV.Visibility = Visibility.Visible;
                btnGVDropdown.Content = "v";
            }
            else if (spGV.Visibility == Visibility.Visible)
            {
                spGV.Visibility = Visibility.Collapsed;
                btnGVDropdown.Content = ">";
            }
        }

        private void btnDVDropdown_Click(object sender, RoutedEventArgs e)
        {
            if(spDV.Visibility == Visibility.Collapsed) {
                spDV.Visibility = Visibility.Visible;
                btnDVDropdown.Content = "v";
            }
            else if (spDV.Visibility == Visibility.Visible)
            {
                spDV.Visibility = Visibility.Collapsed;
                btnDVDropdown.Content = ">";
            }
        }
        
        private void btnSDropdown_Click(object sender, RoutedEventArgs e) {
            if (spS.Visibility == Visibility.Collapsed)
            {
                spS.Visibility = Visibility.Visible;
                btnSDropdown.Content = "v";
            }
            else if (spS.Visibility == Visibility.Visible)
            {
                spS.Visibility = Visibility.Collapsed;
                btnSDropdown.Content = ">";
            }
        }
        #endregion
    }
}
