using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livrable_3.Model
{
    internal class Save
    {
        public string Name { get; set; }
        public int NbFiles { get; set; }
        public long TotalSaveSize { get; set; }
        public int SaveDuration { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
