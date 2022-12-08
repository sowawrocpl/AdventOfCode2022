namespace AdventOfCode;

public class Day_7_NoSpaceLeftOnDevice
{
    public void Execute()
    {
        string filePath = InputPathProvider.GetInputPath("day_7.txt");

        ElfDirectory root = new ElfDirectory("/", null);
        ElfDirectory currentDir = root;

        List<ElfDirectory> flatDirList = new();
        flatDirList.Add(root);

        using (var reader = new StreamReader(filePath))
        {
            while (reader.Peek() >= 0)
            {
                string line = reader.ReadLine();

                if (line.StartsWith('$'))
                {
                    OnCommand(line);
                }
                else
                {
                    OnDickItem(line);
                }
            }
        }

        int missingFreeSpace = root.Size - 40000000;
        var dirToDelete = flatDirList.OrderBy(x => x.Size).First(x => x.Size >= missingFreeSpace);

        Console.WriteLine(dirToDelete.Size);



        void OnCommand(string line)
        {
            var execution = ReadCommand(line);
            if (execution.Command == "cd")
            {
                if (execution.Param == "..")
                {
                    currentDir = currentDir.ParentDirectory;
                }
                else
                {
                    var subDir = execution.Param == "/"
                        ? root
                        : (ElfDirectory) currentDir.Items.Single(x => x.Name == execution.Param);
                    currentDir = subDir;
                }
            }
        }

        void OnDickItem(string line)
        {
            var item = ReadDiskItem(line);
            if (int.TryParse(item.Meta, out int size))
            {
                var file = new ElfFile(item.Name, size);
                currentDir.Items.Add(file);
            }
            else
            {
                var directory = new ElfDirectory(item.Name, currentDir);
                currentDir.Items.Add(directory);
                flatDirList.Add(directory);
            }
        }
    }

    private (string Command, string Param) ReadCommand(string line)
    {
        var parts = line.Split(' ');
        string command = parts[1];
        string param = parts.Length == 3
            ? parts[2]
            : string.Empty;

        return (command, param);
    }

    private (string Meta, string Name) ReadDiskItem(string line)
    {
        var parts = line.Split(' ');
        return (parts[0], parts[1]);
    }

    private interface IDiskItem
    {
        public string Name { get; }

        public int Size { get; }
    }

    private class ElfFile : IDiskItem
    {
        public ElfFile(string name, int size)
        {
            Name = name;
            Size = size;
        }
        public string Name { get; }
        public int Size { get; }
    }

    private class ElfDirectory : IDiskItem
    {
        public ElfDirectory(string name, ElfDirectory parentDirectory)
        {
            Name = name;
            ParentDirectory = parentDirectory;
        }

        public string Name { get; }

        public ElfDirectory ParentDirectory { get; }

        public List<IDiskItem> Items { get; } = new();

        public int Size => Items.Sum(x => x.Size);
    }
}
