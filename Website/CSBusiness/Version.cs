
namespace CSBusiness
{
    public class Version
    {

        public int VersionId { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        
        public bool Visible { get; set; }
        public bool HideRemove { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
    }
}
