using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Traffic.Model;
using CLYLibrary;
using System.Data.SqlClient;
using System.Data;

namespace Traffic.DAL
{
    public static class JieJingRenDAL
    {
        public static void Insert(JieJingRen person)
        {
            SqlHelper.ExecuteNonQuery("insert into T_接警人信息 (案件编号,姓名,警号) values(@案件编号,@姓名,@警号)",
                new SqlParameter("案件编号", person.CaseId),
                new SqlParameter("姓名", person.Name),
                new SqlParameter("警号", person.JingHao));
        }
        public static void Update(JieJingRen person)
        {
            SqlHelper.ExecuteNonQuery("update T_接警人信息 set 姓名=@姓名,警号=@警号 where 案件编号=@案件编号 and isdel=0",
                new SqlParameter("@姓名", person.Name),
                new SqlParameter("@警号", person.JingHao),
                new SqlParameter("@案件编号", person.CaseId));
        }

        public static void DeleteById(string id)
        {
            SqlHelper.ExecuteNonQuery("update T_接警人信息 set isDel=1 where 案件编号=@案件编号",
                new SqlParameter("@案件编号", id));
        }

        public static JieJingRen GetById(string id)
        {
            DataTable dt = SqlHelper.Adapter("select * from T_接警人信息 where 案件编号=@案件编号 and isDel=0",
                new SqlParameter("@案件编号", id));
            return ToJieJingRen(dt.Rows[0]);
        }


        private static JieJingRen ToJieJingRen(DataRow row)
        {
            JieJingRen person = new JieJingRen();
            person.CaseId = (string)row["案件编号"];
            person.Name = (string)row["姓名"];
            person.JingHao = (string)row["警号"];
            return person;
        }
    }
}
