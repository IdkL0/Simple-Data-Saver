using System;
using System.IO;

namespace IdkL0.SimpleDataSaver
{
    public static class DataRegistries
    {
        public static void Register<T>(Action<BinaryWriter, T> write, Func<BinaryReader, T> read, bool rewrite = false)
        {
            if (!rewrite)
            {
                if (TypeRegistry<T>.Write != null || TypeRegistry<T>.Read != null)
                    return;
            }

            TypeRegistry<T>.Write = write ?? throw new ArgumentNullException(nameof(write));
            TypeRegistry<T>.Read = read ?? throw new ArgumentNullException(nameof(read));
        }

        internal static void RegisterDefaults()
        {
            Register<byte>((w, v) => w.Write(v), r => r.ReadByte());
            Register<sbyte>((w, v) => w.Write(v), r => r.ReadSByte());
            Register<short>((w, v) => w.Write(v), r => r.ReadInt16());
            Register<ushort>((w, v) => w.Write(v), r => r.ReadUInt16());
            Register<int>((w, v) => w.Write(v), r => r.ReadInt32());
            Register<uint>((w, v) => w.Write(v), r => r.ReadUInt32());
            Register<float>((w, v) => w.Write(v), r => r.ReadSingle());
            Register<double>((w, v) => w.Write(v), r => r.ReadDouble());
            Register<decimal>((w, v) => w.Write(v), r => r.ReadDecimal());

            Register<bool>((w, v) => w.Write(v), r => r.ReadBoolean());
            Register<char>((w, v) => w.Write(v), r => r.ReadChar());

            Register<string>((w, v) => w.Write(v), r => r.ReadString());
            Register<string>((w, v) => w.Write(v), r => r.ReadString());
        }
    }

    internal static class TypeRegistry<T>
    {
        public static Action<BinaryWriter, T> Write;
        public static Func<BinaryReader, T> Read;

        public static bool IsRegistered => Write != null && Read != null;
    }
}