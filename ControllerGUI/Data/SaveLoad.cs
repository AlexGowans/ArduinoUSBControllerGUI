 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ControllerGUI {
    public class SaveLoad {

        static string fileName = "KeyBinds.scroll";
        static string FilePath() {
            //Get FilePath for save data  C:\Documents and Settings\%USER NAME%\Application Data\FSNESCon
            string systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string dataFilePath = Path.Combine(systemPath, "FSNESCon");
            return dataFilePath;
        }


        public static void Save(Data data) {
            XmlSerializer xs = new XmlSerializer(typeof(Data));
            using (TextWriter sw = new StreamWriter(FilePath())) {
                xs.Serialize(sw, data);
            }
        }

        public static Data Load() {
            Data loadedData;
            try {
                XmlSerializer xs = new XmlSerializer(typeof(Data));
                using (StreamReader sr = new StreamReader(FilePath())) {
                    loadedData = (Data)xs.Deserialize(sr);                   
                }
            }
            catch(Exception) {   //usually on first run or if file deleted
                loadedData = new Data(99,0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00); //mode 99 as flag
            }
            return loadedData;
        }

    }
}
