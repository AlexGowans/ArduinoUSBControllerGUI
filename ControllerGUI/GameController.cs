using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input.Preview.Injection; //Lets us simulate key input



/*
 * The goal here is to load from a database and store the save keybinds 
 */
namespace ControllerGUI {
    class GameController {

        InputInjector inputInjector = InputInjector.TryCreate();

        private int mode = 0; //0: keyboard mapping || 1: Gamepad mapping

        private bool upPressed = false;
        private bool leftPressed = false;
        private bool downPressed = false;
        private bool rightPressed = false;

        private bool aPressed = false;
        private bool bPressed = false;
        private bool xPressed = false;
        private bool yPressed = false;
        private bool lPressed = false;
        private bool rPressed = false;


        #region Global KeyBinds KeyBoardMode
        //Default bindings
        private ushort upBtn    = 0x57;    //w
        private ushort leftBtn  = 0x41;    //a
        private ushort downBtn  = 0x53;    //s        
        private ushort rightBtn = 0x44;    //d

        private ushort aBtn   = 0x55;    //u
        private ushort bBtn   = 0x49;    //i
        private ushort xBtn = 0x4F;    //o
        private ushort yBtn = 0x4A;    //j
        private ushort lBtn = 0x4B;    //k
        private ushort rBtn = 0x4C;    //l
        

        public GameController() {   //read from saved file if it exists and overrite defaults

        }
        #endregion

        #region Keys to give to imported input KEYBOARD MODE
        public ushort UpKey { get { return upBtn; } }
        public ushort LeftKey { get { return leftBtn; } }
        public ushort DownKey { get { return downBtn; } }
        public ushort RightKey { get { return rightBtn; } }

        public ushort AKey { get { return aBtn; } }
        public ushort BKey { get { return bBtn; } }
        public ushort XKey { get { return xBtn; } }
        public ushort YKey { get { return yBtn; } }
        public ushort LKey { get { return lBtn; } }
        public ushort RKey { get { return rBtn; } }
        #endregion


