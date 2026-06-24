using System;
using System.Collections.Generic;

namespace IdkL0.SimpleDataSaver
{
    public struct KeyDataSave : IDisposable
    {
        internal DataSave sv;

        Dictionary<string, (int, object)> data;
        Type[] types;

        public IReadOnlyDictionary<string, (int, object)> Data => data;
        public readonly Type[] Types => types;

        internal KeyDataSave(Type[] typs, Dictionary<string, (int, object)> dat, DataSave save)
        {
            data = dat;
            types = typs;
            sv = save;
        }

        public (int, object) this[string key]
        {
            get => data[key];
        }

        public object GetValue(string key)
        {
            return data[key].Item2;
        }

        public Type GetType(string key)
        {
            return types[data[key].Item1];
        }

        public void Dispose()
        {
            sv.Dispose();
        }
    }
}
