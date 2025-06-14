using System.Net.Http.Json;

namespace cliente.Services;

public class ApiService {
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient) {
        _httpClient = httpClient;
    }

    // Obtener productos
    public async Task<List<ProductoDto>?> ObtenerProductosAsync(string? q = null) {
        var url = "/productos" + (string.IsNullOrWhiteSpace(q) ? "" : $"?q={q}");
        return await _httpClient.GetFromJsonAsync<List<ProductoDto>>(url);
    }

    // Carrito
    public async Task<CarritoDto?> CrearCarritoAsync() =>
        await _httpClient.PostAsJsonAsync("/carritos", new { }).ContinueWith(t => t.Result.Content.ReadFromJsonAsync<CarritoDto>()).Unwrap();

    public async Task<CarritoDto?> ObtenerCarritoAsync(Guid carritoId) =>
        await _httpClient.GetFromJsonAsync<CarritoDto>($"/carritos/{carritoId}");

    public async Task<CarritoDto?> VaciarCarritoAsync(Guid carritoId) =>
        await _httpClient.DeleteAsync($"/carritos/{carritoId}").ContinueWith(t => t.Result.Content.ReadFromJsonAsync<CarritoDto>()).Unwrap();

    public async Task<CarritoDto?> AgregarProductoAsync(Guid carritoId, int productoId, int cantidad) =>
        await _httpClient.PutAsJsonAsync($"/carritos/{carritoId}/{productoId}", cantidad).ContinueWith(t => t.Result.Content.ReadFromJsonAsync<CarritoDto>()).Unwrap();

    public async Task<CarritoDto?> QuitarProductoAsync(Guid carritoId, int productoId) =>
        await _httpClient.DeleteAsync($"/carritos/{carritoId}/{productoId}").ContinueWith(t => t.Result.Content.ReadFromJsonAsync<CarritoDto>()).Unwrap();

    public async Task<CompraDto?> ConfirmarCompraAsync(Guid carritoId, CompraDto compra) =>
        await _httpClient.PutAsJsonAsync($"/carritos/{carritoId}/confirmar", compra).ContinueWith(t => t.Result.Content.ReadFromJsonAsync<CompraDto>()).Unwrap();

    // ...método de ejemplo existente...
    public async Task<DatosRespuesta> ObtenerDatosAsync() {
        try {
            var response = await _httpClient.GetFromJsonAsync<DatosRespuesta>("/api/datos");
            return response ?? new DatosRespuesta { Mensaje = "No se recibieron datos del servidor", Fecha = DateTime.Now };
        } catch (Exception ex) {
            Console.WriteLine($"Error al obtener datos: {ex.Message}");
            return new DatosRespuesta { Mensaje = $"Error: {ex.Message}", Fecha = DateTime.Now };
        }
    }
}

// DTOs para comunicación con la API
public class ProductoDto {
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string ImagenUrl { get; set; } = string.Empty;
}
public class CarritoDto {
    public Guid Id { get; set; }
    public List<ItemCarritoDto> Items { get; set; } = new();
}
public class ItemCarritoDto {
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}
public class CompraDto {
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public string ApellidoCliente { get; set; } = string.Empty;
    public string EmailCliente { get; set; } = string.Empty;
    public List<ItemCompraDto> Items { get; set; } = new();
}
public class ItemCompraDto {
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public int CompraId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
public class DatosRespuesta {
    public string Mensaje { get; set; }
    public DateTime Fecha { get; set; }
}
