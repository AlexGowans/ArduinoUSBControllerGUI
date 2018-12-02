README:

This gui will read a packet from an arduino based controller and then simulate a keypress.
Good for emulators n ting.

Arduino Project will be in another repo

Gui takes a packet from an arduino or otherwise in the following format.
### 			 3
packetNum		 3
11bitBinaryInputs	11
checksum		 3
\r\n			 2
Total Length:		22

Currently Working On:
User selected key mappings









Sources:

KeyDown Checking:
https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.control.onkeydown#Windows_UI_Xaml_Controls_Control_OnKeyDown_Windows_UI_Xaml_Input_KeyRoutedEventArgs_

3D buttons:
https://social.technet.microsoft.com/wiki/contents/articles/30199.make-a-styled-button-in-xaml-for-universal-windows-apps.aspx

Injecting Input:
https://blog.mzikmund.com/2018/01/injecting-input-in-uwp-apps/
https://docs.microsoft.com/en-us/uwp/api/windows.ui.input.preview.injection

Data Management:
	https://docs.microsoft.com/en-us/windows/uwp/get-started/settings-learning-track
