using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Database.Step3.Data
{
    // 부모 클래스의 타입을 제네릭 <T>로 해 놓으면 이를 
    // 상속받는 자식 클래스에서는 T자리에 자신의 타입을 넣는다. 
    public abstract class EntityData<T> where T:new()
    {
        private const string ConnectionString = "server=10.10.14.99;database=Chinook;uid=sa;pwd=3512";
        
        // 데이터를 가져오는 칼럼에 상관없이 
        // 데이터에 연결하는 코드가 같아 상위 클래스로 묶음 
        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;

            connection.Open();

            return connection;
        }

        // 데이터를 가져오는 칼럼에 상관없이 
        // 데이터 접근 시 사용하는 명령을 만드는 법이 같아 상위 클래스로 묶음  
        protected SqlCommand CreateCommand(string procedureName)
        {
            SqlConnection connection = OpenConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            return command;
        }

         // command에 파라미터를 집어넣는 구조가 같아 상위 클래스로 묶음
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
         // Entity 클래스를 구현하는 클래스들이 비슷하게 가질 메서드이지만 
         // 안에 들어가는 메서드의 내용은 클래스 마다 다르다. 
         // 타입을 변경할 수 있는 제네릭으로 메서드를 만든다. 
        protected abstract void FillEntity(SqlDataReader reader, T entity);
         // 가상함수는 자식에서 접근할 수 있도록 최소 protected여야 한다. 
        
        protected List<T> GetAllCore(string procName)
        {
            SqlCommand command = CreateCommand(procName);

            SqlDataReader reader = command.ExecuteReader();

            // 비어있는 리스트를 만든다.
            List<T> list = new List<T>();

            while (reader.Read())
            {
                // 새로운 entity를 만든다. 
                T entity = new T();
                // reader로 읽어온 값을 entity에 하나 채운다.
                FillEntity(reader, entity);
                // 하나의 entity를 list에 넣는다.
                list.Add(entity);
                // reader로 읽어올 값이 있을 때까지 반복한다. 
                // true일 
            }

            command.Connection.Close();

            return list;
        }
    }
}
