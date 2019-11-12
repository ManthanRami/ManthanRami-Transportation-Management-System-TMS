using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    class CmpDal
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["CMPConnectionString"].ConnectionString;

        public List<Contract> GetContracts()
        {
            throw new NotImplementedException();
        }
    }
}
