using System;
using System.Collections.Generic;
using System.Text;

namespace HIS.IdentifierTypes
{
    public class IdentifierTypeSearchDto
    {
        public string Regex { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
    }
}
