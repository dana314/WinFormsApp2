using System;
using System.IO;
public class Logger
{
    public static readonly string LogPath = "app.log";

    public static void Log(string message)
    {
        File.AppendAllText(LogPath, $"{DateTime.Now:HH:mm:ss} - {message}\n");
    }

    public static void LogError(string message, Exception ex = null)
    {
        var errorMsg = $"ERROR: {message}";
        if (ex != null) errorMsg += $"\n{ex}";
        Log(errorMsg);
    }
}