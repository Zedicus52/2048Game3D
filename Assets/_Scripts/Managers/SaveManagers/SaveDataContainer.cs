using System;
using System.Collections.Generic;

namespace Game.Managers
{
    [Serializable]
    public sealed class SaveDataContainer
    {
        public List<string> Keys = new List<string>();
        public List<string> Values = new List<string>();

        public void SetData(string key, string value)
        {
            int index = Keys.IndexOf(key);
            if (index >= 0) Values[index] = value;
            else { Keys.Add(key); Values.Add(value); }
        }

        public string GetData(string key)
        {
            int index = Keys.IndexOf(key);
            return index >= 0 ? Values[index] : null;
        }
    }
}


