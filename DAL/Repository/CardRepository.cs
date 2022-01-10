using DAL.DB;
using MODELS;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CardRepository : IRepository<Card>
    {
        public Database _db { get; set; }
        public CardRepository(Database db)
        {
            _db = db;
        }
        public bool Create(Card data)
        {
            string sql = "INSERT INTO cards (uid, owner, name, type, damage, element, Active) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7)";
            var cmd = new NpgsqlCommand(sql);
            cmd.Parameters.AddWithValue("p1", data.Uid);
            cmd.Parameters.AddWithValue("p2", data.Owner);
            cmd.Parameters.AddWithValue("p3", data.Name);
            cmd.Parameters.AddWithValue("p4", data.Type);
            cmd.Parameters.AddWithValue("p5", data.Damage);
            cmd.Parameters.AddWithValue("p6", data.Element);
            cmd.Parameters.AddWithValue("p7", data.Active);

            if (_db.ExecuteNonQuery(cmd))
            {
                return true;
            }

            return false;
        }

        public bool Delete(string uid)
        {
            string sql = "DELETE FROM cards WHERE id = @p1";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", uid.ToString());

            if (_db.ExecuteNonQuery(cmd))
            {
                return true;
            }

            return false;
        }

        public Card Read(string uid)
        {
            string sql = "SELECT owner, name, type, damage, element, cost, active FROM cards WHERE uid=@p1";
            var cmd = new NpgsqlCommand(sql);
            cmd.Parameters.AddWithValue("p1", uid.ToString());

            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd))
            {

                if (reader.Read())
                {
                    return new Card(uid,                                        //uid
                                    reader.GetValue(0).ToString(),              //owner
                                    reader.GetValue(1).ToString(),              //name
                                    reader.GetValue(2).ToString(),              //type
                                    Convert.ToInt32(reader.GetValue(3)),        //damage
                                    reader.GetValue(4).ToString(),              //element
                                    Convert.ToBoolean(reader.GetValue(5))       //active
                                    );
                }
                return null;
            }
        }

        public List<Card> ReadAll()
        {
            List<Card> res = new();
            string sql = "SELECT * FROM cards";
            var cmd = new NpgsqlCommand(sql);

            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd))
            {
                while (reader.Read())
                {
                    res.Add(new Card(reader.GetValue(0).ToString(),         //uid
                                    reader.GetValue(1).ToString(),          //owner
                                    reader.GetValue(2).ToString(),          //name
                                    reader.GetValue(3).ToString(),          //type
                                    Convert.ToInt32(reader.GetValue(4)),    //damage
                                    reader.GetValue(5).ToString(),          //element
                                    Convert.ToBoolean(reader.GetValue(6))     //active
                                    ));
                }
                return res;
            }
        }

        public bool Update(Card data)
        {
            string sql = "UPDATE cards SET uid=@p1, owner=@p2, name=@p3, type=@p4, damage=@p5, element=@p6, cost=@p7 WHERE uid=@p8";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", data.Uid);
            cmd.Parameters.AddWithValue("p2", data.Owner);
            cmd.Parameters.AddWithValue("p3", data.Name);
            cmd.Parameters.AddWithValue("p4", data.Type);
            cmd.Parameters.AddWithValue("p5", data.Damage);
            cmd.Parameters.AddWithValue("p6", data.Element);
            cmd.Parameters.AddWithValue("p7", data.Active);
            cmd.Parameters.AddWithValue("p8", data.Uid);

            if (_db.ExecuteNonQuery(cmd))
            {
                return true;
            }

            return false;
        }

        public List<Card> GetStackById(string id)
        {
            List<Card> res = new();
            string sql = "SELECT * FROM cards WHERE owner=@p1";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", id.ToString());

            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd))
            {
                while (reader.Read())
                {
                    res.Add(new Card(reader.GetValue(0).ToString(),    //uid
                                    reader.GetValue(1).ToString(),    //owner
                                    reader.GetValue(2).ToString(),              //name
                                    reader.GetValue(3).ToString(),              //type
                                    Convert.ToInt32(reader.GetValue(4)),        //damage
                                    reader.GetValue(5).ToString(),              //element
                                    Convert.ToBoolean(reader.GetValue(6))         //active
                                    ));
                }
                return res;
            }

        }

        public List<Card> GetDeckById(string id)
        {
            List<Card> res = new();
            string sql = "SELECT * FROM cards WHERE owner=@p1 AND active=true";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", id.ToString());

            using (NpgsqlDataReader reader = _db.ExecuteQuery(cmd))
            {
                while (reader.Read())
                {
                    res.Add(new Card(reader.GetValue(0).ToString(),    //uid
                                    reader.GetValue(1).ToString(),    //owner
                                    reader.GetValue(2).ToString(),              //name
                                    reader.GetValue(3).ToString(),              //type
                                    Convert.ToInt32(reader.GetValue(4)),        //damage
                                    reader.GetValue(5).ToString(),              //element
                                    Convert.ToBoolean(reader.GetValue(6))         //active
                                    ));
                }
                return res;
            }
        }

        public bool ResetDeck(string owner)
        {
            string sql = "UPDATE cards SET active=false WHERE owner=@p1";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", owner);

            if (_db.ExecuteNonQuery(cmd))
                return true;

            return false;
        }

        public bool SetDeck(List<string> deck)
        {
            string sql = "UPDATE cards SET active=true WHERE uid=@p1 OR uid=@p2 OR uid=@p3 OR uid=@p4";
            NpgsqlCommand cmd = new(sql);
            cmd.Parameters.AddWithValue("p1", deck.ElementAt(0));
            cmd.Parameters.AddWithValue("p2", deck.ElementAt(1));
            cmd.Parameters.AddWithValue("p3", deck.ElementAt(2));
            cmd.Parameters.AddWithValue("p4", deck.ElementAt(3));

            if (_db.ExecuteNonQuery(cmd))
                return true;

            return false;

            //foreach (string id in deck)
            //{
            //    string sql = "UPDATE cards SET active=true WHERE uid=@p1 OR uid=@p2 OR uid=@p3";
            //    NpgsqlCommand cmd = new(sql);
            //    cmd.Parameters.AddWithValue("p1", id);

            //    if(_db.ExecuteNonQuery(cmd))
            //}

        }
    }
}
