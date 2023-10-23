using Microsoft.EntityFrameworkCore;

namespace EtaLearning.DataAccess.Data.Entities
{
    public class SmartDevice
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Kind { get; set; }
        public int Type { get; set; }
        public DateTime Created { get; set; }
     
    }
}
