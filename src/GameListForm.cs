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
    public partial class GameListForm : Form
    {
        public class GameData
        {
            public string Extension { get; set; }
            public string File { get; set; }
            public string FullName;
        }


        List<DirectoryInfo> GamesDirectories;
        GameData[] Games;
        public GameData SelectedGame;


        public GameListForm()
        {
            InitializeComponent();

            // TODO: Adding/removing directories!
            GamesDirectories = new List<DirectoryInfo>()
            {
                new DirectoryInfo(@"E:\ISOs\PSX"),
            };

            FileInfo[] gameFiles = GamesDirectories[0].GetFiles("*.cue", SearchOption.AllDirectories);
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

            gamesGridView.DataSource = Games;
            gamesGridView.CellDoubleClick += (s, args) =>
            {
                SelectedGame = (GameData)gamesGridView.SelectedRows[0].DataBoundItem;
                Close();
            };
        }

        void addDirectoryButton_Click(object sender, EventArgs e)
        {
        }

        void removeDirectoryButton_Click(object sender, EventArgs e)
        {
        }
    }
}
