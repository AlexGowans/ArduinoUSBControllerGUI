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


        public Data(int _mode, ushort up, ushort left, ushort down, ushort right, ushort a, ushort b, ushort x, ushort y, ushort l, ushort r) {
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

        }


    }
}
