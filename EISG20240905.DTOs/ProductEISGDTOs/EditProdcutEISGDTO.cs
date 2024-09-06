using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EISG20240905.DTOs.ProductEISGDTOs
{
    public class EditProdcutEISGDTO
    {
        public EditProdcutEISGDTO(GetIdResultProductEISGDTO getIdResultProdcutEISGDTO)
        {
            Id = getIdResultProdcutEISGDTO.Id;
            NombreEISG = getIdResultProdcutEISGDTO.NombreEISG;
            DescripcionEISG = getIdResultProdcutEISGDTO.DescripcionEISG;
            Precio = getIdResultProdcutEISGDTO.Precio;
        }
        public EditProdcutEISGDTO()
        {
            NombreEISG = string.Empty;
            Precio = 0;
        }

        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo Nombre no puede tener más de 100 caracteres.")]
        public string NombreEISG { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(255, ErrorMessage = "El campo Descripción no puede tener más de 255 caracteres.")]
        public string? DescripcionEISG { get; set; }

        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        public decimal Precio { get; set; }
    }
}
