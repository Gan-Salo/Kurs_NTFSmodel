using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class MftEntry
    {
        public string FileName { get; set; } //Имя файла
        public List<FileAttribute> FileAttributes { get; set; } //Список атрибутов файла
        public List<MftEntry> SubEntries { get; set; } //Список файлов, на которые имеется указатель
        public MftEntry Parent { get; set; } //Ссылка на сущность-родителя
        public List<int> ClusterIndexes { get; set; } //Список номеров кластеров, в которых записано содержимое
        public string Content { get; set; } //Хранение содержимого в MFT записи, если оно небольшое
        public bool IsReadOnly { get; set; } //Флаг атрибуты 'только для чтения'

        public MftEntry()
        {
            FileAttributes = new List<FileAttribute>();
            SubEntries = new List<MftEntry>();
        }
    }
}
