using System;
using System.IO;
using Newtonsoft.Json;

namespace ADMIC.Data.Models
{

	public class Login
	{
		public int? id_login_app { get; set; }
		public string correo { get; set; }
		public int facebook;
		public int google;
		public int inserted;
		public string contrasena { get; set; }
	}
}



