using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace IdkL0.SimpleDataSaver
{
    public class KeyDataReader : IDisposable
    {
        DataReader rd;

        public KeyDataReader(DataSave data)
        {
            rd = new(data);
        }

        public KeyDataSave Read()
        {
            List<Type> typeep = new();

            int typesCount = rd.Read<int>();
            for (int i = 0; i < typesCount; ++i)
            {
                string veve = rd.Read<string>();

                Type tpep = Type.GetType(veve);

                if (tpep != null)
                {
                    typeep.Add(tpep);
                }
            }

            Dictionary<string, (int, object)> data = new();

            int keysCount = rd.Read<int>();
            for (int i = 0; i < keysCount; ++i)
            {
                string key = rd.Read<string>();

                int typeIdx = rd.Read<int>();

                Type t = typeep[typeIdx];

                //reflection call
                object dat = typeof(DataReader).GetMethod(nameof(Read)).MakeGenericMethod(t).Invoke(rd, null);

                data[key] = (typeIdx, dat);
            }

            return new(typeep.ToArray(), data, rd._data);
        }

        public void Dispose()
        {
            rd.End();
        }
    }
}
