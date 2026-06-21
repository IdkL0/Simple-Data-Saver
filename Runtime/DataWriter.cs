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
                if (SimpleDataSaver.Logs) Debug.Log($"Write type: {typeof(T).Name}, value {value}");
            }
            else
            {
                Debug.LogError($"[DataWriter] Type: {typeof(T).Name} not registred");
            }
        }

        public DataSave End()
        {
            if (SimpleDataSaver.Logs) Debug.Log("End writing");

            _writer.Flush();
            _stream.Position = 0;

            Dispose();
            return new(_stream);
        }

        public void Dispose()
        {
            _writer?.Dispose();
        }
    }
}