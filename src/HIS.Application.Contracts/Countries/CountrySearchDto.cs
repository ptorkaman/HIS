using System;
using System.Collections.Generic;
using System.Text;

namespace HIS.Countries
{
    public class CountrySearchDto
    {
        public string Name { get; set; }
        public string TwoLettersIsoCode { get; set; }
        public string ThreeLettersIsoCode { get; set; }
        public string NumericIsoCode { get; set; }
        public int Sequence { get; set; }
    }
}
