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
        public NtfsTom cluster ; 

        // Добавьте поле для хранения выбранного файла
        private MftEntry selectedFileEntry;
        
        public Form1(string tom_name, int size_cluster, int kolvo_cluster)
        {
            cluster = new NtfsTom(tom_name, size_cluster, kolvo_cluster);
            InitializeComponent();
            InitializeContextMenu();
            CreateFilesAndDirectories();
            RefreshAll();
            Load += MainForm_Load; // Добавьте обработчик события Load формы
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
            // Проверяем, что выбранный узел MFT записи является файлом и выводим ошибку 
            if (parentEntry.FileAttributes.Any(attr => attr.AttributeType == AttributeType.File))
            {
                MessageBox.Show("Выбранный узел не является директорией. Выберите директорию для добавления директории.");
                return; // Прерываем выполнение кода
            }

            var newDirectory = cluster.CreateDirectory("New Directory", parentEntry);
            AddNodeToTreeView(newDirectory, selectedNode);
            dataGridView1.Rows.Clear();
            RefreshAll();
            //PrintMftEntry(parentEntry);
        }

        private void addFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Handler for adding a file
                var selectedNode = treeView1.SelectedNode;
                var parentEntry = (MftEntry)selectedNode.Tag;

            // Проверяем, что выбранный узел MFT записи является файлом и выводим ошибку 
            if (parentEntry.FileAttributes.Any(attr => attr.AttributeType == AttributeType.File))
            {
                MessageBox.Show("Выбранный узел не является директорией. Выберите директорию для добавления файла.");
                return; // Прерываем выполнение кода
            }

            var newFile = cluster.CreateFile("New File.txt", parentEntry, "Example file content");
                AddNodeToTreeView(newFile, selectedNode);
                dataGridView1.Rows.Clear();
                PrintMftEntry(parentEntry);
                RefreshAll();            
        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Handler for deleting a file
            var selectedNode = treeView1.SelectedNode;
            var fileEntry = (MftEntry)selectedNode.Tag;
            //var parentEntry = fileEntry.Parent;
            cluster.DeleteFile(fileEntry);
            selectedNode.Remove(); 
            RefreshAll();
            //dataGridView1.Rows.Clear();
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
                dataGridView1.Rows.Add("File Content:", entry.Content/*cluster.GetFileContent(entry)*/);
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

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            
            var root = cluster.CreateDirectory(cluster.tom_name, null);
            // Создание файлов и директорий

            var file1 = cluster.CreateFile("File1.txt", root, "Пример содержимого файла 1");
            var file2 = cluster.CreateFile("File2.txt", root, "Пример содержимого файла 2");
            var subDir = cluster.CreateDirectory("SubDir", root);
            var subDir2 = cluster.CreateDirectory("SubDir2", subDir);
            var subDir3 = cluster.CreateDirectory("SubDir3", subDir2);
            var subDir4 = cluster.CreateDirectory("SubDir4", subDir3);
            var file3 = cluster.CreateFile("File3.txt", subDir, "Пример содержимого файла 3");
            //cluster.UpdateFileContent(file1, "Првввимер содпппппержимого файла 1");
            cluster.SetFileReadOnly(file3, true);
            // Покажите контекстное меню в указанной позиции
            //contextMenuStrip2.Show(button1, new Point(0, button1.Height));

            //AddNodeToTreeView(root, null);         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void RefreshAll()
        {
            // Очистка DataGridView
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            PopulateTreeView();

            treeView1.AfterSelect += treeView1_AfterSelect;

            // Вывод содержимого всех кластеров
            PrintAllClustersData();

            // Вывод информации о файлах и директориях
            PrintMftEntry(GetMftEntryByName(cluster.tom_name));
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
                    okname_button.Text = selectedEntry.FileName;
                }
                else
                {
                    MessageBox.Show($"Ошибка: {selectedEntry.FileName}");
                    // Если выбранный элемент является директорией, очищаем TextBox и Label
                    filecontent_textbox.Text = string.Empty;
                    okname_button.Text = string.Empty;
                    filename_label.Text = string.Empty;

                }

            }
        }

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
                readonly_checkBox.Enabled = true;
                if (selectedEntry.IsReadOnly)
                {
                    filename_textBox.Enabled = false;
                    filecontent_textbox.Enabled = false;
                    readonly_checkBox.Checked = true;
                }
                else
                {
                    filename_textBox.Enabled = true;
                    filecontent_textbox.Enabled = true;
                    readonly_checkBox.Checked = false;
                }

                if (selectedEntry.FileAttributes.Any(attr => attr.AttributeType == AttributeType.File))
                {                   
                    // Если выбран файл, сохраняем ссылку на него
                    selectedFileEntry = selectedEntry;
                    filecontent_textbox.Text = cluster.GetFileContent(selectedEntry)/*selectedEntry.Content */;
                    filename_textBox.Text = selectedEntry.FileName;
                    filename_label.Text = selectedEntry.FileName;
                    //filecontent_textbox.Text = selectedEntry.FileName;
                    save_button.Enabled = true;
                    readonly_button.Enabled = true;
                }
                else
                {
                    selectedFileEntry = selectedEntry;
                    filename_textBox.Text = selectedEntry.FileName;
                    filename_label.Text = selectedEntry.FileName;
                    filecontent_textbox.Text = string.Empty;
                    save_button.Enabled = false;
                    readonly_checkBox.Enabled = false;
                    readonly_button.Enabled = false;
                }
            }
            else
            {
                selectedFileEntry = null;
                filecontent_textbox.Text = string.Empty;
                filename_textBox.Text = string.Empty;
                filename_label.Text = string.Empty;
                save_button.Enabled = false;
                readonly_checkBox.Enabled = false;
                readonly_button.Enabled = false;
            }
            
        }

        // Обработчик события нажатия на кнопку сохранения
        private void save_button_Click(object sender, EventArgs e)
        {
            if (selectedFileEntry != null)
            {
                // Получение нового содержимого из TextBox
                string newContent = filecontent_textbox.Text;

                // Обновление содержимого файла в MFT и кластерах
                cluster.UpdateFileContent(selectedFileEntry, newContent);
                RefreshAll();
            }
        }

        private void okname_button_Click(object sender, EventArgs e)
        {
            //filename_textBox.Text = null;
            if (selectedFileEntry != null)
            {
                selectedFileEntry.FileName = filename_textBox.Text;
                RefreshAll();
            }
        }

        private void readonly_button_Click(object sender, EventArgs e)
        {
            if (selectedFileEntry != null)
            {
                if (readonly_checkBox.Checked)
                {
                    cluster.SetFileReadOnly(selectedFileEntry, true);
                    filename_textBox.Enabled = false;
                    filecontent_textbox.Enabled = false;                  
                }
                else
                {
                    cluster.SetFileReadOnly(selectedFileEntry, false);
                    filename_textBox.Enabled = true;
                    filecontent_textbox.Enabled = true;                  
                }

            }
            RefreshAll();
        }
    }

    public class NtfsTom
    {
        public string tom_name;
        public List<MftEntry> entries;
        private List<string> clustersData;
        // Размер кластера в символах
        private int ClusterSize = 10;
        private int TotalClusterCount = 20;

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

                //Console.WriteLine($"Created file: {fileName} in '{parentDirectory.FileName}'");
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

                Console.WriteLine($"Error, you try to add file not to directory. Created file: {fileName} in '{parentDirectory.Parent.FileName}'");

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

            Console.WriteLine($"Created directory: {directoryName}");

            return directoryEntry;
        }

        public void UpdateFileContent(MftEntry fileEntry, string newContent)
        {
            if (!fileEntry.IsReadOnly)
            {
                var clusterIndexes = fileEntry.ClusterIndexes;
                //fileEntry.Content = newContent;

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

                    if (newContent.Length <= 5 && newContent.Length > 0)
                    {
                        fileEntry.Content = newContent;
                    }
                    else
                    {
                        fileEntry.Content = null;                       
                        AllocateClusters(fileEntry, newContent);
                    }
                    // Запись нового содержимого файла в кластеры
                    // AllocateClusters(fileEntry, newContent);

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
            }
            else Console.WriteLine($" - can't change, readonly: {fileEntry.FileName}");
            
        }

        public void DeleteFile(MftEntry fileEntry)
        {
            
            // Освобождение всех кластеров, связанных с файлом
            if (fileEntry.FileAttributes.Any(attr => attr.AttributeType == AttributeType.File) /*|| fileEntry.Content != null*/)
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
            string buffer = GetFileContent(fileEntry);
            var clusterIndexes = new List<int>();
            int startIndex = 0;

            while (startIndex < content.Length)
            {            
                var clusterIndex = GetFreeClusterIndex();
                int clusterSize = Math.Min(ClusterSize, content.Length - startIndex);
                var cluster = content.Substring(startIndex, clusterSize);

                if (clusterIndex == -1)
                {
                    MessageBox.Show("Ошибка: Нет свободных кластеров для записи содержимого файла.");
                    AllocateClusters(fileEntry, buffer);
                    break;
                }
                
                clustersData[clusterIndex] = cluster;
                clusterIndexes.Add(clusterIndex);

                startIndex += clusterSize;
            }
            
            fileEntry.ClusterIndexes = clusterIndexes;
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
        private int GetNextFreeClusterIndex(int startIndex)
        {
            for (int i = startIndex; i < TotalClusterCount; i++)
            {
                if (string.IsNullOrEmpty(clustersData[i]))
                    return i;
            }

            return -1;
        }
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
