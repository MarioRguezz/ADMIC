using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ADMIC.Data.Models
{

	public class EmailPOJO
	{
		public string id_usuario { get; set; }
		public string id_convocatoria { get; set; }
		public string api_token { get; set; }
	}

}


