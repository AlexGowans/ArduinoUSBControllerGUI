using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerGUI {
    [Serializable]
    public class Data {

        public int mode { get; set; }

   
        public ushort upBtn { get; set; }
        public ushort leftBtn { get; set; }
        public ushort downBtn { get; set; }
        public ushort rightBtn { get; set; }

        public ushort aBtn { get; set; }
        public ushort bBtn { get; set; }
        public ushort xBtn { get; set; }
        public ushort yBtn { get; set; }
        public ushort lBtn { get; set; }
        public ushort rBtn { get; set; }
        public ushort startBtn { get; set; }
        public ushort selectBtn { get; set; }


        public Data(int _mode, ushort up = 0x57, ushort left = 0x41, ushort down = 0x53, ushort right = 0x44, ushort a = 0x4C, ushort b = 0x4B, ushort x =  0x49, ushort y = 0x4A, ushort l = 0x55, ushort r = 0x52, ushort strt = 0x48, ushort slct = 0x46) {
            mode = _mode;

            upBtn = up;
            leftBtn = left;
            downBtn = down;
            rightBtn = right;

            aBtn = a;
            bBtn = b;
            xBtn = x;
            yBtn = y;
            lBtn = l;
            rBtn = r;
            startBtn = strt;
            selectBtn = slct;
        }


    }
}
