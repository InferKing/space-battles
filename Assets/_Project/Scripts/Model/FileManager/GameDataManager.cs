using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Model.FileManager
{
    public class GameDataManager : IFileManager
    {
        public void Save<T>(string fileName, T data, string folderName = Constants.DefaultDataFolder)
        {
            var pathToDirectory = Path.Combine(Application.persistentDataPath, folderName);

            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            
            if (!Directory.Exists(pathToDirectory))
            {
                Directory.CreateDirectory(pathToDirectory);
            }

            string pathToFile = Path.Combine(pathToDirectory, fileName);

            using FileStream stream = new(pathToFile, FileMode.Create);
            using StreamWriter writer = new(stream);
            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented, settings);
            writer.Write(jsonData);
        }

        public T Load<T>(string fileName, Func<T> defaultFactory = null, string folderName = Constants.DefaultDataFolder) where T : class
        {
            var pathToDirectory = Path.Combine(Application.persistentDataPath, folderName);
            
            string pathToFile = Path.Combine(pathToDirectory, fileName);

            if (!File.Exists(pathToFile))
            {
                return defaultFactory?.Invoke();
            }

            using FileStream stream = new(pathToFile, FileMode.Open);
            using StreamReader reader = new(stream);

            return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
        }
    }
}