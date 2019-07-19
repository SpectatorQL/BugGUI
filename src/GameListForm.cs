using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        public GameData[] Games =
        {
            new GameData { Extension = ".cue", File = "A.cue" },
            new GameData { Extension = ".bin", File = "B.bin" }
        };

        public GameListForm()
        {
            InitializeComponent();

            gamesGridView.DataSource = Games;
        }

        void addDirectoryButton_Click(object sender, EventArgs e)
        {
        }

        void removeDirectoryButton_Click(object sender, EventArgs e)
        {
        }
    }
}
