using EISG20240905.DTOs.ProductEISGDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace EISG20240905.AppWebMVC.Controllers
{
	public class ProductEISGController : Controller
	{
		private readonly HttpClient _httpClientEISG20240905API;

		// Constructor que recibe una instancia de IHttpClientFactory para crear el cliente HTTP
		public ProductEISGController(IHttpClientFactory httpClientFactory)
		{
			_httpClientEISG20240905API = httpClientFactory.CreateClient("EISG20240905API");
		}

		// Método para mostrar la lista de productos
		public async Task<ActionResult> Index(SearchQueryProductEISGDTO searchQueryProductEISGDTO, int CountRow = 0)
		{
			// Configurar valores por defecto
			if (searchQueryProductEISGDTO.SendRowCount == 0)
				searchQueryProductEISGDTO.SendRowCount = 2;
			if (searchQueryProductEISGDTO.Take == 0)
				searchQueryProductEISGDTO.Take = 10;

			var result = new SearchResultProductEISGDTO();

			// Hacer solicitud HTTP POST
			var response = await _httpClientEISG20240905API.PostAsJsonAsync("/product/search", searchQueryProductEISGDTO);

			if (response.IsSuccessStatusCode)
				result = await response.Content.ReadFromJsonAsync<SearchResultProductEISGDTO>();

			// Si result es nulo, inicializar un nuevo objeto
			result = result ?? new SearchResultProductEISGDTO();
			result.Data = result.Data ?? new List<SearchResultProductEISGDTO.ProductEISGDTO>(); // Verifica que Data no sea nulo

			// Configurar valores para la vista
			if (result.CountRow == 0 && searchQueryProductEISGDTO.SendRowCount == 1)
				result.CountRow = CountRow;

			ViewBag.CountRow = result.CountRow;
			searchQueryProductEISGDTO.SendRowCount = 0;
			ViewBag.SearchQuery = searchQueryProductEISGDTO;

			return View(result);
		}

		// Método para mostrar los detalles de un prducto
		public async Task<ActionResult> Details(int id)
		{
			var result = new GetIdResultProductEISGDTO();

			// Realizar una solicitud HTTP GET para obtener los detalles del cliente por ID
			var response = await _httpClientEISG20240905API.GetAsync("/product/" + id);

			if (response.IsSuccessStatusCode)
				result = await response.Content.ReadFromJsonAsync<GetIdResultProductEISGDTO>();

			return View(result ?? new GetIdResultProductEISGDTO());
		}

		// GET: ProductEISGController/Create
		// Método para mostrar el formulario de creación de un producto
		public ActionResult Create()
		{
			return View();
		}

		// POST: ProductEISGController/Create
		// Método para procesar la creación de un producto
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(CreateProductEISGDTO createProductEISGDTO)
		{

			try
			{
				//Realizar una solicitud HTTP POST para crear un nuevo producto
				var response = await _httpClientEISG20240905API.PostAsJsonAsync("/product", createProductEISGDTO);

				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction(nameof(Index));
				}

				ViewBag.Error = "Error al intentar guardar el registro";
				return View();
			}
			catch (Exception ex)
			{
				ViewBag.Error = ex.Message;
				return View();
			}
		}

		// Método para mostrar el formulario de edición de un producto
		// GET: ProductEISGController/Edit/5
		public async Task<ActionResult> Edit(int id)
		{
			var result = new GetIdResultProductEISGDTO();
			var response = await _httpClientEISG20240905API.GetAsync("/product/" + id);

			if (response.IsSuccessStatusCode)
				result = await response.Content.ReadFromJsonAsync<GetIdResultProductEISGDTO>();

			return View(new EditProdcutEISGDTO(result ?? new GetIdResultProductEISGDTO()));
		}

		// POST: ProductEISGController/Edit/5

		// Método para procesar la edición de un producto
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(int id, EditProdcutEISGDTO editProdcutEISGDTO)
		{
			try
			{ // Realizar una solicitud HTTP PUT para editar el producto
				var response = await _httpClientEISG20240905API.PutAsJsonAsync("/product", editProdcutEISGDTO);

				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction(nameof(Index));
				}

				ViewBag.Error = "Error al intentar editar el registro";
				return View();
			}
			catch (Exception ex)
			{
				ViewBag.Error = ex.Message;
				return View();
			}

		}

		// GET: ProductEISGController/Delete/5
		// Método para mostrar la página de confirmación de eliminación de un producto
		public async Task<ActionResult> Delete(int id)
		{
			var result = new GetIdResultProductEISGDTO();
			var response = await _httpClientEISG20240905API.GetAsync("/product/" + id);

			if (response.IsSuccessStatusCode)
				result = await response.Content.ReadFromJsonAsync<GetIdResultProductEISGDTO>();

			return View(result ?? new GetIdResultProductEISGDTO());
		}

		// POST: ProductEISGController/Delete/5
		// Método para procesar la eliminación de un producto
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id, GetIdResultProductEISGDTO getIdResultProductEISGDTOa)
		{
			try
			{
				// Realizar una solicitud HTTP DELETE para eliminar el cliente por ID
				var response = await _httpClientEISG20240905API.DeleteAsync("/product/" + id);

				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction(nameof(Index));
				}

				ViewBag.Error = "Error al intentar eliminar el registro";
				return View(getIdResultProductEISGDTOa);
			}
			catch (Exception ex)
			{
				ViewBag.Error = ex.Message;
				return View(getIdResultProductEISGDTOa);
			}
		}
	}
}
