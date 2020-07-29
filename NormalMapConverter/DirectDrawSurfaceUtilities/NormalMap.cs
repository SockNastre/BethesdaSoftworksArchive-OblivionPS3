using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NormalMapConverter.DirectDrawSurfaceUtilities
{
    /// <summary>
    /// Provides static method for converting PC normal maps to PS3.
    /// </summary>
    public static class NormalMap
    {
        /// <summary>
        /// Converts PC Oblivion normal map to PS3 variant.
        /// </summary>
        /// <param name="path">Path to source normal map.</param>
        /// <param name="outputPath">Where to save new normal map.</param>
        /// <returns>True or false if the function was able to convert.</returns>
        public static bool ConvertToPS3(string path, string outputPath)
        {
            if (!DirectDrawSurface.IsValid(path))
            {
                Console.WriteLine("ERROR: Invalid DDS texture file.");
                return false;
            }

            var normalMap = new MagickImage(path);
            if (normalMap.IsConvertedToPS3())
            {
                Console.WriteLine("ERROR: Channels already converted.");
                return false;
            }

            List<IMagickImage<ushort>> channelList = normalMap.Separate().ToList();
            var newChannelCollection = new MagickImageCollection();

            // Turns blue channel null for later usage, because blue channel is never used
            channelList[2].MakeNull();

            // A8R8G8B8 and DXT3 (both support alpha) will convert to DXT5
            if (normalMap.HasAlpha)
            {
                newChannelCollection.Add(channelList[3]); // ALPHA
                newChannelCollection.Add(channelList[1]); // GREEN
                newChannelCollection.Add(channelList[2]); // NULL
                newChannelCollection.Add(channelList[0]); // RED
            }
            else
            {
                newChannelCollection.Add(channelList[2]); // NULL
                newChannelCollection.Add(channelList[1]); // GREEN
                newChannelCollection.Add(channelList[0]); // RED
            }

            var convertedImage = new MagickImage(newChannelCollection.Combine())
            {
                HasAlpha = normalMap.HasAlpha,
                Depth = normalMap.Depth
            };

            convertedImage.Write(outputPath, MagickFormat.Dds);
            return true;
        }

        /// <summary>
        /// Checks if normal map is already converted to PS3 format.
        /// </summary>
        /// <param name="dds">A DXT1/DXT3/DXT5/A8R8G8B8 DDS format normal map MagickImage.</param>
        /// <returns>True or false if the normal map has been converted to PS3 already.</returns>
        private static bool IsConvertedToPS3(this MagickImage dds)
        {
            List<IMagickImage<ushort>> clonedChannelsList = new MagickImage(dds.Clone()).Separate().ToList();
            clonedChannelsList[1].MakeNull(); // Makes green channel null for upcoming comparison, because green channel is untouched
            int nullCheckIndex = dds.HasAlpha ? 2 : 0;

            return clonedChannelsList[nullCheckIndex].Equals(clonedChannelsList[1]);
        }

        /// <summary>
        /// Makes channel null (all black).
        /// </summary>
        /// <param name="channel">Channel to be nullified.</param>
        private static void MakeNull(this IMagickImage<ushort> channel)
        {
            channel.ColorFuzz = (Percentage)100;
            channel.Settings.FillColor = MagickColors.Black;
            channel.Draw(new DrawableAlpha(0, 0, PaintMethod.Floodfill));
        }
    }
}