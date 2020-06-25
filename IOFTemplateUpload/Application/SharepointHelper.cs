using Microsoft.SharePoint;

namespace IOFTemplateUpload.Application
{
    public class SharepointHelper
    {
        public static void UploadFile(string siteUrl, string folderUrl, string fileName, byte[] file2Upload)
        {
            ESFile retValue = null;
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (var site = new SPSite(siteUrl))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        web.AllowUnsafeUpdates = true;
                        SPFolder folderToUploadUnder = null;
                        folderToUploadUnder = !CheckFolderExist(web, folderUrl) ? web.Folders.Add(folderUrl) : web.GetFolder(folderUrl);
                        SPFile uploadedFile = folderToUploadUnder.Files.Add(fileName, file2Upload, true);
                        retValue = new ESFile
                        {
                            FileName = fileName,
                            FileSize = uploadedFile.Length,
                            FilePath = uploadedFile.Url,
                            SPSiteId = web.Site.ID,
                            SPWebId = web.ID,
                            SPFileId = uploadedFile.UniqueId,
                            SPListId = folderToUploadUnder.ParentListId,
                            StorageRootPath = web.Site.Url,
                            StoragePath = folderToUploadUnder.ServerRelativeUrl.TrimStart('/'),
                            ItemId = uploadedFile.Item.ID,
                            FileExtension = fileName.Substring(fileName.LastIndexOf(".") + 1)
                        };

                        Logging.LogESFILE(retValue);
                        web.AllowUnsafeUpdates = false;
                    }
                }
            });
        }

        public static bool CheckFolderExist(SPWeb web, string folderUrl)
        {
            return web.GetFolder(folderUrl).Exists;
        }

    }
}
