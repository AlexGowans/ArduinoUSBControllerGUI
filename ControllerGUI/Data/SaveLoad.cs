 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace ControllerGUI {
    public class SaveLoad {

        public static Data Load() {
            Data loadedData = new Data(99); //if 99 doesnt change no file was loaded, controller side looks for this

            return loadedData;
        }

        public static void Save(Data data) {

        }

    }
}
