using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using Realms;

namespace ADMIC.Data.Models
{


	public class Evento : RealmObject
	{
		public int? id_evento { get; set; }
		public string titulo { get; set; }
		public string descripcion { get; set; }
		public string fecha_inicio { get; set; }
		public string fecha_fin { get; set; }
		public string evento_estado { get; set; }
        public string id_tipo_evento { get; set; }
		public string direccion { get; set; }
		public double latitud { get; set; }
		public double longitud { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string deleted_at { get; set; }
		public string fecha { get; set; }
		public bool IsEventoActivo { get; set; }
        public string puntos_otorgados { get; set; }
        public string ruta_imagen { get; set; }
        public string area_id { get; set; }
        public IList<Documentos> documentos { get;  }
        public Area area { get; set; }

	}


    public class Documentos : RealmObject
    {
        public int id { get; set; }
        public string id_evento { get; set; }
        public string titulo { get; set; }
        public string ruta_documento { get; set; }
        public string id_formato { get; set; }
        public string deleted_at { get; set; }
        public string created_at { get; set; } 
        public string Icono { get; set; }
        public string updated_at { get; set; }
        public Formato formato { get; set; }
    }

    public class Area  : RealmObject
    {
        public int id { get; set; }
        public string id_direccion { get; set; }
        public string nombre { get; set; }
        public string clave { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string deleted_at { get; set; }
    }


	public class ResponseEvent
	{
		public int status { get; set; }
		public bool success { get; set; }
		public List<object> errors { get; set; }
		public List<Evento> data { get; set; }
	}
}