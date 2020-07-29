using System.IO;

namespace ArchiveInterop.Classes
{
    /// <summary>
    /// Provides static extension method for adding functionality to BinaryWriter streams.
    /// </summary>
    public static class BinaryWriterExtensions
    {
        /// <summary>
        /// Writes string in null-terminated format in BinaryWriter stream.
        /// </summary>
        /// <param name="writer">Strea to write in.</param>
        /// <param name="str">String to write.</param>
        public static void WriteNullTerminatedString(this BinaryWriter writer, string str)
        {
            writer.Write((str + '\0').ToCharArray());
        }
    }
}