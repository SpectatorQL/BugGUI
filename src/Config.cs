using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BugGUI
{
    [Serializable]
    public class GamesDirectory
    {
        public string Name;
        public DirectoryInfo DirectoryInfo;
        public string[] Extensions;

        public override string ToString()
        {
            string result = $"{Name} ({DirectoryInfo.FullName})";
            return result;
        }
    }

    // TODO(SpectatorQL): Clean up serialization!
    public class Config
    {
        string ConfigFileName;
        public List<GamesDirectory> GamesDirectories { get; }

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
                GamesDirectories = (List<GamesDirectory>)formatter.Deserialize(configFileStream);
                configFileStream.Close();
            }
            else
            {
                configFileStream = File.Open(ConfigFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                GamesDirectories = new List<GamesDirectory>();

                formatter.Serialize(configFileStream, GamesDirectories);
                configFileStream.Close();
            }
        }

        void PrintDirectoryList()
        {
#if DEBUG
            for(int i = 0;
                i < GamesDirectories.Count;
                ++i)
            {
                var thing = GamesDirectories[i];
                if(thing != null)
                {
                    Debug.WriteLine($"{i}: {thing.ToString()}");
                }
                else
                {
                    Debug.WriteLine($"{i}: null");
                }
            }
            Debug.Write("\n");
#endif
        }

        public List<GamesDirectory> AddGamesDirectory(GamesDirectory newDirectory)
        {
            GamesDirectories.Add(newDirectory);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream configFileStream = File.Open(ConfigFileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(configFileStream, GamesDirectories);
            configFileStream.Close();

            PrintDirectoryList();

            return GamesDirectories;
        }

        public List<GamesDirectory> RemoveGamesDirectory(int index)
        {
            GamesDirectories.RemoveAt(index);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream configFileStream = File.Open(ConfigFileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(configFileStream, GamesDirectories);
            configFileStream.Close();

            PrintDirectoryList();

            return GamesDirectories;
        }
    }
}
