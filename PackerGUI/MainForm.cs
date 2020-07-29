using ArchiveInterop;
using PackerGUI.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackerGUI
{
    public partial class MainForm : Form
    {
        private Ini SettingsIni;
        private string ArchiveSavePath;
        private bool IsArchiveSaved = true;
        private bool IsPackingCurrently = false;

        public MainForm()
        {
            InitializeComponent();

            string iniPath = AppDomain.CurrentDomain.BaseDirectory + "\\BSA OblivionPS3 Packer.ini";
            Ini settingsIni;

            // All code following is for managing the ini configuration file used by the tool
            if (File.Exists(iniPath))
            {
                settingsIni = new Ini(iniPath);

                if (this.IsIniValid(settingsIni))
                {
                    this.SettingsIni = settingsIni;
                    return;
                }
                else
                {
                    File.Delete(iniPath);
                }
            }

            // When all else fails, code comes here and just remakes the ini file
            settingsIni = new Ini();
            settingsIni.Data.Add("Archive", new Dictionary<string, string> { { "CompressAssets", "False" }, { "UsePS3FileFlags", "True" } });
            settingsIni.Data.Add("DDS", new Dictionary<string, string> { { "ExtendData", "True" }, { "ConvertNormalMaps", "True" } });
            settingsIni.WriteToFile(iniPath);

            this.SettingsIni = settingsIni;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsPackingCurrently)
            {
                DialogResult abortMessage = MessageBox.Show("Archive not finished packing, are you sure you want to exit?",
                    string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                e.Cancel = abortMessage == DialogResult.No;
            }
            else if (!IsArchiveSaved)
            {
                DialogResult exitMessage = MessageBox.Show("Archive not saved, are you sure you want to exit?",
                    string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                e.Cancel = exitMessage == DialogResult.No;
            }

            // Saves ini file
            string iniPath = AppDomain.CurrentDomain.BaseDirectory + "\\BSA OblivionPS3 Packer.ini";
            this.SettingsIni.WriteToFile(iniPath);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ResetForm();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Asset> assetList = listViewAssets.ConvertItemsToAssetList();
            this.PackBSA(this.ArchiveSavePath, assetList);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialogBSA.ShowDialog() == DialogResult.OK)
            {
                this.ArchiveSavePath = saveFileDialogBSA.FileName;
                saveToolStripMenuItem.Enabled = true;

                saveToolStripMenuItem.PerformClick();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogAsset.ShowDialog() == DialogResult.OK)
            {
                listViewAssets.BeginUpdate();
                this.AddAssets(openFileDialogAsset.FileNames);

                listViewAssets.EndUpdate();
                this.SetSaveButtonsEnableState();
            }
        }

        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folderDialog = new OpenFolderDialog();

            if (folderDialog.ShowDialog(this) == DialogResult.OK)
            {
                listViewAssets.BeginUpdate();
                this.AddDirectoryAssets(folderDialog.Folder);

                listViewAssets.EndUpdate();
                this.SetSaveButtonsEnableState();
            }
        }

        private void removeFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsPackingCurrently)
                return;

            foreach (ListViewItem item in listViewAssets.SelectedItems)
            {
                listViewAssets.Items.RemoveAt(item.Index);
            }

            this.SetSaveButtonsEnableState();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listViewAssets.SelectAll();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settingsForm = new Settings(this.SettingsIni);
            settingsForm.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new About();
            aboutForm.ShowDialog();
        }

        private void listViewAssets_DragDrop(object sender, DragEventArgs e)
        {
            if (IsPackingCurrently)
                return;

            listViewAssets.BeginUpdate();

            string dir = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            if (Directory.Exists(dir))
            {
                this.AddDirectoryAssets(dir);
            }
            else
            {
                var assets = (string[])e.Data.GetData(DataFormats.FileDrop);
                this.AddAssets(assets);
            }

            listViewAssets.EndUpdate();
            this.SetSaveButtonsEnableState();
        }

        private void listViewAssets_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = IsPackingCurrently ? DragDropEffects.None : DragDropEffects.Copy;
        }

        private void listViewAssets_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeFilesToolStripMenuItem.Enabled = listViewAssets.SelectedItems.Count > 0 ? true : false;
        }

        private bool IsIniValid(Ini ini)
        {
            try
            {
                string val = ini.Data["Archive"]["CompressAssets"];
                bool.Parse(val);

                val = ini.Data["Archive"]["UsePS3FileFlags"];
                bool.Parse(val);

                val = ini.Data["DDS"]["ExtendData"];
                bool.Parse(val);

                val = ini.Data["DDS"]["ConvertNormalMaps"];
                bool.Parse(val);

                // If the above code works without exceptions, the ini is valid for this tool
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ResetForm()
        {
            this.ArchiveSavePath = string.Empty;
            this.IsArchiveSaved = true;
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            removeFilesToolStripMenuItem.Enabled = false;
            listViewAssets.Items.Clear();
        }

        private void SetSaveButtonsEnableState()
        {
            if (listViewAssets.Items.Count > 0)
            {
                saveToolStripMenuItem.Enabled = !string.IsNullOrEmpty(this.ArchiveSavePath);
                saveAsToolStripMenuItem.Enabled = true;
            }
            else
            {
                saveToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
                IsArchiveSaved = true;
            }
        }

        private void AddAsset(string path, string entryStr)
        {
            if (listViewAssets.FindItemWithText(entryStr) != null)
                return;

            var item = new ListViewItem
            {
                Text = entryStr
            };

            item.SubItems.Add(path);
            listViewAssets.Items.Add(item);
        }

        private void AddAssets(string[] assetPaths)
        {
            int initialItemCount = listViewAssets.Items.Count;
            bool initialArchiveSavedState = this.IsArchiveSaved;

            foreach (string asset in assetPaths)
            {
                this.AddAsset(asset, Path.GetFileName(asset));
            }

            // If the ListView Item count is the same, nothing was added so save state is the same
            this.IsArchiveSaved = initialItemCount == listViewAssets.Items.Count ? initialArchiveSavedState : false;
        }

        private void AddDirectoryAssets(string dir)
        {
            int initialItemCount = listViewAssets.Items.Count;
            bool initialArchiveSavedState = this.IsArchiveSaved;

            int subLength = dir.Length + 1; // +1 accounts for an extra backslash

            foreach (string file in Directory.GetFiles(dir, "*", SearchOption.AllDirectories))
            {
                this.AddAsset(file, file.Substring(subLength));
            }

            // If the ListView Item count is the same, nothing was added so save state is the same
            this.IsArchiveSaved = initialItemCount == listViewAssets.Items.Count ? initialArchiveSavedState : false;
        }

        private async void PackBSA(string path, List<Asset> assetList)
        {
            string formText = this.Text;
            this.Text = "Packing...";
            this.IsPackingCurrently = true;
            menuStripMain.Enabled = false;

            bool compress = bool.Parse(SettingsIni.Data["Archive"]["CompressAssets"]);
            bool usePS3FileFlags = bool.Parse(SettingsIni.Data["Archive"]["UsePS3FileFlags"]);
            bool extendDDS = bool.Parse(SettingsIni.Data["DDS"]["ExtendData"]);
            bool convertNormalMaps = bool.Parse(SettingsIni.Data["DDS"]["ConvertNormalMaps"]);

            await Task.Run(() => BSA.Write(path, assetList, compress, usePS3FileFlags, extendDDS, convertNormalMaps));

            this.IsArchiveSaved = true;
            this.IsPackingCurrently = false;
            menuStripMain.Enabled = true;
            this.Text = formText;
            MessageBox.Show("Done!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}