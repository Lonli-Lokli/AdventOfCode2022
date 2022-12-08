// Copyright (c) 2019 under MIT license.

using System.Collections.Immutable;

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
                activeItem = parsed.command.Split(" ")[1] switch
                {
                    "/" => root,
                    ".." => activeItem.Parent,
                    _ => activeItem.Directories.SingleOrDefault(d => d.Name == parsed.command.Split(" ")[1])
                };
            }
            else // ls
            {
                foreach (var parsedDir in parsed.dirs)
                {
                    parsedDir.Parent = activeItem;
                }

                activeItem.Directories.AddRange(parsed.dirs);
                activeItem.Files.AddRange(parsed.files);
            }
        }

        return CollectDirectories(root, new List<TreeItem>()).Where(dir => dir.Size < 100_000).Sum(c => c.Size);
    }

    public static string SEPARATOR = @"\";
    public static string ROOT = @"\";

    public static int FirstInputFunctional(string input) => input.Split("$ ")
        .Skip(1) // split by commands and their results
        .Select(line => line.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        .Select(block => new { command = block[0], items = block.Skip(1) }) // command is first, output after
        .Aggregate((path: ROOT, files: new List<(string path, int size)>()), (acc, curr) => // setup root here
        {
            switch (curr.command)
            {
                case { } when curr.command.StartsWith("cd"):

                    acc.path = curr.command.Split(" ")[1] switch // decide for each case with cd command
                    {
                        "/" => ROOT,
                        ".." => acc.path.Substring(0, acc.path.LastIndexOf(SEPARATOR)),
                        _ => (acc.path == SEPARATOR ? string.Empty : acc.path) + SEPARATOR + curr.command.Split(" ")[1]
                    };

                    break;
                default: // ls
                    // skip directories, look at at files only
                    acc.files.AddRange(curr.items.Where(line => !line.StartsWith("dir ")).Select(line =>
                        (path: $@"{acc.path}{SEPARATOR}{line.Split(" ")[1]}",
                            size: Int32.Parse(line.Split(" ")[0]))));
                    break;
            }

            return acc;
        })
        .files
        .Aggregate(new List<dynamic>(), (rootFolders, file) =>
        {
            // process each file, do not include last part as it's file name, not folder
            file.path.Split(SEPARATOR, StringSplitOptions.RemoveEmptyEntries).SkipLast(1).Aggregate(
                // first argument here is list of increasing folder names (a/b/c/d.txt will be converted to list of [a, b, c]) 
                new List<string> { string.Empty }, (currentFolders, subPath) =>
                {
                    var subFolderFullPath = currentFolders.Last() + SEPARATOR + subPath; // generate subfolder from part of the file
                    var alreadyStoredFolderIndex = rootFolders.FindIndex(f => f.folder == subFolderFullPath);
                    if (alreadyStoredFolderIndex == -1)
                    {
                        rootFolders.Add(new { folder = subFolderFullPath, file.size }); // folder is new, add to collection with file size
                    }
                    else
                    {
                        rootFolders[alreadyStoredFolderIndex] = new // replace already existing folder with the new one
                        {
                            folder = subFolderFullPath, size = (int)(file.size + rootFolders[alreadyStoredFolderIndex].size)
                        };
                    }
                    currentFolders.Add(subFolderFullPath);
                    return currentFolders;
                });
        
            return rootFolders;
        }).Where(folder => folder.size < 100_000).Select(folder => folder.size).Cast<int>().Sum();

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
                activeItem = parsed.command.Split(" ")[1] switch
                {
                    "/" => root,
                    ".." => activeItem.Parent,
                    _ => activeItem.Directories.SingleOrDefault(d => d.Name == parsed.command.Split(" ")[1])
                };
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