        #region Press Event (called on packet receive) KEYBOARD MODE
        //Up
        public void UpPress() {
            if (!upPressed) {
                upPressed = true;
                //On tap code
            }
            if (mode == 0) { //Keyboard input
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = UpKey;
                inputInjector.InjectKeyboardInput(new[] { info });
            }
            if (mode == 1) { //Gamepad input
                var info = new InjectedInputGamepadInfo();
                info.LeftThumbstickY = 1.0;
                inputInjector.InjectGamepadInput(info);
            }
        }
        public void UpRelease() {
            if (upPressed) {
                upPressed = false;
                if (mode == 0) { //Keyboard input
                    var info = new InjectedInputKeyboardInfo();
                    info.VirtualKey = UpKey;
                    info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                    inputInjector.InjectKeyboardInput(new[] { info });
                }
                if (mode == 1) { //Gamepad input
                    var info = new InjectedInputGamepadInfo();
                    info.LeftThumbstickY = 0.0;
                    inputInjector.InjectGamepadInput(info);
                }
            }
        }
        //Left
        public void LeftPress() {
            if (!leftPressed) {
                leftPressed = true;
                //On tap code
            }
            if (mode == 0) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = LeftKey;
                inputInjector.InjectKeyboardInput(new[] { info });
            }
            if (mode == 1) { //Gamepad input
                var info = new InjectedInputGamepadInfo();
                info.LeftThumbstickX = -1.0;
                inputInjector.InjectGamepadInput(info);
            }
        }
        public void LeftRelease() {
            if (leftPressed) {
                leftPressed = false;
                if (mode == 0) {
                    var info = new InjectedInputKeyboardInfo();
                    info.VirtualKey = LeftKey;
                    info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                    inputInjector.InjectKeyboardInput(new[] { info });
                }
                if (mode == 1) { //Gamepad input
                    var info = new InjectedInputGamepadInfo();
                    info.LeftThumbstickX = 0.0;
                    inputInjector.InjectGamepadInput(info);
                }
            }
        }
        //Down
        public void DownPress() {
            if (!downPressed) {
                downPressed = true;
                //On tap code
            }
            if (mode == 0) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = DownKey;
                inputInjector.InjectKeyboardInput(new[] { info });
            }
            if (mode == 1) { //Gamepad input
                var info = new InjectedInputGamepadInfo();
                info.LeftThumbstickY = -1.0;
                inputInjector.InjectGamepadInput(info);
            }
        }
        public void DownRelease() {
            if (downPressed) {
                downPressed = false;
                if (mode == 0) {
                    var info = new InjectedInputKeyboardInfo();
                    info.VirtualKey = DownKey;
                    info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                    inputInjector.InjectKeyboardInput(new[] { info });
                }
                if (mode == 1) { //Gamepad input
                    var info = new InjectedInputGamepadInfo();
                    info.LeftThumbstickY = 0.0;
                    inputInjector.InjectGamepadInput(info);
                }
            }
        }
        //Right
        public void RightPress() {
            if (!rightPressed) {
                rightPressed = true;
                //On tap code
            }
            if (mode == 0) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = RightKey;
                inputInjector.InjectKeyboardInput(new[] { info });
            }
            if (mode == 1) { //Gamepad input
                var info = new InjectedInputGamepadInfo();
                info.LeftThumbstickX = 1.0;
                inputInjector.InjectGamepadInput(info);
            }

        }
        public void RightRelease() {
            if (rightPressed) {
                rightPressed = false;
                if (mode == 0) {
                    var info = new InjectedInputKeyboardInfo();
                    info.VirtualKey = RightKey;
                    info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                    inputInjector.InjectKeyboardInput(new[] { info });
                }
                if (mode == 1) { //Gamepad input
                    var info = new InjectedInputGamepadInfo();
                    info.LeftThumbstickX = 0.0;
                    inputInjector.InjectGamepadInput(info);
                }
            }
        }

        //1
        public void APress() {
            if (!aPressed) {
                aPressed = true;
                //On tap code
            }
            if (mode == 0) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = AKey;
                inputInjector.InjectKeyboardInput(new[] { info });
            }
        }
        public void ARelease() {
            if (aPressed) {
                aPressed = false;
                if (mode == 0) {
                    var info = new InjectedInputKeyboardInfo();
                    info.VirtualKey = AKey;
                    info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                    inputInjector.InjectKeyboardInput(new[] { info });
                }                
            }
        }
        //2
        public void BPress() {
            if (!bPressed) {
                bPressed = true;
                //On tap code
            }
            if (mode == 0) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = BKey;
                inputInjector.InjectKeyboardInput(new[] { info });
            }
        }
        public void BRelease() {
            if (bPressed) {
                bPressed = false;
                if (mode == 0) {
                    var info = new InjectedInputKeyboardInfo();
                    info.VirtualKey = BKey;
                    info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                    inputInjector.InjectKeyboardInput(new[] { info });
                }
            }
        }
        //3
        public void XPress() {
            if (!xPressed) {
                xPressed = true;
                //On tap code
            }
            if (mode == 0) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = XKey;
                inputInjector.InjectKeyboardInput(new[] { info });
            }
        }
        public void XRelease() {
            if (xPressed) {
                xPressed = false;
                if (mode == 0) {
                    var info = new InjectedInputKeyboardInfo();
                    info.VirtualKey = XKey;
                    info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                    inputInjector.InjectKeyboardInput(new[] { info });
                }                
            }
        }
        //A
        public void YPress() {
            if (!yPressed) {
                yPressed = true;
                //On tap code
            }
            if (mode == 0) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = YKey;
                inputInjector.InjectKeyboardInput(new[] { info });
            }    
        }
        public void YRelease() {
            if (yPressed) {
                yPressed = false;
                if (mode == 0) {
                    var info = new InjectedInputKeyboardInfo();
                    info.VirtualKey = YKey;
                    info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                    inputInjector.InjectKeyboardInput(new[] { info });
                }                
            }
        }
        //B
        public void LPress() {
            if (!lPressed) {
                lPressed = true;
                //On tap code
            }
            if (mode == 0) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = LKey;
                inputInjector.InjectKeyboardInput(new[] { info });
            }

        }
        public void LRelease() {
            if (lPressed) {
                lPressed = false;
                if (mode == 0) {
                    var info = new InjectedInputKeyboardInfo();
                    info.VirtualKey = LKey;
                    info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                    inputInjector.InjectKeyboardInput(new[] { info });
                }                
            }
        }
        //C
        public void RPress() {
            if (!rPressed) {
                rPressed = true;
                //On tap code
            }
            if (mode == 0) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = RKey;
                inputInjector.InjectKeyboardInput(new[] { info });
            }

        }
        public void RRelease() {
            if (rPressed) {
                rPressed = false;
                if (mode == 0) {
                    var info = new InjectedInputKeyboardInfo();
                    info.VirtualKey = RKey;
                    info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                    inputInjector.InjectKeyboardInput(new[] { info });
                }                
            }
        }
        #endregion
    }
}
