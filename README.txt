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

Injecting Input:
https://blog.mzikmund.com/2018/01/injecting-input-in-uwp-apps/
https://docs.microsoft.com/en-us/uwp/api/windows.ui.input.preview.injection

Data Management:
	Filepath
	https://stackoverflow.com/questions/11967302/c-sharp-file-handling-create-file-in-directory-where-executable-exists
	Binary Serialization
	https://www.youtube.com/watch?v=sWWZZByVvlU
