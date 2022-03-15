using Serials.models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Security;

namespace Serials.data
{
    internal class DataProvider
    {
        public static DataProvider Default { get; set; }
        private readonly SqlConnection Connection;
        public DataProvider()
        {
            Connection = new SqlConnection();
        }
        public DataProvider(string username, SecureString password)
        {
            try
            {
                string s = $@"Server=TEDPC\SQLEXPRESS;Database=Serials;User Id={username}; Password={new System.Net.NetworkCredential(string.Empty, password).Password}";
                Connection = new SqlConnection(s);
                Connection.Open();
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<SerialView> GetSerialsView()
        {
            var list = new List<SerialView>();
            SqlCommand sqlCommand = new($"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES") { Connection = Connection };
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            string view = sqlDataReader["TABLE_NAME"].ToString();
            sqlDataReader.Close();
            sqlCommand = new($"select * from {view}") { Connection = Connection };
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                list.Add(new SerialView()
                {
                    Title = sqlDataReader["Title"].ToString(),
                    ReleaseDate = (DateTime)sqlDataReader["ReleaseDate"],
                    Genre = sqlDataReader["Genre"].ToString()
                });
            }
            sqlDataReader.Close();
            return list;
        }

        internal List<Visitor> GetVisitors()
        {
            var items = new List<Visitor>();
            SqlCommand cmd = new SqlCommand("select * from Visitor")
            {
                Connection = Connection
            };
            var sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                items.Add(new()
                {
                    VisitorID = (int)sqlDataReader["VisitorID"],
                    Name = sqlDataReader["Name"].ToString(),
                    Birthday = (DateTime)sqlDataReader["Birthday"]
                });
            }
            sqlDataReader.Close();
            return items;
        }

        internal List<Serial> GetSerials()
        {
            var items = new List<Serial>();
            SqlCommand cmd = new SqlCommand("select * from serial")
            {
                Connection = Connection
            };
            var sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                items.Add(new()
                {
                    SerialID = (int)sqlDataReader["SerialID"],
                    Title = sqlDataReader["Title"].ToString(),
                    GenreID = (int)sqlDataReader["GenreID"],
                    ReleaseDate = (DateTime)sqlDataReader["ReleaseDate"]
                });
            }
            sqlDataReader.Close();
            return items;
        }

