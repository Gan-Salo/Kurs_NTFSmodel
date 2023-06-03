using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class NtfsTom
    {
    public string tom_name;
    public List<MftEntry> entries;
    private List<string> clustersData;
    private int ClusterSize = 10;   // Размер кластера в символах
    private int TotalClusterCount = 20; //Количество кластеров
    public List<string> ClustersData => clustersData;
    public IEnumerable<MftEntry> GetMftEntries()
    {
        return entries;
    }

    public NtfsTom(string tomname, int sizeCluster, int kolvoCluster)
    {
        tom_name = tomname;
        ClusterSize = sizeCluster;
        TotalClusterCount = kolvoCluster;
        entries = new List<MftEntry>();
        clustersData = new List<string>(Enumerable.Repeat(string.Empty, TotalClusterCount));
    }

    /*Создание файла*/
    public MftEntry CreateFile(string fileName, MftEntry parentDirectory, string content)
    {
        if (parentDirectory.FileAttributes.Any(attr => attr.AttributeType == AttributeType.Directory))
        {
            var fileEntry = new MftEntry
            {
                FileName = fileName,
                FileAttributes = new List<FileAttribute> { new FileAttribute { AttributeType = AttributeType.File } },
                SubEntries = new List<MftEntry>(),
                Parent = parentDirectory,
                ClusterIndexes = new List<int>(),
            };

            parentDirectory.SubEntries.Add(fileEntry);
            entries.Add(fileEntry);
            AllocateClusters(fileEntry, content); // Выделение кластеров и запись содержимого файла              
            return fileEntry;
        }
        else
        {
            var fileEntry = new MftEntry
            {
                FileName = fileName,
                FileAttributes = new List<FileAttribute> { new FileAttribute { AttributeType = AttributeType.File } },
                SubEntries = new List<MftEntry>(),
                Parent = parentDirectory.Parent,
                ClusterIndexes = new List<int>(),
            };

            parentDirectory.Parent.SubEntries.Add(fileEntry);
            entries.Add(fileEntry);

            if (content.Length <= 5 && content.Length > 0)
            {
                fileEntry.Content = content;
            }
            else
            {
                fileEntry.Content = null;
                AllocateClusters(fileEntry, content); // Выделение кластеров и запись содержимого файла
            }

            return fileEntry;
        }
    }

    /*Создание директории*/
    public MftEntry CreateDirectory(string directoryName, MftEntry parentDirectory)
    {
        var directoryEntry = new MftEntry
        {
            FileName = directoryName,
            FileAttributes = new List<FileAttribute> { new FileAttribute { AttributeType = AttributeType.Directory } },
            SubEntries = new List<MftEntry>(),
            Parent = parentDirectory
        };

        if (parentDirectory != null)
        {
            parentDirectory.SubEntries.Add(directoryEntry);
        }
        entries.Add(directoryEntry);
        return directoryEntry;
    }

    /*Изменение файла или директории*/
    public void UpdateFileContent(MftEntry fileEntry, string newContent)
    {
        var clusterIndexes = fileEntry.ClusterIndexes;

        int freeclusters = FreeClusterCount();
        //MessageBox.Show($"Свободное место : {freeclusters} кластеров");

        if (newContent.Length > (freeclusters + clusterIndexes.Count) * ClusterSize)
        {
            MessageBox.Show($"Ошибка: Нет места в кластерах для записи содержимого файла.");
        }
        else
        {
            // Освобождение всех кластеров, связанных с файлом
            foreach (var clusterIndex in clusterIndexes)
            {
                clustersData[clusterIndex] = null;
            }

            clusterIndexes.Clear();

            /*Если длина содержимого больше 5 символов, то оно сохраняется в кластеры, если меньше или равно - в MFT*/
            if (newContent.Length <= 5 && newContent.Length > 0)
            {
                fileEntry.Content = newContent;
            }
            else
            {
                fileEntry.Content = null;
                AllocateClusters(fileEntry, newContent);
            }
        }
    }

    /*Удаление файла или директории*/
    public void DeleteFile(MftEntry fileEntry)
    {
        /*Освобождение всех кластеров, связанных с файлом*/
        if (fileEntry.FileAttributes.Any(attr => attr.AttributeType == AttributeType.File) /*|| fileEntry.Content != null*/)
        {
            var clusterIndexes = fileEntry.ClusterIndexes;
            foreach (var clusterIndex in clusterIndexes)
            {
                clustersData[clusterIndex] = null;
            }
        }
        /*Удаление записи из MFT*/
        entries.Remove(fileEntry);

        if (fileEntry.SubEntries != null)
        {
            foreach (var subEntry in fileEntry.SubEntries.ToList())
            {
                DeleteFile(subEntry);
            }
        }

        /*Удаление ссылки на файл у родительской директории*/
        var parentDirectory = fileEntry.Parent;
        if (parentDirectory != null)
        {
            parentDirectory.SubEntries.Remove(fileEntry);
        }
    }

    /*Выделение кластеров под содержимое файла*/
    public void AllocateClusters(MftEntry fileEntry, string content)
    {
        string buffer = GetFileContent(fileEntry);
        var clusterIndexes = new List<int>();
        int startIndex = 0;

        while (startIndex < content.Length)
        {
            var clusterIndex = GetFreeClusterIndex();
            int clusterSize = Math.Min(ClusterSize, content.Length - startIndex);
            var ntfstom = content.Substring(startIndex, clusterSize);
            clustersData[clusterIndex] = ntfstom;
            clusterIndexes.Add(clusterIndex);
            startIndex += clusterSize;
        }

        fileEntry.ClusterIndexes = clusterIndexes;
    }

    /*Выдать индекс первого свободного кластера*/
    public int GetFreeClusterIndex()
    {
        for (int i = 0; i < TotalClusterCount; i++)
        {
            if (string.IsNullOrEmpty(clustersData[i]))
                return i;
        }
        return -1;
    }

    /*Выдать индекс следующего свободного кластера*/
    private int GetNextFreeClusterIndex(int startIndex)
    {
        for (int i = startIndex; i < TotalClusterCount; i++)
        {
            if (string.IsNullOrEmpty(clustersData[i]))
                return i;
        }
        return -1;
    }

    /*Подсчёт свободных кластеров*/
    public int FreeClusterCount()
    {
        int count = 0;
        int currentIndex = GetFreeClusterIndex();

        while (currentIndex != -1)
        {
            count++;
            currentIndex = GetNextFreeClusterIndex(currentIndex + 1);
        }

        return count;
    }

    /*Управление атрибутом ReadOnly*/
    public void SetFileReadOnly(MftEntry entry, bool isReadOnly)
    {
        entry.IsReadOnly = isReadOnly;

        //Обновление атрибута Read-only в записи MFT 
        foreach (var attr in entry.FileAttributes)
        {
            if (attr.AttributeType == AttributeType.File)
            {
                attr.Value = Convert.ToByte(isReadOnly);
                break;
            }
        }
    }

    /*Выдача содержимого файла из кластеров*/
    public string GetFileContent(MftEntry fileEntry)
    {
        string content = "";

        foreach (var clusterIndex in fileEntry.ClusterIndexes)
        {
            content += clustersData[clusterIndex];
        }
        return content;
    }
}
   
}
