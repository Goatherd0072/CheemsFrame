namespace Cheems.Debug
{
    public interface ILogger
    {
        void Debug(string msg);
        void Info(string msg);
        void Warning(string msg);
        void Error(string msg);

    }
}