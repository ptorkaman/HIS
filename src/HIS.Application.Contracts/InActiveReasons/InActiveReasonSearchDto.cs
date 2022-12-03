using HIS.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIS.InActiveReasons
{
    public class InActiveReasonSearchDto
    {
        public InActiveStatusEnum InActiveStatus { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
    }
}
