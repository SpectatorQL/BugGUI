﻿using System;
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
        StringBuilder _argvStringBuilder = new StringBuilder();

        GameListForm _gameListForm;
        GameListForm.GameData SelectedGame;

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
            if(gameTextBox.Text != "")
            {
                string argv = "";
                // NOTE(SpectatorQL): Do we want the program to assume that it is located in the mednafen directory?
                string mednafenPath = @"D:\mednafen-1.22.2-win64\mednafen.exe";
                string[] netplayArgs =
                {
                    "-netplay.host",
                    "-netplay.port",
                    "-netplay.nick",
                    "-netplay.gamekey"
                };

                if(netplayCheckBox.Checked)
                {
                    _argvStringBuilder.Append(" ");
                    _argvStringBuilder.Append("-connect");
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

                _argvStringBuilder.Append(" ");
                _argvStringBuilder.Append("\"");
                _argvStringBuilder.Append(SelectedGame.FullName);
                _argvStringBuilder.Append("\"");

                argv = _argvStringBuilder.ToString();
                _argvStringBuilder.Clear();

                Process.Start(mednafenPath, argv);
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
            // NOTE(SpectatorQL): Do I really have to do this every time?
            _gameListForm = new GameListForm();
            _gameListForm.FormClosing += (s, args) =>
            {
                // TODO(SpectatorQL): Change the rom to load, if one was selected.
                SelectedGame = _gameListForm.SelectedGame;
                gameTextBox.Text = SelectedGame.File;
                this.Show();
            };

            _gameListForm.Show();

            this.Hide();
        }
    }
}
