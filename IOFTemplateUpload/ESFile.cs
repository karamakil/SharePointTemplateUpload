using System;

namespace IOFTemplateUpload
{
    public class ESFile
    {
        public string StorageRootPath { get; set; }
        public string StoragePath { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public Int64 FileSize { get; set; }
        public string FilePath { get; set; }
        public Guid SPSiteId { get; set; }
        public Guid SPWebId { get; set; }
        public Guid SPListId { get; set; }
        public Guid SPFileId { get; set; }
        public int ItemId { get; set; }
    }
}
