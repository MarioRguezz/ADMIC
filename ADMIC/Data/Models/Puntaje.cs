using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Realms;

namespace ADMIC.Data.Models
{



	public class Puntaje
	{
		public int puntos_otorgados { get; set; }
		public int asistio { get; set; }
	}

	public class PuntajeResponse
	{
		public int status { get; set; }
		public bool success { get; set; }
		public List<string> errors { get; set; }
		public Puntaje data { get; set; }
	}
}