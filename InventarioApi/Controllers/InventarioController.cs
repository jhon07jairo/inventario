using InventarioApi.Data;
using InventarioApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioApi.Controllers
{
    #region Verdadera

    [Route("api/inventario")]
    [ApiController]
    //[Authorize] // Protege todos los endpoints con JWT
    public class InventarioController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        //public InventarioController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        // Obtener todos los productos
        [HttpGet("productos")]
        public async Task<ActionResult<List<Producto>>> GetProductos()
        {
            return Ok(await _context.Productos.ToListAsync());
        }

        // Obtener un producto por ID
        [HttpGet("productos/{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto is null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            return Ok(producto);
        }

        // Crear un nuevo producto
        [HttpPost("productos")]
        public async Task<ActionResult<Producto>> CreateProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre) || producto.Stock < 0)
                return BadRequest(new { mensaje = "Datos inválidos" });

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        // Actualizar un producto
        [HttpPut("productos/{id}")]
        public async Task<IActionResult> UpdateProducto(int id, Producto producto)
        {
            if (id != producto.Id)
                return BadRequest(new { mensaje = "ID del producto no coincide" });

            var existingProduct = await _context.Productos.FindAsync(id);
            if (existingProduct is null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            existingProduct.Nombre = producto.Nombre;
            existingProduct.Stock = producto.Stock;
            existingProduct.FechaCreacion = producto.FechaCreacion;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Eliminar un producto
        [HttpDelete("productos/{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto is null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Registrar una entrada o salida de inventario
        [HttpPost("movimientos")]
        public async Task<ActionResult<MovimientoInventario>> RegistrarMovimiento(MovimientoInventario movimiento)
        {
            var producto = await _context.Productos.FindAsync(movimiento.ProductoId);
            if (producto is null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            if (movimiento.TipoMovimiento != "Entrada" && movimiento.TipoMovimiento != "Salida")
                return BadRequest(new { mensaje = "Tipo de movimiento inválido" });

            if (movimiento.TipoMovimiento == "Salida" && producto.Stock < movimiento.Cantidad)
                return BadRequest(new { mensaje = "Stock insuficiente" });

            // Ajustar el stock del producto
            if (movimiento.TipoMovimiento == "Entrada")
                producto.Stock += movimiento.Cantidad;
            else
                producto.Stock -= movimiento.Cantidad;

            _context.MovimientosInventario.Add(movimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovimiento), new { id = movimiento.Id }, movimiento);
        }

        // Obtener todos los movimientos de inventario
        [HttpGet("movimientos")]
        public async Task<ActionResult<List<MovimientoInventario>>> GetMovimientos()
        {
            return await _context.MovimientosInventario.Include(m => m.Producto).ToListAsync();
        }

        // Obtener un movimiento por ID
        [HttpGet("movimientos/{id}")]
        public async Task<ActionResult<MovimientoInventario>> GetMovimiento(int id)
        {
            var movimiento = await _context.MovimientosInventario.Include(m => m.Producto)
                                                                 .FirstOrDefaultAsync(m => m.Id == id);
            if (movimiento is null)
                return NotFound(new { mensaje = "Movimiento no encontrado" });

            return movimiento;
        }

        // Eliminar un movimiento (solo si es necesario)
        [HttpDelete("movimientos/{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            var movimiento = await _context.MovimientosInventario.FindAsync(id);
            if (movimiento is null)
                return NotFound(new { mensaje = "Movimiento no encontrado" });

            _context.MovimientosInventario.Remove(movimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    #endregion

    #region Prueba
    //[Route("api/inventario")]
    //[ApiController]
    //public class InventarioController : ControllerBase
    //{
    //    private static List<Producto> _productos = new List<Producto>
    //    {
    //        new Producto { Id = 1, Nombre = "Laptop Dell", Stock = 10 },
    //        new Producto { Id = 2, Nombre = "Monitor Samsung", Stock = 5 }
    //    };

    //    private static List<MovimientoInventario> _movimientos = new List<MovimientoInventario>();

    //    // 📌 Obtener todos los productos
    //    [HttpGet("productos")]
    //    public ActionResult<List<Producto>> GetProductos()
    //    {
    //        return Ok(_productos);
    //    }

    //    // 📌 Obtener un producto por ID
    //    [HttpGet("productos/{id}")]
    //    public ActionResult<Producto> GetProducto(int id)
    //    {
    //        var producto = _productos.FirstOrDefault(p => p.Id == id);
    //        if (producto == null)
    //            return NotFound(new { mensaje = "Producto no encontrado" });

    //        return Ok(producto);
    //    }

    //    // 📌 Crear un nuevo producto
    //    [HttpPost("productos")]
    //    public ActionResult<Producto> CreateProducto(Producto producto)
    //    {
    //        if (string.IsNullOrWhiteSpace(producto.Nombre) || producto.Stock < 0)
    //            return BadRequest(new { mensaje = "Datos inválidos" });

    //        producto.Id = _productos.Count + 1;
    //        _productos.Add(producto);

    //        return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
    //    }

    //    // 📌 Registrar una entrada o salida de inventario
    //    [HttpPost("movimientos")]
    //    public ActionResult<MovimientoInventario> RegistrarMovimiento(MovimientoInventario movimiento)
    //    {
    //        var producto = _productos.FirstOrDefault(p => p.Id == movimiento.ProductoId);
    //        if (producto == null)
    //            return NotFound(new { mensaje = "Producto no encontrado" });

    //        if (movimiento.TipoMovimiento != "Entrada" && movimiento.TipoMovimiento != "Salida")
    //            return BadRequest(new { mensaje = "Tipo de movimiento inválido" });

    //        if (movimiento.TipoMovimiento == "Salida" && producto.Stock < movimiento.Cantidad)
    //            return BadRequest(new { mensaje = "Stock insuficiente" });

    //        // Ajustar el stock del producto
    //        if (movimiento.TipoMovimiento == "Entrada")
    //            producto.Stock += movimiento.Cantidad;
    //        else
    //            producto.Stock -= movimiento.Cantidad;

    //        movimiento.Id = _movimientos.Count + 1;
    //        movimiento.Fecha = DateTime.UtcNow;
    //        _movimientos.Add(movimiento);

    //        return CreatedAtAction(nameof(GetMovimiento), new { id = movimiento.Id }, movimiento);
    //    }

    //    // 📌 Obtener todos los movimientos de inventario
    //    [HttpGet("movimientos")]
    //    public ActionResult<IEnumerable<MovimientoInventario>> GetMovimientos()
    //    {
    //        return Ok(_movimientos);
    //    }

    //    // 📌 Obtener un movimiento por ID
    //    [HttpGet("movimientos/{id}")]
    //    public ActionResult<MovimientoInventario> GetMovimiento(int id)
    //    {
    //        var movimiento = _movimientos.FirstOrDefault(m => m.Id == id);
    //        if (movimiento == null)
    //            return NotFound(new { mensaje = "Movimiento no encontrado" });

    //        return Ok(movimiento);
    //    }
    //}

    //// 📌 Definición de clases dentro del mismo archivo (Solo para prueba)
    //public class Producto
    //{
    //    public int Id { get; set; }
    //    public string Nombre { get; set; }
    //    public int Stock { get; set; }
    //}

    //public class MovimientoInventario
    //{
    //    public int Id { get; set; }
    //    public int ProductoId { get; set; }
    //    public int Cantidad { get; set; }
    //    public string TipoMovimiento { get; set; } // "Entrada" o "Salida"
    //    public DateTime Fecha { get; set; }
    //}
    #endregion
}

