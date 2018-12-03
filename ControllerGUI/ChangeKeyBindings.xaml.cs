using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ControllerGUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChangeKeyBindings : Page
    {
        Button selectedBinding = new Button(); //store currently selected binding

        public ChangeKeyBindings() {
            this.InitializeComponent();
            FillFromLoad();     //pre fill with existing bindings
        }

        private void btnSave_Click(object sender, RoutedEventArgs e) {  //Save and exit
            btnApply_Click(sender, e);  //save
            btnExit_Click(sender, e);   //exit
        }

        private void FillFromLoad() {
            Data data = SaveLoad.Load();
            upBox.Content = (char)data.upBtn;       //if i dont convert at all OR convert to string, it actually converts to decimal and prints just the number...
            leftBox.Content = (char)data.leftBtn;   //(char) typecast is perfect though
            downBox.Content = (char)data.downBtn;
            rightBox.Content = (char)data.rightBtn;

            aBox.Content = (char)data.aBtn;
            bBox.Content = (char)data.bBtn;
            xBox.Content = (char)data.xBtn;
            yBox.Content = (char)data.yBtn;

            lBox.Content = (char)data.lBtn;
            rBox.Content = (char)data.rBtn;

            startBox.Content = (char)data.startBtn;
            selectBox.Content = (char)data.selectBtn;
        }

        

        #region Buttons
        private void btnApply_Click(object sender, RoutedEventArgs e) { //Save changes but stay
            Data data = SaveLoad.Load(); //Get a copy of already saved data, this way we only overright keybinds, not everything

            data.upBtn = Convert.ToUInt16(upBox.Content);   //ok i think this works, we'll see?            
            data.leftBtn = Convert.ToUInt16(leftBox.Content);
            data.downBtn = Convert.ToUInt16(downBox.Content);
            data.rightBtn = Convert.ToUInt16(rightBox.Content);

            data.aBtn = Convert.ToUInt16(aBox.Content);
            data.bBtn = Convert.ToUInt16(bBox.Content);
            data.xBtn = Convert.ToUInt16(xBox.Content);
            data.yBtn = Convert.ToUInt16(yBox.Content);

            data.lBtn = Convert.ToUInt16(lBox.Content);
            data.rBtn = Convert.ToUInt16(rBox.Content);

            data.startBtn = Convert.ToUInt16(startBox.Content);
            data.selectBtn = Convert.ToUInt16(selectBox.Content);

            SaveLoad.Save(data);    //save it back
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {  //Go back to main page
            this.Frame.Navigate(typeof(MainPage));  
        }

        private void bindingBox_Click(object sender, RoutedEventArgs e) {
            selectedBinding.Background = new SolidColorBrush(Color.FromArgb(255, 93, 88, 88));  //#FF5D5858  //clear previous highlight  
            selectedBinding = (Button)sender;   //I know it's a button calling this
            selectedBinding.Background = new SolidColorBrush(Colors.Red);   //Show pressed
        }


        #endregion

        //Global keydown check from the grid itself
        private void OnKeyDown(object sender, KeyRoutedEventArgs e) {
            selectedBinding.Content = (char)e.Key;  // set the selected binding to the pressed key
            selectedBinding.Background = new SolidColorBrush(Color.FromArgb(255, 93, 88, 88));  //#FF5D5858  //Reset background
            selectedBinding = new Button(); //clear the selected
        }

        
    }
}
