using UnityEngine;

namespace Game.Managers
{
    public sealed class PlayerPrefsSaveManager : ISaveManager
    {
        public T Load<T>(DataType type) where T : new()
        {
            var data = PlayerPrefs.GetString(type.ToString(), string.Empty);

            if (string.IsNullOrEmpty(data))
                return new T();

            var result = JsonUtility.FromJson<T>(data);
            result ??= new T();

            return result;
        }

        public void Save<T>(DataType type, T obj)
        {
            PlayerPrefs.SetString(type.ToString(), JsonUtility.ToJson(obj));
            PlayerPrefs.Save();
        }
    }
}


