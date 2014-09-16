using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Configuration;


namespace CSCore.DataHelper
{
    /// <summary>
    /// Summary description for BaseSqlHelper.
    /// </summary>
    public static class BaseSqlHelper
    {

        #region private utility methods & constructors

        //Since this class provides only static methods, make the default constructor private to prevent 
        //instances from being created with "new BaseSqlHelper()".
        static BaseSqlHelper()
        {
            
        }

        /// <summary>
        /// This method is used to attach array of SqlParameters to a SqlCommand.
        /// 
        /// This method will assign a value of DbNull to any parameter with a direction of
        /// InputOutput and a value of null.  
        /// 
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">An array of SqlParameters to be added to command</param>
        public static void AttachParams(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        /// <summary>
        /// This method assigns an array of values to an array of SqlParameters.
        /// </summary>
        /// <param name="procParams">The proc params.</param>
        /// <param name="parameterValues">array of objects holding the values to be assigned</param>
        private static void AssignParams(SqlParameter[] procParams, object[] parameterValues)
        {
            if ((procParams == null) || (parameterValues == null))
                return;

            if (procParams.Length < parameterValues.Length)
                throw new ArgumentException("Parameter count does not match Parameter Value count.");

            //iterate through the SqlParameters, assigning the values from the corresponding position in the 
            //value array
            for (int i = 0; i < parameterValues.Length; i++)
                procParams[i].Value = parameterValues[i];
        }

        private static int GetCommandTimeout()
        {
            // read the command timeout setting from the app config
            string commandTimeoutSetting = ConfigurationManager.AppSettings["SqlCommandTimeout"];
            int defaultTimeout = 30;

            // if the setting isn't there, CommandTimeout defaults to 30 seconds.
            if (commandTimeoutSetting != null)
            {
                return Int32.Parse(commandTimeoutSetting);
            }
            else
            {
                return defaultTimeout;
            }
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters
        /// to the provided command.
        /// </summary>
        /// <param name="command">the SqlCommand to be prepared</param>
        /// <param name="connection">a valid SqlConnection, on which to execute this command</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="procParams">The proc params.</param>
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, string commandText, SqlParameter[] procParams)
        {
            PrepareCommand(command, connection, commandText, procParams, GetCommandTimeout());
        }


        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters
        /// to the provided command.
        /// </summary>
        /// <param name="command">the SqlCommand to be prepared</param>
        /// <param name="connection">a valid SqlConnection, on which to execute this command</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="procParams">The proc params.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, string commandText, SqlParameter[] procParams, int commandTimeout)
        {
            //if the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            command.Connection = connection;

            // set the timeout, only if its larger than the default timeout.
            if (commandTimeout > 30)
            {
                command.CommandTimeout = commandTimeout;
            }

            command.CommandText = commandText;
            command.CommandType = CommandType.StoredProcedure;
            //attach the command parameters if they are provided
            if (procParams != null)
            {
                AttachParams(command, procParams);
            }
        }

        #endregion private utility methods & constructors

        //public static Action<string, Exception, SqlCommand> OnLogException;
        #region Log Methods
        private static void LogException(string connectionString, Exception ex, SqlCommand sqlCommand)
        {			
            throw new ApplicationException("Database error", ex);
        }

        private static void LogError(string clientName, Exception ex, SqlCommand cmd)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("clientName: ");
            sb.Append(clientName);
            sb.Append("CommandText: ");
            sb.Append(cmd.CommandText);
            sb.Append("\n");
            sb.Append("ParamString: ");

            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                SqlParameter sqlParam = cmd.Parameters[i];

                if (i > 0)
                    sb.Append(", ");

                sb.Append(sqlParam.ParameterName).Append(" = ");
                if ((sqlParam.Value == DBNull.Value) || (sqlParam.Value == null))
                    sb.Append("NULL");
                else
                    switch (sqlParam.SqlDbType)
                    {
                        case SqlDbType.Char:
                        case SqlDbType.VarChar:
                        case SqlDbType.UniqueIdentifier:
                        case SqlDbType.DateTime:
                        case SqlDbType.SmallDateTime:
                        case SqlDbType.Text:
                            sb.AppendFormat("'{0}'", sqlParam.Value.ToString());
                            break;
                        default:
                            sb.Append(sqlParam.Value.ToString());
                            break;
                    }
            }

            sb.Append("\n");
            sb.Append("Message: ");
            sb.Append(ex.Message);
            sb.Append("\n");
            sb.Append("StackTrace: ");
            sb.Append(ex.StackTrace);
            sb.Append("\n");

