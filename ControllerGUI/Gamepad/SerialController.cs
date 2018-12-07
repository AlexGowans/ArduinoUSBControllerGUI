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
using System.ComponentModel;


namespace ControllerGUI {
    class SerialController : INotifyPropertyChanged {

        //MainPage mw = Application.Current.Resources.OfType<MainPage>().FirstOrDefault();

        private SerialDevice serialPort = null;     //Our port/device

        DataWriter dataWriterObject = null;         //So we can write
        DataReader dataReaderObject = null;         //So we can read

        public ObservableCollection<DeviceInformation> listOfDevices;      //Our device list

        private CancellationTokenSource readCancellationTokenSource;      //Cancelation Token

        string received = "";
        private UInt16 upBtn;
        private UInt16 leftBtn;
        private UInt16 downBtn;
        private UInt16 rightBtn;
        private UInt16 aBtn;
        private UInt16 bBtn;
        private UInt16 xBtn;
        private UInt16 yBtn;
        private UInt16 lBtn;
        private UInt16 rBtn;
        private UInt16 startBtn;
        private UInt16 selectBtn;

        private Int32 recChkSum;

        private GameController gameController = new GameController();

        #region XAML BINDINGS
        private string _txtMessage = "No Device connected";
        public string txtMessage {
            get { return _txtMessage; }
            set {
                _txtMessage = value;
                OnPropertyChanged("txtMessage");
            }
        }
        private string _txtReceived;
        public string txtReceived {
            get { return _txtReceived; }
            set {
                _txtReceived = value;
                OnPropertyChanged("txtReceived");
            }
        }
        private string _txtCalChkSum = "Null";
        public string txtCalChkSum {
            get { return _txtCalChkSum; }
            set {
                _txtCalChkSum = value;
                OnPropertyChanged("txtCalChkSum");
            }
        }
        private string _txtChkSum = "Null";
        public string txtChkSum {
            get { return _txtChkSum; }
            set {
                _txtChkSum = value;
                OnPropertyChanged("txtChkSum");
            }
        }
        private string _txtPacketNum = "Null";
        public string txtPacketNum {
            get { return _txtPacketNum; }
            set {
                _txtPacketNum = value;
                OnPropertyChanged("txtPacketNum");
            }
        }
        private string _txtBinOut = "Null";
        public string txtBinOut {
            get { return _txtBinOut; }
            set {
                _txtBinOut = value;
                OnPropertyChanged("txtBinOut");
            }
        }
        private string _txtErrorMsg = "All clear";
        public string txtErrorMsg {
            get { return _txtErrorMsg; }
            set {
                _txtErrorMsg = value;
                OnPropertyChanged("txtErrorMsg");
            }
        }

        private string _txtUpBtn;
        public string txtUpBtn {
            get { return _txtUpBtn; }
            set {
                _txtUpBtn = value;
                OnPropertyChanged("txtUpBtn");
            }
        }
        private string _txtLeftBtn;
        public string txtLeftBtn {
            get { return _txtLeftBtn; }
            set {
                _txtLeftBtn = value;
                OnPropertyChanged("txtLeftBtn");
            }
        }
        private string _txtDownBtn;
        public string txtDownBtn {
            get { return _txtDownBtn; }
            set {
                _txtDownBtn = value;
                OnPropertyChanged("txtDownBtn");
            }
        }
        private string _txtRightBtn;
        public string txtRightBtn {
            get { return _txtRightBtn; }
            set {
                _txtRightBtn = value;
                OnPropertyChanged("txtRightBtn");
            }
        }

