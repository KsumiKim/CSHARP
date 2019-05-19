using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Database.Step3.Data
{
    public abstract class EntityData<T> where T:new()
    {
        private const string ConnectionString = "server=10.10.14.99;database=Chinook;uid=sa;pwd=3512";

        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;

            connection.Open();

            return connection;
        }

        protected SqlCommand CreateCommand(string procedureName)
        {
            SqlConnection connection = OpenConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            return command;
        }

        protected void AddParameter(SqlCommand command, string name, object value, SqlDbType dbType, int? size = null)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.SqlDbType = dbType;

            if (size.HasValue)
                parameter.Size = size.Value;

            command.Parameters.Add(parameter);
        }

        protected T GetByPKCore(string procName, 
            string paramName, int value)
        {
            SqlCommand command = CreateCommand(procName);

            AddParameter(command, paramName, 
                value, SqlDbType.Int);

            SqlDataReader reader = command.ExecuteReader();

            T entity = new T();

            while (reader.Read())
            {
                FillEntity(reader, entity);
            }

            command.Connection.Close();

            return entity;
        }

        // CTRL+R, CTRL+R
        protected abstract void FillEntity(SqlDataReader reader, T entity);

        protected List<T> GetAllCore(string procName)
        {
            SqlCommand command = CreateCommand(procName);

            SqlDataReader reader = command.ExecuteReader();

            List<T> list = new List<T>();

            while (reader.Read())
            {
                T entity = new T();
                FillEntity(reader, entity);

                list.Add(entity);
            }

            command.Connection.Close();

            return list;
        }
    }
}
