using PackerGUI.Classes;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PackerGUI
{
    public partial class Settings : Form
    {
        private Ini SettingsIni;

        public Settings(Ini settingsIni)
        {
            InitializeComponent();

            this.SettingsIni = settingsIni;
            checkBoxCompressAssets.Checked = bool.Parse(settingsIni.Data["Archive"]["CompressAssets"]);
            checkBoxUsePS3FileFlags.Checked = bool.Parse(settingsIni.Data["Archive"]["UsePS3FileFlags"]);
            checkBoxExtendDDS.Checked = bool.Parse(settingsIni.Data["DDS"]["ExtendData"]);
            checkBoxConvertNormalMaps.Checked = bool.Parse(settingsIni.Data["DDS"]["ConvertNormalMaps"]);
        }

        private void Settings_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            MessageBox.Show("\"Compress assets\" - Compresses assets inside of BSA using zlib, may decrease BSA size but increase loading times.\n\n" +
                "\"Use PS3 file flags\" - Use PS3 file flags opposed to PC file flags, on PS3 one or two BSAs don't use PS3 file flags.\n\n" +
                "\"Extend data\" - Extends data for DDS files.\n\n" +
                "\"Convert normal maps\" - Converts normal maps from PC variant to PS3 variant.\n\n" +
                "DISCLAIMER: The last 3 settings should be turned on when packing for PS3 except under specific circumstances! Turning those settings " +
                "off may result in BSAs that don't load right on PS3.",
                "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonResetSettings_Click(object sender, EventArgs e)
        {
            checkBoxCompressAssets.Checked = false;
            checkBoxUsePS3FileFlags.Checked = true;
            checkBoxExtendDDS.Checked = true;
            checkBoxConvertNormalMaps.Checked = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.SettingsIni.Data["Archive"]["CompressAssets"] = checkBoxCompressAssets.Checked.ToString();
            this.SettingsIni.Data["Archive"]["UsePS3FileFlags"] = checkBoxUsePS3FileFlags.Checked.ToString();
            this.SettingsIni.Data["DDS"]["ExtendData"] = checkBoxExtendDDS.Checked.ToString();
            this.SettingsIni.Data["DDS"]["ConvertNormalMaps"] = checkBoxConvertNormalMaps.Checked.ToString();

            this.Close();
        }
    }
}