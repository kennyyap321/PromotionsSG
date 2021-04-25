using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.DBTableModels
{
    public class CodeTable
    {
        [Key]
        public int CodeTableId { get; set; }
        public string Code { get; set; }
        public string CodeDesc { get; set; }
    }
}
