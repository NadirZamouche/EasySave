using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Livrable_3.ViewModel
{
    public class FullBackupViewModel
    {
        public class logs
        {
            public string name { get; set; }
            public string source { get; set; }
            public string destination { get; set; }
            public double size { get; set; }
            public DateTime startTime { get; set; }
        }
        bool pause = false;
        bool cancel = false;

        public void Cancel()
        {
            cancel = true;
        }
        public void Pause()
        {
            pause = true;
        }
        public void Resume()
        {
            pause = false;
        }
        List<logs> _datafile = new List<logs>();
        public void CopyConvert(string sourceDirectory, string targetDirectory, bool DsubDirectory, bool metier, long size)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);
            _datafile.Clear();
            CopyAll(diSource, diTarget, DsubDirectory, metier, size);

        }
        
        public void CopyAll(DirectoryInfo source, DirectoryInfo target, bool DsubDirectory, bool metier, long size)
        {
            
            Directory.CreateDirectory(target.FullName);
            
            // Copy each file into the new directory.
            foreach (FileInfo file in source.GetFiles())
            {
                while (pause == true)
                {
                    Thread.Sleep(1000);
                }
                if (cancel == true)
                {
                    return;
                }
                DateTime timenowfile = DateTime.Now;
                long length = new System.IO.FileInfo(file.FullName).Length;
                
                
                if (metier == true)
                {
                    if (System.IO.Path.GetExtension(file.FullName) != ".exe" && length < size)
                    {
                        file.CopyTo(System.IO.Path.Combine(target.FullName, file.Name), true);
                        _datafile.Add(new logs()
                        {
                            name = file.Name,
                            source = file.FullName,
                            destination = System.IO.Path.Combine(target.FullName, file.Name),
                            size = length,
                            startTime = timenowfile,

                        });
                    }

                }
                else
                {
                    if (length < size)
                    {
                        file.CopyTo(System.IO.Path.Combine(target.FullName, file.Name), true);
                        _datafile.Add(new logs()
                        {
                            name = file.Name,
                            source = file.FullName,
                            destination = System.IO.Path.Combine(target.FullName, file.Name),
                            size = length,
                            startTime = timenowfile,

                        });
                        

                    }
                }

            }
            
            
            if (DsubDirectory == true)
            {
                // Copy each subdirectory using recursion.
                foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                {
                    DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                    CopyAll(diSourceSubDir, nextTargetSubDir, DsubDirectory, metier, size);
                    
                }
            }
            string StrResultJsonfile = JsonConvert.SerializeObject(_datafile.ToArray(), Newtonsoft.Json.Formatting.Indented);
            DateTime date = DateTime.Now;
            string time = date.ToString();
            var charsToRemovef = new string[] { ":", "/" };
            foreach (var c in charsToRemovef)
            {
                time = time.Replace(c, ".");
            }
            string path = @"C:\000\log\json\" + time + "complet.txt";
            File.Create(path).Dispose();

            using (TextWriter tw = new StreamWriter(path))
            {
                tw.Write(StrResultJsonfile);
            }
            XNode node = JsonConvert.DeserializeXNode(@"{
   
      ""row"": " + StrResultJsonfile + " \r\n}", "Root");

            string pathxml = @"C:\000\log\xml\" + time + "complet.xml";
            File.Create(pathxml).Dispose();

            using (TextWriter tw = new StreamWriter(pathxml))
            {
                tw.Write(node);
            }

        }
    }
}
