using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colorful;
using Console = Colorful.Console;

namespace ProjetVersion1
{
    internal class Logo
    {
        public static void WriteLogo()
        {
            string Logo = @"                            __ _                                                                       _               __   _____ 
                           / _| |                                                                     (_)             /  | |  _  |
 _ __  _ __ ___  ___  ___ | |_| |_ ______ ___  __ _ ___ _   _ ___  __ ___   _____  __   _____ _ __ ___ _  ___  _ __   `| | | |/' |
| '_ \| '__/ _ \/ __|/ _ \|  _| __|______/ _ \/ _` / __| | | / __|/ _` \ \ / / _ \ \ \ / / _ \ '__/ __| |/ _ \| '_ \   | | |  /| |
| |_) | | | (_) \__ \ (_) | | | |_      |  __/ (_| \__ \ |_| \__ \ (_| |\ V /  __/  \ V /  __/ |  \__ \ | (_) | | | | _| |_\ |_/ /
| .__/|_|  \___/|___/\___/|_|  \__|      \___|\__,_|___/\__, |___/\__,_| \_/ \___|   \_/ \___|_|  |___/_|\___/|_| |_| \___(_)___/ 
| |                                                      __/ |                                                                    
|_|                                                     |___/                                                                     

";
            Console.WriteLine(Logo, Color.BlueViolet);
        }
    }
}
