using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EISG20240905.DTOs.ProductEISGDTOs
{
	public class GetIdResultProductEISGDTO
	{
		public int Id { get; set; }

		[Display(Name = "Nombre")]
		public string NombreEISG { get; set; }

		[Display(Name = "Descripción")]
		public string? DescripcionEISG { get; set; }

		public decimal Precio { get; set; }
	}
}
