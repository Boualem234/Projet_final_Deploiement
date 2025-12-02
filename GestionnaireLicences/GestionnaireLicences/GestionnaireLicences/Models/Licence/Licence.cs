using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionnaireLicences.Models.Base;


namespace GestionnaireLicences.Models.Licence
{

    public class Licence : ModelBase
    {
        public Licence()
            : base(0)
        { }
        public Licence (int id, string nomLogiciel, string typeLicence, DateTime? dateExpiration, int? nombreUtilisateurs)
            : base(id)
        {
            NombreUtilisateurs = nombreUtilisateurs;
            NomLogiciel = nomLogiciel;
            DateExpiration = dateExpiration;
            TypeLicence = typeLicence;
        }
        public string NomLogiciel { get; set; }

        public string TypeLicence { get; set; }

        public DateTime? DateExpiration { get; set; }

        public int? NombreUtilisateurs { get; set; }
    }
}
