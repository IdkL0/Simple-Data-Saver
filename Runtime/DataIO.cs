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
    }
}
