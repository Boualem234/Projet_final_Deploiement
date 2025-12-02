using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionnaireLicences.DataAccessLayer.Factories;

namespace GestionnaireLicences.DataAccessLayer
{
    public class DAL
    {
        private LicenceFactory _licenceFact = null;
        public LicenceFactory LicenceFact
        {
            get
            {
                if (_licenceFact == null)
                {
                    _licenceFact = new LicenceFactory();
                }

                return _licenceFact;
            }
        }
    }
}
