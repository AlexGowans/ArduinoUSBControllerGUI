﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;



namespace ControllerGUI {
    public class SaveLoad {

        static ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public static Data Load() {
            Data data = new Data(99); //if 99 doesnt change no file was loaded, controller side looks for this
            if ((string)localSettings.Values["First Run"] == "initialised") {
            
                data.mode = (int)localSettings.Values["Mode"];

                data.upBtn = (ushort)localSettings.Values["upBtn"];
                data.leftBtn = (ushort)localSettings.Values["leftBtn"];
                data.downBtn = (ushort)localSettings.Values["downBtn"];
                data.rightBtn = (ushort)localSettings.Values["rightBtn"];

                data.aBtn = (ushort)localSettings.Values["aBtn"];
                data.bBtn = (ushort)localSettings.Values["bBtn"];
                data.xBtn = (ushort)localSettings.Values["xBtn"];
                data.yBtn = (ushort)localSettings.Values["yBtn"];
                data.lBtn = (ushort)localSettings.Values["lBtn"];
                data.rBtn = (ushort)localSettings.Values["rBtn"];
                data.startBtn = (ushort)localSettings.Values["startBtn"];
                data.selectBtn = (ushort)localSettings.Values["selectBtn"];
            }

            return data;
        }

        public static void Save(Data data) {
            localSettings.Values["First Run"] = "initialised";

            localSettings.Values["Mode"] = data.mode;

            localSettings.Values["upBtn"] = data.upBtn;
            localSettings.Values["leftBtn"] = data.leftBtn;
            localSettings.Values["downBtn"] = data.downBtn;
            localSettings.Values["rightBtn"] = data.rightBtn;

            localSettings.Values["aBtn"] = data.aBtn;
            localSettings.Values["bBtn"] = data.bBtn;
            localSettings.Values["xBtn"] = data.xBtn;
            localSettings.Values["yBtn"] = data.yBtn;
            localSettings.Values["lBtn"] = data.lBtn;
            localSettings.Values["rBtn"] = data.rBtn;
            localSettings.Values["startBtn"] = data.startBtn;
            localSettings.Values["selectBtn"] = data.selectBtn;
        }

    }
}
