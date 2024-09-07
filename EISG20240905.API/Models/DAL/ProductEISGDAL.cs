using EISG20240905.API.Models.EN;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
namespace EISG20240905.API.Models.DAL
{
	public class ProductEISGDAL
	{
		readonly EISG20240905Context _context;

		// Constructor que recibe un objeto Context para
		// interactuar con la base de datos.
		public ProductEISGDAL(EISG20240905Context eISG20240905Context)
		{
			_context = eISG20240905Context;
		}

		// Método para crear un nuevo producto en la base de datos.
		public async Task<int> Create(ProductEISG productEISG)
		{
			_context.Add(productEISG);
			return await _context.SaveChangesAsync();
		}

		// Método para obtener un producto por su ID.
		public async Task<ProductEISG> GetById(int id)
		{
			var productEISG = await _context.ProductsEISG.FirstOrDefaultAsync(s => s.Id == id);
			return productEISG != null ? productEISG : new ProductEISG();
		}

		// Método para editar un producto existente en la base de datos.
		public async Task<int> Edit(ProductEISG productEISG)
		{
			int result = 0;
			var productEISGUpdate = await GetById(productEISG.Id);
			if (productEISGUpdate.Id != 0)
			{
				// Actualiza los datos del producto.
				productEISGUpdate.NombreEISG = productEISG.NombreEISG;
				productEISGUpdate.DescripcionEISG = productEISG.DescripcionEISG;
				productEISGUpdate.Precio = productEISG.Precio;
				result = await _context.SaveChangesAsync();
			}
			return result;
		}

		// Método para eliminar un producto de la base de datos por su ID.
		public async Task<int> Delete(int id)
		{
			int result = 0;
			var productEISGDelete = await GetById(id);
			if (productEISGDelete.Id > 0)
			{
				// Elimina el producto de la base de datos.
				_context.ProductsEISG.Remove(productEISGDelete);
				result = await _context.SaveChangesAsync();
			}
			return result;
		}

		// Método privado para construir una consulta IQueryable para buscar productos con filtros.
		private IQueryable<ProductEISG> Query(ProductEISG productEISG)
		{
			var query = _context.ProductsEISG.AsQueryable();
			if (!string.IsNullOrWhiteSpace(productEISG.NombreEISG))
				query = query.Where(s => s.NombreEISG.Contains(productEISG.NombreEISG));
			if (productEISG.Precio > 0)
				query = query.Where(s => s.Precio == productEISG.Precio);
			return query;
		}

		// Método para contar la cantidad de resultados de búsqueda con filtros.
		public async Task<int> CountSearch(ProductEISG productEISG)
		{
			return await Query(productEISG).CountAsync();
		}

		// Método para buscar productoa con filtros, paginación y ordenamiento.
		public async Task<List<ProductEISG>> Search(ProductEISG productEISG, int take = 10, int skip = 0)
		{
			take = take == 0 ? 10 : take;
			var query = Query(productEISG);
			query = query.OrderByDescending(s => s.Id).Skip(skip).Take(take);
			return await query.ToListAsync();
		}
	}
}
