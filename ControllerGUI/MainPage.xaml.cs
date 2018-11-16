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

        private SerialDevice serialPort = null;     //Our port/device

        DataWriter dataWriterObject = null;         //So we can write
        DataReader dataReaderObject = null;         //So we can read

        private ObservableCollection<DeviceInformation> listOfDevices;      //Our device list

        private CancellationTokenSource readCancellationTokenSource;      //Cancelation Token

        string received = "";
        private UInt16 upBtn;
        private UInt16 leftBtn;
        private UInt16 downBtn;
        private UInt16 rightBtn;
        private UInt16 oneBtn;
        private UInt16 twoBtn;
        private UInt16 threeBtn;
        private UInt16 aBtn;
        private UInt16 bBtn;
        private UInt16 cBtn;

        private Int32 recChkSum;

        private GameController gameController;


        public MainPage() {
            this.InitializeComponent();

            listOfDevices = new ObservableCollection<DeviceInformation>();  //Prepare our list

            ListAvailablePorts();   //Get a port

            gameController = new GameController();
        }

        #region CONNECT AND LISTEN
        //Get every connected device in a list
        private async void ListAvailablePorts() {
            try {   //I love try catch
                string aqs = SerialDevice.GetDeviceSelector();
                var dis = await DeviceInformation.FindAllAsync(aqs);    //wait until done] get all the devices

                for (int i = 0; i < dis.Count; i++) {        //This is a for loop
                    listOfDevices.Add(dis[i]);      //Add them to our list 1 by 1 (so we dont have to await to fetch it each time?)
                }

                lstSerialDevices.ItemsSource = listOfDevices; //Show list in XAML

                lstSerialDevices.SelectedIndex = -1;
            }
            catch (Exception ex) {
                txtMessage.Text = ex.Message;       //Dont message your ex, bad idea
            }
        }

        //Click to initiate
        private void btnConnectToDevice_Click(object sender, RoutedEventArgs e) {
            SerialPortConfiguration();
        }
        //Serial Config
        private async void SerialPortConfiguration() {
            var selection = lstSerialDevices.SelectedItems;
            //Nothing selected
            if (selection.Count <= 0) {
                txtMessage.Text = "You forgot to pick a device";
                return;
            }
            //Somthing selected
            DeviceInformation entry = (DeviceInformation)selection[0]; //only use first selection

            try {   //Try to set up the serial port to 8and1N configuration
                serialPort = await SerialDevice.FromIdAsync(entry.Id);
                serialPort.WriteTimeout = TimeSpan.FromMilliseconds(1000);  //how long to try before giving up
                serialPort.ReadTimeout = TimeSpan.FromMilliseconds(1000);   //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                serialPort.BaudRate = 115200;                   //baud rate, must match device
                serialPort.Parity = SerialParity.None;          //n
                serialPort.StopBits = SerialStopBitCount.One;   //and1
                serialPort.DataBits = 8;                        //8
                serialPort.Handshake = SerialHandshake.None;    //or is this n?
                txtMessage.Text = "Serial port correctly configured!";      //Report success to user

                readCancellationTokenSource = new CancellationTokenSource();        //cancellation token

                Listen();   //begin listening
            }
            catch (Exception ex) {
                txtMessage.Text = ex.Message;   //Shows your error // fitting?
            }
        }
        //Listen
        private async void Listen() {//   !!while(true) inside!!
            try {
                if (serialPort != null) { //Ensure there's actually a device
                    dataReaderObject = new DataReader(serialPort.InputStream); //get ready to read

                    while (true) {
                        await ReadData(readCancellationTokenSource.Token);
                    }
                }
            }
            catch (Exception ex) {
                txtMessage.Text = ex.Message;

                //if (ex.GetType.Name=="TaskCanelledExcption")      //Wayne left this here commented
            }
            finally {

            }
        }
        #endregion

        #region RECEIVE
        //Read Data] Runs continuously in a while(true) loop
        private async Task ReadData(CancellationToken cancellationToken) {
            Task<UInt32> loadAsyncTask;

            int calChkSum = 0;

            uint ReadBufferLength = 1;

            cancellationToken.ThrowIfCancellationRequested();

            dataReaderObject.InputStreamOptions = InputStreamOptions.Partial;

            loadAsyncTask = dataReaderObject.LoadAsync(ReadBufferLength).AsTask(cancellationToken);

            UInt32 bytesRead = await loadAsyncTask;

            if (bytesRead > 0) {
                received += dataReaderObject.ReadString(bytesRead);
                //txtReceived.Text = received + txtReceived.Text;

                if (received[0] == '#') {    //checking the packet follows protocol    ###
                    if (received.Length > 3) {      //Is it a complete packet
                        if (received[2] == '#') {        //Is it still following protocol?
                            if (received.Length > 21) {     //Full length?
                                                                
                                #region Calculate the Check Sum and prepare to compare
                                for (int i = 3; i < 17; i++) {
                                    calChkSum += (byte)received[i];
                                }
                                txtCalChkSum.Text = Convert.ToString(calChkSum);

                                recChkSum = Convert.ToInt32(received.Substring(17, 3));
                                calChkSum %= 1000;
                                #endregion

                                #region Packet is Legit
                                if (recChkSum == calChkSum) {
                                    #region Display Raw Data : Debuging
                                    txtReceived.Text = received + txtReceived.Text;
                                    //###,packetId,BinaryInputs                               
                                    txtPacketNum.Text = received.Substring(3, 3);   // 3 is where packet num start and it is 3 long] 0,1,2 are ###
                                    txtBinOut.Text = received.Substring(6, 11);     //11 bit binary inputs
                                    txtChkSum.Text = received.Substring(17, 3);     //Check Sum
                                    #endregion


                                    #region Get Input States
                                    upBtn = Convert.ToUInt16(received.Substring(6, 1));
                                    leftBtn = Convert.ToUInt16(received.Substring(7, 1));
                                    downBtn = Convert.ToUInt16(received.Substring(8, 1));
                                    rightBtn = Convert.ToUInt16(received.Substring(9, 1));

                                    oneBtn = Convert.ToUInt16(received.Substring(10, 1));
                                    twoBtn = Convert.ToUInt16(received.Substring(11, 1));
                                    threeBtn = Convert.ToUInt16(received.Substring(12, 1));
                                    aBtn = Convert.ToUInt16(received.Substring(13, 1));
                                    bBtn = Convert.ToUInt16(received.Substring(14, 1));
                                    cBtn = Convert.ToUInt16(received.Substring(15, 1));
                                    //theres one more not used yet
                                    #endregion

                                    txtUpBtn.Text = Convert.ToString(upBtn);
                                    txtLeftBtn.Text = Convert.ToString(leftBtn);
                                    txtDownBtn.Text = Convert.ToString(downBtn);
                                    txtRightBtn.Text = Convert.ToString(rightBtn);
                                    txtOneBtn.Text = Convert.ToString(oneBtn);
                                    txtTwoBtn.Text = Convert.ToString(twoBtn);
                                    txtThreeBtn.Text = Convert.ToString(threeBtn);
                                    txtABtn.Text = Convert.ToString(aBtn);
                                    txtBBtn.Text = Convert.ToString(bBtn);
                                    txtCBtn.Text = Convert.ToString(cBtn);
                                }
                                #endregion
                                received = "";//Clear the buffer for the next pass
                            }

                        }
                        else {                          //It's not yo
                            received = "";
                        }
                    }
                }
                else {                    //Otherwise clear the buffer
                    received = "";
                }
            }
        }
        #endregion

        #region SEND
        //Send Data]
        private async void btnWrite_Click(object sender, RoutedEventArgs e) {
            if (serialPort != null) {    //dont send if nothing to send to
                var dataPacket = txtSend.Text.ToString();
                dataWriterObject = new DataWriter(serialPort.OutputStream);
                await SendPacket(dataPacket);

                if (dataWriterObject != null) {     //Clear the datawriter
                    dataWriterObject.DetachStream();
                    dataWriterObject = null;
                }
            }
        }

        private async Task SendPacket(string value) {
            var dataPacket = value;

            Task<UInt32> storeAsyncTask;

            if (dataPacket.Length != 0) {
                dataWriterObject.WriteString(dataPacket);

                storeAsyncTask = dataWriterObject.StoreAsync().AsTask();

                UInt32 bytesWritten = await storeAsyncTask;
                if (bytesWritten > 0) {
                    txtMessage.Text = "Value sent correctly";
                }
            }
            else {
                txtMessage.Text = "No value sent yo";
            }
        }
        #endregion
    }
}
