using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

namespace SaveSystem{
    public class DataSaver : MonoBehaviour
    {
        public static void Save(object object_, string objectName)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fileStream = File.Open(Application.persistentDataPath + "/" + objectName + ".bin", FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, object_);
                fileStream.Close();
            }
        }

        public static object Load(string objectName)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            if (!File.Exists(Application.persistentDataPath + "/" + objectName + ".bin"))
                return null;

            using (FileStream fileStream = (File.Open(Application.persistentDataPath + "/" + objectName + ".bin", FileMode.Open)))
            {
                object obj = binaryFormatter.Deserialize(fileStream) as object;

                return obj;
            }
        }

    }
}
