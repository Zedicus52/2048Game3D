namespace Game.Managers
{
    public interface ISaveManager
    {
        void Save<T>(DataType type, T obj);
        T Load<T>(DataType type) where T : new();
    }
}

