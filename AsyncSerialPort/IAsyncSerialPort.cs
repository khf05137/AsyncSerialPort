using System.Threading.Tasks;

namespace khf05137.IO.Ports
{
    public interface IAsyncSerialPort : ISerialPort
    {
        Task<int> ReadAsync(byte[] buffer, int offset, int count);
        Task<int> ReadAsync(char[] buffer, int offset, int count);
        Task<int> ReadByteAsync();
        Task<int> ReadCharAsync();
        Task<string> ReadLineAsync();
        Task<string> ReadToAsync(string value);
        Task WriteAsync(byte[] buffer, int offset, int count);
        void WriteAsync(char[] buffer, int offset, int count);
        Task WriteAsync(string text);
        void WriteLineAsync(string text);
    }
}