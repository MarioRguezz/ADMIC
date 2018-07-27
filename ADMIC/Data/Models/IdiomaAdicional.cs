using System;
using System.IO;
using Newtonsoft.Json;

namespace ADMIC.Data.Models
{

	public class IdiomaAdicional
	{
		public int? id_idioma_adicional { get; set; }
		public int id_datos_usuario { get; set; }
		public int conversacion { get; set; }
		public int lectura { get; set; }
		public int escritura { get; set; }
	}
}