        private string _txtABtn;
        public string txtABtn {
            get { return _txtABtn; }
            set {
                _txtABtn = value;
                OnPropertyChanged("txtABtn");
            }
        }
        private string _txtBBtn;
        public string txtBBtn {
            get { return _txtBBtn; }
            set {
                _txtBBtn = value;
                OnPropertyChanged("txtBBtn");
            }
        }
        private string _txtXBtn;
        public string txtXBtn {
            get { return _txtXBtn; }
            set {
                _txtXBtn = value;
                OnPropertyChanged("txtXBtn");
            }
        }
        private string _txtYBtn;
        public string txtYBtn {
            get { return _txtYBtn; }
            set {
                _txtYBtn = value;
                OnPropertyChanged("txtYBtn");
            }
        }
        private string _txtLBtn;
        public string txtLBtn {
            get { return _txtLBtn; }
            set {
                _txtLBtn = value;
                OnPropertyChanged("txtLBtn");
            }
        }
        private string _txtRBtn;
        public string txtRBtn {
            get { return _txtRBtn; }
            set {
                _txtRBtn = value;
                OnPropertyChanged("txtRBtn");
            }
        }
        private string _txtStartBtn;
        public string txtStartBtn {
            get { return _txtStartBtn; }
            set {
                _txtStartBtn = value;
                OnPropertyChanged("txtStartBtn");
            }
        }
        private string _txtSelectBtn;
        public string txtSelectBtn {
            get { return _txtSelectBtn; }
            set {
                _txtSelectBtn = value;
                OnPropertyChanged("txtSelectBtn");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        #region FIND DEVICES
        public async void ListAvailablePorts() {
            listOfDevices = new ObservableCollection<DeviceInformation>();  //Prepare our list
            try {   //I love try catch
                string aqs = SerialDevice.GetDeviceSelector();
                var dis = await DeviceInformation.FindAllAsync(aqs);    //wait until done] get all the devices

                for (int i = 0; i < dis.Count; i++) {        //This is a for loop
                    listOfDevices.Add(dis[i]);      //Add them to our list 1 by 1 (so we dont have to await to fetch it each time?)
                }

                
            }
            catch (Exception ex) {
                txtMessage = ex.Message;       //Dont message your ex, bad idea
            }
        }
        #endregion

        #region CONNECT AND LISTEN
        //Serial Config
        public async void SerialPortConfiguration(IList<Object> selection) {
            //Nothing selected
            if (selection.Count <= 0) {
                txtMessage = "You forgot to pick a device";
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
                txtMessage = "Serial port correctly configured!";      //Report success to user

                readCancellationTokenSource = new CancellationTokenSource();        //cancellation token

                Listen();   //begin listening
            }
            catch (Exception ex) {
                txtMessage = ex.Message;   //Shows your error // fitting?
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
                txtMessage = ex.Message;

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
                                for (int i = 3; i < 18; i++) {
                                    calChkSum += (byte)received[i];
                                }
                                txtCalChkSum = Convert.ToString(calChkSum);

                                recChkSum = Convert.ToInt32(received.Substring(18, 3));
                                calChkSum %= 1000;
                                #endregion

                                #region Packet is Legit
                                if (recChkSum == calChkSum) {
                                    #region Display Raw Data : Debuging
                                    txtReceived = received + txtReceived;
                                    //###,packetId,BinaryInputs                               
                                    txtPacketNum = received.Substring(3, 3);   // 3 is where packet num start and it is 3 long] 0,1,2 are ###
                                    txtBinOut = received.Substring(6, 12);     //11 bit binary inputs
                                    txtChkSum = received.Substring(18, 3);     //Check Sum
                                    #endregion


                                    #region Get Input States
                                    upBtn = Convert.ToUInt16(received.Substring(6, 1));
                                    leftBtn = Convert.ToUInt16(received.Substring(7, 1));
                                    downBtn = Convert.ToUInt16(received.Substring(8, 1));
                                    rightBtn = Convert.ToUInt16(received.Substring(9, 1));

                                    aBtn = Convert.ToUInt16(received.Substring(10, 1));
                                    bBtn = Convert.ToUInt16(received.Substring(11, 1));
                                    xBtn = Convert.ToUInt16(received.Substring(12, 1));
                                    yBtn = Convert.ToUInt16(received.Substring(13, 1));
                                    lBtn = Convert.ToUInt16(received.Substring(14, 1));
                                    rBtn = Convert.ToUInt16(received.Substring(15, 1));
                                    startBtn = Convert.ToUInt16(received.Substring(16, 1));
                                    selectBtn = Convert.ToUInt16(received.Substring(17, 1));
                                    //theres one more not used yet
                                    #endregion

                                    #region Display Input States for user
                                    txtUpBtn = Convert.ToString(upBtn);
                                    txtLeftBtn = Convert.ToString(leftBtn);
                                    txtDownBtn = Convert.ToString(downBtn);
                                    txtRightBtn = Convert.ToString(rightBtn);
                                    txtABtn = Convert.ToString(aBtn);
                                    txtBBtn = Convert.ToString(bBtn);
                                    txtXBtn = Convert.ToString(xBtn);
                                    txtYBtn = Convert.ToString(yBtn);
                                    txtLBtn = Convert.ToString(lBtn);
                                    txtRBtn = Convert.ToString(rBtn);
                                    txtStartBtn = Convert.ToString(startBtn);
                                    txtSelectBtn = Convert.ToString(selectBtn);
                                    #endregion

                                    #region Activate Keys
                                    if (upBtn == 1) gameController.UpPress();
                                    else gameController.UpRelease(); //simulate release  
                                    if (leftBtn == 1) gameController.LeftPress();
                                    else gameController.LeftRelease(); //simulate release
                                    if (downBtn == 1) gameController.DownPress();
                                    else gameController.DownRelease(); //simulate release 
                                    if (rightBtn == 1) gameController.RightPress();
                                    else gameController.RightRelease(); //simulate release 

                                    if (aBtn == 1) gameController.APress();
                                    else gameController.ARelease(); //simulate release 
                                    if (bBtn == 1) gameController.BPress();
                                    else gameController.BRelease(); //simulate release 
                                    if (xBtn == 1) gameController.XPress();
                                    else gameController.XRelease(); //simulate release 
                                    if (yBtn == 1) gameController.YPress();
                                    else gameController.YRelease(); //simulate release 
                                    if (lBtn == 1) gameController.LPress();
                                    else gameController.LRelease(); //simulate release 
                                    if (rBtn == 1) gameController.RPress();
                                    else gameController.RRelease(); //simulate release
                                    if (startBtn == 1) gameController.StartPress();
                                    else gameController.StartRelease(); //simulate release
                                    if (selectBtn == 1) gameController.SelectPress();
                                    else gameController.SelectRelease(); //simulate release 
                                    #endregion
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
        public async void PreparePacketSend(string dataPacket) {
            if (serialPort != null) {    //dont send if nothing to send to
                dataPacket = "###" + dataPacket;
                int checkSum = 0;
                for (int i = 3; i < 12; i++) {
                     checkSum += (byte)dataPacket[i];//calculate check sum
                }
                checkSum %= 1000;
                dataPacket = dataPacket + checkSum.ToString() + @"\r\n";
                dataWriterObject = new DataWriter(serialPort.OutputStream);
                await SendPacket(dataPacket);

                if (dataWriterObject != null) {     //Clear the datawriter
                    dataWriterObject.DetachStream();
                    dataWriterObject = null;
                }
            }
        }

        private async Task SendPacket(string dataPacket) {
            Task<UInt32> storeAsyncTask;

            if (dataPacket.Length != 0) {
                dataWriterObject.WriteString(dataPacket);

                storeAsyncTask = dataWriterObject.StoreAsync().AsTask();

                UInt32 bytesWritten = await storeAsyncTask;
                if (bytesWritten > 0) {
                    txtMessage = "Value sent correctly  " + dataPacket;
                }
            }
            else {
                txtMessage = "No value sent yo";
            }
        }
        #endregion

        public async void Disconnect() {
            serialPort.Dispose();
        }
    }
}
