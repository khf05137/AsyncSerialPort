using System;
using System.IO;
using System.IO.Ports;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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

                var readBuff = new byte[1] { 0xff };
                mock.Setup(x => x.Read(readBuff, 0, 1)).Returns(1);
                Assert.AreEqual(1, port.Read(readBuff, 0, 1));
                mock.Verify(x => x.Read(readBuff, 0, 1), Times.Once);


            }
        }
    }
}
