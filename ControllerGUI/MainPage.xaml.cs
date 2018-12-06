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
            DataContext = serialController;     //the serial controller has bindings for alot of our xaml
        }

        private async void RefreshDeviceList() {     //Refresh your devices
            serialController.ListAvailablePorts();   //Get a port
            lstSerialDevices.ItemsSource = serialController.listOfDevices; //Show list in XAML
            lstSerialDevices.SelectedIndex = -1;
        }

        

        #region Buttons
        //Click to initiate
        private async void btnConnectToDevice_Click(object sender, RoutedEventArgs e) { //async to help memory
            serialController.SerialPortConfiguration(lstSerialDevices.SelectedItems);
        }

        private void btnRefreshList_Click(object sender, RoutedEventArgs e) {
            RefreshDeviceList();
        }

        //Send
        private async void btnWrite_Click(object sender, RoutedEventArgs e) {
            //Get Colour
            int r = 255;
            int g = 255;
            int b = 255;
            string rs = r.ToString();
            while(rs.Length < 3) {
                rs = "0" + rs;
            }
            string gs = g.ToString();
            while (gs.Length < 3)
            {
                gs = "0" + gs;
            }
            string bs = b.ToString();
            while (bs.Length < 3)
            {
                bs = "0" + bs;
            }
            serialController.PreparePacketSend(rs.ToString()+gs.ToString()+bs.ToString());
        }
        
        //Open/Close dropdown menus
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
        
        //Go to bindings page
        private void btnChangeBindings_Click(object sender, RoutedEventArgs e) {
            //if(serialController != null) {
            //    serialController.Disconnect();
            //}
            this.Frame.Navigate(typeof(ChangeKeyBindings));
        }
        #endregion

        private void myColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {

        }
    }
}
