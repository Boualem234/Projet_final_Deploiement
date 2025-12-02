using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionnaireLicences.Models.Licence;
using GestionnaireLicences.DataAccessLayer.Factories.Base;
using MySql.Data.MySqlClient;

namespace GestionnaireLicences.DataAccessLayer.Factories
{
    public class LicenceFactory : FactoryBase
    {
        private Licence CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            int id = (int)mySqlDataReader["Id"];
            string nomLogiciel = mySqlDataReader["nom_logiciel"].ToString();
            string typeLicence = mySqlDataReader["type_licence"].ToString();
            int? nombreUtilisateur = mySqlDataReader["nombre_utilisateurs"] != DBNull.Value ? (int?)mySqlDataReader["nombre_utilisateurs"] : null;
            DateTime? dateExpiration = mySqlDataReader["date_expiration"] != DBNull.Value ? (DateTime?)mySqlDataReader["date_expiration"] : null;
            //DateTime dateExpiration = new DateTime();
            return new Licence(id, nomLogiciel, typeLicence, dateExpiration, nombreUtilisateur);
        }
        /*
        public Licence CreateEmpty()
        {
            return new Licence(0, string.Empty, string.Empty, 0);
        }*/

        public List<Licence> GetAll()
        {
            List<Licence> licences = new List<Licence>();

            MySqlConnection mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(CnnStr);
                mySqlCnn.Open();

                using (MySqlCommand mySqlCmd = mySqlCnn.CreateCommand())
                {
                    mySqlCmd.CommandText = "SELECT * FROM Licences";

                    using (MySqlDataReader mySqlDataReader = mySqlCmd.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            licences.Add(CreateFromReader(mySqlDataReader));
                        }

                        mySqlDataReader.Close();
                    }
                }
            }
            finally
            {
                if (mySqlCnn != null)
                {
                    mySqlCnn.Close();
                }
            }

            return licences;
        }
        public void Delete(int id)
        {
            MySqlConnection mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(CnnStr);
                mySqlCnn.Open();

                using (MySqlCommand mySqlCmd = mySqlCnn.CreateCommand())
                {
                    mySqlCmd.CommandText = "DELETE FROM licences WHERE Id=@Id";
                    mySqlCmd.Parameters.AddWithValue("@Id", id);
                    mySqlCmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (mySqlCnn != null)
                {
                    mySqlCnn.Close();
                }
            }

        }
        public void Save(Licence licence)
        {
            MySqlConnection mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(CnnStr);
                mySqlCnn.Open();

                using (MySqlCommand mySqlCmd = mySqlCnn.CreateCommand())
                {
                    if (licence.Id == 0)
                    {
                        // On sait que c'est un nouveau produit avec Id == 0,
                        // car c'est ce que nous avons affecter dans la fonction CreateEmpty().
                        mySqlCmd.CommandText = "INSERT INTO licences (nom_logiciel, type_licence, date_expiration, nombre_utilisateurs)" +
                                               "VALUES (@nom_logiciel, @type_licence, @date_expiration, @nombre_utilisateurs)";
                    }
                    else
                    {
                        mySqlCmd.CommandText = "UPDATE licences " +
                                               "SET nom_logiciel=@nom_logiciel, type_licence=@type_licence, date_expiration=@date_expiration, nombre_utilisateurs=@nombre_utilisateurs " +
                                               "WHERE Id=@Id";

                        mySqlCmd.Parameters.AddWithValue("@Id", licence.Id);
                    }

                    mySqlCmd.Parameters.AddWithValue("@nom_logiciel", licence.NomLogiciel.Trim());
                    mySqlCmd.Parameters.AddWithValue("@type_licence", licence.TypeLicence.Trim());
                    mySqlCmd.Parameters.AddWithValue("@date_expiration", licence.DateExpiration);
                    mySqlCmd.Parameters.AddWithValue("@nombre_utilisateurs", licence.NombreUtilisateurs);

                    mySqlCmd.ExecuteNonQuery();

                    if (licence.Id == 0)
                    {
                        // Si c'était un nouveau produit (requête INSERT),
                        // nous affectons le nouvel Id de l'instance au cas où il serait utilisé dans le code appelant.
                        licence.Id = (int)mySqlCmd.LastInsertedId;
                    }
                }
            }
            finally
            {
                if (mySqlCnn != null)
                {
                    mySqlCnn.Close();
                }
            }
        }
        /*
        public Licence GetFirst()
        {
            Licence licence = null;

            MySqlConnection mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(CnnStr);
                mySqlCnn.Open();

                using (MySqlCommand mySqlCmd = mySqlCnn.CreateCommand())
                {
                    mySqlCmd.CommandText = "SELECT * FROM tp3_licences LIMIT 1";

                    using (MySqlDataReader mySqlDataReader = mySqlCmd.ExecuteReader())
                    {
                        if (mySqlDataReader.Read())
                        {
                            licence = CreateFromReader(mySqlDataReader);
                        }

                        mySqlDataReader.Close();
                    }
                }
            }
            finally
            {
                if (mySqlCnn != null)
                {
                    mySqlCnn.Close();
                }
            }

            return licence;
        }

        public Licence Get(int id)
        {
            Licence licence = null;

            MySqlConnection mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(CnnStr);
                mySqlCnn.Open();

                using (MySqlCommand mySqlCmd = mySqlCnn.CreateCommand())
                {
                    mySqlCmd.CommandText = "SELECT * FROM tp3_licences WHERE Id = @Id";
                    mySqlCmd.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader mySqlDataReader = mySqlCmd.ExecuteReader())
                    {
                        if (mySqlDataReader.Read())
                        {
                            licence = CreateFromReader(mySqlDataReader);
                        }

                        mySqlDataReader.Close();
                    }
                }
            }
            finally
            {
                if (mySqlCnn != null)
                {
                    mySqlCnn.Close();
                }
            }

            return licence;
        }

        public void Save(Licence licence)
        {
            MySqlConnection mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(CnnStr);
                mySqlCnn.Open();

                using (MySqlCommand mySqlCmd = mySqlCnn.CreateCommand())
                {
                    if (licence.Id == 0)
                    {
                        // On sait que c'est un nouveau produit avec Id == 0,
                        // car c'est ce que nous avons affecter dans la fonction CreateEmpty().
                        mySqlCmd.CommandText = "INSERT INTO tp3_licences(Code, Name, CategoryId, Scale, Vendor, Description, QuantityInStock, BuyPrice, MSRP) " +
                                               "VALUES (@Code, @Name, 1, 1, '', '', @QuantityInStock, 0, 0)";
                    }
                    else
                    {
                        mySqlCmd.CommandText = "UPDATE tp3_licences " +
                                               "SET Code=@Code, Name=@Name, QuantityInStock=@QuantityInStock " +
                                               "WHERE Id=@Id";

                        mySqlCmd.Parameters.AddWithValue("@Id", licence.Id);
                    }

                    mySqlCmd.Parameters.AddWithValue("@Code", licence.Code.Trim());
                    mySqlCmd.Parameters.AddWithValue("@Name", licence.Name.Trim());
                    mySqlCmd.Parameters.AddWithValue("@QuantityInStock", licence.QuantityInStock);

                    mySqlCmd.ExecuteNonQuery();

                    if (licence.Id == 0)
                    {
                        // Si c'était un nouveau produit (requête INSERT),
                        // nous affectons le nouvel Id de l'instance au cas où il serait utilisé dans le code appelant.
                        licence.Id = (int)mySqlCmd.LastInsertedId;
                    }
                }
            }
            finally
            {
                if (mySqlCnn != null)
                {
                    mySqlCnn.Close();
                }
            }
        }

        public void Delete(int id)
        {
            MySqlConnection mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(CnnStr);
                mySqlCnn.Open();

                using (MySqlCommand mySqlCmd = mySqlCnn.CreateCommand())
                {
                    mySqlCmd.CommandText = "DELETE FROM tp3_licences WHERE Id=@Id";
                    mySqlCmd.Parameters.AddWithValue("@Id", id);
                    mySqlCmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (mySqlCnn != null)
                {
                    mySqlCnn.Close();
                }
            }
        
        }*/
    }
}
