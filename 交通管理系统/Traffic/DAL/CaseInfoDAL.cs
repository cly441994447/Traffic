using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLYLibrary;
using Traffic.Model;
using System.Data.SqlClient;
using System.Data;

namespace Traffic.DAL
{
    public static class CaseInfoDAL
    {
        public static  void Insert(CaseInfo caseInfo)
        {
            SqlHelper.ExecuteNonQuery("insert into T_案件基本信息(案件编号,案件性质,案件来源,案件简要案情描述) values(@案件编号,@案件性质,@案件来源,@案件简要案情描述)",
                new SqlParameter("@案件编号", caseInfo.CaseId),
                new SqlParameter("@案件性质", caseInfo.CaseLevel),
                new SqlParameter("案件来源", caseInfo.CaseSource),
                new SqlParameter("@案件简要案情描述", caseInfo.CaseDescribe));
        }

        public static void Update(CaseInfo caseInfo)
        {
            SqlHelper.ExecuteNonQuery("update T_案件基本信息 set 案件性质=@案件性质,案件来源=@案件来源,案件简要案情描述=@案件简要案情描述 where 案件编号=@案件编号 and isDel=0",
                new SqlParameter("@案件性质", caseInfo.CaseLevel),
                new SqlParameter("案件来源", caseInfo.CaseSource),
                new SqlParameter("@案件简要案情描述", caseInfo.CaseDescribe),
                new SqlParameter("@案件编号", caseInfo.CaseId));
        }

        public static void DeleteById(string id)
        {
            SqlHelper.ExecuteNonQuery("update T_案件基本信息 set isDel=1 where 案件编号=@案件编号",
                new SqlParameter("@案件编号", id));
        }

        public static CaseInfo GetById(string id)
        {
            DataTable dt= SqlHelper.Adapter("select * from T_案件基本信息 where 案件编号=@案件编号 and isDel=0",
            new SqlParameter ("@案件编号",id));
            return ToCaseInfo(dt.Rows[0]);
        }

        public static CaseInfo[] GetAll()
        {
            DataTable dt= SqlHelper.Adapter("select * from T_案件基本信息 where isDel=0");
            CaseInfo[] cases = new CaseInfo[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cases[i] = ToCaseInfo(dt.Rows[i]);
            }
            return cases;
        }



        private static CaseInfo ToCaseInfo(DataRow row)
        {
            CaseInfo caseInfo = new CaseInfo();
            caseInfo.CaseId = (string)row["案件编号"];
            caseInfo.CaseLevel = (string)row["案件性质"];
            caseInfo.CaseSource = (string)row["案件来源"];
            caseInfo.CaseDescribe = (string)row["案件简要案情描述"];
            return caseInfo;
        }
    }
}
