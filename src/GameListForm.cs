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
        // NOTE(SpectatorQL): Should this thing be a struct? It's just 3 pointers.
        public class GameData
        {
            public string Extension { get; set; }
            public string FileName { get; set; }
            public string FullName;

            // NOTE(SpectatorQL): We may need to sort based on not just extensions alone
            // but both extension and file name depending on what we want to actually achieve.
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

        
        List<GameData> Games = new List<GameData>();
        public GameData SelectedGame;

        Config Config;

        public GameListForm(Config config)
        {
            InitializeComponent();

            Config = config;

            directoryList.DataSource = Config.GamesDirectories;
            directoryList.SelectedValueChanged += (s, args) =>
            {
                if(directoryList.SelectedItem != null)
                {
                    Debug.Assert(directoryList.SelectedItem is GamesDirectory);
                    GamesDirectory selectedDirectory = (GamesDirectory)directoryList.SelectedItem;
                    
                    Games.Clear();
                    if(selectedDirectory.Extensions.Length != 0)
                    {
                        for(int i = 0;
                            i < selectedDirectory.Extensions.Length;
                            ++i)
                        {
                            FileInfo[] gameFiles = selectedDirectory.DirectoryInfo
                                .GetFiles($"*{selectedDirectory.Extensions[i]}", SearchOption.AllDirectories);
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
                    }
                    else
                    {
                        FileInfo[] gameFiles = selectedDirectory.DirectoryInfo.GetFiles("*", SearchOption.AllDirectories);
                        for(int i = 0;
                            i < gameFiles.Length;
                            ++i)
                        {
                            Games.Add(new GameData
                            {
                                Extension = gameFiles[i].Extension,
                                FileName = gameFiles[i].Name,
                                FullName = gameFiles[i].FullName
                            });
                        }
                    }
                    Games.Sort(GameData.CompareExtensions);

                    gamesGridView.DataSource = null;
                    gamesGridView.Rows.Clear();
                    gamesGridView.Columns.Clear();
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
            GameDirectoryForm gameDirectoryForm = new GameDirectoryForm(DirectoryFormIntent.Add, null);
            gameDirectoryForm.FormClosing += (s, args) =>
            {
                if(gameDirectoryForm.ResultID == DirectoryFormResult.Added)
                {
                    directoryList.DataSource = null;
                    directoryList.DataSource = Config.AddGamesDirectory(gameDirectoryForm.Directory);
                }

                Enabled = true;
            };

            Enabled = false;
            gameDirectoryForm.Show();
        }

        void editDirectoryButton_Click(object sender, EventArgs e)
        {
            if(directoryList.SelectedItem != null)
            {
                Debug.Assert(directoryList.SelectedItem is GamesDirectory);
                GamesDirectory selectedDirectory = (GamesDirectory)directoryList.SelectedItem;
                GameDirectoryForm gameDirectoryForm = new GameDirectoryForm(DirectoryFormIntent.Edit, selectedDirectory);
                gameDirectoryForm.FormClosing += (s, args) =>
                {
                    if(gameDirectoryForm.ResultID == DirectoryFormResult.Edited)
                    {
                        Config.Update();

                        var temp = directoryList.DataSource;
                        directoryList.DataSource = null;
                        directoryList.DataSource = temp;
                    }

                    Enabled = true;
                };

                Enabled = false;
                gameDirectoryForm.Show();
            }
        }

        void removeDirectoryButton_Click(object sender, EventArgs e)
        {
            if(directoryList.SelectedItem != null)
            {
                Debug.Assert(directoryList.SelectedItem is GamesDirectory);
                List<GamesDirectory> newList = Config.RemoveGamesDirectory(directoryList.SelectedIndex);
                directoryList.DataSource = null;
                directoryList.DataSource = newList;
            }
        }
    }
}
