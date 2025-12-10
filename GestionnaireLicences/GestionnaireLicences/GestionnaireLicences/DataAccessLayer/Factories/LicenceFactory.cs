using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionnaireLicences.Models.Licence;
using GestionnaireLicences.DataAccessLayer.Factories.Base;
using System.Data.SqlClient; // Changé de MySql.Data.MySqlClient

namespace GestionnaireLicences.DataAccessLayer.Factories
{
    public class LicenceFactory : FactoryBase
    {
        private Licence CreateFromReader(SqlDataReader sqlDataReader) // Changé de MySqlDataReader
        {
            int id = (int)sqlDataReader["Id"];
            string nomLogiciel = sqlDataReader["nom_logiciel"].ToString();
            string typeLicence = sqlDataReader["type_licence"].ToString();
            int? nombreUtilisateur = sqlDataReader["nombre_utilisateurs"] != DBNull.Value ? (int?)sqlDataReader["nombre_utilisateurs"] : null;
            DateTime? dateExpiration = sqlDataReader["date_expiration"] != DBNull.Value ? (DateTime?)sqlDataReader["date_expiration"] : null;
            return new Licence(id, nomLogiciel, typeLicence, dateExpiration, nombreUtilisateur);
        }

        public List<Licence> GetAll()
        {
            List<Licence> licences = new List<Licence>();

            SqlConnection sqlCnn = null; // Changé de MySqlConnection

            try
            {
                sqlCnn = new SqlConnection(CnnStr);
                sqlCnn.Open();

                using (SqlCommand sqlCmd = sqlCnn.CreateCommand()) // Changé de MySqlCommand
                {
                    sqlCmd.CommandText = "SELECT * FROM Licences";

                    using (SqlDataReader sqlDataReader = sqlCmd.ExecuteReader()) // Changé de MySqlDataReader
                    {
                        while (sqlDataReader.Read())
                        {
                            licences.Add(CreateFromReader(sqlDataReader));
                        }

                        sqlDataReader.Close();
                    }
                }
            }
            finally
            {
                if (sqlCnn != null)
                {
                    sqlCnn.Close();
                }
            }

            return licences;
        }

        public void Delete(int id)
        {
            SqlConnection sqlCnn = null; // Changé de MySqlConnection

            try
            {
                sqlCnn = new SqlConnection(CnnStr);
                sqlCnn.Open();

                using (SqlCommand sqlCmd = sqlCnn.CreateCommand()) // Changé de MySqlCommand
                {
                    sqlCmd.CommandText = "DELETE FROM licences WHERE Id=@Id";
                    sqlCmd.Parameters.AddWithValue("@Id", id);
                    sqlCmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (sqlCnn != null)
                {
                    sqlCnn.Close();
                }
            }
        }

        public void Save(Licence licence)
        {
            using (SqlConnection sqlCnn = new SqlConnection(CnnStr))
            {
                sqlCnn.Open();

                using (SqlCommand sqlCmd = sqlCnn.CreateCommand())
                {
                    if (licence.Id == 0)
                    {
                        sqlCmd.CommandText =
                            "INSERT INTO licences (nom_logiciel, type_licence, date_expiration, nombre_utilisateurs) " +
                            "VALUES (@nom_logiciel, @type_licence, @date_expiration, @nombre_utilisateurs); " +
                            "SELECT CAST(SCOPE_IDENTITY() AS int);";
                    }
                    else
                    {
                        sqlCmd.CommandText =
                            "UPDATE licences " +
                            "SET nom_logiciel=@nom_logiciel, type_licence=@type_licence, date_expiration=@date_expiration, nombre_utilisateurs=@nombre_utilisateurs " +
                            "WHERE Id=@Id";

                        sqlCmd.Parameters.AddWithValue("@Id", licence.Id);
                    }

                    sqlCmd.Parameters.AddWithValue("@nom_logiciel", licence.NomLogiciel.Trim());
                    sqlCmd.Parameters.AddWithValue("@type_licence", licence.TypeLicence.Trim());

                    if (licence.DateExpiration.HasValue)
                        sqlCmd.Parameters.AddWithValue("@date_expiration", licence.DateExpiration.Value);
                    else
                        sqlCmd.Parameters.Add(new SqlParameter("@date_expiration", System.Data.SqlDbType.DateTime) { Value = DBNull.Value });

                    if (licence.NombreUtilisateurs.HasValue)
                        sqlCmd.Parameters.AddWithValue("@nombre_utilisateurs", licence.NombreUtilisateurs.Value);
                    else
                        sqlCmd.Parameters.Add(new SqlParameter("@nombre_utilisateurs", System.Data.SqlDbType.Int) { Value = DBNull.Value });

                    if (licence.Id == 0)
                    {
                        // INSERT + récupération de l'ID
                        licence.Id = (int)sqlCmd.ExecuteScalar();
                    }
                    else
                    {
                        // UPDATE
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
