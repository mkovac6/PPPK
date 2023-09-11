using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSQL;
using TSQL.Statements;
using TSQL.Tokens;

namespace SQLManager.parser
{
    public static class TSQLParser
    {
        public static List<string> Parse(string raw)
        {
            List<string> output = new List<string>();
            foreach (TSQLToken token in TSQLTokenizer.ParseTokens(raw))
            {
                output.Add("type: " + token.Type.ToString() + ", value: " + token.Text);
            }
            return output;
        }
    }
}
