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
                    try
                    {
                        var basePath = AppDomain.CurrentDomain.BaseDirectory;
                        var config = new ConfigurationBuilder()
                            .SetBasePath(basePath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .Build();
                        
                        var sectionConnectionString = config.GetSection("ConnectionString");
                        ApiRestConnectionString connectionString = sectionConnectionString.Get<ApiRestConnectionString>();
                        _cnnStr = connectionString.BuildConnectionString();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Erreur lors de la lecture de appsettings.json : {ex.Message}", ex);
                    }
                }

                return _cnnStr;
            }
        }
    }
}
