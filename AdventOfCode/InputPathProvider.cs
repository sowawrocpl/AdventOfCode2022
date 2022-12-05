namespace AdventOfCode;

public static class InputPathProvider
{
    public static string GetInputPath(string fileName)
    {
        string projectDir = Environment.CurrentDirectory.Split("bin")[0];
        return Path.Combine(projectDir, "inputs", fileName);
    }
}
