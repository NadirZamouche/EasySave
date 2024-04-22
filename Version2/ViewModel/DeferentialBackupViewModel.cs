using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Livrable_3.ViewModel
{
    internal class DeferentialBackupViewModel
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
        public void CopyDirectoryDiff(string sourceDir, string destinationDir, double size, bool metier)
        {
            _datafile.Clear();
            List<string> DestList = Directory.EnumerateFiles(destinationDir, "*.*", SearchOption.AllDirectories).ToList(); ;
            List<string> SrcList = Directory.EnumerateFiles(sourceDir, "*.*", SearchOption.AllDirectories).ToList();
            List<FileInfo> srcList = new List<FileInfo>();
            foreach (var item in SrcList)
            {
                srcList.Add(new FileInfo(item));
            }
            List<FileInfo> desList = new List<FileInfo>();
            foreach (var item in DestList)
            {
                desList.Add(new FileInfo(item));
            }
            List<FileInfo> changedNbList = new List<FileInfo>();
            List<FileInfo> NewFiles = new List<FileInfo>();

            foreach (FileInfo file in srcList)
            {
                int counter = 0;
                foreach (var item in desList)
                {
                    string pathFile = file.FullName;
                    pathFile = pathFile.Replace(sourceDir, "");
                    string pathDestFile = item.FullName;
                    pathDestFile = pathDestFile.Replace(destinationDir, "");
                    if (pathFile == pathDestFile)
                    {
                        counter++;
                        if (file.Length == item.Length)
                        {
                            break;
                        }
                        else
                        {
                            changedNbList.Add(file);
                        }
                    }

                }
                if (counter == 0)
                {
                    NewFiles.Add(file);
                }

            }
            DateTime timenowfile = DateTime.Now;

            foreach (FileInfo file in NewFiles)
            {
                string pathFile = file.FullName;
                pathFile = pathFile.Replace(sourceDir, "");
                string targetFilePath = destinationDir + @"\" + pathFile;
                Directory.CreateDirectory(new FileInfo(targetFilePath).DirectoryName);
                long length = new System.IO.FileInfo(file.FullName).Length;
                if (length < size)
                {
                    if (metier == true)
                    {

                        if (System.IO.Path.GetExtension(targetFilePath) != ".exe")
                        {
                            while (pause == true)
                            {
                                Thread.Sleep(1000);
                            }
                            if (cancel == true)
                            {
                                return;
                            }
                            file.CopyTo(targetFilePath, true);
                            _datafile.Add(new logs()
                            {
                                name = file.Name,
                                source = file.FullName,
                                destination = System.IO.Path.Combine(targetFilePath, file.Name),
                                size = length,
                                startTime = timenowfile,

                            });
                        }
                    }
                    else
                    {
                        while (pause == true)
                        {
                            Thread.Sleep(1000);
                        }
                        if (cancel == true)
                        {
                            return;
                        }
                        file.CopyTo(targetFilePath, true);
                        _datafile.Add(new logs()
                        {
                            name = file.Name,
                            source = file.FullName,
                            destination = System.IO.Path.Combine(targetFilePath, file.Name),
                            size = length,
                            startTime = timenowfile,

                        });
                    }
                }



            }
            foreach (FileInfo file in changedNbList)
            {
                string pathFile = file.FullName;
                pathFile = pathFile.Replace(sourceDir, "");
                string targetFilePath = destinationDir + @"\" + pathFile;
                file.Directory.Create();
                long length = new System.IO.FileInfo(file.FullName).Length;
                if (length < size)
                {
                    if (metier == true)
                    {
                        if (System.IO.Path.GetExtension(targetFilePath) != ".exe")
                        {
                            while (pause == true)
                            {
                                Thread.Sleep(1000);
                            }
                            if (cancel == true)
                            {
                                return;
                            }
                            file.CopyTo(targetFilePath, true);
                            _datafile.Add(new logs()
                            {
                                name = file.Name,
                                source = file.FullName,
                                destination = System.IO.Path.Combine(targetFilePath, file.Name),
                                size = length,
                                startTime = timenowfile,

                            });
                        }
                    }
                    else
                    {
                        while (pause == true)
                        {
                            Thread.Sleep(1000);
                        }
                        if (cancel == true)
                        {
                            return;
                        }
                        file.CopyTo(targetFilePath, true);
                        _datafile.Add(new logs()
                        {
                            name = file.Name,
                            source = file.FullName,
                            destination = System.IO.Path.Combine(targetFilePath, file.Name),
                            size = length,
                            startTime = timenowfile,

                        });
                    }
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
            string path = @"C:\000\log\json\" + time + "différentielle.txt";
            File.Create(path).Dispose();

            using (TextWriter tw = new StreamWriter(path))
            {
                tw.Write(StrResultJsonfile);
            }
            XNode node = JsonConvert.DeserializeXNode(@"{
   
      ""row"": " + StrResultJsonfile + " \r\n}", "Root");

            string pathxml = @"C:\000\log\xml\" + time + "différentielle.xml";
            File.Create(pathxml).Dispose();

            using (TextWriter tw = new StreamWriter(pathxml))
            {
                tw.Write(node);
            }
        }
    }
}
