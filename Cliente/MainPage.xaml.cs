using Cliente.ConexionApi;
using Cliente.Models;
using System.Diagnostics;

namespace Cliente
{
    public partial class MainPage : ContentPage
    {
        private readonly IRestConexionApi conexionApi;

        public MainPage(IRestConexionApi restConexionApi)
        {
            InitializeComponent();
            this.conexionApi = restConexionApi;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //coleccionPlatosView.ItemsSource = await conexionApi.ObtenerPlatosAsync();
            coleccionPlatosView.ItemsSource = new List<Plato>() { 
                new Plato(){Id=1,Nombre="Plato 1"},
                new Plato(){Id=2,Nombre="Plato 2"},
                new Plato(){Id=3,Nombre="Plato 3"}
            };
        }
        async void OnAddPlatoClic(object sender, EventArgs e) { 
            Debug.WriteLine("Botón de agregar plato presionado");
        }
        async void OnPlatoCambiado(object sender, SelectionChangedEventArgs e) {
            Debug.WriteLine("Botón de plato cambiado presionado");
        }
    }
}
