using ArchiveInterop.Classes;
using NormalMapConverter.DirectDrawSurfaceUtilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Zlib;

namespace ArchiveInterop
{
    public static class BSA
    {
        /// <summary>
        /// Writes BSA for Oblivion PS3.
        /// </summary>
        /// <param name="path">Real filesystem path to write BSA to.</param>
        /// <param name="assetList">List of <see cref="Asset"/> to write in BSA.</param>
        /// <param name="compress">True or false if assets in BSA should be compressed.</param>
        /// <param name="usePS3FileFlags">True or false if PS3 file flags should be used.</param>
        /// <param name="extendDDS">True or false if DDS texture file data should be extended.</param>
        /// <param name="convertNormalMaps">True or false if normal maps should be converted to PS3.</param>
        public static void Write(string path, List<Asset> assetList, bool compress, bool usePS3FileFlags, bool extendDDS, bool convertNormalMaps)
        {
            var folderDict = new Dictionary<string, List<Asset>>();
            var extList = new List<string>();
            uint totalFolderNameLength = 0;
            uint totalFileNameLength = 0;

            // Sets up all assets to be in folderDict
            foreach (Asset asset in assetList)
            {
                string folderName = Path.GetDirectoryName(asset.EntryStr);

                if (!folderDict.ContainsKey(folderName))
                {
                    folderDict.Add(folderName, new List<Asset>());
                    totalFolderNameLength += (uint)folderName.Length + 1; // Includes null terminator, not prefixed length sbyte
                }

                totalFileNameLength += (uint)asset.FileName.Length + 1; // Includes null terminator
                folderDict[folderName].Add(asset);
                if (!extList.Contains(asset.Extension)) extList.Add(asset.Extension);
            }

            // Begins writing of archive
            using (var writer = new BinaryWriter(File.Open(path, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                writer.Write(0x00415342); // "BSA" + 0x00, archive magic
                writer.Write(103); // Version
                writer.Write(36); // Folder records offset
                writer.Write(compress ? 0x00000707 : 0x00000703);
                writer.Write(folderDict.Count());
                writer.Write(assetList.Count());
                writer.Write(totalFolderNameLength);
                writer.Write(totalFileNameLength);
                writer.Write(BSA.GetFileFlags(extList, usePS3FileFlags)); // Texture archive file flags

                var folderOrdList = new List<string>(); // Keeps order of folders, with folder names
                var folderOffList = new List<uint>();

                // Loops through the folder dictionary, while also organizing folders to follow Oblivion BSA rules
                foreach (KeyValuePair<string, List<Asset>> folder in folderDict.OrderBy(folder => OblivionBSAHash.GetPC(folder.Key)))
                {
                    folderOrdList.Add(folder.Key);
                    folder.Value.Sort((a, b) => a.Hash.CompareTo(b.Hash)); // Assets in folder sorted to follow Oblivion BSA rules

                    writer.Write(OblivionBSAHash.GetPC(folder.Key));
                    writer.Write(folder.Value.Count());
                    writer.Write(-1); // Temporary, file record offset
                }

                foreach (string folder in folderOrdList)
                {
                    folderOffList.Add((uint)writer.BaseStream.Position);
                    writer.Write((byte)(folder.Length + 1));
                    writer.Write(folder.ToCharArray());
                    writer.Write((byte)0);

                    foreach (Asset asset in folderDict[folder])
                    {
                        writer.Write(asset.Hash);
                        writer.Write((long)-1); // Temporary, asset size + offset
                    }
                }

                foreach (string folder in folderOrdList)
                {
                    foreach (Asset asset in folderDict[folder])
                    {
                        writer.WriteNullTerminatedString(asset.FileName);
                    }
                }

                foreach (string folder in folderOrdList)
                {
                    foreach (Asset asset in folderDict[folder])
                    {
                        asset.Offset = (uint)writer.BaseStream.Position;
                        byte[] data;

                        if (convertNormalMaps && asset.IsDDS && asset.IsNormalMap)
                        {
                            string tempPath = Path.GetTempFileName();
                            bool converted = NormalMap.ConvertToPS3(asset.RealPath, tempPath);
                      
                            data = File.ReadAllBytes(converted ? tempPath : asset.RealPath);
                            File.Delete(tempPath); // Cleans up
                        }
                        else
                        {
                            data = File.ReadAllBytes(asset.RealPath);
                        }

                        bool extendAsset = extendDDS && asset.IsDDS && !asset.IsExtended;

                        if (compress)
                        {
                            asset.OriginalSize = extendAsset ? (uint)data.Length + (uint)asset.EntryStr.Length + 1 : (uint)data.Length;

                            writer.Write(asset.OriginalSize);
                            writer.Write(BSA.GetCompressedZlibData(data, asset, extendAsset));
                        }
                        else
                        {
                            asset.Size = (uint)data.Length;
                            writer.Write(data);

                            if (extendAsset)
                            {
                                asset.Size += (uint)asset.EntryStr.Length + 1; // +1 is accounting for null terminator
                                writer.Write(asset.EntryStr);
                            }
                        }
                    }
                }

                writer.BaseStream.Position = 36; // Folder record offset
                foreach (uint off in folderOffList)
                {
                    writer.BaseStream.Position += 12; // Skips hash and file count
                    writer.Write(off + totalFileNameLength);
                }

                foreach (string folder in folderOrdList)
                {
                    writer.BaseStream.Position += folder.Length + 2;

                    foreach (Asset asset in folderDict[folder])
                    {
                        writer.BaseStream.Position += 8; // Skips hash

                        writer.Write(asset.Size);
                        writer.Write(asset.Offset);
                    }
                }
            }
        }

        /// <summary>
        /// Compressed data using the zlib compression library.
        /// </summary>
        /// <param name="data">Data to be compressed.</param>
        /// <param name="asset">Asset class that is associated with that data.</param>
        /// <param name="extendData">True or false if file data should be extendeed</param>
        /// <returns>Compressed data</returns>
        private static byte[] GetCompressedZlibData(byte[] data, Asset asset, bool extendData)
        {
            if (data == null || data.Length == 0) 
                return data;

            if (extendData)
            {
                // Length byte + entry string
                byte[] extensionData = (new byte[1] { (byte)asset.EntryStr.Length }).Concat(Encoding.ASCII.GetBytes(asset.EntryStr.ToLower())).ToArray();
                data = data.Concat(extensionData).ToArray();
            }

            using (var inStream = new MemoryStream(data))
            {
                var outStream = new MemoryStream();
                var compressStream = new ZlibStream(outStream, System.IO.Compression.CompressionLevel.Optimal, true);

                int bufferSize;
                var buffer = new byte[4096];

                while ((bufferSize = inStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    compressStream.Write(buffer, 0, bufferSize);
                }

                compressStream.Close();
                byte[] outStreamData = outStream.ToArray();

                asset.Size = (uint)outStreamData.Length + 4;
                return outStreamData;
            }
        }

        /// <summary>
        /// Generates file flags for BSA and returns them.
        /// </summary>
        /// <param name="extensionList">List of file extensions.</param>
        /// <param name="usePS3FileFlags">True or false if PS3 file flags should be used instead of PC.</param>
        /// <returns>File flags for BSA header.</returns>
        private static uint GetFileFlags(List<string> extensionList, bool usePS3FileFlags)
        {
            uint fileFlags = usePS3FileFlags ? 0xCDCD0000 : 0x00000000;

            foreach (string ext in extensionList)
            {
                switch (ext.Substring(1))
                {
                    case "ctl":
                    default: // Miscellaneous
                        {
                            fileFlags |= 1 << 8;
                            break;
                        }

                    case "nif":
                        {
                            fileFlags |= 1 << 0;
                            break;
                        }

                    case "dds":
                        {
                            fileFlags |= 1 << 1;
                            break;
                        }

                    case "xml":
                        {
                            fileFlags |= 1 << 2;
                            break;
                        }

                    case "wav":
                        {
                            fileFlags |= 1 << 3;
                            break;
                        }

                    case "mp3":
                        {
                            fileFlags |= 1 << 4;
                            break;
                        }

                    case "txt":
                    case "html":
                    case "bat":
                    case "scc":
                        {
                            fileFlags |= 1 << 5;
                            break;
                        }

                    case "spt":
                    case "stg":
                        {
                            fileFlags |= 1 << 6;
                            break;
                        }

                    case "tex":
                    case "fnt":
                        {
                            fileFlags |= 1 << 7;
                            break;
                        }
                }
            }

            return fileFlags;
        }
    }
}
