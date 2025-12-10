using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionnaireLicences.SQL;
using Microsoft.Extensions.Configuration;

namespace GestionnaireLicences.DataAccessLayer.Factories.Base
{
    public class FactoryBase
    {
        private string _cnnStr = string.Empty;

        public string CnnStr
        {
            get
            {
                if (_cnnStr == string.Empty)
                {
                    var test = AppDomain.CurrentDomain;
                    var config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
                    var sectionConnectionString = config.GetSection("ConnectionString");
                    ApiRestConnectionString connectionString = sectionConnectionString.Get<ApiRestConnectionString>();
                    _cnnStr = connectionString.BuildConnectionString();
                }

                return _cnnStr;
            }
        }
    }
}
