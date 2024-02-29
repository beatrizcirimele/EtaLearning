using System;
using System.Collections.Generic;

namespace EtaLearning.DataAccess.Data.Entities
{
    public class DbClient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        // Use SmartDevice entity from the correct namespace
        public List<SmartDevice> SmartDevices { get; set; }
    }
}
