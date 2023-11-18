using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using System.Linq;

namespace Assets.Scripts.Json
{
    public static class JsonManager
    {

        public static void SaveSingleIntoJson<T>(T newRow, string path)
        {
            if (File.Exists(path))
            {
                // Save the list
                string json = JsonConvert.SerializeObject(newRow, Formatting.Indented);
                System.IO.File.WriteAllText(path, json);
            }
        }

        public static void SaveListIntoJson<T>(List<T> newList, string path)
        {
            if (File.Exists(path))
            {
                // Save the list
                string json = JsonConvert.SerializeObject(newList, Formatting.Indented);
                System.IO.File.WriteAllText(path, json);
            }
        }   

        public static List<T> LoadJson<T>(string path)
        {
            if (File.Exists(path))
            {
                string JsonString = File.ReadAllText(path);
                Debug.Log(JsonString);
                if (JsonString != "" && JsonString != "{}")
                {
                    return JsonConvert.DeserializeObject<List<T>>(JsonString);
                }
            }
            else
            {
                File.Create(path);
            }
            return null;
        }

    }
}
