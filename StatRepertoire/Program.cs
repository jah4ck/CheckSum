using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Security.Cryptography;


namespace StatRepertoire
{
    class Program
    {
        static void Main(string[] args)
        {

            //DirToXml.BuildTree(@"C:\Windows\Microsoft.NET\Framework");
            StreamWriter write = new StreamWriter(@"C:\TEMP\RESULT_JS\MD5.txt");

            string[] lstFile = Directory.GetFiles(@"C:\Windows\Microsoft.NET", "*", SearchOption.AllDirectories);
            foreach (string file in lstFile)
            {
                try
                {
                    string nom = file.Substring(file.LastIndexOf(@"\") + 1);
                    string md5 = GetMD5HashFromFile(file);
                    write.WriteLine(nom + ";" + file + @"\" + file.ToString() + ";" + md5);
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
                
            }
           // write.WriteLine(DirToXml.BuildTree(@"C:\Windows\Microsoft.NET"));
            write.Close();
            Console.WriteLine("done!");
            Console.ReadLine();
                        
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
