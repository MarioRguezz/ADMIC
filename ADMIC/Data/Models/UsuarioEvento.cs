using System;
using System.IO;
using Newtonsoft.Json;

namespace ADMIC.Data.Models
{

	public class UsuarioEvento
	{
		public string id_evento { get; set; }
		public string id_usuario { get; set; }
		public string api_token { get; set; } 
	}
}
