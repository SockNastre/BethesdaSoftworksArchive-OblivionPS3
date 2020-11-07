using System.IO;

namespace ArchiveInterop.Classes
{
    /// <summary>
    /// Provides static extension methods to add functionality to BinaryWriter.
    /// </summary>
    public static class BinaryWriterExtensions
    {
        /// <summary>
        /// Writes string in null-terminated format in BinaryWriter.
        /// </summary>
        /// <param name="writer">BinaryWriter to write in.</param>
        /// <param name="str">String to write.</param>
        public static void WriteNullTerminatedString(this BinaryWriter writer, string str)
        {
            writer.Write((str + '\0').ToCharArray());
        }
    }
}
