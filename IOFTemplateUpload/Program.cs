using IOFTemplateUpload.Application;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace IOFTemplateUpload
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var fileArray = GetFolderFiles();
                for (int i = 0; i < fileArray.Length; i++)
                {
                    byte[] fileByte = File.ReadAllBytes(fileArray[i]);
                    var fileName = Path.GetFileName(fileArray[i]);
                    var fileNameWithNoSepcialChar = RemoveFileNameSpecialCharacters(fileName);
                    var noSpaces = fileNameWithNoSepcialChar.Replace(" ", string.Empty);
                    string siteUrl = ConfigurationManager.AppSettings["SharepointSiteUrl"];
                    string folderUrl = ConfigurationManager.AppSettings["SharepointFolder"];
                    SharepointHelper.UploadFile(siteUrl, folderUrl, noSpaces, fileByte);
                }
            }
            catch (System.Exception ex)
            {
                Logging.FileErrorLogging(ex);
            }
        }

        public static string[] GetFolderFiles()
        {
            string[] filePaths = Directory.GetFiles(ConfigurationManager.AppSettings["TemplateFolderPath"]);
            return filePaths;
        }

        public static string RemoveFileNameSpecialCharacters(string fileName)
        {
            string retValue = Regex.Replace(fileName, "[|?{}#$%&:<>*~\"-]", " ").Replace('/', '-');//last replace for doc reference
            retValue = Regex.Replace(retValue, @"\.+", "."); //csa..pdf=>csa.pdf
            return retValue.Length > 100 ? (retValue.Substring(0, 95)) + fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf(".")) : retValue;//+ "..."+extension =>...,pdf will throw exception
        }

    }
}
