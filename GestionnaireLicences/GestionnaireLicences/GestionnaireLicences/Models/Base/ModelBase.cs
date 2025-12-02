using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionnaireLicences.Models.Base
{
    public class ModelBase
    {
        /// <summary>
        /// Identifiant unique du produit dans la base de données.
        /// </summary>
        public int Id { get; set; }

        public ModelBase(int id)
        {
            Id = id;
        }
    }
}