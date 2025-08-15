namespace Cheems.Debug
{
    /// <summary>
    /// Logger Log输出的内容
    /// </summary>
    [System.Flags]
    public enum ELogType : byte
    {
        Debug = 1,
        Info = 2,
        Warning = 4,
        Error = 8,
    }
}