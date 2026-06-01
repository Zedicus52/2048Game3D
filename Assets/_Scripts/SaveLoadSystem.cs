using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadSystem
{
    private readonly string _path; 
    
    public SaveLoadSystem()
    {
        _path = Application.persistentDataPath + "/data.fun";
    }

    public void SaveScore(int score)
    {
        FileStream stream = new FileStream(_path,FileMode.Create);
        BinaryFormatter writer = new BinaryFormatter();
        writer.Serialize(stream, score);
        stream.Close();
    }

    public int ReadScore()
    {
        if (File.Exists(_path))
        {
            FileStream stream = new FileStream(_path,FileMode.Open);
            BinaryFormatter writer = new BinaryFormatter();
            return (int) writer.Deserialize(stream);
        }

        return -1;
    }
    
}

