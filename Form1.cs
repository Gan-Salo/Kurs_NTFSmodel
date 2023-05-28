using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public NtfsTom cluster = new NtfsTom();
        //cluster ;
        // Добавьте поле для хранения выбранного файла
        private MftEntry selectedFileEntry;

        public Form1()
        {
            InitializeComponent();
            InitializeContextMenu();
            CreateFilesAndDirectories();

        }

        private void InitializeContextMenu()
        {
            // Initialize context menu items
            contextMenuStrip2 = new ContextMenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem("Add Directory");
            toolStripMenuItem2 = new ToolStripMenuItem("Add File");
            toolStripMenuItem3 = new ToolStripMenuItem("Delete File");

            // Add menu items to the context menu
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] {
                toolStripMenuItem1,
                toolStripMenuItem2,
                toolStripMenuItem3
            });

            // Set event handlers for menu items
            toolStripMenuItem1.Click += addDirectoryToolStripMenuItem_Click;
            toolStripMenuItem2.Click += addFileToolStripMenuItem_Click;
            toolStripMenuItem3.Click += deleteFileToolStripMenuItem_Click;

            // Assign the context menu to the DataGridView control
            treeView1.ContextMenuStrip = contextMenuStrip2;
        }

        private void addDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Handler for adding a directory
            var selectedNode = treeView1.SelectedNode;
            var parentEntry = (MftEntry)selectedNode.Tag;
            var newDirectory = cluster.CreateDirectory("New Directory", parentEntry);
            AddNodeToTreeView(newDirectory, selectedNode);
            dataGridView1.Rows.Clear();
            PrintMftEntry(parentEntry);
        }

        private void addFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Handler for adding a file
            var selectedNode = treeView1.SelectedNode;
            var parentEntry = (MftEntry)selectedNode.Tag;
            var newFile = cluster.CreateFile("New File.txt", parentEntry, "Example file content");
            AddNodeToTreeView(newFile, selectedNode);
            dataGridView1.Rows.Clear();
            PrintMftEntry(parentEntry);
        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Handler for deleting a file
            var selectedNode = treeView1.SelectedNode;
            var fileEntry = (MftEntry)selectedNode.Tag;
            //var parentEntry = fileEntry.Parent;
            cluster.DeleteFile(fileEntry);
            selectedNode.Remove();
            dataGridView1.Rows.Clear();
            //PrintMftEntry(parentEntry);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void PrintMftEntry(MftEntry entry, string indent = "")
        {
            dataGridView1.Rows.Add(entry.FileName, entry.IsReadOnly ? "Read-only" : "Writable");

            foreach (var attr in entry.FileAttributes)
            {
                if (attr.AttributeType == AttributeType.File)
                {
                    dataGridView1.Rows.Add("File Attribute: File", string.Empty);
                    break;
                }
                else if (attr.AttributeType == AttributeType.Directory)
                {
                    dataGridView1.Rows.Add("File Attribute: Directory", string.Empty);
                    break;
                }
            }

            if (entry.FileAttributes.Any(attr => attr.AttributeType == AttributeType.File))
            {
                dataGridView1.Rows.Add("Cluster Indexes:", string.Join(", ", entry.ClusterIndexes));
                dataGridView1.Rows.Add("File Content:", cluster.GetFileContent(entry));
            }

            foreach (var subEntry in entry.SubEntries)
            {
                PrintMftEntry(subEntry, indent + "\t");
            }
        }

        //private void AddNodeToTreeView(MftEntry entry, TreeNode parentNode)
        //{
        //    var newNode = new TreeNode(entry.FileName);
        //    newNode.Tag = entry;
        //    parentNode.Nodes.Add(newNode);
        //}
    

    private void PrintAllClustersData()
        {
            for (int i = 0; i < cluster.ClustersData.Count; i++)
            {
                var clusterData = cluster.ClustersData[i];
                if (clusterData != null)
                {
                    dataGridView2.Rows.Add($"Cluster {i}:", clusterData);
                }
                else
                {
                    dataGridView2.Rows.Add($"Cluster {i}:", "<Empty>");
                }
            }
        }
        private void CreateFilesAndDirectories()
        {
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Type", "Type");

            dataGridView2.Columns.Add("Name", "Name");
            dataGridView2.Columns.Add("Type", "Type");
            
            var root = cluster.CreateDirectory("C:", null);
            // Создание файлов и директорий

            var file1 = cluster.CreateFile("File1.txt", root, "Пример содержимого файла 1");
            var file2 = cluster.CreateFile("File2.txt", root, "Пример содержимого файла 2");
            var subDir = cluster.CreateDirectory("SubDir", root);
            var subDir2 = cluster.CreateDirectory("SubDir2", subDir);
            var subDir3 = cluster.CreateDirectory("SubDir3", subDir2);
            var subDir4 = cluster.CreateDirectory("SubDir4", subDir3);
            var file3 = cluster.CreateFile("File3.txt", subDir, "Пример содержимого файла 3");
            //cluster.UpdateFileContent(file1, "Првввимер содпппппержимого файла 1");
            
            // Покажите контекстное меню в указанной позиции
            //contextMenuStrip2.Show(button1, new Point(0, button1.Height));

            //AddNodeToTreeView(root, null);         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Очистка DataGridView
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            PopulateTreeView();

            treeView1.AfterSelect += treeView1_AfterSelect;
            // Покажите контекстное меню в указанной позиции
            //contextMenuStrip2.Show(button1, new Point(0, button1.Height));

            //AddNodeToTreeView(GetMftEntryByName("C:"), null);

            // Вывод содержимого всех кластеров
            PrintAllClustersData();

            // Вывод информации о файлах и директориях
            PrintMftEntry(GetMftEntryByName("C:"));

            //// Установка атрибута Read-only
            //cluster.SetFileReadOnly(file1, true);
            //cluster.SetFileReadOnly(file3, true);

            //// Вывод информации о файлах и директориях после изменения
            //PrintMftEntry(root);
            
            //PrintAllClustersData();

            //// Изменение содержимого файла
            
            ////cluster.UpdateFileContent(file1, "Пр");

            //var file5 = cluster.CreateFile("File5.txt", root, "!!файла 5");

            //// Вывод информации о файлах и директориях после изменения содержимого файла
            //PrintMftEntry(root);

            //// Вывод содержимого всех кластеров
            //PrintAllClustersData();

            //cluster.DeleteFile(file3);
            //var file6 = cluster.CreateFile("File6.txt", root, "!!фdddddddddddddddddddddddайла 6");
            //// Вывод информации о файлах и директориях после изменения содержимого файла
            //PrintMftEntry(root);
          
        }

        private void AddNodeToTreeView(MftEntry entry, TreeNode parentNode)
        {
            TreeNode node;
            if (parentNode != null)
                node = parentNode.Nodes.Add(entry.FileName);
            else
                node = treeView1.Nodes.Add(entry.FileName);

            foreach (var subEntry in entry.SubEntries)
            {
                AddNodeToTreeView(subEntry, node);
            }
        }

        //private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        //{
        //    // Обновление иерархии узлов при раскрытии узла
        //    foreach (TreeNode node in e.Node.Nodes)
        //    {
        //        node.Nodes.Clear();
        //        var entry = (MftEntry)node.Tag;
        //        foreach (var subEntry in entry.SubEntries)
        //        {
        //            AddNodeToTreeView(subEntry, node);
        //        }
        //    }
        //}
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right) // Проверяем, что нажата правая кнопка мыши
            {
                treeView1.SelectedNode = e.Node; // Выделяем выбранный узел
                                                 // Покажите контекстное меню в указанной позиции
                contextMenuStrip2.Show(treeView1, e.Location);
            }
            else if (e.Button == MouseButtons.Left) // Проверяем, что нажата левая кнопка мыши
            {
                var selectedNode = e.Node;
                var selectedEntry = (MftEntry)selectedNode.Tag;

                // Проверяем, является ли выбранный элемент файлом
                if (selectedEntry.FileAttributes.Any(attr => attr.AttributeType == AttributeType.File))
                {
                    // Выводим содержимое файла в TextBox
                    filecontent_textbox.Text = cluster.GetFileContent(selectedEntry);

                    // Выводим имя файла в Label
                    filename_label.Text = selectedEntry.FileName;
                }
                else
                {
                    MessageBox.Show($"Ошибка: {selectedEntry.FileName}");
                    // Если выбранный элемент является директорией, очищаем TextBox и Label
                    filecontent_textbox.Text = string.Empty;
                    filename_label.Text = string.Empty;
                }
            }
        }


        //private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    // Вывод информации о выбранном файле или директории
        //    var entry = (MftEntry)e.Node.Tag;
        //    dataGridView1.Rows.Clear();
        //    PrintMftEntry(entry);
        //}


        public MftEntry GetMftEntryByName(string fileName)
        {
            foreach (var entry in cluster.entries)
            {
                if (entry.FileName == fileName)
                {
                    return entry;
                }
            }

            return null; // Если не найдено совпадение, возвращаем null
        }

        private void PopulateTreeView()
        {
            treeView1.Nodes.Clear();

            // Получаем коллекцию MftEntry из объекта cluster
            //IEnumerable<MftEntry> entries = cluster.GetMftEntries();


            Dictionary<MftEntry, TreeNode> addedNodes = new Dictionary<MftEntry, TreeNode>();

            foreach (MftEntry entry in cluster.entries)
            {
                if (!addedNodes.ContainsKey(entry))
                {
                    TreeNode node = CreateTreeNode(entry, addedNodes);
                    treeView1.Nodes.Add(node);
                }
            }
            treeView1.ExpandAll();
        }

        private TreeNode CreateTreeNode(MftEntry entry, Dictionary<MftEntry, TreeNode> addedNodes)
        {
            TreeNode node = new TreeNode(entry.FileName);
            node.Tag = entry; // Сохраняем ссылку на MftEntry в свойстве Tag узла

            addedNodes.Add(entry, node);

            foreach (MftEntry subEntry in entry.SubEntries)
            {
                if (addedNodes.ContainsKey(subEntry))
                {
                    // Если ветвь уже добавлена, используем существующий узел
                    TreeNode subNode = addedNodes[subEntry];
                    node.Nodes.Add(subNode);
                }
                else
                {
                    // Если ветвь еще не добавлена, создаем новый узел
                    TreeNode subNode = CreateTreeNode(subEntry, addedNodes);
                    node.Nodes.Add(subNode);
                }
            }

            return node;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag is MftEntry)
            {
                MftEntry selectedEntry = (MftEntry)e.Node.Tag;
                if (selectedEntry.FileAttributes.Any(attr => attr.AttributeType == AttributeType.File))
                {
                    // Если выбран файл, сохраняем ссылку на него
                    selectedFileEntry = selectedEntry;
                    filecontent_textbox.Text = selectedEntry.Content;
                    filename_label.Text = selectedEntry.FileName;
                    save_button.Enabled = true;

                }
                else
                {
                    selectedFileEntry = null;
                    filecontent_textbox.Text = string.Empty;
                    filename_label.Text = selectedEntry.FileName;
                    save_button.Enabled = false;
                }
            }
            else
            {
                selectedFileEntry = null;
                filecontent_textbox.Text = string.Empty;
                filename_label.Text = string.Empty;
                save_button.Enabled = false;
            }
        }

        // Обработчик события нажатия на кнопку сохранения
        private void save_button_Click(object sender, EventArgs e)
        {
            // Получение выбранного файла из списка или дерева файловой системы
           // MftEntry selectedFile = GetSelectedFile(); // Здесь GetSelectedFile() - ваш метод получения выбранного файла

            if (selectedFileEntry != null)
            {
                // Получение нового содержимого из TextBox
                string newContent = filecontent_textbox.Text;

                // Обновление содержимого файла в MFT и кластерах
                cluster.UpdateFileContent(selectedFileEntry, newContent);
            }
        }
        //private void save_button_Click(object sender, EventArgs e)
        //{

        //    if (selectedFileEntry != null)
        //    {
        //        // Сохраняем содержимое из textbox в выбранный файл
        //        selectedFileEntry.Content = filecontent_textbox.Text;
        //    }
        //}
    }



    public class NtfsTom
    {
        public List<MftEntry> entries;
        private List<string> clustersData;
        // Размер кластера в символах
        private const int ClusterSize = 10;
        private const int TotalClusterCount = 20;

        public List<string> ClustersData => clustersData;
        public IEnumerable<MftEntry> GetMftEntries()
        {
            return entries;
        }

        public NtfsTom()
        {
            entries = new List<MftEntry>();
            clustersData = new List<string>(Enumerable.Repeat(string.Empty, TotalClusterCount));
        }

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
                    Content = content
                };

                parentDirectory.SubEntries.Add(fileEntry);
                entries.Add(fileEntry);

                Console.WriteLine($"Created file: {fileName} in '{parentDirectory.FileName}'");

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
                    Content = content
                };

                parentDirectory.Parent.SubEntries.Add(fileEntry);
                entries.Add(fileEntry);

                Console.WriteLine($"Error, you try to add file not to directory. Created file: {fileName} in '{parentDirectory.Parent.FileName}'");

                AllocateClusters(fileEntry, content); // Выделение кластеров и запись содержимого файла

                return fileEntry;
            }
        }

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
            else
            {
                ;//Console.WriteLine("Ошибка: parentDirectory равен null.");
            }
            entries.Add(directoryEntry);

            Console.WriteLine($"Created directory: {directoryName}");

            return directoryEntry;
        }

        public void UpdateFileContent(MftEntry fileEntry, string newContent)
        {
            if (!fileEntry.IsReadOnly)
            {
                var clusterIndexes = fileEntry.ClusterIndexes;
                fileEntry.Content = newContent;
                // Освобождение всех кластеров, связанных с файлом
                foreach (var clusterIndex in clusterIndexes)
                {
                    clustersData[clusterIndex] = null;
                }

                clusterIndexes.Clear();

                // Запись нового содержимого файла в кластеры
                AllocateClusters(fileEntry, newContent);

                // Обновление размера файла в записи MFT
                var fileSize = (ushort)newContent.Length;
                foreach (var attr in fileEntry.FileAttributes)
                {
                    if (attr.AttributeType == AttributeType.File)
                    {
                        attr.Value = BitConverter.GetBytes(fileSize)[0];
                        break;
                    }
                }

                Console.WriteLine($"Updated content of file: {fileEntry.FileName}");
            }
            else Console.WriteLine($" - can't change, readonly: {fileEntry.FileName}");
        }

        public void DeleteFile(MftEntry fileEntry)
        {
            
            // Освобождение всех кластеров, связанных с файлом
            if (fileEntry.FileAttributes.Any(attr => attr.AttributeType == AttributeType.File) || fileEntry.Content != null)
            {
                var clusterIndexes = fileEntry.ClusterIndexes;
                foreach (var clusterIndex in clusterIndexes)
                {
                    clustersData[clusterIndex] = null;
                }
            }
            // Удаление записи из MFT
            entries.Remove(fileEntry);

            if (fileEntry.SubEntries != null)
            {
                foreach (var subEntry in fileEntry.SubEntries.ToList())
                {
                    DeleteFile(subEntry);
                }
            }
            // Удаление ссылки на файл у родительской директории
            var parentDirectory = fileEntry.Parent;
            if (parentDirectory != null)
            {
                parentDirectory.SubEntries.Remove(fileEntry);
            }

            Console.WriteLine($"Deleted file: {fileEntry.FileName}");
        }

        // Выделение кластеров
        public void AllocateClusters(MftEntry fileEntry, string content)
        {
            var clusterIndexes = new List<int>();
            int startIndex = 0;

            while (startIndex < content.Length)
            {
                int clusterSize = Math.Min(ClusterSize, content.Length - startIndex);
                var cluster = content.Substring(startIndex, clusterSize);
                var clusterIndex = GetFreeClusterIndex();

                if (clusterIndex == -1)
                {
                    Console.WriteLine("Not enough free clusters to allocate.");
                    return;
                }

                clustersData[clusterIndex] = cluster;
                clusterIndexes.Add(clusterIndex);

                startIndex += clusterSize;
            }

            fileEntry.ClusterIndexes = clusterIndexes;
            Console.WriteLine($"Allocated {clusterIndexes.Count} clusters for file: {fileEntry.FileName}");
        }

        public int GetFreeClusterIndex()
        {
            for (int i = 0; i < TotalClusterCount; i++)
            {
                if (string.IsNullOrEmpty(clustersData[i]))
                    return i;
            }

            return -1;
        }

        public int FreeClusterCount()
        {
            return clustersData.Count(c => c == null);
        }

        public void SetFileReadOnly(MftEntry entry, bool isReadOnly)
        {
            entry.IsReadOnly = isReadOnly;

            // Обновление атрибута Read-only в записи MFT для файла
            foreach (var attr in entry.FileAttributes)
            {
                if (attr.AttributeType == AttributeType.File)
                {
                    attr.Value = Convert.ToByte(isReadOnly);
                    break;
                }
            }
        }

        

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
   
    public class MftEntry
    {
        public string FileName { get; set; }
        public List<FileAttribute> FileAttributes { get; set; }
        public List<MftEntry> SubEntries { get; set; }
        public MftEntry Parent { get; set; }
        public List<int> ClusterIndexes { get; set; }
        public string Content { get; set; }
        public bool IsReadOnly { get; set; }

        public MftEntry()
        {
            FileAttributes = new List<FileAttribute>();
            SubEntries = new List<MftEntry>();
        }
    }

    public class FileAttribute
    {
        public AttributeType AttributeType { get; set; }
        public byte Value { get; set; }
    }

    public enum AttributeType
    {
        File,
        Directory
    }
}
