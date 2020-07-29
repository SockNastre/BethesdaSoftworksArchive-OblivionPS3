using ArchiveInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PackerCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            // If there are no arguments passed then it acts as if help command is ran
            if (args.Count() == 0)
            {
                Program.ShowHelpText();
                return;
            }

            switch (args[0].ToLower())
            {
                default:
                    {
                        if (args.Count() == 1 && Directory.Exists(args[0]))
                        {
                            // If a single existing folder is inputted, the tool will autopack that
                            // The following codes sets up that process

                            string inputDir = args[0];
                            args = new string[6];
                            args[0] = string.Empty; // Can equal blank string value
                            args[1] = "-useps3settings";

                            args[2] = "-i";
                            args[3] = inputDir;

                            args[4] = "-o";
                            args[5] = Path.GetDirectoryName(inputDir) + "\\output.bsa";

                            goto case "-p";
                        }

                        Program.ShowHelpText(true);
                        break;
                    }

                case "-h":
                case "-help":
                    {
                        Program.ShowHelpText();
                        break;
                    }

                case "-p":
                case "-pack":
                    {
                        string inputDir = string.Empty;
                        string outputPath = string.Empty;

                        bool compress = false;
                        bool usePS3FileFlags = false;
                        bool extendDDS = false;
                        bool convertNormalMaps = false;

                        for (uint i = 1; i < args.Count(); i++)
                        {
                            string option = args[i].ToLower();

                            switch (option)
                            {
                                default:
                                    {
                                        Program.ShowHelpText(true);
                                        return;
                                    }

                                case "-compress":
                                    {
                                        compress = true;
                                        break;
                                    }

                                case "-useps3fileflags":
                                    {
                                        usePS3FileFlags = true;
                                        break;
                                    }

                                case "-extenddds":
                                    {
                                        extendDDS = true;
                                        break;
                                    }

                                case "-convertnormals":
                                    {
                                        convertNormalMaps = true;
                                        break;
                                    }

                                case "-useps3settings":
                                    {
                                        usePS3FileFlags = true;
                                        extendDDS = true;
                                        convertNormalMaps = true;
                                        break;
                                    }

                                case "-i":
                                case "-indir":
                                    {
                                        i++;
                                        inputDir = args[i];
                                        break;
                                    }

                                case "-o":
                                case "-out":
                                    {
                                        i++;
                                        outputPath = args[i];
                                        break;
                                    }
                            }
                        }

                        if (string.IsNullOrEmpty(inputDir) || string.IsNullOrEmpty(outputPath))
                        {
                            Program.ShowHelpText(true);
                            return;
                        }

                        Console.WriteLine("Reading and verifying files...");
                        var assetList = new List<Asset>();

                        foreach (string file in Directory.GetFiles(inputDir, "*", SearchOption.AllDirectories))
                        {
                            var assetFile = new Asset(file.Substring(inputDir.Length + 1), file);
                            assetList.Add(assetFile);
                        }

                        Console.WriteLine("Packing...");
                        BSA.Write(outputPath, assetList, compress, usePS3FileFlags, extendDDS, convertNormalMaps);

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

            Console.WriteLine("BethesdaSoftworksArchive OblivionPS3 Packer Cli\nCopyright (c) 2020  SockNastre\nVersion: 1.0.0.1\n\n" +
                ".NET Zlib Implementation\nLink: https://www.codeproject.com/Tips/830793/NET-ZLib-Implementation \nLicense (CPOL): https://www.codeproject.com/info/cpol10.aspx \n\n" +
                "Magick.NET\nCopyright 2013-2020 Dirk Lemstra\nLicense (Apache 2.0): https://github.com/dlemstra/Magick.NET/blob/master/License.txt \n\n" + new string('-', 50) +
                "\n\nUsage: \"BSA OblivionPS3 Packer Cli.exe\" <Command> <Options>\n\nCommands:\n-pack (-p)\n-help (-h)\n\n" +
                "Pack Options:\n-useps3settings\n-useps3fileflags (PS3)\n-extenddds (PS3)\n-convertnormals (PS3)\n-compress\n-indir (-i)\n-out (-o)\n\n" +
                "Examples:\n\n\"BSA OblivionPS3 Packer Cli.exe\" -pack -useps3settings -compress -i \"C:\\Data\" -o \"C:\\ps3output.bsa\"\n" +
                new string(' ', 33) + "-pack -compress -i \"C:\\Data\" -o \"C:\\output.bsa\"\n\n");
        }
    }
}