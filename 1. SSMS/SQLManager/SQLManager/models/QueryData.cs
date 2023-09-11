using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.models
{
    public struct QueryData
    {

        public QueryData(DataSet data, string message, List<string> list)
        {
            Data = data;
            Message = message;
            Parsed = list;
        }

        public DataSet Data { get; }
        public string Message { get; }
        
        public List<string> Parsed { get; }
    }
}
