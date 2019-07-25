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
            public string FileName { get; set; }
            public string FullName;

            public static int CompareExtensions(GameData self, GameData other)
            {
                int result = string.Compare(self.Extension, other.Extension);
                return result;
            }

            public static int CompareFileNames(GameData self, GameData other)
            {
                int result = string.Compare(self.FileName, other.FileName);
                return result;
            }
        }


        // NOTE(SpectatorQL): Perhaps we should change this to List<GameData>
        // and just clear the contents of each item instead of allocating new objects all the time?
        // Also, changing GameData to be a struct may be even better with that approach.
        GameData[] Games;
        public GameData SelectedGame;

        Func<DirectoryInfo, List<DirectoryInfo>> AddDirectoryProc;
        Func<int, List<DirectoryInfo>> RemoveDirectoryProc;


        public GameListForm(Config config)
        {
            InitializeComponent();

            AddDirectoryProc = config.AddGamesDirectory;
            RemoveDirectoryProc = config.RemoveGamesDirectory;

            
            directoryList.DataSource = config.GamesDirectories;
            directoryList.SelectedValueChanged += (s, args) =>
            {
                if(directoryList.SelectedItem != null)
                {
                    Debug.Assert(directoryList.SelectedItem is DirectoryInfo);
                    FileInfo[] gameFiles = config.GamesDirectories[directoryList.SelectedIndex].GetFiles("*", SearchOption.AllDirectories);
                    int len = gameFiles.Length;
                    Games = new GameData[len];
                    for(int i = 0;
                        i < len;
                        ++i)
                    {
                        Games[i] = new GameData
                        {
                            Extension = gameFiles[i].Extension,
                            FileName = gameFiles[i].Name,
                            FullName = gameFiles[i].FullName
                        };
                    }

                    gamesGridView.DataSource = Games;
                }
                else
                {
                    gamesGridView.DataSource = null;
                }
            };
            directoryList.SelectedItem = null;

            
            gamesGridView.CellDoubleClick += (s, args) =>
            {
                SelectedGame = (GameData)gamesGridView.SelectedRows[0].DataBoundItem;
                Close();
            };
            gamesGridView.ColumnHeaderMouseClick += (s, args) =>
            {
                Debug.Assert(gamesGridView.DataSource is GameData[]);
                int columnIndex = args.ColumnIndex;
                DataGridViewColumn column = gamesGridView.Columns[columnIndex];
                if(columnIndex == 0)
                {
                    Array.Sort(Games, GameData.CompareExtensions);
                }
                else if(columnIndex == 1)
                {
                    Array.Sort(Games, GameData.CompareFileNames);
                }
                gamesGridView.DataSource = Games;
                gamesGridView.Invalidate();
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
