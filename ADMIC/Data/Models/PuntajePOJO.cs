using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Realms;

namespace ADMIC.Data.Models
{



    public class PuntajePOJO
    {
        public string api_token { get; set; }
        public string id_evento { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
    }

}