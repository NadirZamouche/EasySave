using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetVersion1
{
    internal class File_details
    {
        public string Name { get; set; }
        public string FileSource { get; set; }
        public string FileTarget { get; set; }
        public long FileSize { get; set; }
        public int FileSaveDuration { get; set; }
        public DateTime SavedAt { get; set; }
    }
}
