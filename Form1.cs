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
        public NtfsTom ntfstom; 

        // Добавьте поле для хранения выбранного файла
        private MftEntry selectedFileEntry;
        
        public Form1(string tom_name, int size_cluster, int kolvo_cluster)
        {
            ntfstom = new NtfsTom(tom_name, size_cluster, kolvo_cluster);
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
        
        /*Добавление директории*/
        private void addDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Handler for adding a directory
            var selectedNode = treeView1.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("Ошибка. Выберите узел для создания файла.");
                return;
            }
            
            var parentEntry = (MftEntry)selectedNode.Tag;
            //Проверяем, что выбранный узел MFT записи является файлом и выводим ошибку 
            if (parentEntry.FileAttributes.Any(attr => attr.AttributeType == AttributeType.File))
            {
                MessageBox.Show("Выбранный узел не является директорией. Выберите директорию для добавления директории.");
                return;
            }

            var newDirectory = ntfstom.CreateDirectory("New Directory", parentEntry);
            AddNodeToTreeView(newDirectory, selectedNode);
            dataGridView1.Rows.Clear();
            RefreshAll();
        }
        
        /*Добавление файла*/
        private void addFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = treeView1.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("Ошибка. Выберите узел для создания файла.");
                return;
            }          
            var parentEntry = (MftEntry)selectedNode.Tag;

            //Проверяем, что выбранный узел MFT записи является файлом и выводим ошибку 
            if (parentEntry.FileAttributes.Any(attr => attr.AttributeType == AttributeType.File))
            {
                MessageBox.Show("Выбранный узел не является директорией. Выберите директорию для добавления файла.");
                return; 
            }

            var newFile = ntfstom.CreateFile("New File.txt", parentEntry, "Example file content");
            AddNodeToTreeView(newFile, selectedNode);
            dataGridView1.Rows.Clear();
            PrintMftEntry(parentEntry);
            RefreshAll();            
        }

        /*Удаление элемента*/
        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Событие на удаление файла при выборе в контекстном меню соотв. пункта
            var selectedNode = treeView1.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("Выберите узел для удаления файла или директории.");
                return;
            }
            else if (selectedNode.Parent == null)
            {
                MessageBox.Show("Ошибка. Невозможно удалить том целиком.");
                return;
            }
            var fileEntry = (MftEntry)selectedNode.Tag;

            ntfstom.DeleteFile(fileEntry);
            selectedNode.Remove(); 
            RefreshAll();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        /*Вывод содержимого MFT в таблицу*/
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
                dataGridView1.Rows.Add("File Content:", entry.Content/*ntfstom.GetFileContent(entry)*/);
            }

            foreach (var subEntry in entry.SubEntries)
            {
                PrintMftEntry(subEntry, indent + "\t");
            }
        }

        /*Вывод содержимого кластеров в таблицу*/
        private void PrintAllClustersData()
        {
            for (int i = 0; i < ntfstom.ClustersData.Count; i++)
            {
                var clusterData = ntfstom.ClustersData[i];
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

        /*Начальное создание файлов и директорий*/
        private void CreateFilesAndDirectories()
        {           
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Type", "Type");

            dataGridView2.Columns.Add("Number", "Number");
            dataGridView2.Columns.Add("Content", "Content");

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            
            var root = ntfstom.CreateDirectory(ntfstom.tom_name, null);
            
            // Создание файлов и директорий
            var file1 = ntfstom.CreateFile("File1.txt", root, "Пример содержимого файла 1");
            var file2 = ntfstom.CreateFile("File2.txt", root, "Пример содержимого файла 2");
            var subDir = ntfstom.CreateDirectory("SubDir", root);
            var subDir2 = ntfstom.CreateDirectory("SubDir2", subDir);
            var subDir3 = ntfstom.CreateDirectory("SubDir3", subDir2);
            var subDir4 = ntfstom.CreateDirectory("SubDir4", subDir3);
            var file3 = ntfstom.CreateFile("File3.txt", subDir, "Пример содержимого файла 3");         
            ntfstom.SetFileReadOnly(file3, true);
       
        }

        /*Ручное обновление содержимого*/
        private void button1_Click(object sender, EventArgs e)
        {
            RefreshAll();
        }

        /*Обновление содержимого*/
        private void RefreshAll()
        {
            // Очистка DataGridView
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            PopulateTreeView();           
           
            treeView1.AfterSelect += treeView1_AfterSelect;

            /*Вывод содержимого всех кластеров*/
            PrintAllClustersData();

            /*Вывод информации из MFT о файлах и директориях*/
            PrintMftEntry(GetMftEntryByName(ntfstom.tom_name));
        }

        /*Добавление узла в структуру тома*/
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

        /*Выдача MFT записи по имени*/
        public MftEntry GetMftEntryByName(string fileName)
        {
            foreach (var entry in ntfstom.entries)
            {
                if (entry.FileName == fileName)
                {
                    return entry;
                }
            }
            return null; // Если не найдено совпадение, возвращаем null
        }
        
        /*Формирование структуры тома*/
        private void PopulateTreeView()
        {
            treeView1.Nodes.Clear();
            Dictionary<MftEntry, TreeNode> addedNodes = new Dictionary<MftEntry, TreeNode>();

            foreach (MftEntry entry in ntfstom.entries)
            {
                if (!addedNodes.ContainsKey(entry))
                {
                    TreeNode node = CreateTreeNode(entry, addedNodes);
                    treeView1.Nodes.Add(node);
                }
            }
            treeView1.ExpandAll();
        }

        /*Создание узла в структуре тома*/
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

        /*Обработка события при нажатии на элемент тома*/
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

                    if (selectedEntry.Content != null)
                    {
                        filecontent_textbox.Text = selectedEntry.Content;
                    }
                    else
                    {
                       filecontent_textbox.Text = ntfstom.GetFileContent(selectedEntry);                                        
                    }
                    filename_textBox.Text = selectedEntry.FileName;
                    save_button.Enabled = true;
                    readonly_button.Enabled = true;
                }
                else
                {
                    selectedFileEntry = selectedEntry;
                    filename_textBox.Text = selectedEntry.FileName;
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
                save_button.Enabled = false;
                readonly_checkBox.Enabled = false;
                readonly_button.Enabled = false;
            }
            
        }

        /*Нажатие на кнопку сохранения нового содержимого в файле*/
        private void save_button_Click(object sender, EventArgs e)
        {
            if (selectedFileEntry != null)
            {
                // Получение нового содержимого из TextBox
                string newContent = filecontent_textbox.Text;

                // Обновление содержимого файла в MFT и кластерах
                ntfstom.UpdateFileContent(selectedFileEntry, newContent);
                RefreshAll();
            }
        }

        /*Кнопка переименования*/
        private void okname_button_Click(object sender, EventArgs e)
        {
            if (selectedFileEntry != null)
            {
                selectedFileEntry.FileName = filename_textBox.Text;
                RefreshAll();
            }
        }

        /*Кнопка сохранения состояния readonly*/
        private void readonly_button_Click(object sender, EventArgs e)
        {
            if (selectedFileEntry != null)
            {
                if (readonly_checkBox.Checked)
                {
                    ntfstom.SetFileReadOnly(selectedFileEntry, true);
                    filename_textBox.Enabled = false;
                    filecontent_textbox.Enabled = false;                  
                }
                else
                {
                    ntfstom.SetFileReadOnly(selectedFileEntry, false);
                    filename_textBox.Enabled = true;
                    filecontent_textbox.Enabled = true;                  
                }
            }
            RefreshAll();
        }
    }
}
