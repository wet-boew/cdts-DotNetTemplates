using System;
using System.Runtime.InteropServices;

namespace DiffCopy.Extensions
{
    public static class ByteExtensions
    {
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int memcmp(byte[] b1, byte[] b2, UIntPtr count);

        /// <summary>
        /// Performs an ultra-fast byte array comparison.
        /// </summary>
        /// <param name="b1">The first array of bytes to compare.</param>
        /// <param name="b2">The array of bytes to compare.</param>
        /// <returns>true if the byte arrays are equal; otherwise, false</returns>
        public static bool ByteArrayCompare(this byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true; //reference equality check

            if (b1 == null || b2 == null || b1.Length != b2.Length) return false;

            return memcmp(b1, b2, new UIntPtr((uint)b1.Length)) == 0;
        }
    }
}
