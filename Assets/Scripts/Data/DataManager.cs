using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace PaintRush.Data
{
    public class DataManager
    {
        private static readonly string XDATA_SAVE_FILENAME = "XData";
        public static void SaveGame(XData gameData)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Create(Application.persistentDataPath + "/" + XDATA_SAVE_FILENAME);
            formatter.Serialize(fileStream, gameData);
            fileStream.Close();
            Debug.Log("GameData saved at: " + Application.persistentDataPath + "/" + XDATA_SAVE_FILENAME);
        }

        public static XData LoadGame()
        {
            Debug.Log("Game loaded from: " + Application.persistentDataPath + "/" + XDATA_SAVE_FILENAME);
            if (File.Exists(Application.persistentDataPath + "/" + XDATA_SAVE_FILENAME))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = File.Open(Application.persistentDataPath + "/" + XDATA_SAVE_FILENAME, FileMode.Open);
                XData gameData = (XData)formatter.Deserialize(fileStream);
                fileStream.Close();
                return gameData;
            }
            else
            {
                XData gameData = new XData();

                SaveGame(gameData);

                return gameData;
            }
        }

        public static void PerformDataMigration(XData oldData)
        {
            XData newData = new XData();
            newData.Textures = oldData.Textures;
        }
    }
}