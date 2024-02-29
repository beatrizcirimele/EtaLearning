using System.ComponentModel.DataAnnotations;


namespace EtaLearning.DataAccess.Data.Entities
{
    public class SmartDevice
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Kind { get; set; }
        public int Type { get; set; }
        public DateTime Created { get; set; }
        public int ClientId { get; set; }  // Foreign key property
        public DbClient Client { get; set; }
        public DateTime LastModified { get; set; }
    }
}
