using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugGUI
{
    public partial class NewGameDirectoryForm : Form
    {
        public GamesDirectory NewGamesDirectory;

        public NewGameDirectoryForm()
        {
            InitializeComponent();
        }

        void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult dialogResult = folderDialog.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                pathBox.Text = folderDialog.SelectedPath;
            }
        }

        void addDirectoryButton_Click(object sender, EventArgs e)
        {
            // NOTE(SpectatorQL): Perhaps adding MessageBoxes with warnings is a good idea?
            if(nameBox.Text != ""
                && pathBox.Text != ""
                && nameBox.Text != null
                && pathBox.Text != null)
            {
                string[] extensions;
                if(extensionsBox.Text != "")
                {
                    extensions = extensionsBox.Text.Split(';');
                    for(int i = 0;
                        i < extensions.Length;
                        ++i)
                    {
                        if(extensions[i][0] != '.')
                        {
                            extensions[i] = '.' + extensions[i];
                        }
                    }
                }
                else
                {
                    extensions = new string[0];
                }

                NewGamesDirectory = new GamesDirectory()
                {
                    Name = nameBox.Text,
                    DirectoryInfo = new DirectoryInfo(pathBox.Text),
                    Extensions = extensions,
                };

                Close();
            }
        }
    }
}
