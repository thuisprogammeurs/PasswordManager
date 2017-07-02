using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zzz.Core.Models
{
    public class Password
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string AccessAddress { get; set; }

        public string Notes { get; set; }

        public Group PasswordGroup { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
