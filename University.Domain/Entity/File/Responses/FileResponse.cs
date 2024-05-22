namespace University.Domain.Entity.File.Responses
{
    public class FileResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public int OwnerId { get; set; }

        public FileResponse(FileEntity file)
        {
            Id = file.Id;
            Name = file.Name;
            Size = file.Size;
            OwnerId = file.OwnerId;
        }

        public static implicit operator FileResponse(FileEntity file)
        {
            return new FileResponse(file);
        }
    }
}
