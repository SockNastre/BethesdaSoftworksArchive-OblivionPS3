using System.IO;

namespace ArchiveInterop
{
    /// <summary>
    /// Contains metadata that aids with the BSA packing process.
    /// </summary>
    public class Asset
    {
        public string EntryStr { get; set; }
        public string RealPath { get; set; }
        public bool IsDDS { get; set; }
        public bool IsExtended { get; set; }

        // These fields get set during packing
        public uint Size { get; set; }
        public uint OriginalSize { get; set; }
        public uint Offset { get; set; }

        public string FileName => Path.GetFileName(EntryStr);
        public string FileNameNoExtension => Path.ChangeExtension(FileName, null);
        public string Extension => Path.GetExtension(FileName);
        public ulong Hash => this.IsDDS ? OblivionBSAHash.GetPS3(FileName) : OblivionBSAHash.GetPC(FileNameNoExtension, Extension);
        public bool IsNormalMap => this.IsDDS && FileNameNoExtension.EndsWith("_n");

        /// <summary>
        /// Creates asset class with entry string and real filesystem path fields set.
        /// </summary>
        /// <param name="entryStr">Path to be written in BSA.</param>
        /// <param name="realPath">Real filesystem path to find asset.</param>
        public Asset(string entryStr, string realPath)
        {
            this.EntryStr = entryStr;
            this.RealPath = realPath;

            // Checks DDS-related things about the asset
            if (this.Extension.Equals(".dds"))
            {
                this.IsDDS = true;

                using (var reader = new BinaryReader(File.Open(this.RealPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    int entryStrLen = entryStr.Length;

                    // Goes to where the extended entry string data start may be
                    reader.BaseStream.Position = reader.BaseStream.Length - entryStrLen - 1;

                    if (reader.ReadByte() == entryStrLen && new string(reader.ReadChars(entryStrLen)).Equals(entryStr))
                    {
                        this.IsExtended = true;
                    }
                }
            }
        }
    }
}