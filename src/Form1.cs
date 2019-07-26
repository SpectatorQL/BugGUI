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
    public partial class Form1 : Form
    {
        TextBox[] NetplayTextBoxes;
        StringBuilder ArgvStringBuilder = new StringBuilder();

        GameListForm GameListForm;
        string SelectedGamePath;

        Config Config = new Config();

        public Form1()
        {
            InitializeComponent();

            NetplayTextBoxes = new TextBox[]
            {
                hostText,
                portText,
                nickText,
                gamekeyText
            };
        }

        void startButton_Click(object sender, EventArgs e)
        {
            if(SelectedGamePath != null || SelectedGamePath != "")
            {
                string argv = "";
#if DEBUG
                string mednafenPath = @"D:\mednafen-1.22.2-win64\mednafen.exe";
#else
                string mednafenPath = "mednafen.exe";
#endif
                string[] netplayArgs =
                {
                    "-netplay.host",
                    "-netplay.port",
                    "-netplay.nick",
                    "-netplay.gamekey"
                };

                if(netplayCheckBox.Checked)
                {
                    ArgvStringBuilder.Append(" -connect");
                    for(int i = 0;
                        i < NetplayTextBoxes.Length;
                        ++i)
                    {
                        ArgvStringBuilder.Append(" ");
                        ArgvStringBuilder.Append(netplayArgs[i]);
                        ArgvStringBuilder.Append(" ");
                        ArgvStringBuilder.Append(NetplayTextBoxes[i].Text);
                    }
                }

                ArgvStringBuilder.Append(" \"");
                ArgvStringBuilder.Append(SelectedGamePath);
                ArgvStringBuilder.Append("\"");

                argv = ArgvStringBuilder.ToString();
                ArgvStringBuilder.Clear();
                
                Process mednafen = Process.Start(mednafenPath, argv);
                mednafen.WaitForExit();
            }
            else
            {
                MessageBox.Show("No game selected!", "Error");
            }
        }

        void netplayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            for(int i = 0;
                i < NetplayTextBoxes.Length;
                ++i)
            {
                NetplayTextBoxes[i].Enabled = box.Checked ? true : false;
            }
        }

        void selectGameMenuItem_Click(object sender, EventArgs e)
        {
            GameListForm = new GameListForm(Config);
            GameListForm.FormClosing += (s, args) =>
            {
                if(GameListForm.SelectedGame != null)
                {
                    SelectedGamePath = GameListForm.SelectedGame.FullName;
                    gameTextBox.Text = GameListForm.SelectedGame.FileName;
                }

                Show();
            };
            GameListForm.Show();

            Hide();
        }
    }
}
