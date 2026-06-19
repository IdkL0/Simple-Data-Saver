using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IdkL0.SimpleDataSaver
{
    public struct DataSave : IDisposable
    {
        internal MemoryStream _stream;

        public DataSave(MemoryStream stream)
        {
            _stream = stream;
        }

        public byte[] GetBytes()
        {
            _stream.Position = 0;
            return _stream.ToArray();
        }

        public string ToString(System.Text.Encoding encoding = null)
        {
            if (encoding == null) encoding = System.Text.Encoding.UTF8;

            byte[] bytes = GetBytes();

            if (bytes.Length == 0) return "";

            return encoding.GetString(bytes);
        }

        public void Dispose()
        {
            _stream?.Dispose();
        }
    }
}
