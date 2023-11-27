using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System;
using System.Linq;

public static class File_Handler 
{
    
    public static void saveListToJson<T>(List<T> toSave, string filename)
    {
        Debug.Log(getPath(filename));   
        string content = JsonHelper.ToJson<T>(toSave.ToArray());
        writeFile(getPath(filename),content);
    }
    public static void saveToJson<T>(T toSave, string filename)
    {
        Debug.Log(getPath(filename));
        string content = JsonUtility.ToJson(toSave);
        writeFile(getPath(filename), content);
    }

    public static List<T> readListFromJson<T>(string filename)
    {
        string content = readFile(getPath(filename));
        if(string.IsNullOrEmpty(content) || content == "{}") 
        {
            return new List<T>();

        }
        else
        {
            List<T> results = JsonHelper.FromJson<T>(content).ToList();
            return results;
        }


    }
    public static T readFromJson<T>(string filename)
    {
        string content = readFile(getPath(filename));
        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return default(T);

        }
        else
        {
            T results = JsonUtility.FromJson<T>(content);
            return results;
        }


    }

    public static string getPath(string filename)
    {
        return Application.persistentDataPath +"/"+filename;
    }

    private static void writeFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path,FileMode.Create);

        using(StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }
    private static string readFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }



}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
