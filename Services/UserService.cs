using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nekomaid_Club_Bot.Services
{
    public class UserService
    {
        public const string Path = "Services\\users.json";
        private Dictionary<ulong, Dictionary<ulong, string>> _dict;

        public UserService(Dictionary<ulong, Dictionary<ulong, string>> dict)
        {
            _dict = dict;
        }

        public static Dictionary<ulong, Dictionary<ulong, string>> getUserItem()
        {
            var returndict = System.IO.File.ReadAllText(Path);
            return null;
            //return returndict == "" ? new Dictionary<ulong, Dictionary<ulong, string>>() : JsonConvert.DeserializeObject<Dictionary<ulong, Dictionary<ulong, string>>(returndict);
        }
    }
}
