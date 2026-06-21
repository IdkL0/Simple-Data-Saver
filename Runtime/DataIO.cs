using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace IdkL0.SimpleDataSaver
{
    public static class DataIO
    {
        public static void WriteToStream(Stream stream, DataSave data)
        {
            if (!stream.CanWrite) return;

            if (SimpleDataSaver.Logs) Debug.Log($"Write to stream");
            stream.Write(data.GetBytes());
        }

        public static async Task WriteToStreamAsync(Stream stream, DataSave data, CancellationToken ct = default)
        {
            if (!stream.CanWrite) throw new System.ArgumentException("Stream can't be read");

            if (SimpleDataSaver.Logs) Debug.Log($"Write to stream (Async)");
            byte[] bytes = data.GetBytes();
            await stream.WriteAsync(bytes.AsMemory(), ct).ConfigureAwait(false);
        }

        public static FileStream CreateFileSave(string filename, DataSave data)
        {
            FileStream stream = File.Create(filename);
            if (SimpleDataSaver.Logs) Debug.Log($"File created {filename}");

            WriteToStream(stream, data);

            stream.Close();

            return stream;
        }

        public static async Task<FileStream> CreateFileSaveAsync(string filename, DataSave data, CancellationToken ct = default)
        {
            FileStream stream = File.Create(filename);
            if (SimpleDataSaver.Logs) Debug.Log($"File created {filename} (Async)");

            await WriteToStreamAsync(stream, data, ct).ConfigureAwait(false);

            stream.Close();

            return stream;
        }

        public static DataSave ReadFromStream(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            if (SimpleDataSaver.Logs) Debug.Log($"Reading stream");

            if (stream.CanRead) stream.CopyTo(ms);

            return new DataSave(ms);
        }

        public static async Task<DataSave> ReadFromStreamAsync(Stream stream, CancellationToken ct = default)
        {
            MemoryStream ms = new MemoryStream();
            if (SimpleDataSaver.Logs) Debug.Log($"Reading stream (Async)");

            if (stream.CanRead) await stream.CopyToAsync(ms);

            return new DataSave(ms);
        }

        public static DataSave ReadFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                FileStream stream = File.OpenRead(filename);
                if (SimpleDataSaver.Logs) Debug.Log($"Opened file {filename}");

                DataSave save = ReadFromStream(stream);

                stream.Close();

                return save;
            }
            else
            {
                throw new ArgumentException($"File is not existing, dir: {filename}");
            }
        }

        public static async Task<DataSave> ReadFromFileAsync(string filename, CancellationToken ct = default)
        {
            if (File.Exists(filename))
            {
                FileStream stream = File.OpenRead(filename);
                if (SimpleDataSaver.Logs) Debug.Log($"Opened file {filename} (Async)");

                DataSave save = await ReadFromStreamAsync(stream, ct);

                stream.Close();

                return save;
            }
            else
            {
                throw new ArgumentException($"File is not existing, dir: {filename}");
            }
        }
    }
}
