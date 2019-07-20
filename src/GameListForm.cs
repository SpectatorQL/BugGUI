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
    public partial class GameListForm : Form
    {
        public class GameData
        {
            public string Extension { get; set; }
            public string File { get; set; }
            public string FullName;
        }

        
        GameData[] Games;
        public GameData SelectedGame;

        Func<DirectoryInfo, List<DirectoryInfo>> AddDirectoryProc;
        Func<int, List<DirectoryInfo>> RemoveDirectoryProc;


        public GameListForm(Config config)
        {
            InitializeComponent();

            AddDirectoryProc = config.AddGamesDirectory;
            RemoveDirectoryProc = config.RemoveGamesDirectory;

            // TODO(SpectatorQL): Clean this up!
            FileInfo[] gameFiles = config.GamesDirectories[0].GetFiles("*.cue", SearchOption.AllDirectories);
            Games = new GameData[gameFiles.Length];
            for(int i = 0;
                i < Games.Length;
                ++i)
            {
                Games[i] = new GameData
                {
                    Extension = gameFiles[i].Extension,
                    File = gameFiles[i].Name,
                    FullName = gameFiles[i].FullName
                };
            }

            // TODO(SpectatorQL): directoryList.DataSourceChanged
            directoryList.DataSource = config.GamesDirectories;

            gamesGridView.DataSource = Games;
            gamesGridView.CellDoubleClick += (s, args) =>
            {
                SelectedGame = (GameData)gamesGridView.SelectedRows[0].DataBoundItem;
                Close();
            };
        }

        void addDirectoryButton_Click(object sender, EventArgs e)
        {
            // TODO(SpectatorQL): Check if the directory has already been added.
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult dialogResult = folderDialog.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                DirectoryInfo newDirectoryInfo = new DirectoryInfo(folderDialog.SelectedPath);
                // NOTE(SpectatorQL): Yup, that's apparently how we have to do it.
                directoryList.DataSource = null;
                directoryList.DataSource = AddDirectoryProc(newDirectoryInfo);
            }
        }

        void removeDirectoryButton_Click(object sender, EventArgs e)
        {
            Debug.Assert(directoryList.SelectedItem is DirectoryInfo);
            List<DirectoryInfo> newList = RemoveDirectoryProc(directoryList.SelectedIndex);
            directoryList.DataSource = null;
            directoryList.DataSource = newList;
        }
    }
}
