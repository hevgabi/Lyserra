using Lyserra.PlayerAndAttributes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace Lyserra.Game
{
    public class LyserraDB : IDisposable
    {
        private string folderPath = "Database";
        private string dbFile = "Lyserra.db";
        private string fullPath;
        private readonly string _dbPath;
        private SQLiteConnection _connection;

        public LyserraDB(string dbPath)
        {
            
            if (_dbPath == null)
            {
                _dbPath = dbPath;
                initializeDatabase();
                _connection = new SQLiteConnection($"Data Source={dbFile}");
                _connection.Open();
            } else
            {
                _dbPath = dbPath;
                initializeDatabase();
                _connection = new SQLiteConnection($"Data Source={_dbPath}");
                _connection.Open();
            }
        }

        //======================== MASTER METHODS ========================//

        public long insertMaster(Master master)
        {
            string sql = "INSERT INTO Master (masterName, specialTrait, masterType) VALUES (@masterName, @specialTrait, @masterType);";

            using var cmd = new SQLiteCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@masterName", master.MasterName ?? "");
            cmd.Parameters.AddWithValue("@specialTrait", master.SpecialTrait ?? "");
            cmd.Parameters.AddWithValue("@masterType", master.MasterType ?? "");

            cmd.ExecuteNonQuery();
            master.MasterID = _connection.LastInsertRowId;
            return master.MasterID;
        }

        public Master getMasterById(long masterId)
        {
            string sql = "SELECT * FROM Master WHERE masterID=@masterID";

            using var cmd = new SQLiteCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@masterID", masterId);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Master(reader["masterName"].ToString())
                {
                    MasterID = Convert.ToInt64(reader["masterID"]),
                    SpecialTrait = reader["specialTrait"].ToString(),
                    MasterType = reader["masterType"].ToString()
                };
            }

            return null;
        }

        public bool updateMaster(Master master)
        {
            string sql = @"UPDATE Master 
                           SET masterName=@masterName, specialTrait=@specialTrait, masterType=@masterType 
                           WHERE masterID=@masterID;";

            using var cmd = new SQLiteCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@masterName", master.MasterName);
            cmd.Parameters.AddWithValue("@specialTrait", master.SpecialTrait);
            cmd.Parameters.AddWithValue("@masterType", master.MasterType);
            cmd.Parameters.AddWithValue("@masterID", master.MasterID);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool deleteMaster(long masterId)
        {
            string sql = "DELETE FROM Master WHERE masterID=@masterID;";

            using var cmd = new SQLiteCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@masterID", masterId);

            return cmd.ExecuteNonQuery() > 0;


        }

        public List<Master> searchMasters(string name)
        {
            string sql = "SELECT * FROM Master WHERE MasterName LIKE @name;";
            using var cmd = new SQLiteCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@name", "%" + name + "%");

            using var reader = cmd.ExecuteReader();
            var list = new List<Master>();

            while (reader.Read())
            {
                list.Add(new Master(reader["MasterName"].ToString())
                {
                    MasterID = (long)reader["MasterID"],
                    SpecialTrait = reader["SpecialTrait"].ToString(),
                    MasterType = reader["MasterType"].ToString()
                });
            }

            return list;
        }

        public List<Master> getAllMasters()
        {
            string sql = "SELECT * FROM Master;";
            using var cmd = new SQLiteCommand(sql, _connection);
            using var reader = cmd.ExecuteReader();

            var list = new List<Master>();

            while (reader.Read())
            {
                list.Add(new Master(reader["MasterName"].ToString())
                {
                    MasterID = (long)reader["MasterID"],
                    SpecialTrait = reader["SpecialTrait"].ToString(),
                    MasterType = reader["MasterType"].ToString()
                });
            }

            return list;
        }



        //======================== PET METHODS ========================//

        public long insertPet(Pet pet)
        {
            string sql = @"INSERT INTO Pet 
            (masterID, Type, petName, weight, age, breed, hairColor, colorDesign, hairCut, eyeColor, accessory,
             personality, scent, mutation, element, crystal, evolution, strength, mana, defense, speed)
            VALUES
            (@masterID, @type, @petName, @weight, @age, @breed, @hairColor, @colorDesign, @hairCut, @eyeColor,
             @accessory, @personality, @scent, @mutation, @element, @crystal, @evolution, @strength, @mana, @defense, @speed);";

            using var cmd = new SQLiteCommand(sql, _connection);

            cmd.Parameters.AddWithValue("@masterID", pet.MasterID);
            cmd.Parameters.AddWithValue("@type", pet.Type ?? "");
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
            cmd.Parameters.AddWithValue("@strength", pet.Strength);
            cmd.Parameters.AddWithValue("@mana", pet.Mana);
            cmd.Parameters.AddWithValue("@defense", pet.Defense);
            cmd.Parameters.AddWithValue("@speed", pet.Speed);

            cmd.ExecuteNonQuery();
            pet.PetID = _connection.LastInsertRowId;
            return pet.PetID;
        }


        public Pet getPetById(long petId)
        {
            string sql = "SELECT * FROM Pet WHERE petID=@petID";

            using var cmd = new SQLiteCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@petID", petId);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
                return mapPetFromReader(reader);

            return null;
        }


        public List<Pet> getPetsByMasterId(long masterId)
        {
            var list = new List<Pet>();
            string sql = "SELECT * FROM Pet WHERE masterID=@masterID ORDER BY petID";

            using var cmd = new SQLiteCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@masterID", masterId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(mapPetFromReader(reader));
            }

            return list;
        }


        public bool updatePet(Pet pet)
        {
            string sql = @"UPDATE Pet SET 
                masterID=@masterID, Type=@type, petName=@petName, weight=@weight, age=@age, breed=@breed, 
                hairColor=@hairColor, colorDesign=@colorDesign, hairCut=@hairCut, eyeColor=@eyeColor, 
                accessory=@accessory, personality=@personality, scent=@scent, mutation=@mutation, element=@element,
                crystal=@crystal, evolution=@evolution, strength=@strength, mana=@mana, defense=@defense, speed=@speed
                WHERE petID=@petID;";

            using var cmd = new SQLiteCommand(sql, _connection);

            cmd.Parameters.AddWithValue("@masterID", pet.MasterID);
            cmd.Parameters.AddWithValue("@type", pet.Type);
            cmd.Parameters.AddWithValue("@petName", pet.Name);
            cmd.Parameters.AddWithValue("@weight", pet.Weight);
            cmd.Parameters.AddWithValue("@age", pet.Age);
            cmd.Parameters.AddWithValue("@breed", pet.Breed);
            cmd.Parameters.AddWithValue("@hairColor", pet.HairColor);
            cmd.Parameters.AddWithValue("@colorDesign", pet.ColorDesign);
            cmd.Parameters.AddWithValue("@hairCut", pet.HairCut);
            cmd.Parameters.AddWithValue("@eyeColor", pet.EyeColor);
            cmd.Parameters.AddWithValue("@accessory", pet.Accessory);
            cmd.Parameters.AddWithValue("@personality", pet.Personality);
            cmd.Parameters.AddWithValue("@scent", pet.Scent);
            cmd.Parameters.AddWithValue("@mutation", pet.Mutation);
            cmd.Parameters.AddWithValue("@element", pet.Element);
            cmd.Parameters.AddWithValue("@crystal", pet.Crystal);
            cmd.Parameters.AddWithValue("@evolution", pet.Evolution);
            cmd.Parameters.AddWithValue("@strength", pet.Strength);
            cmd.Parameters.AddWithValue("@mana", pet.Mana);
            cmd.Parameters.AddWithValue("@defense", pet.Defense);
            cmd.Parameters.AddWithValue("@speed", pet.Speed);
            cmd.Parameters.AddWithValue("@petID", pet.PetID);

            return cmd.ExecuteNonQuery() > 0;
        }


        public bool deletePet(long petId)
        {
            string sql = "DELETE FROM Pet WHERE petID=@petID;";

            using var cmd = new SQLiteCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@petID", petId);

            return cmd.ExecuteNonQuery() > 0;
        }

        public void deletePetsByMasterId(long masterId)
        {
            string sql = "DELETE FROM Pet WHERE MasterID = @id;";
            using var cmd = new SQLiteCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@id", masterId);
            cmd.ExecuteNonQuery();
        }



        //======================== OBJECT MAPPER ========================//

        private Pet mapPetFromReader(SQLiteDataReader reader)
        {
            string type = reader["Type"]?.ToString() ?? "Unknown";
            string name = reader["petName"]?.ToString() ?? "Unnamed";

            Pet pet;

            if (type.Equals("Dog", StringComparison.OrdinalIgnoreCase)) pet = new Dog(name);
            else if (type.Equals("Cat", StringComparison.OrdinalIgnoreCase)) pet = new Cat(name);
            else pet = new Dog(name);

            pet.PetID = Convert.ToInt64(reader["petID"]);
            pet.MasterID = Convert.ToInt64(reader["masterID"]);

            pet.Type = type;
            pet.Name = name;

            pet.Weight = reader["weight"]?.ToString();
            pet.Age = reader["age"]?.ToString();
            pet.Breed = reader["breed"]?.ToString();
            pet.HairColor = reader["hairColor"]?.ToString();
            pet.ColorDesign = reader["colorDesign"]?.ToString();
            pet.HairCut = reader["hairCut"]?.ToString();
            pet.EyeColor = reader["eyeColor"]?.ToString();
            pet.Accessory = reader["accessory"]?.ToString();
            pet.Personality = reader["personality"]?.ToString();
            pet.Scent = reader["scent"]?.ToString();
            pet.Mutation = reader["mutation"]?.ToString();
            pet.Element = reader["element"]?.ToString();
            pet.Crystal = reader["crystal"]?.ToString();
            pet.Evolution = reader["evolution"]?.ToString();

            pet.Strength = reader["strength"] != DBNull.Value ? Convert.ToByte(reader["strength"]) : (byte)0;
            pet.Mana = reader["mana"] != DBNull.Value ? Convert.ToByte(reader["mana"]) : (byte)0;
            pet.Defense = reader["defense"] != DBNull.Value ? Convert.ToByte(reader["defense"]) : (byte)0;
            pet.Speed = reader["speed"] != DBNull.Value ? Convert.ToByte(reader["speed"]) : (byte)0;

            return pet;
        }

        public List<Pet> searchPets(string name)
        {
            string sql = "SELECT * FROM Pet WHERE Name LIKE @name;";
            using var cmd = new SQLiteCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@name", "%" + name + "%");

            using var reader = cmd.ExecuteReader();
            var list = new List<Pet>();

            while (reader.Read())
            {
                list.Add(mapPetFromReader(reader));
            }

            return list;
        }



        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }

        public void initializeDatabase()
        {

            fullPath = Path.Combine(folderPath, dbFile);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine("Created folder: Database");
            }

            if (!File.Exists(fullPath))
            {
                SQLiteConnection.CreateFile(fullPath);
                Console.WriteLine("Created Lyserra.db");
            }

            _connection = new SQLiteConnection($"Data Source={fullPath};Version=3;");
            _connection.Open();

 
            createTables();
        }

        public void createTables()
        {
            string createMaster = @"
            CREATE TABLE IF NOT EXISTS Master (
                masterID INTEGER PRIMARY KEY AUTOINCREMENT,
                masterName TEXT NOT NULL,
                specialTrait TEXT,
                masterType TEXT
            );";

            string createPet = @"
            CREATE TABLE IF NOT EXISTS Pet (
                petID INTEGER PRIMARY KEY AUTOINCREMENT,
                masterID INTEGER NOT NULL,
                Type TEXT,
                petName TEXT,
                weight TEXT,
                age TEXT,
                breed INTEGER,
                hairColor TEXT,
                colorDesign TEXT,
                hairCut TEXT,
                eyeColor TEXT,
                accessory TEXT,
                personality TEXT,
                scent TEXT,
                mutation TEXT,
                element TEXT,
                crystal TEXT,
                evolution TEXT,
                strength INTEGER,
                mana INTEGER,
                defense INTEGER,
                speed INTEGER,
                FOREIGN KEY(masterID) REFERENCES Master(masterID)
            );";

            using var cmd = new SQLiteCommand(createMaster, _connection);
            cmd.ExecuteNonQuery();

            using var cmd2 = new SQLiteCommand(createPet, _connection);
            cmd2.ExecuteNonQuery();
        }
    }
}