            //ApplicationLog.LogToEventViewer(sb.ToString(), TraceEventType.Critical, CategoryType.Database);
        }
        #endregion Log

        #region BaseSqlHelper Execution methods

        public static void BeginExecuteNonQuery(string connectionString, string commandText, SqlParameter[] procParams, AsyncCallback callback, object stateObject)
        {
            ExecuteNonQuery(connectionString, commandText, procParams, true, callback, stateObject);
        }

        public static void ExecuteNonQuery(string connectionString, string commandText, SqlParameter[] procParams)
        {
            ExecuteNonQuery(connectionString, commandText, procParams, false, null, null);
        }

        private static void ExecuteNonQuery(string connectionString, string commandText, SqlParameter[] procParams, bool async, AsyncCallback callback, object stateObject)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    //Automatically assign culture value to procedure parameter 
                   
                    // create and prepare command
                    BaseSqlHelper.PrepareCommand(cmd, cn, commandText, procParams);
                   
                    // execute proc and return 
                    if (async)
                    {
                        if (callback != null)
                        {
                            cmd.BeginExecuteNonQuery(callback, stateObject);
                        }
                        else
                        {
                            cmd.BeginExecuteNonQuery();
                        }
                    }
                    else
                    {
                        int retVal = cmd.ExecuteNonQuery();
                    }
                    cmd.Parameters.Clear();
                  
