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
    public enum DirectoryFormIntent
    {
        Add,
        Edit,
    }

    public enum DirectoryFormResult
    {
        Canceled,
        Added,
        Edited,
    }

    public partial class GameDirectoryForm : Form
    {
        public GamesDirectory Directory;
        DirectoryFormIntent IntentID;
        public DirectoryFormResult ResultID;

        public GameDirectoryForm(DirectoryFormIntent intent, GamesDirectory directory)
        {
            InitializeComponent();
            IntentID = intent;
            switch(intent)
            {
                case DirectoryFormIntent.Add:
                {
                    break;
                }

                case DirectoryFormIntent.Edit:
                {
                    Directory = directory;

                    nameBox.Text = directory.Name;
                    pathBox.Text = directory.DirectoryInfo.FullName;

                    string extensionsString = "";
                    for(int i = 0;
                        i < directory.Extensions.Length;
                        ++i)
                    {
                        extensionsString += directory.Extensions[i] + ';';
                    }
                    extensionsBox.Text = extensionsString;
                    break;
                }

                default:
                {
                    throw new NullReferenceException("Invalid default case!");
                    break;
                }
            }
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
            if(IntentID == DirectoryFormIntent.Add)
            {
                if(nameBox.Text != ""
                    && pathBox.Text != ""
                    && nameBox.Text != null
                    && pathBox.Text != null)
                {
                    string[] extensions;
                    if(extensionsBox.Text != "")
                    {
                        extensions = extensionsBox.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
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

                    Directory = new GamesDirectory()
                    {
                        Name = nameBox.Text,
                        DirectoryInfo = new DirectoryInfo(pathBox.Text),
                        Extensions = extensions,
                    };

                    ResultID = DirectoryFormResult.Added;
                    Close();
                }
            }
            else if(IntentID == DirectoryFormIntent.Edit)
            {
                if(nameBox.Text != ""
                    && pathBox.Text != ""
                    && nameBox.Text != null
                    && pathBox.Text != null)
                {
                    Directory.Name = nameBox.Text;

                    if(pathBox.Text != Directory.DirectoryInfo.FullName)
                    {
                        Directory.DirectoryInfo = new DirectoryInfo(pathBox.Text);
                    }

                    string[] newExtensions;
                    if(extensionsBox.Text != "")
                    {
                        newExtensions = extensionsBox.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        for(int i = 0;
                            i < newExtensions.Length;
                            ++i)
                        {
                            if(newExtensions[i][0] != '.')
                            {
                                newExtensions[i] = '.' + newExtensions[i];
                            }
                        }
                    }
                    else
                    {
                        newExtensions = new string[0];
                    }
                    Directory.Extensions = newExtensions;

                    ResultID = DirectoryFormResult.Edited;
                    Close();
                }
            }
        }
    }
}
