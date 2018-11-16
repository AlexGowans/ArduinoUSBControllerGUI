using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/*
 * The goal here is to load from a database and store the save keybinds 
 */
namespace ControllerGUI {
    class GameController {

        private readonly char upBtn    = 'w';
        private readonly char leftBtn  = 'a';
        private readonly char downBtn  = 's';        
        private readonly char rightBtn = 'd';

        private readonly char oneBtn   = 'u';
        private readonly char twoBtn   = 'i';
        private readonly char threeBtn = 'o';
        private readonly char aBtn = 'j';
        private readonly char bBtn = 'k';
        private readonly char cBtn = 'l';
        

        public GameController() {   //read from saved file if it exists and overrite defaults

        }


        #region Keys to give to imported input
        public char UpKey { get { return upBtn; } }
        public char LeftKey { get { return downBtn; } }
        public char DownKey { get { return leftBtn; } }
        public char RightKey { get { return rightBtn; } }

        public char OneKey { get { return oneBtn; } }
        public char TwoKey { get { return twoBtn; } }
        public char ThreeKey { get { return threeBtn; } }
        public char AKey { get { return aBtn; } }
        public char BKey { get { return bBtn; } }
        public char CKey { get { return cBtn; } }
        #endregion


        #region Press Event (called on packet receive)
        public void UpPress() {

        }
        #endregion
    }
}
