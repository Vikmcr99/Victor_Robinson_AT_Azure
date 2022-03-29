using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core
{
    public class Friend
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Telphone { get; set; }

        public string Email { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

    }
}
