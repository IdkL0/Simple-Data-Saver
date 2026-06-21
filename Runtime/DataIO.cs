using System.IO;

namespace IdkL0.SimpleDataSaver
{
    public static class DataIO
    {
        public static void WriteToStream(Stream stream, DataSave data)
        {
            if (!stream.CanWrite) return;

            stream.Write(data.GetBytes());
        }

        public static FileStream CreateFileSave(string filename, DataSave data)
        {
            FileStream stream = File.Create(filename);

            WriteToStream(stream, data);

            stream.Close();

            return stream;
        }

        public static DataSave ReadFromStream(Stream stream)
        {
            MemoryStream ms = new MemoryStream();

            if (stream.CanRead) stream.CopyTo(ms);

            return new DataSave(ms);
        }

        public static DataSave ReadFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                FileStream stream = File.OpenRead(filename);

                return ReadFromStream(stream);
            }

            MemoryStream ms = new MemoryStream();
            return new DataSave(ms);
        }
    }
}
