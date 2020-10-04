using System.ComponentModel;
using System.IO.Ports;
using System.Threading.Tasks;

namespace khf05137.IO.Ports
{
    public class AsyncSerialPort : SerialPort, IAsyncSerialPort
    {
        public AsyncSerialPort() : base() { }
        public AsyncSerialPort(IContainer container) : base(container) { }
        public AsyncSerialPort(string portName) : base(portName) { }
        public AsyncSerialPort(string portName, int baudRate) : base(portName, baudRate) { }
        public AsyncSerialPort(string portName, int baudRate, Parity parity) : base(portName, baudRate, parity) { }
        public AsyncSerialPort(string portName, int baudRate, Parity parity, int dataBits) : base(portName, baudRate, parity, dataBits) { }
        public AsyncSerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits) : base(portName, baudRate, parity, dataBits, stopBits) { }

        public Task<int> ReadAsync(byte[] buffer, int offset, int count) => Task.Run(() => Read(buffer, offset, count));
        public Task<int> ReadAsync(char[] buffer, int offset, int count) => Task.Run(() => Read(buffer, offset, count));
        public Task<int> ReadByteAsync() => Task.Run(() => ReadByte());
        public Task<int> ReadCharAsync() => Task.Run(() => ReadChar());
        public Task<string> ReadLineAsync() => Task.Run(() => ReadLine());
        public Task<string> ReadToAsync(string value) => Task.Run(() => ReadTo(value));
        public Task WriteAsync(byte[] buffer, int offset, int count) => Task.Run(() => Write(buffer, offset, count));
        public Task WriteAsync(string text) => Task.Run(() => Write(text));
        public void WriteAsync(char[] buffer, int offset, int count) => Task.Run(() => Write(buffer, offset, count));
        public void WriteLineAsync(string text) => Task.Run(() => WriteLine(text));
    }
}
