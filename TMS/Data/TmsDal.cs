using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    public class TmsDal
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TMSConnectionString"].ConnectionString;
    }
}
