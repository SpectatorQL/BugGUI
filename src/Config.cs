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

#if true
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

        public void AddGamesDirectory()
        {
        }

        public void RemoveGamesDirectory()
        {
        }
    }
}
