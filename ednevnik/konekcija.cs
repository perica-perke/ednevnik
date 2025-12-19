using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Ednevnik410b
{
    internal class konekcija
    {
        public static SqlConnection povezi()
        {
            SqlConnection rezultat = new SqlConnection("Data Source=DESKTOP-6LPEK0P\\SQLEXPRESS;Initial catalog=dnevnik410b;Integrated security=true");
            return rezultat;
        }
    }
}