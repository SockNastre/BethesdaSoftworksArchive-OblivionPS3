using System.IO;

namespace NormalMapConverter.DirectDrawSurfaceUtilities
{
    /// <summary>
    /// Provides static method for checking validity of DirectDraw Surface files.
    /// </summary>
    public static class DirectDrawSurface
    {
        /// <summary>
        /// Magic number of every file.
        /// </summary>
        private const int Magic = 0x20534444;

        /// <summary>
        /// Size of the metadata in header for DXT1, DXT3, DXT5, and A8R8B8G8 DirectDraw Surface files.
        /// </summary>
        private const int MetaSize = 0x7C;

        /// <summary>
        /// Size of header for DXT1, DXT3, DXT5, and A8R8B8G8 DirectDraw Surface files; this includes magic number.
        /// </summary>
        private const int HeaderSize = MetaSize + 4;

        /// <summary>
        /// Checks if file is valid DirectDraw Surface texture file.
        /// </summary>
        /// <param name="path">Real filesystem path to file.</param>
        /// <returns>True or false if file is valid.</returns>
        public static bool IsValid(string path)
        {
            using (var reader = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                if (reader.ReadInt32() != DirectDrawSurface.Magic || reader.ReadInt32() != DirectDrawSurface.MetaSize || reader.BaseStream.Length < DirectDrawSurface.HeaderSize)
                {
                    return false;
                }

                return true;
            }
        }
    }
}