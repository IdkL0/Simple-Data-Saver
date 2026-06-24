using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace IdkL0.SimpleDataSaver.Additional
{
    public static class AdditionalRegistries
    {
        public static void Register()
        {
            DataRegistries.Register<Vector3>(RegWVector3, RegRVector3);
            DataRegistries.Register<Vector2>(RegWVector2, RegRVector2);
            DataRegistries.Register<Vector3Int>(RegWVector3Int, RegRVector3Int);
            DataRegistries.Register<Vector2Int>(RegWVector2Int, RegRVector2Int);
            DataRegistries.Register<Vector4>(RegWVector4, RegRVector4);

            DataRegistries.Register<Quaternion>(RegWQuaternion, RegRQuaternion);

            DataRegistries.Register<Color>(RegWColor, RegRColor);
            DataRegistries.Register<Color32>(RegWColor32, RegRColor32);
        }

        #region Vector3

        private static void RegWVector3(BinaryWriter wr, Vector3 v)
        {
            wr.Write(v.x);
            wr.Write(v.y);
            wr.Write(v.z);
        }

        private static Vector3 RegRVector3(BinaryReader rd)
        {
            float x = rd.ReadSingle();
            float y = rd.ReadSingle();
            float z = rd.ReadSingle();

            return new(x, y, z);
        }

        #endregion

        #region Vector2

        private static void RegWVector2(BinaryWriter wr, Vector2 v)
        {
            wr.Write(v.x);
            wr.Write(v.y);
        }

        private static Vector2 RegRVector2(BinaryReader rd)
        {
            float x = rd.ReadSingle();
            float y = rd.ReadSingle();

            return new(x, y);
        }

        #endregion

        #region Vector3Int

        private static void RegWVector3Int(BinaryWriter wr, Vector3Int v)
        {
            wr.Write(v.x);
            wr.Write(v.y);
            wr.Write(v.z);
        }

        private static Vector3Int RegRVector3Int(BinaryReader rd)
        {
            int x = rd.ReadInt32();
            int y = rd.ReadInt32();
            int z = rd.ReadInt32();

            return new(x, y, z);
        }

        #endregion

        #region Vector2Int

        private static void RegWVector2Int(BinaryWriter wr, Vector2Int v)
        {
            wr.Write(v.x);
            wr.Write(v.y);
        }

        private static Vector2Int RegRVector2Int(BinaryReader rd)
        {
            int x = rd.ReadInt32();
            int y = rd.ReadInt32();

            return new(x, y);
        }

        #endregion

        #region Vector4

        private static void RegWVector4(BinaryWriter wr, Vector4 v)
        {
            wr.Write(v.x);
            wr.Write(v.y);
            wr.Write(v.z);
            wr.Write(v.w);
        }

        private static Vector4 RegRVector4(BinaryReader rd)
        {
            float x = rd.ReadSingle();
            float y = rd.ReadSingle();
            float z = rd.ReadSingle();
            float w = rd.ReadSingle();

            return new(x, y, z, w);
        }

        #endregion

        #region Quaternion

        private static void RegWQuaternion(BinaryWriter wr, Quaternion v)
        {
            wr.Write(v.x);
            wr.Write(v.y);
            wr.Write(v.z);
            wr.Write(v.w);
        }

        private static Quaternion RegRQuaternion(BinaryReader rd)
        {
            float x = rd.ReadSingle();
            float y = rd.ReadSingle();
            float z = rd.ReadSingle();
            float w = rd.ReadSingle();

            return new(x, y, z, w);
        }

        #endregion

        #region Color

        private static void RegWColor(BinaryWriter wr, Color v)
        {
            wr.Write(v.r);
            wr.Write(v.g);
            wr.Write(v.b);
            wr.Write(v.a);
        }

        private static Color RegRColor(BinaryReader rd)
        {
            float r = rd.ReadSingle();
            float g = rd.ReadSingle();
            float b = rd.ReadSingle();
            float a = rd.ReadSingle();

            return new(r, g, b, a);
        }

        #endregion

        #region Color32

        private static void RegWColor32(BinaryWriter wr, Color32 v)
        {
            wr.Write(v.r);
            wr.Write(v.g);
            wr.Write(v.b);
            wr.Write(v.a);
        }

        private static Color32 RegRColor32(BinaryReader rd)
        {
            byte r = rd.ReadByte();
            byte g = rd.ReadByte();
            byte b = rd.ReadByte();
            byte a = rd.ReadByte();

            return new(r, g, b, a);
        }

        #endregion
    }
}
