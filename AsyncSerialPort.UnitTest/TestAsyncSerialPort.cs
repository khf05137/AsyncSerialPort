using System;
using System.Configuration;
using System.IO;
using System.IO.Ports;
using System.Reactive.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using khf05137.IO.Ports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32.SafeHandles;
using Moq;

namespace khf05137.IO.Ports.UnitTest
{
    [TestClass]
    public class TestAsyncSerialPort
    {
        [TestMethod]
        public void TestOpenClose()
        {
            var mock = new Mock<ISerialPort>();

            using (var port = new AsyncSerialPort(mock.Object))
            {
                port.Open();

                port.Close();
            }

            mock.Verify(x => x.Open(), Times.Once);
            mock.Verify(x => x.Close(), Times.Once);
            mock.Verify(x => x.Dispose(), Times.Once);
            mock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void TestProperties()
        {
            var mock = new Mock<ISerialPort>();

            using (var port = new AsyncSerialPort(mock.Object))
            {
                mock.SetupGet(x => x.BaseStream).Returns(Stream.Null);
                Assert.AreEqual(Stream.Null, port.BaseStream);

                mock.SetupGet(x => x.BaudRate).Returns(115200);
                Assert.AreEqual(115200, port.BaudRate);
                port.BaudRate = 460800;
                mock.VerifySet(x => x.BaudRate = 460800, Times.Once);

                mock.SetupGet(x => x.BreakState).Returns(true);
                Assert.AreEqual(true, port.BreakState);
                port.BreakState = false;
                mock.VerifySet(x => x.BreakState = false, Times.Once);

                mock.SetupGet(x => x.BytesToRead).Returns(1024);
                Assert.AreEqual(1024, port.BytesToRead);

                mock.SetupGet(x => x.BytesToWrite).Returns(2048);
                Assert.AreEqual(2048, port.BytesToWrite);

                mock.SetupGet(x => x.CDHolding).Returns(true);
                Assert.AreEqual(true, port.CDHolding);

                mock.SetupGet(x => x.CtsHolding).Returns(true);
                Assert.AreEqual(true, port.CtsHolding);

                mock.SetupGet(x => x.DataBits).Returns(8);
                Assert.AreEqual(8, port.DataBits);
                port.DataBits = 7;
                mock.VerifySet(x => x.DataBits = 7, Times.Once);

                mock.SetupGet(x => x.DiscardNull).Returns(true);
                Assert.AreEqual(true, port.DiscardNull);
                port.DiscardNull = false;
                mock.VerifySet(x => x.DiscardNull = false, Times.Once);

                mock.SetupGet(x => x.DsrHolding).Returns(true);
                Assert.AreEqual(true, port.DsrHolding);

                mock.SetupGet(x => x.DtrEnable).Returns(true);
                Assert.AreEqual(true, port.DtrEnable);
                port.DtrEnable = false;
                mock.VerifySet(x => x.DtrEnable = false, Times.Once);

                mock.SetupGet(x => x.Encoding).Returns(Encoding.ASCII);
                Assert.AreEqual(Encoding.ASCII, port.Encoding);
                port.Encoding = Encoding.UTF8;
                mock.VerifySet(x => x.Encoding = Encoding.UTF8, Times.Once);

                mock.SetupGet(x => x.Handshake).Returns(Handshake.XOnXOff);
                Assert.AreEqual(Handshake.XOnXOff, port.Handshake);
                port.Handshake = Handshake.RequestToSend;
                mock.VerifySet(x => x.Handshake = Handshake.RequestToSend, Times.Once);

                mock.SetupGet(x => x.IsOpen).Returns(true);
                Assert.AreEqual(true, port.IsOpen);

                mock.SetupGet(x => x.NewLine).Returns("\r");
                Assert.AreEqual("\r", port.NewLine);
                port.NewLine = "\n";
                mock.VerifySet(x => x.NewLine = "\n", Times.Once);

                mock.SetupGet(x => x.Parity).Returns(Parity.Even);
                Assert.AreEqual(Parity.Even, port.Parity);
                port.Parity = Parity.Odd;
                mock.VerifySet(x => x.Parity = Parity.Odd, Times.Once);

                mock.SetupGet(x => x.ParityReplace).Returns(0x01);
                Assert.AreEqual(0x01, port.ParityReplace);
                port.ParityReplace = 0xff;
                mock.VerifySet(x => x.ParityReplace = 0xff, Times.Once);

                mock.SetupGet(x => x.PortName).Returns("COM1");
                Assert.AreEqual("COM1", port.PortName);
                port.PortName = "COM2";
                mock.VerifySet(x => x.PortName = "COM2", Times.Once);

                mock.SetupGet(x => x.ReadBufferSize).Returns(4098);
                Assert.AreEqual(4098, port.ReadBufferSize);
                port.ReadBufferSize = 2048;
                mock.VerifySet(x => x.ReadBufferSize = 2048, Times.Once);

                mock.SetupGet(x => x.ReadTimeout).Returns(3000);
                Assert.AreEqual(3000, port.ReadTimeout);
                port.ReadTimeout = 1000;
                mock.VerifySet(x => x.ReadTimeout = 1000, Times.Once);

                mock.SetupGet(x => x.ReceivedBytesThreshold).Returns(1024);
                Assert.AreEqual(1024, port.ReceivedBytesThreshold);
                port.ReceivedBytesThreshold = 512;
                mock.VerifySet(x => x.ReceivedBytesThreshold = 512, Times.Once);

                mock.SetupGet(x => x.RtsEnable).Returns(true);
                Assert.AreEqual(true, port.RtsEnable);
                port.RtsEnable = false;
                mock.VerifySet(x => x.RtsEnable = false, Times.Once);

                mock.SetupGet(x => x.StopBits).Returns(StopBits.One);
                Assert.AreEqual(StopBits.One, port.StopBits);
                port.StopBits = StopBits.Two;
                mock.VerifySet(x => x.StopBits = StopBits.Two, Times.Once);

                mock.SetupGet(x => x.WriteBufferSize).Returns(256);
                Assert.AreEqual(256, port.WriteBufferSize);
                port.WriteBufferSize = 128;
                mock.VerifySet(x => x.WriteBufferSize = 128, Times.Once);

                mock.SetupGet(x => x.WriteTimeout).Returns(500);
                Assert.AreEqual(500, port.WriteTimeout);
                port.WriteTimeout = 400;
                mock.VerifySet(x => x.WriteTimeout = 400, Times.Once);
            }
        }

        [TestMethod]
        public void TestOtherMethods()
        {
            var mock = new Mock<ISerialPort>();
            using (var port = new AsyncSerialPort(mock.Object))
            {
                port.DiscardInBuffer();
                mock.Verify(x => x.DiscardInBuffer(), Times.Once);

                port.DiscardOutBuffer();
                mock.Verify(x => x.DiscardOutBuffer(), Times.Once);

                var readBuff = new byte[1];
                mock.Setup(x => x.Read(readBuff, 0, 1)).Returns(1);
                Assert.AreEqual(1, port.Read(readBuff, 0, 1));
                mock.Verify(x => x.Read(readBuff, 0, 1), Times.Once);

                var readfCharBuff = new char[1];
                mock.Setup(x => x.Read(readfCharBuff, 0, 1)).Returns(1);
                Assert.AreEqual(1, port.Read(readfCharBuff, 0, 1));
                mock.Verify(x => x.Read(readfCharBuff, 0, 1), Times.Once);

                mock.Setup(x => x.ReadByte()).Returns(0xff);
                Assert.AreEqual(0xff, port.ReadByte());
                mock.Verify(x => x.ReadByte(), Times.Once);

                mock.Setup(x => x.ReadChar()).Returns('A');
                Assert.AreEqual('A', port.ReadChar());
                mock.Verify(x => x.ReadChar(), Times.Once);

                mock.Setup(x => x.ReadExisting()).Returns("ABC");
                Assert.AreEqual("ABC", port.ReadExisting());
                mock.Verify(x => x.ReadExisting(), Times.Once);

                mock.Setup(x => x.ReadLine()).Returns("XYZ");
                Assert.AreEqual("XYZ", port.ReadLine());
                mock.Verify(x => x.ReadLine(), Times.Once);

                mock.Setup(x => x.ReadTo("\t")).Returns("LMN");
                Assert.AreEqual("LMN", port.ReadTo("\t"));
                mock.Verify(x => x.ReadTo("\t"), Times.Once);

                byte[] writeBuff = new byte[] { 0, 1, 2 };
                port.Write(writeBuff, 0, 1);
                mock.Verify(x => x.Write(writeBuff, 0, 1), Times.Once);

                char[] writeCharBuff = new char[] { 'A', 'B', 'C' };
                port.Write(writeCharBuff, 0, 1);
                mock.Verify(x => x.Write(writeCharBuff, 0, 1), Times.Once);

                var writeStr = "ABC";
                port.Write(writeStr);
                mock.Verify(x => x.Write(writeStr), Times.Once);

                var writeStr2 = "XYZ";
                port.WriteLine(writeStr2);
                mock.Verify(x => x.WriteLine(writeStr2), Times.Once);
            }
        }

        [TestMethod]
        public async Task TestMethodAsync()
        {
            var mock = new Mock<ISerialPort>();
            using (var port = new AsyncSerialPort(mock.Object))
            {
                var readBuff = new byte[1];
                mock.Setup(x => x.Read(readBuff, 0, 1)).Returns(1);
                Assert.AreEqual(1, await port.ReadAsync(readBuff, 0, 1));
                mock.Verify(x => x.Read(readBuff, 0, 1), Times.Once);

                var readfCharBuff = new char[1];
                mock.Setup(x => x.Read(readfCharBuff, 0, 1)).Returns(1);
                Assert.AreEqual(1, await port.ReadAsync(readfCharBuff, 0, 1));
                mock.Verify(x => x.Read(readfCharBuff, 0, 1), Times.Once);

                mock.Setup(x => x.ReadByte()).Returns(0xff);
                Assert.AreEqual(0xff, await port.ReadByteAsync());
                mock.Verify(x => x.ReadByte(), Times.Once);

                mock.Setup(x => x.ReadChar()).Returns('A');
                Assert.AreEqual('A', await port.ReadCharAsync());
                mock.Verify(x => x.ReadChar(), Times.Once);

                mock.Setup(x => x.ReadLine()).Returns("XYZ");
                Assert.AreEqual("XYZ", await port.ReadLineAsync());
                mock.Verify(x => x.ReadLine(), Times.Once);

                mock.Setup(x => x.ReadTo("\t")).Returns("LMN");
                Assert.AreEqual("LMN", await port.ReadToAsync("\t"));
                mock.Verify(x => x.ReadTo("\t"), Times.Once);

                byte[] writeBuff = new byte[] { 0, 1, 2 };
                await port.WriteAsync(writeBuff, 0, 1);
                mock.Verify(x => x.Write(writeBuff, 0, 1), Times.Once);

                char[] writeCharBuff = new char[] { 'A', 'B', 'C' };
                await port.WriteAsync(writeCharBuff, 0, 1);
                mock.Verify(x => x.Write(writeCharBuff, 0, 1), Times.Once);

                var writeStr = "ABC";
                await port.WriteAsync(writeStr);
                mock.Verify(x => x.Write(writeStr), Times.Once);

                var writeStr2 = "XYZ";
                await port.WriteLineAsync(writeStr2);
                mock.Verify(x => x.WriteLine(writeStr2), Times.Once);
            }
        }

        [TestMethod]
        public void TestDataReceivedEvent()
        {
            var mock = new Mock<ISerialPort>();
            mock.SetupAdd(x => x.DataReceived += It.IsAny<SerialDataReceivedEventHandler>());
            mock.SetupRemove(x => x.DataReceived -= It.IsAny<SerialDataReceivedEventHandler>());

            using (var port = new AsyncSerialPort(mock.Object))
            {
                bool received = false;
                using (Observable.FromEventPattern<SerialDataReceivedEventHandler, SerialDataReceivedEventArgs>(
                    x => port.DataReceived += x, x => port.DataReceived -= x)
                    .Select(x => x.EventArgs)
                    .Subscribe(x =>
                    {
                        received = true;
                    }))
                {
                    mock.Raise(x => x.DataReceived += null, SerialDataReceivedEventArgs.Empty as SerialDataReceivedEventArgs);
                    Assert.IsTrue(received);
                }
            }

            mock.VerifyAdd(x => x.DataReceived += It.IsAny<SerialDataReceivedEventHandler>(), Times.Once);
            mock.VerifyRemove(x => x.DataReceived -= It.IsAny<SerialDataReceivedEventHandler>(), Times.Once);
        }

        [TestMethod]
        public void TestErrorReceivedEvent()
        {
            var mock = new Mock<ISerialPort>();
            mock.SetupAdd(x => x.ErrorReceived += It.IsAny<SerialErrorReceivedEventHandler>());
            mock.SetupRemove(x => x.ErrorReceived -= It.IsAny<SerialErrorReceivedEventHandler>());

            using (var port = new AsyncSerialPort(mock.Object))
            {
                bool received = false;
                using (Observable.FromEventPattern<SerialErrorReceivedEventHandler, SerialErrorReceivedEventArgs>(
                    x => port.ErrorReceived += x, x => port.ErrorReceived -= x)
                    .Select(x => x.EventArgs)
                    .Subscribe(x =>
                    {
                        received = true;
                    }))
                {
                    mock.Raise(x => x.ErrorReceived += null, SerialDataReceivedEventArgs.Empty as SerialDataReceivedEventArgs);
                    Assert.IsTrue(received);
                }
            }

            mock.VerifyAdd(x => x.ErrorReceived += It.IsAny<SerialErrorReceivedEventHandler>(), Times.Once);
            mock.VerifyRemove(x => x.ErrorReceived -= It.IsAny<SerialErrorReceivedEventHandler>(), Times.Once);
        }

        [TestMethod]
        public void TestPinChangedEvent()
        {
            var mock = new Mock<ISerialPort>();
            mock.SetupAdd(x => x.PinChanged += It.IsAny<SerialPinChangedEventHandler>());
            mock.SetupRemove(x => x.PinChanged -= It.IsAny<SerialPinChangedEventHandler>());

            using (var port = new AsyncSerialPort(mock.Object))
            {
                bool received = false;
                using (Observable.FromEventPattern<SerialPinChangedEventHandler, SerialPinChangedEventArgs>(
                    x => port.PinChanged += x, x => port.PinChanged -= x)
                    .Select(x => x.EventArgs)
                    .Subscribe(x =>
                    {
                        received = true;
                    }))
                {
                    mock.Raise(x => x.PinChanged += null, SerialDataReceivedEventArgs.Empty as SerialDataReceivedEventArgs);
                    Assert.IsTrue(received);
                }
            }

            mock.VerifyAdd(x => x.PinChanged += It.IsAny<SerialPinChangedEventHandler>(), Times.Once);
            mock.VerifyRemove(x => x.PinChanged -= It.IsAny<SerialPinChangedEventHandler>(), Times.Once);
        }
    }
}
