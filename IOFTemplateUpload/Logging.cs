using System;
using System.Configuration;
using System.IO;

namespace IOFTemplateUpload
{
    public static class Logging
    {

        #region Static Methods


        public static void FileErrorLogging(string message, string fileName, Exception ex)
        {
            string strPath = ConfigurationManager.AppSettings["LogFile"];
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Institution Name: " + fileName);
                sw.WriteLine("Message: " + message);

                string msg = string.Format("Source: {0} \nMessage: {1} \nStackTrace: {2} \nInnerException: {3}"
                , ex.Message
                , ex.StackTrace
                , ex.Source
                , ex.InnerException != null ? ex.InnerException.Message : "");
                sw.WriteLine("Exception: " + msg);
            }
        }

        public static void FileErrorLogging(Exception ex)
        {
            string strPath = ConfigurationManager.AppSettings["LogFile"];
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                string msg = string.Format("Source: {0} \nMessage: {1} \nStackTrace: {2} \nInnerException: {3}"
                , ex.Message
                , ex.StackTrace
                , ex.Source
                , ex.InnerException != null ? ex.InnerException.Message : "");
                sw.WriteLine("Exception: " + msg);
                sw.WriteLine(string.Format("==================={0}===============", DateTime.Now));
            }
        }

        public static void LogESFILE(ESFile eSFile)
        {
            string strPath = ConfigurationManager.AppSettings["LogFile"];
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("FileName:  " + eSFile.FileName);
                sw.WriteLine("SPWebId: " + eSFile.SPWebId);
                sw.WriteLine("SPSiteId: " + eSFile.SPSiteId);
                sw.WriteLine("SPFileId: " + eSFile.SPFileId);
                sw.WriteLine("SPListId: " + eSFile.SPListId);
                //sw.WriteLine("ItemId: " + eSFile.ItemId);
                //sw.WriteLine(string.Format("FileName = {0}, SPWebId = {1}, SPSiteId = {2}, SPFileId = {3}, SPListId = {4}", eSFile.FileName, eSFile.SPWebId, eSFile.SPSiteId, eSFile.SPFileId, eSFile.SPListId));
                sw.WriteLine("=================================="+ DateTime.Now);
            }
        }

        #endregion

    }
}
