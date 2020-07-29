using NormalMapConverter.DirectDrawSurfaceUtilities;
using System;
using System.IO;
using System.Linq;

namespace NormalMapConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            // If there are no arguments passed then it acts as if help command is ran but through an invalid usage
            if (args.Count() == 0)
            {
                Program.ShowHelpText(true);
                return;
            }

            switch (args[0].ToLower())
            {
                default:
                    {
                        // Allows for drag & drop of file/folders to be converted
                        if (args.Count() == 1)
                        {
                            if (File.Exists(args[0]))
                            {
                                string input = args[0];
                                args = new string[3];
                                args[0] = string.Empty; // Can equal blank string value

                                args[1] = "-in";
                                args[2] = input;

                                goto case "-c";
                            }
                        }

                        Program.ShowHelpText(true);
                        return;
                    }

                case "-h":
                case "-help":
                    {
                        Program.ShowHelpText();
                        return;
                    }

                case "-c":
                case "-convert":
                    {
                        string input = null;
                        string output = null;

                        for (uint i = 1; i < args.Count(); i++)
                        {
                            string option = args[i];

                            switch (option)
                            {
                                default:
                                    {
                                        Program.ShowHelpText(true);
                                        return;
                                    }

                                case "-in":
                                    {
                                        i++;
                                        input = args[i];

                                        break;
                                    }

                                case "-out":
                                    {
                                        i++;
                                        output = args[i];

                                        break;
                                    }
                            }
                        }

                        if (string.IsNullOrEmpty(input))
                        {
                            Program.ShowHelpText(true);
                            return;
                        }

                        Console.WriteLine("Converting " + Path.GetFileName(input) + "...");
                        NormalMap.ConvertToPS3(input, string.IsNullOrEmpty(output) ? input : output);

                        Console.WriteLine("\nDone!\n");
                        break;
                    }
            }
        }

        private static void ShowHelpText(bool isInvalidUsage = false)
        {
            if (isInvalidUsage)
            {
                Console.WriteLine("Invalid usage\n");
            }

            Console.WriteLine("NormalMapConverter\nCopyright (c) 2020  SockNastre\nVersion: 1.0.0.0\n\n" +
                "Magick.NET\nCopyright 2013-2020 Dirk Lemstra\nLicense (Apache 2.0): https://github.com/dlemstra/Magick.NET/blob/master/License.txt \n\n" +
                new string('-', 50) + "\n\nUsage: NormalMapConverter.exe <Command> <Options>\n\nCommands:\n-convert (-c)\n-help (-h)\n\n" +
                "Convert Options:\n-in\n-out\n\nExamples:\n\nNormalMapConverter.exe -c -in \"C:\\oblivionPC_n.dds\" -out \"C:\\oblivionPS3_n.dds\"\n\n");
        }
    }
}