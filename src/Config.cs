using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BugGUI
{
    public class Config
    {
        string ConfigFileName;
        public List<DirectoryInfo> GamesDirectories { get; }

        public Config()
        {
            ConfigFileName = "bugdata.dat";

#if false
            if(File.Exists(ConfigFileName))
            {
                File.Delete(ConfigFileName);
            }
#endif

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream configFileStream;
            if(File.Exists(ConfigFileName))
            {
                configFileStream = File.Open(ConfigFileName, FileMode.Open, FileAccess.Read, FileShare.None);
                GamesDirectories = (List<DirectoryInfo>)formatter.Deserialize(configFileStream);
            }
            else
            {
                configFileStream = File.Open(ConfigFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                GamesDirectories = new List<DirectoryInfo>()
                {
                    new DirectoryInfo(@"E:\ISOs\PSX")
                };

                formatter.Serialize(configFileStream, GamesDirectories);
            }
        }

        public List<DirectoryInfo> AddGamesDirectory(DirectoryInfo newDirectory)
        {
            GamesDirectories.Add(newDirectory);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream configFileStream = File.Open(ConfigFileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(configFileStream, GamesDirectories);

            return GamesDirectories;
        }

        public List<DirectoryInfo> RemoveGamesDirectory(int index)
        {
            GamesDirectories.RemoveAt(index);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream configFileStream = File.Open(ConfigFileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(configFileStream, GamesDirectories);

            return GamesDirectories;
        }
    }
}
