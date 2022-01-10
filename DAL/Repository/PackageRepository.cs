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
    public class PackageRepository : IRepository<Package>
    {
        public Database DB;
        public PackageRepository(Database db)
        {
            DB = db;
        }
        public bool Create(Package data)
        {
            foreach (Card card in data.Cards)
            {
                string sql = "INSERT INTO packages (uid, package, name, type, damage, element, Active) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7)";
                var cmd = new NpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("p1", card.Uid);
                cmd.Parameters.AddWithValue("p2", card.Owner);
                cmd.Parameters.AddWithValue("p3", card.Name);
                cmd.Parameters.AddWithValue("p4", card.Type);
                cmd.Parameters.AddWithValue("p5", card.Damage);
                cmd.Parameters.AddWithValue("p6", card.Element);
                cmd.Parameters.AddWithValue("p7", card.Active);

                if (DB.ExecuteNonQuery(cmd) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Package Read(string id)
        {
            Package result = new();
            result.Cards = new List<Card>();

            string sql = "SELECT * FROM packages WHERE package=@p1";
            var cmd = new NpgsqlCommand(sql);
            cmd.Parameters.AddWithValue("p1", id);

            using (NpgsqlDataReader reader = DB.ExecuteQuery(cmd))
            {
                if (!reader.HasRows)
                    return null;

                while (reader.Read())
                {
                    result.Cards.Add(new Card(reader.GetValue(0).ToString(),
                                                reader.GetValue(1).ToString(),
                                                reader.GetValue(2).ToString(),
                                                reader.GetValue(3).ToString(),
                                                Convert.ToInt32(reader.GetValue(4)),
                                                reader.GetValue(5).ToString(),
                                                Convert.ToBoolean(reader.GetValue(6))));
                }
                return result;
            }
        }

        public List<Package> ReadAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Package data)
        {
            throw new NotImplementedException();
        }
    }
}
