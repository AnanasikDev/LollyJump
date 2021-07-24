using System.IO;
public static class CacheController
{
    public static void Write(string path, int n, string end = "\n")
    {
        var writer = new StreamWriter(path, true);
        writer.Write(n.ToString() + end);
        writer.Close();
    }
    public static string GetLast(string path)
    {
        string[] text = File.ReadAllLines(path);
        return text[text.Length - 1].Remove('\n');
    }
}