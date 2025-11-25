using Lyserra.PlayerAndAttributes;
using System;
using System.Data.SQLite;

namespace Lyserra.Game
{
    public static class LyserraDB
    {
        private static string connectionString = "Data Source=Lyserra.db";

        public static void initialize()
        {
            if (!File.Exists("Lyserra.db"))
            {
                SQLiteConnection.CreateFile("Lyserra.db");
            }
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = File.ReadAllText("schema.sql");
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public static void insertMaster(Master master)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Master (masterName, specialTrait, masterType) VALUES (@masterName, @specialTrait, @type)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@masterName", master.MasterName);
                    cmd.Parameters.AddWithValue("@specialTrait", master.SpecialTrait ?? "");
                    cmd.Parameters.AddWithValue("@type", master.MasterType ?? "");
                    cmd.ExecuteNonQuery();
                    master.MasterID = Convert.ToInt32((long)new SQLiteCommand("SELECT last_insert_rowid()", conn).ExecuteScalar());
                }
            }
        }

        public static List<Master> loadMasters()
        {
            List<Master> master = new List<Master>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT masterID, masterName, specialTrait, masterType FROM Master";
                using (var cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Master m = new Master(reader["masterName"].ToString());
                        m.MasterID = Convert.ToInt32(reader["masterID"]);
                        m.MasterType = reader["masterType"].ToString();
                        master.Add(m);
                    }
                }
            }
            return master;
        }

        public static void insertPet(Pet pet)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO Pet (masterID, petName, weight, age, breed, hairColor, colorDesign, 
                              hairCut, eyeColor, accessory, personality, scent, mutation, element, crystal, evolution)
                              VALUES (@masterID, @petName, @weight, @age, @breed, @hairColor, @colorDesign, 
                              @hairCut, @eyeColor, @accessory, @personality, @scent, @mutation, @element, @crystal, @evolution)";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@masterID", pet.MasterID ?? "");
                    cmd.Parameters.AddWithValue("@petName", pet.Name ?? "");
                    cmd.Parameters.AddWithValue("@weight", pet.Weight ?? "");
                    cmd.Parameters.AddWithValue("@age", pet.Age ?? "");
                    cmd.Parameters.AddWithValue("@breed", pet.Breed ?? "");
                    cmd.Parameters.AddWithValue("@hairColor", pet.HairColor ?? "");
                    cmd.Parameters.AddWithValue("@colorDesign", pet.ColorDesign ?? "");
                    cmd.Parameters.AddWithValue("@hairCut", pet.HairCut ?? "");
                    cmd.Parameters.AddWithValue("@eyeColor", pet.EyeColor ?? "");
                    cmd.Parameters.AddWithValue("@accessory", pet.Accessory ?? "");
                    cmd.Parameters.AddWithValue("@personality", pet.Personality ?? "");
                    cmd.Parameters.AddWithValue("@scent", pet.Scent ?? "");
                    cmd.Parameters.AddWithValue("@mutation", pet.Mutation ?? "");
                    cmd.Parameters.AddWithValue("@element", pet.Element ?? "");
                    cmd.Parameters.AddWithValue("@crystal", pet.Crystal ?? "");
                    cmd.Parameters.AddWithValue("@evolution", pet.Evolution ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}