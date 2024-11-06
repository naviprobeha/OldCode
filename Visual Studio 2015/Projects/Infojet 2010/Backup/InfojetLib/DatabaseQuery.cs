using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for DatabaseQuery.
	/// </summary>
	public class DatabaseQuery
	{
		private Database database;
		private SqlCommand sqlCommand;

		public DatabaseQuery(Database database, SqlCommand sqlCommand)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.sqlCommand = sqlCommand;
		}

		public void addStringParameter(string name, string parameterValue, int length)
		{
			SqlParameter sqlParam = new SqlParameter(name, SqlDbType.NVarChar, length);
			sqlParam.Value = this.filterValue(parameterValue);
			sqlParam.Direction = ParameterDirection.Input;

			sqlCommand.Parameters.Add(sqlParam);
		}

        public void addDateTimeParameter(string name, DateTime parameterValue)
        {
            SqlParameter sqlParam = new SqlParameter(name, SqlDbType.DateTime);
            sqlParam.Value = parameterValue;
            sqlParam.Direction = ParameterDirection.Input;

            sqlCommand.Parameters.Add(sqlParam);
        }

		public void addIntParameter(string name, int parameterValue)
		{
			SqlParameter sqlParam = new SqlParameter(name, SqlDbType.Int);
			sqlParam.Value = parameterValue;
			sqlParam.Direction = ParameterDirection.Input;

			sqlCommand.Parameters.Add(sqlParam);
		}

		public void addSmallIntParameter(string name, int parameterValue)
		{
			SqlParameter sqlParam = new SqlParameter(name, SqlDbType.SmallInt);
			sqlParam.Value = parameterValue;
			sqlParam.Direction = ParameterDirection.Input;

			sqlCommand.Parameters.Add(sqlParam);
		}

		public void addDecimalParameter(string name, float parameterValue)
		{
			SqlParameter sqlParam = new SqlParameter(name, SqlDbType.Decimal);
            sqlParam.Precision = 10;
            sqlParam.Scale = 5;
			Decimal decParamVal = decimal.Parse(parameterValue.ToString());
			sqlParam.Value = decParamVal;
			
			sqlParam.Direction = ParameterDirection.Input;

			sqlCommand.Parameters.Add(sqlParam);
		}

		public void addBoolParameter(string name, bool parameterValue)
		{
			SqlParameter sqlParam = new SqlParameter(name, SqlDbType.TinyInt);

			if (parameterValue) 
				sqlParam.Value = 1;									
			else
				sqlParam.Value = 0;

			sqlParam.Direction = ParameterDirection.Input;

			sqlCommand.Parameters.Add(sqlParam);
		}

		public SqlDataReader executeQuery()
		{
			sqlCommand.Prepare();
			return sqlCommand.ExecuteReader();
		}

		public SqlDataAdapter executeDataAdapterQuery()
		{
			sqlCommand.Prepare();
			return new SqlDataAdapter(sqlCommand);
		}

		public void execute()
		{
			sqlCommand.Prepare();
			sqlCommand.ExecuteNonQuery();
		}

		private string filterValue(string parameterValue)
		{
            if (parameterValue != null)
{
			parameterValue = parameterValue.Replace("'", "");
			parameterValue = parameterValue.Replace("\"", "");
}
			return parameterValue;
		}

	}
}
