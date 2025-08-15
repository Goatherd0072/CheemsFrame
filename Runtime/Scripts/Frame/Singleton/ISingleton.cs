namespace Cheems
{

    /// <summary>
    /// The singleton interface.
    /// </summary>
    public interface ISingleton
    {
        public void InitializeSingleton();

        public void       ClearSingleton();
        // public ISingleton GetActive();//提前调用，而不是懒汉模式
    }

    public enum ESingletonInitializationStatus
    {
        None,
        Initializing,
        Initialized
    }

}