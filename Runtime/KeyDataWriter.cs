using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace IdkL0.SimpleDataSaver
{
    public class KeyDataWriter : IDisposable
    {
        DataWriter wr;

        List<Type> typeList;
        public IReadOnlyList<Type> TypeList => typeList;

        Dictionary<string, (int, object)> data;
        private int keysCount = 0;

        public KeyDataWriter()
        {
            wr = new();

            typeList = new();
            data = new();
        }

        public void Write<T>(string key, T value)
        {
            wr.Write(key);

            int typ = GetTypeList<T>();
            wr.Write(typ);

            wr.Write(value);

            keysCount++;
            data[key] = (typ, value);
        }

        private int GetTypeList<T>()
        {
            Type t = typeof(T);

            if (typeList.Contains(t))
            {
                return typeList.IndexOf(t);
            }
            else
            {
                typeList.Add(t);
                return typeList.Count - 1;
            }
        }

        public KeyDataSave End()
        {
            byte[] sv = wr.End().GetBytes();

            DataWriter finalWr = new();

            finalWr.Write(typeList.Count);
            foreach (Type t in typeList)
            {
                finalWr.Write($"{t.AssemblyQualifiedName}");
            }

            finalWr.Write(keysCount);
            finalWr._stream.Write(sv);

            return new(typeList.ToArray(), data, finalWr.End());
        }

        public void Dispose()
        {
            wr?.Dispose();
        }
    }
}
