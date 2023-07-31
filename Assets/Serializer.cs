using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Serializer
{
    public static T DeserializeFromFile<T>(string fileName)
    {
        try
        {
            var path = Path.Combine(Application.streamingAssetsPath, fileName);
            using (StreamReader sr = new StreamReader(path))
            {
                JsonTextReader jsonReader = new JsonTextReader(sr);
                var serializer = new JsonSerializer();
                T grid = serializer.Deserialize<T>(jsonReader);
                return grid;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error during deserialization: {ex}");
        }

        return default(T);
    }

    public static T DeserializeFromString<T>(string json)
    {
        var obj = JsonConvert.DeserializeObject<T>(json);
        return obj;
    }

    public async static void Serialize<T>(T grid, string fileName)
    {
        var path = Path.Combine(Application.streamingAssetsPath, fileName);
        using (StreamWriter sr = new StreamWriter(path))
        {
            JsonTextWriter jsonWriter = new JsonTextWriter(sr);
            var a = JObject.FromObject(grid);
            await jsonWriter.WriteRawAsync(a.ToString());
        }
    }
}
