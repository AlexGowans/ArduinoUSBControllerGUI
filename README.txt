README:

This gui will read a packet from an arduino based controller and then simulate a keypress.
Good for emulators n ting.

USE:
Mainpage initiates a serialController and connects to the device when prompted
This serialController is also the dataContext for the XAML bindings.
Serial controller reads packets from the arduino (see below) in order to give commands to its own game controller class
the game controller uses injected input to simulate key presses


PACKET INFO:
Gui takes a packet from an arduino or otherwise in the following format.
### 			 3
packetNum		 3
12bitBinaryInputs	12
checksum		 3
\r\n			 4
Total Length:		25
This packet is sent constantly at an ideal rate of:
40packets/S
25mS/packet
1000bytes/S

GUI to Arduino Packet for RGB.
###			 3
R - 000-255		 3
G - 000-255		 3
B - 000-255		 3
checksum		 3
\r\n			 4
Total Length		19
This packet is sent only when a change is made on the colour slider in the GUI settings








Sources:

Colours:
https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.media.solidcolorbrush

KeyDown Checking:
https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.control.onkeydown#Windows_UI_Xaml_Controls_Control_OnKeyDown_Windows_UI_Xaml_Input_KeyRoutedEventArgs_

3D buttons:
https://social.technet.microsoft.com/wiki/contents/articles/30199.make-a-styled-button-in-xaml-for-universal-windows-apps.aspx

Injecting Input:
https://blog.mzikmund.com/2018/01/injecting-input-in-uwp-apps/
https://docs.microsoft.com/en-us/uwp/api/windows.ui.input.preview.injection

Data Management:
	https://docs.microsoft.com/en-us/windows/uwp/get-started/settings-learning-track
