using Cliente.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Cliente.ConexionApi
{
    public class RestConexionApi : IRestConexionApi
    {
        public readonly HttpClient httpClient;
        private readonly string dominio;
        private readonly string url;
        private readonly JsonSerializerOptions jsonSerializerOptions;
        public RestConexionApi()
        {
            httpClient = new HttpClient();
            //dominio = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7025" : "https://192.168.1.212:7025";
            dominio = DeviceInfo.Platform == DevicePlatform.Android ? "https://192.168.1.212:7025" : "https://192.168.1.212:7025";
            url = $"{dominio}/api/v1";
            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<List<Plato>> ObtenerPlatosAsync()
        {
            List<Plato> platos = new List<Plato>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) { 
                Debug.WriteLine("No hay conexión a Internet");
                return platos;
            }
            try {
                HttpResponseMessage respuesta = await httpClient.GetAsync($"{url}/plato");
                if(respuesta.IsSuccessStatusCode)
                {
                    string contenido = await respuesta.Content.ReadAsStringAsync();
                    platos = JsonSerializer.Deserialize<List<Plato>>(contenido, jsonSerializerOptions);
                }else
                {
                    Debug.WriteLine($"Error al obtener los platos: {respuesta.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener los platos: {ex.Message}");
            }
            return platos;
        }

        public async Task CrearPlatoAsync(Plato plato)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No hay conexión a Internet");
                return;
            }
            try
            {
                string platoSer = JsonSerializer.Serialize(plato, jsonSerializerOptions);
                StringContent contenido = new StringContent(platoSer, Encoding.UTF8, "application/json");
                HttpResponseMessage respuesta = await httpClient.PostAsync($"{url}/plato", contenido);
                if (respuesta.IsSuccessStatusCode)
                    Debug.WriteLine("Plato creado correctamente");
                else
                    Debug.WriteLine($"Error al crear el plato: {respuesta.StatusCode}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al crear el plato: {ex.Message}");
            }
            return;
        }
        public async Task ActualizarPlatoAsync(Plato plato)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No hay conexión a Internet");
                return;
            }
            try
            {
                string platoSer = JsonSerializer.Serialize(plato, jsonSerializerOptions);
                StringContent contenido = new StringContent(platoSer, Encoding.UTF8, "application/json");
                HttpResponseMessage respuesta = await httpClient.PutAsync($"{url}/plato/{plato.Id}", contenido);
                if (respuesta.IsSuccessStatusCode)
                    Debug.WriteLine("Plato actualizado correctamente");
                else
                    Debug.WriteLine($"Error al actualizar el plato: {respuesta.StatusCode}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al actualizar el plato: {ex.Message}");
            }
            return;
        }


        public async Task EliminarPlatoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No hay conexión a Internet");
                return;
            }
            try
            {
                HttpResponseMessage respuesta = await httpClient.DeleteAsync($"{url}/plato/{id}");
                if (respuesta.IsSuccessStatusCode)
                    Debug.WriteLine("Plato eliminado correctamente");
                else
                    Debug.WriteLine($"Error al eliminar el plato: {respuesta.StatusCode}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar el plato: {ex.Message}");
            }
            return;
        }
    }
}
