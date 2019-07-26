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
        List<GameData> Games;
        public GameData SelectedGame;

        Func<GamesDirectory, List<GamesDirectory>> AddDirectoryProc;
        Func<int, List<GamesDirectory>> RemoveDirectoryProc;


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
                    Debug.Assert(directoryList.SelectedItem is GamesDirectory);
                    GamesDirectory selectedDirectory = (GamesDirectory)directoryList.SelectedItem;

                    Games = new List<GameData>();
                    for(int i = 0;
                        i < selectedDirectory.Extensions.Length;
                        ++i)
                    {
                        FileInfo[] gameFiles = selectedDirectory.DirectoryInfo
                            .GetFiles(selectedDirectory.Extensions[i], SearchOption.AllDirectories);
                        for(int j = 0;
                            j < gameFiles.Length;
                            ++j)
                        {
                            Games.Add(new GameData
                            {
                                Extension = gameFiles[j].Extension,
                                FileName = gameFiles[j].Name,
                                FullName = gameFiles[j].FullName
                            });
                        }
                    }
                    Games.Sort(GameData.CompareExtensions);

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
                Debug.Assert(gamesGridView.DataSource is List<GameData>);
                int columnIndex = args.ColumnIndex;
                DataGridViewColumn column = gamesGridView.Columns[columnIndex];
                if(columnIndex == 0)
                {
                    Games.Sort(GameData.CompareExtensions);
                }
                else if(columnIndex == 1)
                {
                    Games.Sort(GameData.CompareFileNames);
                }
                gamesGridView.DataSource = Games;
                gamesGridView.Invalidate();
            };
        }

        void addDirectoryButton_Click(object sender, EventArgs e)
        {
            // TODO(SpectatorQL): Check if the directory has already been added.
            // TODO(SpectatorQL): Add a window where the user can specify the name of the new directory
            // as well as the desired file extensions.
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult dialogResult = folderDialog.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                DirectoryInfo newDirectoryInfo = new DirectoryInfo(folderDialog.SelectedPath);
                GamesDirectory newDirectory = new GamesDirectory
                {
                    DirectoryInfo = newDirectoryInfo,
                    Name = "Foo",
                    Extensions = new string[] { "*.cue" },
                };
                // NOTE(SpectatorQL): Yup, that's apparently how we have to do it.
                directoryList.DataSource = null;
                directoryList.DataSource = AddDirectoryProc(newDirectory);
            }
        }

        void removeDirectoryButton_Click(object sender, EventArgs e)
        {
            Debug.Assert(directoryList.SelectedItem is GamesDirectory);
            List<GamesDirectory> newList = RemoveDirectoryProc(directoryList.SelectedIndex);
            directoryList.DataSource = null;
            directoryList.DataSource = newList;
        }
    }
}
