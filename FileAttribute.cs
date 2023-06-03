using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    /*Список атрибутов файла*/
    public class FileAttribute
    {
        public AttributeType AttributeType { get; set; }
    public byte Value { get; set; }
}

/*Список типов сущности в томе*/
public enum AttributeType
{
    File,
    Directory
}
}
