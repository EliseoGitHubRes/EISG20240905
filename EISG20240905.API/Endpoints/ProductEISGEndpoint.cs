using EISG20240905.API.Models.DAL;
using EISG20240905.API.Models.EN;
using EISG20240905.DTOs.ProductEISGDTOs;

namespace EISG20240905.API.Endpoints
{
    public static class ProductEISGEndpoint
    {
        // Método para configurar los endpoints relacionados con los productos
        public static void AddProductEISGEndpoints(this WebApplication app)
        {
            // Configurar un endpoint de tipo POST para buscar productos
            app.MapPost("/product/search", async (SearchQueryProductEISGDTO productEISGDTO, ProductEISGDAL productEISGDAL) =>
            {
                // Crear un objeto 'Product' a partir de los datos proporcionados
                var productEISG = new ProductEISG
                {
                    NombreEISG = productEISGDTO.NombreEISG_Like != null ? productEISGDTO.NombreEISG_Like : string.Empty,
                    Precio = productEISGDTO.Precio_Like != null ? productEISGDTO.Precio_Like.Value : 0
                };

                // Inicializar una lista de productos y una variable para contar las filas
                var productsEISG = new List<ProductEISG>();
                int countRow = 0;

                // Verificar si se debe enviar la cantidad de filas
                if (productEISGDTO.SendRowCount == 2)
                {
                    // Realizar una búsqueda de productos y contar las filas
                    productsEISG = await productEISGDAL.Search(productEISG, skip: productEISGDTO.Skip, take: productEISGDTO.Take);
                    if (productsEISG.Count > 0)
                        countRow = await productEISGDAL.CountSearch(productEISG);
                }
                else
                {
                    // Realizar una búsqueda de productos sin contar las filas
                    productsEISG = await productEISGDAL.Search(productEISG, skip: productEISGDTO.Skip, take: productEISGDTO.Take);
                }

                // Crear un objeto 'SearchResultProductDTO' para almacenar los resultados
                var productEISGResult = new SearchResultProductEISGDTO
                {
                    Data = new List<SearchResultProductEISGDTO.ProductEISGDTO>(),
                    CountRow = countRow
                };

                // Mapear los resultados a objetos 'ProductDTO' y agregarlos al resultado
                productsEISG.ForEach(s => {
                    productEISGResult.Data.Add(new SearchResultProductEISGDTO.ProductEISGDTO
                    {
                        Id = s.Id,
                        NombreEISG = s.NombreEISG,
                        DescripcionEISG = s.DescripcionEISG,
                        Precio = s.Precio
                    });
                });

                // Devolver los resultados
                return productEISGResult;
            });

            // Configurar un endpoint de tipo GET para obtener un producto por ID
            app.MapGet("/product/{id}", async (int id, ProductEISGDAL productEISGDAL) =>
            {
                // Obtener un producto por ID
                var productEISG = await productEISGDAL.GetById(id);

                // Crear un objeto 'GetIdResultProductEISGDTO' para almacenar el resultado
                var productEISGResult = new GetIdResultProductEISGDTO
                {
                    Id = productEISG.Id,
                    NombreEISG = productEISG.NombreEISG,
                    DescripcionEISG = productEISG.DescripcionEISG,
                    Precio = productEISG.Precio
                };

                // Verificar si se encontró el cliente y devolver la respuesta correspondiente
                if (productEISGResult.Id > 0)
                    return Results.Ok(productEISGResult);
                else
                    return Results.NotFound(productEISGResult);
            });

            // Configurar un endpoint de tipo POST para crear un nuevo producto
            app.MapPost("/product", async (CreateProductEISGDTO productEISGDTO, ProductEISGDAL productEISGDAL) =>
            {
                // Crear un objeto 'Product' a partir de los datos proporcionados
                var productEISG = new ProductEISG
                {
                    NombreEISG = productEISGDTO.NombreEISG,
                    DescripcionEISG = productEISGDTO.DescripcionEISG,
                    Precio = productEISGDTO.Precio
                };

                // Intentar crear el producto y devolver el resultado correspondiente
                int result = await productEISGDAL.Create(productEISG);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });

            // Configurar un endpoint de tipo PUT para editar un producto existente
            app.MapPut("/product", async (EditProdcutEISGDTO editProdcutEISGDTO, ProductEISGDAL productEISGDAL) =>
            {
                // Crear un objeto 'Customer' a partir de los datos proporcionados
                var productEISG = new ProductEISG
                {
                    Id = editProdcutEISGDTO.Id,
                    NombreEISG = editProdcutEISGDTO.NombreEISG,
                    DescripcionEISG = editProdcutEISGDTO.DescripcionEISG,
                    Precio = editProdcutEISGDTO.Precio
                };

                // Intentar editar el producto y devolver el resultado correspondiente
                int result = await productEISGDAL.Edit(productEISG);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });

            // Configurar un endpoint de tipo DELETE para eliminar un producto por ID
            app.MapDelete("/product/{id}", async (int id, ProductEISGDAL productEISGDAL) =>
            {
                // Intentar eliminar el producto y devolver el resultado correspondiente
                int result = await productEISGDAL.Delete(id);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });
        }
    }
}
