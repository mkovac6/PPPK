using SQLManager.models;
using SQLManager.parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSQL;
using TSQL.Tokens;

namespace SQLManager.dal
{
    class SqlRepository : IRepository
    {
        private static string cs;
        private const string CON = "Server={0};Uid={1};Pwd={2}";
        private const string msg = "Msg {0}, Level {1}, State {2}, Line {3}\r\n\r\n{4}\r\n \r\n{5}";


        private const string SelectDatabases = "SELECT name As Name from sys.databases";

        public static string message;


        public IEnumerable<Database> GetDatabases()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = SelectDatabases;
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new Database
                            {
                                Name = dr[nameof(Database.Name)].ToString()
                            };
                        }

                    }
                }
            }
        }

        public void Connect(string servername, string username, string password)
        {
            using (SqlConnection con = new SqlConnection(string.Format(CON, servername, username, password)))
            {
                cs = con.ConnectionString;
                con.Open();
            }
        }

        public QueryData Query(string rawQueryString)
        {
            //Query example -> select * from AdventureWorksOBP.dbo.Grad
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += OnInfoMessageGenerated;
                con.FireInfoMessageEventOnUserErrors = true;

                using (SqlCommand command = new SqlCommand(rawQueryString, con))
                {

                    command.StatementCompleted += OnStatementCompleted;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        using (DataSet set = new DataSet())
                        {
                            adapter.Fill(set);
                            return new QueryData(set, message, TSQLParser.Parse(rawQueryString));
                        }
                    }
                }
            }

        }

        static void OnStatementCompleted(object sender, StatementCompletedEventArgs args)
        {
            message = string.Format("({0} row(s) affected)\r\n\r\n", args.RecordCount);
        }

        private static void OnInfoMessageGenerated(object sender, SqlInfoMessageEventArgs args)
        {
            foreach (SqlError err in args.Errors)
            {
                message = string.Format(msg, err.Number, err.Class, err.State, err.LineNumber, err.Message, DateTime.Now.ToString());
            }
        }

        public string GetMessage() => message;
    }
}
