using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace temperature_converter_app_ef_core.Models
{
    public class ConvertOrAddUser
    {
        public List<User> Users { get; set; }
        public Conversion Conversion { get; set; }
        public User User { get; set; }
    }
}