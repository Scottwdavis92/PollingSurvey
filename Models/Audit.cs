using System.ComponentModel.DataAnnotations;

namespace PollingApplication.Models
{
    public class Audit
    {
        [Key]
        public int Id { get; set; }
        public string Action { get; set; }
        public string Modifiedby { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Details { get; set; }
    }
}
