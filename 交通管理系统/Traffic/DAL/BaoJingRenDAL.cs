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
    public static class BaoJingRenDAL
    {
        public static void Insert(BaoJingRen person)
        {
            SqlHelper.ExecuteNonQuery("insert into T_报警人信息 (案件编号,姓名,性别,身份证号,报警电话,联系方式,家庭地址) values(@案件编号,@姓名,@性别,@身份证号,@报警电话,@联系方式,@家庭地址)",
                new SqlParameter("@案件编号", person.CaseId),
                new SqlParameter("@姓名", person.Name),
                new SqlParameter("@性别", person.Sex),
                new SqlParameter("@身份证号", person.PeopleId),
                new SqlParameter("@报警电话", person.BaoJingPhone),
                new SqlParameter("@联系方式", person.LianXiFangShi),
                new SqlParameter("@家庭住址", person.Address));
        }


        public static void Upadte(BaoJingRen person)
        {
            SqlHelper.ExecuteNonQuery("update T_报警人信息 set 姓名=@姓名,性别=@性别,身份证号=@身份证号,报警电话=@报警电话,联系方式=@联系方式,家庭地址=@家庭地址 where 案件编号=@案件编号",
                new SqlParameter("@姓名", person.Name),
                new SqlParameter("@性别", person.Sex),
                new SqlParameter("@身份证号", person.PeopleId),
                new SqlParameter("@报警电话", person.BaoJingPhone),
                new SqlParameter("@联系方式", person.LianXiFangShi),
                new SqlParameter("@家庭住址", person.Address),
                new SqlParameter("@案件编号", person.CaseId));
        }

        public static void DeleteById(string Id)
        {
            SqlHelper.ExecuteNonQuery("update T_报警人信息 set isDel=1 where 案件编号=@案件编号",
                new SqlParameter("@案件编号", Id));
        }

        public static BaoJingRen GetById(string id)
        {
            DataTable dt= SqlHelper.Adapter("select * from T_报警人信息 where 案件编号=@案件编号 and isdel=0",
                new SqlParameter("@案件编号",id));
            return ToBaoJingRen(dt.Rows[0]);
        }



        private static BaoJingRen ToBaoJingRen(DataRow row)
        {
            BaoJingRen person = new BaoJingRen();
            person.CaseId = (string)row["案件编号"];
            person.Name = (string)row["姓名"];
            person.Sex = (string)row["性别"];
            person.PeopleId = (string)row["身份证号"];
            person.BaoJingPhone = (string)row["报警电话"];
            person.LianXiFangShi = (string)row["联系方式"];
            person.Address = (string)row["家庭住址"];
            return person;

        }
    }
}
