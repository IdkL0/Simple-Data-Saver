using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IdkL0.SimpleDataSaver
{
    public static class DataIO
    {
        public static void WriteToStream(Stream stream, DataSave data)
        {
            if (!stream.CanWrite) return;

            stream.Write(data.GetBytes());
        }

        public static async Task WriteToStreamAsync(Stream stream, DataSave data, CancellationToken ct = default)
        {
            if (!stream.CanWrite) throw new System.ArgumentException("Stream can't be read");

            byte[] bytes = data.GetBytes();
            await stream.WriteAsync(bytes.AsMemory(), ct).ConfigureAwait(false);
        }

        public static FileStream CreateFileSave(string filename, DataSave data)
        {
            FileStream stream = File.Create(filename);

            WriteToStream(stream, data);

            stream.Close();

            return stream;
        }

        public static async Task<FileStream> CreateFileSaveAsync(string filename, DataSave data, CancellationToken ct = default)
        {
            FileStream stream = File.Create(filename);

            await WriteToStreamAsync(stream, data, ct).ConfigureAwait(false);

            stream.Close();

            return stream;
        }

        public static DataSave ReadFromStream(Stream stream)
        {
            MemoryStream ms = new MemoryStream();

            if (stream.CanRead) stream.CopyTo(ms);

            return new DataSave(ms);
        }

        public static async Task<DataSave> ReadFromStreamAsync(Stream stream, CancellationToken ct = default)
        {
            MemoryStream ms = new MemoryStream();

            if (stream.CanRead) await stream.CopyToAsync(ms);

            return new DataSave(ms);
        }

        public static DataSave ReadFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                FileStream stream = File.OpenRead(filename);

                DataSave save = ReadFromStream(stream);

                stream.Close();

                return save;
            }

            MemoryStream ms = new MemoryStream();
            return new DataSave(ms);
        }

        public static async Task<DataSave> ReadFromFileAsync(string filename, CancellationToken ct = default)
        {
            if (File.Exists(filename))
            {
                FileStream stream = File.OpenRead(filename);

                DataSave save = await ReadFromStreamAsync(stream);

                stream.Close();

                return save;
            }

            MemoryStream ms = new MemoryStream();
            return new DataSave(ms);
        }
    }
}
