using Cliente.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cliente.ConexionApi
{
    public interface IRestConexionApi
    {
        Task<List<Plato>> ObtenerPlatosAsync();
        Task CrearPlatoAsync(Plato plato);
        Task ActualizarPlatoAsync(Plato plato);
        Task EliminarPlatoAsync(int id);
    }
}
