using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EISG20240905.DTOs.ProductEISGDTOs
{
	public class SearchQueryProductEISGDTO
	{
		[Display(Name = "Nombre")]
		public string? NombreEISG_Like { get; set; }

		[Display(Name = "Precio")]
		public decimal? Precio_Like { get; set; }

		[Display(Name = "Pagina")]
		public int Skip { get; set; }

		[Display(Name = "Cantidad Reg.")]
		public int Take { get; set; }
		/// <summary>
		/// 1 = No se cuenta los resultados de la busqueda
		/// 2= Cuenta los resultados de la busqueda
		/// </summary>
		public byte SendRowCount { get; set; }
	}
}
