using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traffic.Model
{
    public class CaseInfo   //案件基本信息
    {
        public string CaseId { get; set; } //案件编号
        public string CaseLevel { get; set; } //案件性质
        public string CaseSource { get; set; }  //案件来源
        public string CaseDescribe { get; set; }  //案件简要案情描述
    }
}
