using SQLManager.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.dal
{
    interface IRepository
    {
        IEnumerable<Database> GetDatabases();
        QueryData Query(string queryString);
        string GetMessage();
        void Connect(string servername, string username, string password);
    }
}
