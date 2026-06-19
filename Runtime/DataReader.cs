using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace IdkL0.SimpleDataSaver
{
    public class DataReader : IDisposable
    {
        public DataSave _data;
        BinaryReader _reader;

        public DataReader(DataSave data)
        {
            _data = data;
            _reader = new(_data._stream, Encoding.UTF8, true);

            ResetPosition();
        }

        public T Read<T>()
        {
            T val = default(T);

            if (TypeRegistry<T>.IsRegistered)
            {
                val = TypeRegistry<T>.Read(_reader);
            }
            else
            {
                Debug.LogError($"[DataSaver] Type: {typeof(T).Name} not registred");
            }

            return val;
        }

        public void ResetPosition()
        {
            _data._stream.Position = 0;
        }

        public void End(bool disposeData = false)
        {
            if (disposeData)
            {
                _data.Dispose();
            }
            else
            {
                ResetPosition();
            }

            Dispose();
        }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}
