using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using PersonManager.Models;
using PersonManager.Utils;

namespace PersonManager.Dal
{
    class SqlRepository : IRepository
    {   // cannot be const
        private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public void AddPerson(Person person)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(nameof(Person.FirstName), person.FirstName);
                    cmd.Parameters.AddWithValue(nameof(Person.LastName), person.LastName);
                    cmd.Parameters.AddWithValue(nameof(Person.Age), person.Age);
                    cmd.Parameters.AddWithValue(nameof(Person.Email), person.Email);
                    cmd.Parameters.Add(new SqlParameter(nameof(Person.Picture), SqlDbType.Binary, person.Picture.Length)
                    {
                        Value = person.Picture
                    });
                    SqlParameter idPerson = new SqlParameter(nameof(Person.IDPerson), SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idPerson);
                    cmd.ExecuteNonQuery();
                    person.IDPerson = (int)idPerson.Value;
                }
            }
        }

        public void DeletePerson(Person person)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(nameof(Person.IDPerson), person.IDPerson);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Person> GetPeople()
        {
            IList<Person> people = new List<Person>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            people.Add(ReadPerson(dr));
                        }
                    }
                }
            }
            return people;
        }

        public Person GetPerson(int idPerson)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(nameof(Person.IDPerson), idPerson);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return ReadPerson(dr);
                        }
                    }
                }
            }
            throw new Exception("Person does not exist");
        }

        private static Person ReadPerson(SqlDataReader dr) => new Person
        {
            IDPerson = (int)dr[nameof(Person.IDPerson)],
            FirstName = dr[nameof(Person.FirstName)].ToString(),
            LastName = dr[nameof(Person.LastName)].ToString(),
            Age = (int)dr[nameof(Person.Age)],
            Email = dr[nameof(Person.Email)].ToString(),
            Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, 5)
        };

        public void UpdatePerson(Person person)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(nameof(Person.FirstName), person.FirstName);
                    cmd.Parameters.AddWithValue(nameof(Person.LastName), person.LastName);
                    cmd.Parameters.AddWithValue(nameof(Person.Age), person.Age);
                    cmd.Parameters.AddWithValue(nameof(Person.Email), person.Email);
                    cmd.Parameters.AddWithValue(nameof(Person.IDPerson), person.IDPerson);
                    cmd.Parameters.Add(new SqlParameter(nameof(Person.Picture), SqlDbType.Binary, person.Picture.Length)
                    {
                        Value = person.Picture
                    });
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
