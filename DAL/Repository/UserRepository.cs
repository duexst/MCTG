using DAL.DB;
using MODELS;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserRepository : IRepository<User>
    {
        public Database _db { get; set; }
        public UserRepository(Database db)
        {
            _db = db;
        }
        public bool Create(User data)
        {
            string sql = "INSERT INTO users (guid, username, password, coins, elo) VALUES (@p1, @p2, @p3, @p4, @p5)";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", data.Guid);
            cmd.Parameters.AddWithValue("p2", data.Username);
            cmd.Parameters.AddWithValue("p3", data.Password);
            cmd.Parameters.AddWithValue("p4", data.Coins);
            cmd.Parameters.AddWithValue("p5", data.Elo);

            if (_db.ExecuteNonQuery(cmd))
            {
                return true;
            }

            return false;
        }

        public bool Delete(string id)
        {
            string sql = "DELETE FROM users WHERE guid = @p1";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", id);

            if (_db.ExecuteNonQuery(cmd))
            {
                return true;
            }

            return false;
        }

        public User Read(string id)
        {
            string sql = "SELECT username, password, coins, elo FROM users WHERE guid=@p1";
            var cmd = new NpgsqlCommand(sql);
            cmd.Parameters.AddWithValue("p1", id);

            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd)) {

                if (reader.Read())
                {
                    return new User(id, reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), Convert.ToInt32(reader.GetValue(2)), Convert.ToInt32(reader.GetValue(3)));
                }
                return null;
            }
        }

        public List<User> ReadAll()
        {
            List<User> res = new();
            string sql = "SELECT * FROM users";
            var cmd = new NpgsqlCommand(sql);
            
            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd))
            {
                while (reader.Read())
                {                   
                        res.Add(new User(reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), Convert.ToInt32(reader.GetValue(3)), Convert.ToInt32(reader.GetValue(4))));                  
                }
                return res;
            }
        }

        public bool Update(User data)
        {
            string sql = "UPDATE users SET guid=@p1, username=@p2, password=@p3, coins=@p4, elo=@p5 WHERE guid=@p6";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", data.Guid);
            cmd.Parameters.AddWithValue("p2", data.Username);
            cmd.Parameters.AddWithValue("p3", data.Password);
            cmd.Parameters.AddWithValue("p4", data.Coins);
            cmd.Parameters.AddWithValue("p5", data.Elo);
            cmd.Parameters.AddWithValue("p6", data.Guid);

            if (_db.ExecuteNonQuery(cmd))
            {
                return true;
            }

            return false;
        }

        public User FindUserByName(string username)
        {
            string sql = "SELECT * FROM users WHERE username=@p1";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", username);

            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd))
            {

                if (reader.Read())
                {
                    return new User(reader.GetValue(0).ToString(), username, reader.GetValue(2).ToString(), Convert.ToInt32(reader.GetValue(3).ToString()), Convert.ToInt32(reader.GetValue(4)));
                }
                return null;
            }
        }

        public UserScore GetUserScore(string id)
        {
            string sql = "SELECT username, elo FROM users WHERE guid=@p1";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", id);

            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd))
            {
                if (reader.Read())
                {
                    return new UserScore(reader.GetValue(0).ToString(), Convert.ToInt32(reader.GetValue(1)));
                }

                return null;
            }
        }
        public List<UserScore> GetScoreBoard()
        {
            List<UserScore> res = new();

            string sql = "SELECT username, elo FROM users ORDER BY elo DESC";
            NpgsqlCommand cmd = new(sql);
            
            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd))
            {
                while (reader.Read())
                {
                    res.Add(new UserScore(reader.GetValue(0).ToString(), Convert.ToInt32(reader.GetValue(1))));
                }

                return res;
            }
        }

        public int GetElo(string id)
        {
            string sql = "SELECT elo FROM users WHERE guid=@p1";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", id);

            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd))
            {
                if (reader.Read())
                {
                    return Convert.ToInt32(reader.GetValue(0));
                }

                return 0;
            }
        }

        public int GetCoins(string id)
        {
            string sql = "SELECT coins FROM users WHERE guid=@p1";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", id);

            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd))
            {
                if (reader.Read())
                {
                    return Convert.ToInt32(reader.GetValue(0));
                }

                return 0;
            }
        }

        public bool RestrictedUpdate(User user)
        {
            if(DuplicateUser(user.Guid) > 1)
            {
                return false;
            }

            string sql = "UPDATE users SET username=@p1, password=@p2 WHERE guid=@p3";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", user.Username);
            cmd.Parameters.AddWithValue("p2", user.Password);
            cmd.Parameters.AddWithValue("p3", user.Guid);

            if (_db.ExecuteNonQuery(cmd))
            {
                return true;
            }

            return false;
        }

        public int DuplicateUser(string username)
        {
            string sql = "SELECT username FROM users WHERE username=@p1";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", username);

            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd))
            {
                int i = 0;

                while (reader.Read())
                {
                    i++;
                }
                return i;
            }
        }
    }
}
