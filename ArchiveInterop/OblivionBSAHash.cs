using System;
using System.IO;

namespace ArchiveInterop
{
    /// <summary>
    /// Provides static methods for hashing strings for PS3 and PC Oblivion BSAs.
    /// </summary>
    public class OblivionBSAHash
    {
        /// <summary>
        /// Based on code from <see href="https://en.uesp.net/wiki/Tes4Mod:Hash_Calculation#C.23">UESP wiki</see>.
        /// Hashes string based on PS3 hashing algorithm, should only be used on texture (DirectDraw Surface) files.
        /// </summary>
        /// <param name="str">String to be hashed.</param>
        /// <returns>Hashed string as ulong</returns>
        public static ulong GetPS3(string str)
        {
            string name = Path.ChangeExtension(str, null).ToLower();
            string ext = Path.GetExtension(str).ToLower();
            var lastChar = (byte)(name.Length == 0 ? 0x00 : name[name.Length - 1]); // Last character of "name" string

            var hashBytes = new byte[]
            {
                lastChar,
                lastChar,
                (byte)name.Length,
                (byte)(name.Length == 0 ? 0x00 : name[0])
            };

            // Extra handling if second-to-last character is underscore
            if (name.Length > 2 && name[name.Length - 2].Equals('_'))
            {
                name = name.Remove(name.Length - 2); // "_X" must be removed from the filename
                hashBytes[2] -= 2; // Reduces name length byte by 2

                hashBytes[1] = (byte)(name.Length == 0 ? 0x00 : name[name.Length - 1]);
                hashBytes[0] ^= 0x80;
            }
            else
            {
                hashBytes[0] = 0x80;
            }

            hashBytes[1] ^= 0x80;
            uint hash1 = BitConverter.ToUInt32(hashBytes, 0);

            uint hash2 = 0;
            for (int i = 1; i < name.Length - 1; i++)
            {
                hash2 = hash2 * 0x1003F + (byte)name[i];
            }

            uint hash3 = 0;
            for (int i = 0; i < ext.Length; i++)
            {
                hash3 = hash3 * 0x1003F + (byte)ext[i];
            }

            return (((ulong)(hash2 + hash3)) << 32) + hash1;
        }

        /// <summary>
        /// From <see href="https://en.uesp.net/wiki/Tes4Mod:Hash_Calculation#C.23">UESP wiki</see>.
        /// Hashes string based on PC hashing algorithm.
        /// </summary>
        /// <param name="name">Name of file/str</param>
        /// <param name="ext">Extension if hashing a file name</param>
        /// <returns>Hashed name and extension as ulong</returns>
        public static ulong GetPC(string name, string ext = "")
        {
            var hashBytes = new byte[]
            {
                    (byte)(name.Length == 0 ? 0x00 : name[name.Length - 1]),
                    (byte)(name.Length < 3 ? 0x00 : name[name.Length - 2]),
                    (byte)name.Length,
                    (byte)(name.Length == 0 ? 0x00 : name[0])
            };

            uint hash1 = BitConverter.ToUInt32(hashBytes, 0);

            switch (ext)
            {
                case ".kf":
                    hash1 |= 0x80;
                    break;

                case ".nif":
                    hash1 |= 0x8000;
                    break;

                case ".dds":
                    hash1 |= 0x8080;
                    break;

                case ".wav":
                    hash1 |= 0x80000000;
                    break;
            }

            uint hash2 = 0;
            for (int i = 1; i < name.Length - 2; i++)
            {
                hash2 = hash2 * 0x1003F + (byte)name[i];
            }

            uint hash3 = 0;
            for (int i = 0; i < ext.Length; i++)
            {
                hash3 = hash3 * 0x1003F + (byte)ext[i];
            }

            return (((ulong)(hash2 + hash3)) << 32) + hash1;
        }
    }
}