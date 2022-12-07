// Copyright (c) 2019 under MIT license.

namespace Day_07;

public static class Run
{
    public static int FirstInput(string input)
    {
        var root = new TreeItem();
        var activeItem = root;
        foreach (var chunk in input.Split("$ ").Skip(1)
                     .Select(line => line.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)))
        {
            var data = chunk.ToArray();
            var parsed = ParseBlock(data, activeItem);
            if (parsed.command.StartsWith("cd"))
            {
                switch (parsed.command.Split(" ")[1])
                {
                    case "/":
                        activeItem = root;
                        break;
                    case "..":
                        activeItem = activeItem.Parent;
                        break;
                    default:
                        activeItem =
                            activeItem.Directories.SingleOrDefault(d => d.Name == parsed.command.Split(" ")[1]);
                        break;
                }
            }
            else // ls
            {
                foreach (var parsedDir in parsed.dirs)
                {
                    parsedDir.Parent = activeItem;
                }

                if (activeItem.Directories.Any(d => parsed.dirs.Any(p => p.Name == d.Name)))
                {
                    throw new ApplicationException();
                }

                activeItem.Directories.AddRange(parsed.dirs);
                activeItem.Files.AddRange(parsed.files);
            }
        }

        return CollectDirectories(root, new List<TreeItem>()).Where(dir => dir.Size < 100_000).Sum(c => c.Size);
    }


    public static List<TreeItem> CollectDirectories(TreeItem active, List<TreeItem> dirs)
    {
        dirs.Add(active);
        foreach (var dir in active.Directories)
        {
            dirs.Add(dir);

            foreach (var child in dir.Directories)
            {
                CollectDirectories(child, dirs);
            }
        }

        return dirs;
    }
    public static (string command, FileItem[] files, TreeItem[] dirs) ParseBlock(string[] input, TreeItem parent)
    {
        var command = input[0];
        var files = new List<FileItem>();
        var dirs = new List<TreeItem>();
        foreach (var s in input.Skip(1))
        {
            if (s.StartsWith("dir"))
            {
                dirs.Add(new TreeItem()
                {
                    Name = s.Split(" ")[1],
                    Parent = parent
                });
            }
            else
            {
                files.Add(new FileItem()
                {
                    Name = s.Split(" ")[1],
                    Size = Int32.Parse(s.Split(" ")[0])
                });
            }
        }

        return (command, files.ToArray(), dirs.ToArray());
    }


    public static int SecondInput(string input)
    {
        var root = new TreeItem();
        var activeItem = root;
        foreach (var chunk in input.Split("$ ").Skip(1)
                     .Select(line => line.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)))
        {
            var data = chunk.ToArray();
            var parsed = ParseBlock(data, activeItem);
            if (parsed.command.StartsWith("cd"))
            {
                switch (parsed.command.Split(" ")[1])
                {
                    case "/":
                        activeItem = root;
                        break;
                    case "..":
                        activeItem = activeItem.Parent;
                        break;
                    default:
                        activeItem =
                            activeItem.Directories.SingleOrDefault(d => d.Name == parsed.command.Split(" ")[1]);
                        break;
                }
            }
            else // ls
            {
                foreach (var parsedDir in parsed.dirs)
                {
                    parsedDir.Parent = activeItem;
                }

                if (activeItem.Directories.Any(d => parsed.dirs.Any(p => p.Name == d.Name)))
                {
                    throw new ApplicationException();
                }

                activeItem.Directories.AddRange(parsed.dirs);
                activeItem.Files.AddRange(parsed.files);
            }
        }

        return CollectDirectories(root, new List<TreeItem>()).OrderBy(dir => dir.Size)
            .First(dir => dir.Size > (30_000_000 - (70_000_000 - root.Size))).Size;
    }


    public class TreeItem
    {
        public TreeItem Parent { get; set; }

        public List<TreeItem> Directories { get; } = new List<TreeItem>();
        public List<FileItem> Files { get; } = new List<FileItem>();

        public string Name { get; set; }

        public int Size
        {
            get { return Files.Sum(f => f.Size) + Directories.Sum(d => d.Size); }
        }
    }

    public class FileItem
    {
        public string Name { get; set; }

        public int Size { get; set; }
    }
}