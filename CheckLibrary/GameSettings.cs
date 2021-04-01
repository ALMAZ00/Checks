using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CheckLibrary
{
    [Serializable]
    public class GameSettings
    {
        public string Name1 { get; private set; }
        public string Name2 { get; private set; }
        public string FilePath { get; private set; }

        public GameSettings(string path = null)
        {
            SetFilePath(path);
            SetNamesFromFile();
        }
        private void SetFilePath(string path)
        {
            if (string.IsNullOrEmpty(path) == false)
            {
                FilePath = path;
            }
            else
            {
                FilePath = "settings.txt";
            }
        }
        private void SetNamesFromFile()
        {
            CheckPath(FilePath);
            OnFileExist(FilePath);
        }
        private void CheckPath(string FilePath)
        {
            if (File.Exists(FilePath) == false)
            {
                CreateFile(FilePath);
            }
        }
        private void OnFileExist(string path)
        {
            try
            {
                string text = ReadFile(path);
                SetNames(text, path);
            }
            catch (FileNotFoundException)
            {
               
            }
        }
        private string ReadFile(string path)
        {
            string text;

            using (StreamReader stream = new StreamReader(path))
            {
                text = stream.ReadLine();
                if (string.IsNullOrEmpty(text))
                {
                    CreateFile(path);
                }
            }

            return text;
        }
        private void SetNames(string namesString, string path)
        {
            string[] names = namesString.Split(' ');

            if (names.Length == 2)
            {
                SetNamesFrom(names);
            }
            else
            {
                CreateFile(path);
            }
        }

        private void SetNamesFrom(string[] names)
        {
            Name1 = names[0];
            Name2 = names[1];
        }

        private void CreateFile(string path)
        {
            string names = "Igor Vasya";
            try
            {
                SetNamesInFile(names, path);
            }
            catch (FileNotFoundException)
            {
               
            }
        }
        private void SetNamesInFile(string names, string path)
        {
            File.Delete(path);
            using (FileStream stream = new FileStream(path, FileMode.CreateNew))
            {
                using (StreamWriter flStream = new StreamWriter(stream))
                {
                    flStream.Write(names);
                }
            }
        }
    }
}
