using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

namespace Kr1
{
    public partial class Form1 : Form
    {
        string path = "C:/Users/Student/Documents/Visual Studio 2019/Code Snippets/Visual C#/My Code Snippets/Prob1.txt";//путь читаемого файла
        string path2 = "C:/Users/Student/Documents/Visual Studio 2019/Code Snippets/Visual C#/My Code Snippets/NewProb1.txt";//путь файла, который создаётся посредством копирования
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TryExplicitCatchFinally.Mai(path);//считает количество символов
            TryExplicitCatchFinally.Mai2(path);//считает длинну + записывает информацию в байтах по файлу в переменную типа MemoryStream
            TryExplicitCatchFinally.Mai3(path, path2);//сохдаёт копию файла в той же папке, используя класс Stream
        }
    }
    class TryExplicitCatchFinally
    {
        public static void Mai(string path)
        {
            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader(path);
                string contents = streamReader.ReadToEnd();
                var info = new StringInfo(contents);
                Console.WriteLine($"The file has {info.LengthInTextElements} text elements.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file cannot be found.");
            }
            catch (IOException)
            {
                Console.WriteLine("An I/O error has occurred.");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("There is insufficient memory to read the file.");
            }
            finally
            {
                streamReader?.Dispose();
            }
        }
        public static void Mai2(string path)
        {
            //создаю MemoryStream, чтобы туда сохранять
            MemoryStream destination = new MemoryStream();

            using (FileStream source = File.Open(path,
                FileMode.Open))
            {

                Console.WriteLine("Source length: {0}", source.Length.ToString());

                // копирую
                source.CopyTo(destination);
            }

            Console.WriteLine("Destination length: {0}", destination.Length.ToString());
            /*
            int length = 1000000;
            long start = 1000000;
            byte[] buffer = new byte[length];
            using FileStream fileStream = File.OpenRead(path);
            fileStream.Seek(start, SeekOrigin.Begin);
            fileStream.Read(buffer, 0, length);*/
        }
        public static void Mai3(string path, string path2)
        {

            try
            {
                // Create the file, or overwrite if the file exists.
                

                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(path))
                { 
                    string s = "";
                    using (FileStream fs = File.Create(path2))
                    {
                        while ((s = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(s);
                            byte[] info = new UTF8Encoding(true).GetBytes(s);
                            // Add some information to the file.
                            fs.Write(info, 0, info.Length);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
