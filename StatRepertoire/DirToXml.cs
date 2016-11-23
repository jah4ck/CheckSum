using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace StatRepertoire
{
    public static class DirToXml
    {
        
        public static XElement BuildTree(string dirName)

        {

            var di = new DirectoryInfo(dirName);
            var files = di.GetFiles();
            string toto = "";
            
            foreach (var file in files)

            {
                if (file.ToString().Contains(".dll"))
                {
                    toto = (file + ";" + dirName + @"\" + file.ToString() + ";" + GetMD5HashFromFile(dirName + @"\" + file.ToString()));
                }
                //write.Flush();
            }
            

            var subdirs = di.GetDirectories();

            // each item is a "directory" having 5 attributes

            // name is the name of the folder

            // fullpath is the full path including the name of the folder

            // size is the size of all files in the folder (in bytes)

            // files is the number of files in the folder

            // subdirs is the count of possible sub directories in the folder
            var elem = new XElement(new XElement("directory"));
            if (toto.Length > 5)
            {
                elem = new XElement("directory",

                    new XAttribute("name", toto));
            }

            foreach (var dinf in subdirs)

            {
                
                var elemDir = BuildTree(dirName + "\\" + dinf.Name);
                //Console.WriteLine(dirName + ";" + dirsize);
                
                    elem.Add(elemDir);
                

            }

 

            return elem;

        }

        public static string GetMD5HashFromFile(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }

    
    }
}
