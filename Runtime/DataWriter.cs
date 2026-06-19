using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace IdkL0.SimpleDataSaver
{
    public class DataWriter : IDisposable
    {
        private readonly MemoryStream _stream;
        private readonly BinaryWriter _writer;

        public DataWriter()
        {
            _stream = new MemoryStream();
            _writer = new BinaryWriter(_stream, Encoding.UTF8, leaveOpen: true);
        }

        public void Write<T>(T value)
        {
            if (TypeRegistry<T>.IsRegistered)
            {
                TypeRegistry<T>.Write(_writer, value);
            }
            else
            {
                Debug.LogError($"[DataSaver] Type: {typeof(T).Name} not registred");
            }
        }

        public void Close()
        {
            _writer.Flush();
            _stream.Position = 0;
        }

        public byte[] ToArray()
        {
            Close();
            return _stream.ToArray();
        }

        public void Dispose()
        {
            _writer?.Dispose();
            _stream?.Dispose();
        }
    }
}