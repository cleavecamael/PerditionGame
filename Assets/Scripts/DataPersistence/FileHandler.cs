using System;
using System.IO;
using UnityEngine;

public static class FileHandler
{
    public static bool CheckFileExists(string filename)
    {
        return File.Exists(Path.Combine(Application.persistentDataPath, filename));
    }

    public static void Save(string filename, string contents)
    {
        string fullpath = Path.Combine(Application.persistentDataPath, filename);

        try
        {
            using FileStream stream = new(fullpath, FileMode.Create);
            using StreamWriter writer = new(stream);
            writer.Write(contents);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save to {fullpath}. Exception: {e}.");
        }
    }

    public static string Load(string filename)
    {
        string fullpath = Path.Combine(Application.persistentDataPath, filename);
        string result = null;

        try
        {
            using FileStream stream = new(fullpath, FileMode.Open);
            using StreamReader reader = new(stream);
            result = reader.ReadToEnd();
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load from {fullpath}. Exception: {e}.");
        }

        return result;
    }
}