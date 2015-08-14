using SqlServerDataAdapter;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemParameters.UpdateNotification
{
    public class UpdateParameters
    {
        ISqlServerDataFactory _dataFactory;
        public UpdateParameters(string nxjcConnString)
        {
            _dataFactory = new SqlServerDataFactory(nxjcConnString);
        }
        public int Update(string organizationId, Name name, TypeModify typeModify)
        {
            int result = 0;

            string updateSql = @"update system_Flag_Data_Modified set flag_modified=@flagModified,
                               DateModified=@dateModified,TypeModify=@typeModified
                               where OrganizationID=@organizationId and Name=@name";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@organizationId", organizationId));
            parameters.Add(new SqlParameter("@name", GetNameByEnum(name)));
            parameters.Add(new SqlParameter("@typeModified", typeModify));
            parameters.Add(new SqlParameter("@dateModified", DateTime.Now));
            parameters.Add(new SqlParameter("@flagModified", 1));

            _dataFactory.ExecuteSQL(updateSql, parameters.ToArray());

            return result;
        }

        private string GetNameByEnum(Name name)
        {
            string result;
            switch (name)
            {
                case Name.DCS:
                    result = "DCS";
                    break;
                case Name.班次:
                    result = "班次";
                    break;
                case Name.报警周期:
                    result = "报警周期";
                    break;
                case Name.电表:
                    result = "电表";
                    break;
                case Name.峰谷平:
                    result = "峰谷平";
                    break;
                case Name.公共公式:
                    result = "公共公式";
                    break;
                case Name.公式:
                    result = "公式";
                    break;
                case Name.水泥品种:
                    result = "水泥品种";
                    break;
                default:
                    result = "";
                    break;
            }
            return result;
        }
    }
}