        internal List<SerialActor> GetSerialActors()
        {
            var items = new List<SerialActor>();
            SqlCommand cmd = new SqlCommand("select * from SerialActor")
            {
                Connection = Connection
            };
            var sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                items.Add(new()
                {
                    SerialID = (int)sqlDataReader["SerialID"],
                    ActorID = (int)sqlDataReader["ActorID"]
                });
            }
            sqlDataReader.Close();
            return items;
        }

        internal int AddVisitor(DateTime birthday, string name)
        {
            SqlCommand sqlCommand = new($"insert Visitor(Name, Birthday) values('{name}','{birthday:dd/MM/yyyy}')") { Connection = Connection };
            sqlCommand.ExecuteNonQuery();
            sqlCommand = new($"SELECT IDENT_CURRENT('Visitor')") { Connection = Connection };
            return Convert.ToInt32(sqlCommand.ExecuteScalar());
        }

        internal List<Genre> GetGenres()
        {
            var items = new List<Genre>();
            SqlCommand cmd = new SqlCommand("select * from Genre")
            {
                Connection = Connection
            };
            var sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                items.Add(new()
                {
                    GenreID = (int)sqlDataReader["GenreID"],
                    Name = sqlDataReader["Name"].ToString()
                });
            }
            sqlDataReader.Close();
            return items;
        }

        internal List<Country> GetCountries()
        {
            var items = new List<Country>();
            SqlCommand cmd = new SqlCommand("select * from Country")
            {
                Connection = Connection
            };
            var sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                items.Add(new()
                {
                    CountryID = (int)sqlDataReader["CountryID"],
                    Name = sqlDataReader["Name"].ToString()
                });
            }
            sqlDataReader.Close();
            return items;
        }

        internal int AddSerial(string title, DateTime releaseDate, int genreID)
        {
            SqlCommand sqlCommand = new($"insert Serial(Title, ReleaseDate, GenreID) values('{title}','{releaseDate:dd/MM/yyyy}',{genreID})") { Connection = Connection };
            sqlCommand.ExecuteNonQuery();
            sqlCommand = new($"SELECT IDENT_CURRENT('Serial')") { Connection = Connection };
            return Convert.ToInt32(sqlCommand.ExecuteScalar());
        }

        internal int AddGenre(string name)
        {
            SqlCommand sqlCommand = new($"insert Genre(Name) values('{name}')") { Connection = Connection };
            sqlCommand.ExecuteNonQuery();
            sqlCommand = new($"SELECT IDENT_CURRENT('Genre')") { Connection = Connection };
            return Convert.ToInt32(sqlCommand.ExecuteScalar());
        }

        internal List<Comment> GetComments()
        {
            var items = new List<Comment>();
            SqlCommand cmd = new SqlCommand("select * from Comment")
            {
                Connection = Connection
            };
            var sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                items.Add(new()
                {
                    CommentID = (int)sqlDataReader["CommentID"],
                    SerialID = (int)sqlDataReader["SerialID"],
                    VisitorID = (int)sqlDataReader["VisitorID"],
                    CommentDate = (DateTime)sqlDataReader["CommentDate"],
                    CommentText = sqlDataReader["CommentText"].ToString()

                });
            }
            sqlDataReader.Close();
            return items;
        }

        internal void AddSerialActor(int serialID, int actorID)
        {
            SqlCommand sqlCommand = new($"insert SerialActor(SerialID, ActorID) values({serialID}, {actorID})") { Connection = Connection };
            sqlCommand.ExecuteNonQuery();
        }

        internal void UpdateSerialActor<T>(T value, int serialid, int actorid, [CallerMemberName] string propertyName = null)
        {
            SqlCommand sqlCommand = new($"update SerialActor set {propertyName} = {value} where SerialID={serialid} and ActorID={actorid}")
            {
                Connection = Connection
            };
            sqlCommand.ExecuteNonQuery();
        }

        internal int AddCountry(string name)
        {
            SqlCommand sqlCommand = new($"insert Country(Name) values('{name}')") { Connection = Connection };
            sqlCommand.ExecuteNonQuery();
            sqlCommand = new($"SELECT IDENT_CURRENT('Country')") { Connection = Connection };
            return Convert.ToInt32(sqlCommand.ExecuteScalar());
        }

        internal string GetRole()
        {
            SqlCommand sqlCommand = new($"exec GetRoles") { Connection = Connection };
            SqlDataReader reader = sqlCommand.ExecuteReader();
            _ = reader.Read();
            string role = reader.HasRows ? reader["Role"].ToString() : throw new Exception();
            if (role == null)
            {
                role = string.Empty;
            }
            reader.Close();
            return role;
        }

        internal void DeleteSerialActor(int serialID, int actorID)
        {
            SqlCommand sqlCommand = new($"delete SerialActor where SerialID={serialID} and ActorID={actorID}")
            {
                Connection = Connection
            };
            sqlCommand.ExecuteNonQuery();
        }

        internal int AddComment(int serialID, int visitorID, DateTime commentDate, string commentText)
        {
            SqlCommand sqlCommand = new($"insert Comment(CommentText, CommentDate, SerialID, VisitorID) values('{commentText}','{commentDate:dd/MM/yyyy}',{serialID},{visitorID})") { Connection = Connection };
            sqlCommand.ExecuteNonQuery();
            sqlCommand = new($"SELECT IDENT_CURRENT('Comment')") { Connection = Connection };
            return Convert.ToInt32(sqlCommand.ExecuteScalar());
        }

        internal int AddActor(string name, int countryID)
        {
            SqlCommand sqlCommand = new($"insert Actor(Name,CountryID) values('{name}', {countryID})") { Connection = Connection };
            sqlCommand.ExecuteNonQuery();
            sqlCommand = new($"SELECT IDENT_CURRENT('Actor')") { Connection = Connection };
            return Convert.ToInt32(sqlCommand.ExecuteScalar());
        }

        internal void AddUser(string login, SecureString password, int genreID)
        {
            SqlCommand sqlCommand = new($"exec CreateUser '{login}', '{new System.Net.NetworkCredential(string.Empty, password).Password}', {genreID}")
            {
                Connection = Connection
            };
            sqlCommand.ExecuteNonQuery();
        }

        public void ConnectionClose()
        {
            Connection.Close();
        }

        public object GetActors()
        {
            var items = new List<Actor>();
            SqlCommand cmd = new SqlCommand("select * from actor")
            {
                Connection = Connection
            };
            var sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                items.Add(new()
                {
                    ActorID = (int)sqlDataReader["ActorID"],
                    Name = sqlDataReader["Name"].ToString(),
                    CountryID = (int)sqlDataReader["ActorID"]
                });
            }
            sqlDataReader.Close();
            return items;
        }

        internal void DeleteItem(string table, int id)
        {
            SqlCommand sqlCommand = new($"delete {table} where {table}ID={id}")
            {
                Connection = Connection
            };
            sqlCommand.ExecuteNonQuery();
        }

        internal void Update<T>(T value, string table, int id, [CallerMemberName] string propertyName = null)
        {
            string valuestring = string.Empty;
            if (value is string)
            {
                valuestring = $"'{ value }'";
            }
            else if (value is DateTime)
            {
                valuestring = $"'{value:dd/MM/yyyy}'";
            }
            else
            {
                valuestring = $"{value}";
            }
            SqlCommand sqlCommand = new($"update {table} set {propertyName} = {valuestring} where {table}ID={id}")
            {
                Connection = Connection
            };
            sqlCommand.ExecuteNonQuery();
        }
    }
}
