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

        private bool upPressed = false;
        private bool leftPressed = false;
        private bool downPressed = false;
        private bool rightPressed = false;

        private bool onePressed = false;
        private bool twoPressed = false;
        private bool threePressed = false;
        private bool aPressed = false;
        private bool bPressed = false;
        private bool cPressed = false;


        #region Global KeyBinds
        //Default bindings
        private readonly ushort upBtn    = 0x57;    //w
        private readonly ushort leftBtn  = 0x41;    //a
        private readonly ushort downBtn  = 0x53;    //s        
        private readonly ushort rightBtn = 0x44;    //d

        private readonly ushort oneBtn   = 0x55;    //u
        private readonly ushort twoBtn   = 0x49;    //i
        private readonly ushort threeBtn = 0x4F;    //o
        private readonly ushort aBtn = 0x4A;    //j
        private readonly ushort bBtn = 0x4B;    //k
        private readonly ushort cBtn = 0x4C;    //l
        

        public GameController() {   //read from saved file if it exists and overrite defaults

        }
        #endregion

        #region Keys to give to imported input
        public ushort UpKey { get { return upBtn; } }
        public ushort LeftKey { get { return leftBtn; } }
        public ushort DownKey { get { return downBtn; } }
        public ushort RightKey { get { return rightBtn; } }

        public ushort OneKey { get { return oneBtn; } }
        public ushort TwoKey { get { return twoBtn; } }
        public ushort ThreeKey { get { return threeBtn; } }
        public ushort AKey { get { return aBtn; } }
        public ushort BKey { get { return bBtn; } }
        public ushort CKey { get { return cBtn; } }
        #endregion


        #region Press Event (called on packet receive)
        //Up
        public void UpPress() {
            if (!upPressed) {
                upPressed = true;
                //On tap code
            }
            var info = new InjectedInputKeyboardInfo();
            info.VirtualKey = UpKey;
            inputInjector.InjectKeyboardInput(new[] { info });              
        }
        public void UpRelease() {
            if (upPressed) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = UpKey;
                info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                inputInjector.InjectKeyboardInput(new[] { info });
                upPressed = false;
            }
        }
        //Left
        public void LeftPress() {
            if (!leftPressed) {
                leftPressed = true;
                //On tap code
            }
            var info = new InjectedInputKeyboardInfo();
            info.VirtualKey = LeftKey;
            inputInjector.InjectKeyboardInput(new[] { info });
        }
        public void LeftRelease() {
            if (leftPressed) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = LeftKey;
                info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                inputInjector.InjectKeyboardInput(new[] { info });
                leftPressed = false;
            }
        }
        //Down
        public void DownPress() {
            if (!downPressed) {
                downPressed = true;
                //On tap code
            }
            var info = new InjectedInputKeyboardInfo();
            info.VirtualKey = DownKey;
            inputInjector.InjectKeyboardInput(new[] { info });
        }
        public void DownRelease() {
            if (downPressed) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = DownKey;
                info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                inputInjector.InjectKeyboardInput(new[] { info });
                downPressed = false;
            }
        }
        //Right
        public void RightPress() {
            if (!rightPressed) {
                rightPressed = true;
                //On tap code
            }
            var info = new InjectedInputKeyboardInfo();
            info.VirtualKey = RightKey;
            inputInjector.InjectKeyboardInput(new[] { info });
            
        }
        public void RightRelease() {
            if (rightPressed) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = RightKey;
                info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                inputInjector.InjectKeyboardInput(new[] { info });
                rightPressed = false;
            }
        }

        //1
        public void OnePress() {
            if (!onePressed) {
                onePressed = true;
                //On tap code
            }
            var info = new InjectedInputKeyboardInfo();
            info.VirtualKey = OneKey;
            inputInjector.InjectKeyboardInput(new[] { info });
        }
        public void OneRelease() {
            if (onePressed) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = OneKey;
                info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                inputInjector.InjectKeyboardInput(new[] { info });
                onePressed = false;
            }
        }
        //2
        public void TwoPress() {
            if (!twoPressed) {
                twoPressed = true;
                //On tap code
            }
            var info = new InjectedInputKeyboardInfo();
            info.VirtualKey = TwoKey;
            inputInjector.InjectKeyboardInput(new[] { info });
        }
        public void TwoRelease() {
            if (twoPressed) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = TwoKey;
                info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                inputInjector.InjectKeyboardInput(new[] { info });
                twoPressed = false;
            }
        }
        //3
        public void ThreePress() {
            if (!threePressed) {
                threePressed = true;
                //On tap code
            }
            var info = new InjectedInputKeyboardInfo();
            info.VirtualKey = ThreeKey;
            inputInjector.InjectKeyboardInput(new[] { info });
        }
        public void ThreeRelease() {
            if (threePressed) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = ThreeKey;
                info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                inputInjector.InjectKeyboardInput(new[] { info });
                threePressed = false;
            }
        }
        //A
        public void APress() {
            if (!aPressed) {
                aPressed = true;
                //On tap code
            }
            var info = new InjectedInputKeyboardInfo();
            info.VirtualKey = AKey;
            inputInjector.InjectKeyboardInput(new[] { info });

        }
        public void ARelease() {
            if (aPressed) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = AKey;
                info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                inputInjector.InjectKeyboardInput(new[] { info });
                aPressed = false;
            }
        }
        //B
        public void BPress() {
            if (!bPressed) {
                bPressed = true;
                //On tap code
            }
            var info = new InjectedInputKeyboardInfo();
            info.VirtualKey = BKey;
            inputInjector.InjectKeyboardInput(new[] { info });

        }
        public void BRelease() {
            if (bPressed) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = BKey;
                info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                inputInjector.InjectKeyboardInput(new[] { info });
                bPressed = false;
            }
        }
        //C
        public void CPress() {
            if (!cPressed) {
                cPressed = true;
                //On tap code
            }
            var info = new InjectedInputKeyboardInfo();
            info.VirtualKey = CKey;
            inputInjector.InjectKeyboardInput(new[] { info });

        }
        public void CRelease() {
            if (cPressed) {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = CKey;
                info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                inputInjector.InjectKeyboardInput(new[] { info });
                cPressed = false;
            }
        }
        #endregion
    }
}
