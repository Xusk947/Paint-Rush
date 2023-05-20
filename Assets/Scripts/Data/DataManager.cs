using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace PaintRush.Data
{
    public class DataManager
    {
        public static readonly string XDATA_SAVE_FILENAME = "XData";
        public static readonly string VARSDATA_SAVE_FILENAME = "XVars";
        public static void Save(SerializableData gameData, string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Create(Application.persistentDataPath + "/" + fileName);
            formatter.Serialize(fileStream, gameData);
            fileStream.Close();
            Debug.Log("GameData saved at: " + Application.persistentDataPath + "/" + fileName);
        }

        public static T Load<T>(string fileName)
        {
            string fullFileName = "Game loaded from: " + Application.persistentDataPath + "/" + fileName;
            if (File.Exists(Application.persistentDataPath + "/" + fileName))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = File.Open(fullFileName, FileMode.Open);
                T gameData = (T)formatter.Deserialize(fileStream);
                fileStream.Close();
                return gameData;
            }
            throw new FileNotFoundException("File:" + typeof(T).FullName + " not found at: " + fullFileName);
        }
    }
}