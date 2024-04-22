using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livrable_3.ViewModel
{
    internal class EncryptionViewModel
    {
        public string item;
        public int key;
        public string destination;
        public void Encryption()
        {

            string cr_contents = System.IO.File.ReadAllText(item);
            string cr_filename = System.IO.Path.GetFileNameWithoutExtension(item);
            string cr_extention = System.IO.Path.GetExtension(item);
            string crypted = EncryptDecrypt(cr_contents, key);
            if (cr_filename.Contains("_crypted"))
            {
                var charsToRemovef = new string[] { "_crypted" };
                foreach (var c in charsToRemovef)
                {
                    cr_filename = cr_filename.Replace(c, string.Empty);
                }
                System.IO.File.WriteAllText(destination + @"\" + cr_filename + "_derypted" + cr_extention, crypted);
            }
            else if (cr_filename.Contains("_derypted"))
            {
                var charsToRemovef = new string[] { "_derypted" };
                foreach (var c in charsToRemovef)
                {
                    cr_filename = cr_filename.Replace(c, string.Empty);
                }
                System.IO.File.WriteAllText(destination + @"\" + cr_filename + "_crypted" + cr_extention, crypted);
            }
            else
            {
                System.IO.File.WriteAllText(destination + @"\" + cr_filename + "_crypted" + cr_extention, crypted);
            }
            
        }
        public string EncryptDecrypt(string szPlainText, int szEncryptionKey)
        {
            StringBuilder szInputStringBuild = new StringBuilder(szPlainText);
            StringBuilder szOutStringBuild = new StringBuilder(szPlainText.Length);
            char Textch;
            for (int iCount = 0; iCount < szPlainText.Length; iCount++)
            {
                Textch = szInputStringBuild[iCount];
                Textch = (char)(Textch ^ szEncryptionKey);
                szOutStringBuild.Append(Textch);
            }
            return szOutStringBuild.ToString();

        }
    }
}
