﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khf05137.IO.Ports
{
    public static class SerialPortEx
    {
        public static ISerialPort AsInterface(this SerialPort port) => new InternalSerialPort(port);
    }

    internal class InternalSerialPort : ISerialPort
    {
        SerialPort port;

        public InternalSerialPort() => this.port = new SerialPort();
        public InternalSerialPort(SerialPort port) => this.port = port;
        public InternalSerialPort(IContainer container) => this.port = new SerialPort(container);
        public InternalSerialPort(string portName) => this.port = new SerialPort(portName);
        public InternalSerialPort(string portName, int baudRate) => this.port = new SerialPort(portName, baudRate);
        public InternalSerialPort(string portName, int baudRate, Parity parity) => this.port = new SerialPort(portName, baudRate, parity);
        public InternalSerialPort(string portName, int baudRate, Parity parity, int dataBits) => this.port = new SerialPort(portName, baudRate, parity, dataBits);
        public InternalSerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits) => this.port = new SerialPort(portName, baudRate, parity, dataBits, StopBits);

        public Stream BaseStream => this.port.BaseStream;

        public int BaudRate { get => this.port.BaudRate; set => this.port.BaudRate = value; }
        public bool BreakState { get => this.port.BreakState; set => this.port.BreakState = value; }

        public int BytesToRead => this.port.BytesToRead;

        public int BytesToWrite => this.port.BytesToWrite;

        public bool CDHolding => this.port.CDHolding;

        public bool CtsHolding => this.port.CtsHolding;

        public int DataBits { get => this.port.DataBits; set => this.port.DataBits = value; }
        public bool DiscardNull { get => this.port.DiscardNull; set => this.port.DiscardNull = value; }

        public bool DsrHolding => this.port.DsrHolding;

        public bool DtrEnable { get => this.port.DtrEnable; set => this.port.DtrEnable = value; }
        public Encoding Encoding { get => this.port.Encoding; set => this.port.Encoding = value; }
        public Handshake Handshake { get => this.port.Handshake; set => this.port.Handshake = value; }

        public bool IsOpen => this.port.IsOpen;

        public string NewLine { get => this.port.NewLine; set => this.port.NewLine = value; }
        public Parity Parity { get => this.port.Parity; set => this.port.Parity = value; }
        public byte ParityReplace { get => this.port.ParityReplace; set => this.port.ParityReplace = value; }
        public string PortName { get => this.port.PortName; set => this.port.PortName = value; }
        public int ReadBufferSize { get => this.port.ReadBufferSize; set => this.port.ReadBufferSize = value; }
        public int ReadTimeout { get => this.port.ReadTimeout; set => this.port.ReadTimeout = value; }
        public int ReceivedBytesThreshold { get => this.port.ReceivedBytesThreshold; set => this.port.ReceivedBytesThreshold = value; }
        public bool RtsEnable { get => this.port.RtsEnable; set => this.port.RtsEnable = value; }
        public StopBits StopBits { get => this.port.StopBits; set => this.port.StopBits = value; }
        public int WriteBufferSize { get => this.port.WriteBufferSize; set => this.port.WriteBufferSize = value; }
        public int WriteTimeout { get => this.port.WriteTimeout; set => this.port.WriteTimeout = value; }

        public event SerialDataReceivedEventHandler DataReceived;
        public event SerialErrorReceivedEventHandler ErrorReceived;
        public event SerialPinChangedEventHandler PinChanged;

        public void Close() => this.port.Close();

        public void DiscardInBuffer() => this.port.DiscardInBuffer();

        public void DiscardOutBuffer() => this.port.DiscardOutBuffer();

        public void Dispose()
        {
            this.port?.Dispose();
            this.port = null;
        }

        public void Open() => this.port.Open();

        public int Read(byte[] buffer, int offset, int count) => this.port.Read(buffer, offset, count);
        public int Read(char[] buffer, int offset, int count) => this.port.Read(buffer, offset, count);
        public int ReadByte() => this.port.ReadByte();
        public int ReadChar() => this.port.ReadChar();
        public string ReadExisting() => this.port.ReadExisting();
        public string ReadLine() => this.port.ReadLine();
        public string ReadTo(string value) => this.port.ReadTo(value);

        public void Write(byte[] buffer, int offset, int count) => this.port.Write(buffer, offset, count);
        public void Write(char[] buffer, int offset, int count) => this.port.Write(buffer, offset, count);
        public void Write(string text) => this.port.Write(text);
        public void WriteLine(string text) => this.port.WriteLine(text);
    }
}