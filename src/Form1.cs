using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugGUI
{
    // TODO(SpectatorQL): Customizable paths.
    public partial class Form1 : Form
    {
        TextBox[] _netplayTextBoxes;

        public Form1()
        {
            InitializeComponent();

            _netplayTextBoxes = new TextBox[]
            {
                hostText,
                portText,
                nickText,
                gamekeyText
            };

            // TODO(SpectatorQL): FileSystemWatcher!!!
            DirectoryInfo gameDirInfo = new DirectoryInfo(@"E:\ISOs\PSX");
            FileInfo[] cueFiles = gameDirInfo.GetFiles("*.cue", SearchOption.AllDirectories);
            gameList.DataSource = cueFiles;
        }

        void startButton_Click(object sender, EventArgs e)
        {
            string argv = "";
            string mednafenPath = @"D:\mednafen-1.22.2-win64\mednafen.exe";
            string[] netplayArgs =
            {
                "-netplay.hostname",
                "-netplay.port",
                "-netplay.nick",
                "-netplay.gamekey"
            };
            
            StringBuilder sb = new StringBuilder();
            if(netplayCheckBox.Checked)
            {
                for(int i = 0;
                    i < _netplayTextBoxes.Length;
                    ++i)
                {
                    sb.Append(netplayArgs[i]);
                    sb.Append(" ");
                    sb.Append(_netplayTextBoxes[i].Text);
                }
            }

            sb.Append(" ");
            sb.Append("\"");
            sb.Append(((FileInfo)gameList.SelectedItem).FullName);
            sb.Append("\"");
            argv = sb.ToString();

            Process.Start(mednafenPath, argv);
        }

        void netplayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            for(int i = 0;
                i < _netplayTextBoxes.Length;
                ++i)
            {
                _netplayTextBoxes[i].Enabled = box.Checked ? true : false;
            }
        }
    }
}