                    return;
                }
            }
            catch (Exception ex) 
            {
                LogException(connectionString, ex, cmd);
            }
        }

        public static void ExecuteNonQuery(string connectionString, string commandText, params object[] paramValues)
        {
            // get parameters and assign values
            SqlParameter[] procParams = BaseSqlHelper.GetParams(connectionString, commandText);
            BaseSqlHelper.AssignParams(procParams, paramValues);
            // propagate execution to overloaded method w/ parameter array
            BaseSqlHelper.ExecuteNonQuery(connectionString, commandText, procParams);

            return;
        }

        public static SqlParameterCollection ExecuteReturnParam(string connectionString, string commandText, params object[] paramValues)
        {
            // get parameters and assign values
            SqlParameter[] procParams = BaseSqlHelper.GetParams(connectionString, commandText);
            BaseSqlHelper.AssignParams(procParams, paramValues);
            // propagate execution to overloaded method w/ parameter array
            return BaseSqlHelper.ExecuteReturnParam(connectionString, commandText, procParams);
        }

        public static SqlParameterCollection ExecuteReturnParam(string connectionString, string commandText, SqlParameter[] procParams)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    // create and prepare command
                    BaseSqlHelper.PrepareCommand(cmd, cn, commandText, procParams);
                    // execute proc and return parameter collection
                    cmd.ExecuteNonQuery();
                    //CesTrace.Trace(cmd, sw);
                }
            }
            catch (Exception ex)
            {
                LogException(connectionString, ex, cmd);
            }
            return cmd.Parameters;
        }

        public static int ExecuteReturnValue(string connectionString, string commandText, ref SqlParameter[] procParams)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    
                    // create and prepare command
                    BaseSqlHelper.PrepareCommand(cmd, cn, commandText, procParams);
                    // execute proc and return parameter collection
                    SqlParameter returnValue = new SqlParameter("RETURN_VALUE", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);

                    cmd.ExecuteNonQuery();
                    
                }
            }
            catch (Exception ex)
            {
                LogException(connectionString, ex, cmd);
            }
            return Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
        }

        public static object ExecuteScalar(string connectionString, string commandText, SqlParameter[] procParams)
        {
            SqlCommand cmd = new SqlCommand();
            object retVal = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    
                    // create and prepare command
                    BaseSqlHelper.PrepareCommand(cmd, cn, commandText, procParams);
                    // execute proc and return object

                    retVal = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return retVal;
                }
            }
            catch (Exception ex)
            {
                LogException(connectionString, ex, cmd);
            }
            return null;
        }

        public static object ExecuteScalar(string connectionString, string commandText, params object[] paramValues)
        {
            // get parameters and assign values
            SqlParameter[] procParams = BaseSqlHelper.GetParams(connectionString, commandText);
            BaseSqlHelper.AssignParams(procParams, paramValues);
            // propagate execution to overloaded method w/ parameter array
            return BaseSqlHelper.ExecuteScalar(connectionString, commandText, procParams);
        }

        public static SqlDataReader ExecuteReader(string connectionString, string commandText, SqlParameter[] procParams)
        {
            return ExecuteReader(connectionString, commandText, procParams, GetCommandTimeout());
        }

        public static SqlDataReader ExecuteReader(string connectionString, string commandText, SqlParameter[] procParams, int commandTimeout)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader retVal = null;
            try
            {
                // create connection and command and prepare command
                SqlConnection cn = new SqlConnection(connectionString);
                //Automatically assign culture value to procedure parameter 
                //SetCultureParam(procParams);
                BaseSqlHelper.PrepareCommand(cmd, cn, commandText, procParams, commandTimeout);
                // execute procedure and return data reader
                retVal = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                LogException(connectionString, ex, cmd);
            }
            return retVal;
        }

        public static SqlDataReader ExecuteReader(string connectionString, string commandText, params object[] paramValues)
        {
            // get parameters and assign values
            SqlParameter[] procParams = BaseSqlHelper.GetParams(connectionString, commandText);
            BaseSqlHelper.AssignParams(procParams, paramValues);
           return BaseSqlHelper.ExecuteReader(connectionString, commandText, procParams);
        }

        public static SqlDataReader ExecuteReader(string connectionString, string commandText)
        {
            return BaseSqlHelper.ExecuteReader(connectionString, commandText, (SqlParameter[])null);
        }

        public static string ExecuteForXml(string connectionString, string commandText)
        {
            return BaseSqlHelper.ExecuteForXml(connectionString, commandText, (SqlParameter[])null);
        }

        public static string ExecuteForXml(string connectionString, string commandText, SqlParameter[] procParams)
        {
            return ExecuteForXml(connectionString, commandText, procParams, GetCommandTimeout());
        }

        public static string ExecuteForXml(string connectionString, string commandText, SqlParameter[] procParams, int commandTimeout)
        {
            SqlCommand cmd = new SqlCommand();
            StringBuilder xml = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                  
                    // create and prepare command, execute to datareader
                    BaseSqlHelper.PrepareCommand(cmd, cn, commandText, procParams, commandTimeout);
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        
                        // build and return xml string
                        xml = new StringBuilder();
                        while (dr.Read())
                            xml.Append(dr.GetString(0));

                        cmd.Parameters.Clear();

                    }
                }
            }
            catch (Exception ex)
            {
                LogException(connectionString, ex, cmd);
            }
            return xml.ToString();
        }

        public static string ExecuteForXml(string connectionString, string commandText, params object[] paramValues)
        {
            // get parameters and assign values
            SqlParameter[] procParams = BaseSqlHelper.GetParams(connectionString, commandText);
            BaseSqlHelper.AssignParams(procParams, paramValues);
            // defer execution to overloaded method w/ parameter array
            return BaseSqlHelper.ExecuteForXml(connectionString, commandText, procParams);
        }

        public static void Execute(string connectionString, string commandText, SqlParameter[] procParams,
            DataTableMapping[] tableMappings, DataSet DS)
        {
            Execute(connectionString, commandText,
                procParams, tableMappings, DS, MissingSchemaAction.Add);
        }

        public static void Execute(string connectionString, string commandText, SqlParameter[] procParams,
            DataTableMapping[] tableMappings, DataSet DS, MissingSchemaAction schemaAction)
        {
            Execute(connectionString, commandText,
                procParams, tableMappings, DS, schemaAction, GetCommandTimeout());
        }

        public static void Execute(string connectionString, string commandText, SqlParameter[] procParams,
            DataTableMapping[] tableMappings, DataSet DS, MissingSchemaAction schemaAction, int commandTimeout)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                   
                    // create and prepare command
                    BaseSqlHelper.PrepareCommand(cmd, cn, commandText, procParams, commandTimeout);
                    // create data adapter, add table mappings and set selectcommand property
                    SqlDataAdapter sqlDa = new SqlDataAdapter();
                    foreach (DataTableMapping dMap in tableMappings)
                        sqlDa.TableMappings.Add(dMap);
                    sqlDa.SelectCommand = cmd;
                    sqlDa.MissingSchemaAction = schemaAction;
                    // fill data set and return
                    sqlDa.Fill(DS);
                  
                }
            }
            catch (Exception ex)
            {
                LogException(connectionString, ex, cmd);
            }
        }

        /// <summary>
        /// Executes the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="procParams">The proc params.</param>
        /// <param name="table">The table.</param>
        /// <param name="strictSchema">
        /// If true, the table's schema should be held strictly and not be
        /// appended even if the resulting set contains extra data.  If false, any extra columns
        /// returned will be appended to the structure of the table.
        /// </param>
        public static void Execute(string connectionString, string commandText, SqlParameter[] procParams,
           DataTable table, bool strictSchema)
        {
            Execute(connectionString, commandText, procParams, table, strictSchema, GetCommandTimeout());
        }

        /// <summary>
        /// Execute a stored procedure and store the results
        /// </summary>
        /// <param name="connectionString">String containing parameters needed to connect to the database</param>
        /// <param name="commandText">Name of the stored procedure or query</param>
        /// <param name="procParams">array of parameters to pass in to the stored procedure</param>
        /// <param name="table">DataTable to be filled with results of the query</param>
        /// <param name="strictSchema">If true, the table's schema should be held strictly and not be
        /// appended even if the resulting set contains extra data.  If false, any extra columns
        /// returned will be appended to the structure of the table.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        public static void Execute(string connectionString, string commandText, SqlParameter[] procParams,
            DataTable table, bool strictSchema, int commandTimeout)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    
                    // create and prepare command
                    BaseSqlHelper.PrepareCommand(cmd, cn, commandText, procParams, commandTimeout);
                    // create data adapter, add table mappings and set selectcommand property
                    SqlDataAdapter sqlDa = new SqlDataAdapter();
                    sqlDa.SelectCommand = cmd;
                    sqlDa.MissingSchemaAction = (strictSchema ? MissingSchemaAction.Ignore : MissingSchemaAction.Add);
                    // fill data set and return
                    sqlDa.Fill(table);
                    return;
                }
            }
            catch (Exception ex)
            {
                LogException(connectionString, ex, cmd);
            }
        }

        public static XmlReader ExecuteXmlReader(SqlConnection cn, string commandText, SqlParameter[] procParams)
        {
            SqlCommand cmd = new SqlCommand();
            XmlReader retVal = null;
            try
            {
               
                // create and prepare command
                cmd = new SqlCommand(commandText, cn);
                BaseSqlHelper.PrepareCommand(cmd, cn, commandText, procParams);
                // execute procedure and return XmlReader
                retVal = cmd.ExecuteXmlReader();
                cmd.Parameters.Clear();
                //CesTrace.Trace(cmd, sw);
            }
            catch (Exception ex)
            {
                string connectionString = cn.ConnectionString;
                LogException(connectionString, ex, cmd);
            }
            return retVal;
        }

        public static DataSet GetDataSet(SqlDataReader sqlDataReader)
        {
            DataSet result = new DataSet();
            DataTable table;
            DataRow row;

            do
            {
                table = new DataTable();

                for (int i = 0; i < sqlDataReader.FieldCount; i++)
                    table.Columns.Add(new DataColumn(sqlDataReader.GetName(i)));

                while (sqlDataReader.Read())
                {
                    row = table.NewRow();

                    for (int i = 0; i < table.Columns.Count; i++)
                        row[table.Columns[i].ColumnName] = sqlDataReader[table.Columns[i].ColumnName];

                    table.Rows.Add(row);
                }

                result.Tables.Add(table);

            } while (sqlDataReader.NextResult());
            
            return result;
        }

        #endregion BaseSqlHelper Execution methods

        #region SqlParameter helper methods


        private static SqlParameter[] DiscoverParams(string connection, string proc)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(proc, cn))
            {
                try
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlCommandBuilder.DeriveParameters(cmd);

                    cmd.Parameters.RemoveAt(0);

                    SqlParameter[] discoveredParams = new SqlParameter[cmd.Parameters.Count];

                    cmd.Parameters.CopyTo(discoveredParams, 0);

                    // BaseSqlHelper CS Customization:
                    FixTableValuedParameters(discoveredParams);

                    return discoveredParams;
                }
                catch (Exception ex)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SqlCommandBuilder.DeriveParameters() failed\n");
                    sb.Append("Connection: ");
                    sb.Append(connection);
                    sb.Append("\n");
                    sb.Append("Proc: ");
                    sb.Append(proc);
                    sb.Append("\n");
                    sb.Append("CurrentDomain.FriendlyName: ");
                    sb.Append(System.AppDomain.CurrentDomain.FriendlyName);
                    sb.Append("\n");
                    sb.Append("Message: ");
                    sb.Append(ex.Message);
                    sb.Append("\n");
                    sb.Append("StackTrace: ");
                    sb.Append(ex.StackTrace);
                    sb.Append("\n");
                    System.Diagnostics.EventLog.WriteEntry("DiscoverParams", sb.ToString(), System.Diagnostics.EventLogEntryType.Error);
                    // ApplicationLog.LogToEventViewer(sb.ToString(), TraceEventType.Critical, CategoryType.Database);
                    throw;
                }
            }
        }

        /// <summary>
        /// BaseSqlHelper CS Customization:
        /// Looks for table valued parameters and strip database name from TypeName
        /// Otherwise error is raised that database name is not allowed.
        /// </summary>
        private static void FixTableValuedParameters(SqlParameter[] parameters)
        {
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter.SqlDbType == SqlDbType.Structured)
                {
                    string[] parts = parameter.TypeName.Split('.');
                    if (parts.Length == 3)
                        parameter.TypeName = parts[1] + "." + parts[2];
                }
            }
        }

        //deep copy of cached SqlParameter array
        private static SqlParameter[] CloneParams(SqlParameter[] originalParams)
        {
            SqlParameter[] clonedParams = new SqlParameter[originalParams.Length];

            for (int i = 0, j = originalParams.Length; i < j; i++)
                clonedParams[i] = (SqlParameter)((ICloneable)originalParams[i]).Clone();

            return clonedParams;
        }




        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="proc">the TSQL stored procedure name</param>
        /// <returns>an array of SqlParameters</returns>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        public static SqlParameter[] GetParams(string connection, string proc)
        {
            SqlParameter[] cachedParams = null;

            // this could theoretically fail if in between the containskey and the retrieval call, the value is invalidated since it's not inside the lock
            // but this will make it faster, and invalidate should not be called that often.
            cachedParams = DiscoverParams(connection, proc);
            return CloneParams(cachedParams);
        }

        /// <summary>
        /// 
        /// </summary>
        public static string GetConnection()
        {
            return ConfigHelper.GetDBConnection();
        }
        

        /// <summary>
        /// Creates DataTableMapping array out of string of tableNames
        /// </summary>
        /// <param name="tableNames">The table names.</param>
        /// <returns></returns>
        public static DataTableMapping[] CreateDataTableMapping(string[] tableNames)
        {
            DataTableMapping[] dtm = new DataTableMapping[tableNames.Length];

            // Add the table mappings specified by the user
            if (tableNames != null && tableNames.Length > 0)
            {
                const string tableName = "Table";
                for (int index = 0; index < tableNames.Length; index++)
                {
                    if (tableNames[index] == null || tableNames[index].Length == 0) throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                    dtm[index] = new DataTableMapping(tableName + ((index == 0) ? "" : index.ToString()), tableNames[index]);
                }
            }

            return dtm;
        }

        public static SqlParameter GetSqlParameter(string parameterName, SqlParameter[] arrayOfSqlParameters)
        {
            foreach (SqlParameter param in arrayOfSqlParameters)
            {
                if (param.ParameterName == parameterName)
                    return param;
            }

            return null;
        }

        /// <summary>
        /// Giving an array of SqlParameters, set the value for a specific name
        /// </summary>
        /// <param name="parameterName">Parameter Name</param>
        /// <param name="parameterValue">Parameter Value</param>
        /// <param name="arrayOfSqlParameters">SqlParameter[] array</param>
        public static void SetSqlParameter(string parameterName, object parameterValue, SqlParameter[] arrayOfSqlParameters)
        {
            foreach (SqlParameter param in arrayOfSqlParameters)
            {
                if (param.ParameterName.ToLower() == parameterName.ToLower())
                {
                    param.Value = parameterValue;
                    return;
                }
            }
        }

        /// <summary>
        /// Giving an array of SqlParameters, set the value for a specific name
        /// </summary>
        /// <param name="parameterName">Parameter Name</param>
        /// <param name="parameterValue">Parameter Value</param>
        /// <param name="direction">Parameter Direction</param>
        /// <param name="arrayOfSqlParameters">SqlParameter[] array</param>
        public static void SetSqlParameter(string parameterName, object parameterValue, ParameterDirection direction, SqlParameter[] arrayOfSqlParameters)
        {
            foreach (SqlParameter param in arrayOfSqlParameters)
            {
                if (param.ParameterName == parameterName)
                {
                    param.Value = parameterValue;
                    param.Direction = direction;
                    return;
                }
            }

            throw new Exception(String.Format(@"ParameterName {0} does not exist.", parameterName));
        }

        #endregion SqlParameter helper methods

        public static Nullable<T> ParseNullable<T>(object rowItem) where T : struct
        {
            if (rowItem is DBNull)
                return new Nullable<T>();
            else
                return (T)rowItem;
        }
    } // BaseSqlHelper
} // CS.Data
