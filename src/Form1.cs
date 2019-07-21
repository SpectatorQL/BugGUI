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
        TextBox[] _netplayTextBoxes;
        StringBuilder _argvStringBuilder = new StringBuilder();

        GameListForm _gameListForm;
        string SelectedGamePath;

        Config Config = new Config();

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
                    _argvStringBuilder.Append(" -connect");
                    for(int i = 0;
                        i < _netplayTextBoxes.Length;
                        ++i)
                    {
                        _argvStringBuilder.Append(" ");
                        _argvStringBuilder.Append(netplayArgs[i]);
                        _argvStringBuilder.Append(" ");
                        _argvStringBuilder.Append(_netplayTextBoxes[i].Text);
                    }
                }

                _argvStringBuilder.Append(" \"");
                _argvStringBuilder.Append(SelectedGamePath);
                _argvStringBuilder.Append("\"");

                argv = _argvStringBuilder.ToString();
                _argvStringBuilder.Clear();
                
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
                i < _netplayTextBoxes.Length;
                ++i)
            {
                _netplayTextBoxes[i].Enabled = box.Checked ? true : false;
            }
        }

        void selectGameMenuItem_Click(object sender, EventArgs e)
        {
            _gameListForm = new GameListForm(Config);
            _gameListForm.FormClosing += (s, args) =>
            {
                if(_gameListForm.SelectedGame != null)
                {
                    SelectedGamePath = _gameListForm.SelectedGame.FullName;
                    gameTextBox.Text = _gameListForm.SelectedGame.File;
                }

                Show();
            };
            _gameListForm.Show();

            Hide();
        }
    }
}
