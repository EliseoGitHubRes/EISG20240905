using EISG20240905.DTOs.ProductEISGDTOs;

namespace EISG20240905.AppWebBlazor.Data
{
	public class ProductEISGService
	{
		readonly HttpClient _httpClientEISG20240905API;

		// Constructor que recibe una instancia de IHttpClientFactory para crear el cliente HTTP
		public ProductEISGService(IHttpClientFactory httpClientFactory)
		{
			_httpClientEISG20240905API = httpClientFactory.CreateClient("EISG20240905API");
		}

		// Método para buscar productos utilizando una solicitud HTTP POST
		public async Task<SearchResultProductEISGDTO> Search(SearchQueryProductEISGDTO searchQueryProductEISGDTO)
		{
			var response = await _httpClientEISG20240905API.PostAsJsonAsync("/product/search", searchQueryProductEISGDTO);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<SearchResultProductEISGDTO>();
				return result ?? new SearchResultProductEISGDTO();
			}
			return new SearchResultProductEISGDTO(); // Devolver un objeto vacío en caso de error o respuesta no exitosa
		}

		// Método para obtener un producto por su ID utilizando una solicitud HTTP GET
		public async Task<GetIdResultProductEISGDTO> GetById(int id)
		{
			var response = await _httpClientEISG20240905API.GetAsync("/product/" + id);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<GetIdResultProductEISGDTO>();
				return result ?? new GetIdResultProductEISGDTO();
			}
			return new GetIdResultProductEISGDTO(); // Devolver un objeto vacío en caso de error o respuesta no exitosa
		}

		// Método para crear un nuevo Producto utilizando una solicitud HTTP POST
		public async Task<int> Create(CreateProductEISGDTO createProductEISGDTO)
		{
			int result = 0;
			var response = await _httpClientEISG20240905API.PostAsJsonAsync("/product", createProductEISGDTO);
			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				if (int.TryParse(responseBody, out result) == false)
					result = 0;
			}
			return result;
		}

		// Método para editar un producto existente utilizando una solicitud HTTP PUT
		public async Task<int> Edit(EditProdcutEISGDTO editProdcutEISGDTO)
		{
			int result = 0;
			var response = await _httpClientEISG20240905API.PutAsJsonAsync("/product", editProdcutEISGDTO);
			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				if (int.TryParse(responseBody, out result) == false)
					result = 0;
			}
			return result;
		}

		// Método para eliminar un producto por su ID utilizando una solicitud HTTP DELETE
		public async Task<int> Delete(int id)
		{
			int result = 0;
			var response = await _httpClientEISG20240905API.DeleteAsync("/product/" + id);
			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				if (int.TryParse(responseBody, out result) == false)
					result = 0;
			}
			return result;
		}
	}
}
