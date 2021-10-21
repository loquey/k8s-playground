using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kube_app.Models
{
    public class CacheEntryModel
    {
        public string Key { get; set; }
        public string Value {  get; set; }
        public int ExpirationInMins { get; set; }
    }
}
