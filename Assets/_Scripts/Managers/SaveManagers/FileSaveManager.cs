using System;
using System.IO;
using UnityEngine;

namespace Game.Managers
{
    public sealed class FileSaveManager : ISaveManager
    {
        private readonly string _fileName = "save.dat";
        private readonly string _filePath;

        private readonly SaveDataContainer _container;
        public FileSaveManager()
        {
            _filePath = Path.Combine(Application.persistentDataPath, _fileName);
            _container = new SaveDataContainer();
        }

        public T Load<T>(DataType type) where T : new()
        {
            string data = _container.GetData(type.ToString());

            if (string.IsNullOrEmpty(data))
                return new T();

            try
            {
                T result = JsonUtility.FromJson<T>(data);
                return result ?? new T();
            }
            catch (Exception e)
            {
                LogExeption(e);
                return new T();
            }
        }

        public void Save<T>(DataType type, T obj)
        {
            string json = JsonUtility.ToJson(obj);
            _container.SetData(type.ToString(), json);

            string fullJson = JsonUtility.ToJson(_container);

            try
            {
                File.WriteAllText(_filePath, fullJson);

            }
            catch (Exception e)
            {
                LogExeption(e);
            }
        }

        private void LogExeption(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}
