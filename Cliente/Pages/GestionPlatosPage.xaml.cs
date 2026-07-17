using Cliente.ConexionApi;
using Cliente.Models;

namespace Cliente.Pages;

[QueryProperty(nameof(plato), "Plato")]
public partial class GestionPlatosPage : ContentPage
{
    private readonly IRestConexionApi restConexionApi;
	private Plato _plato;
	public Plato plato {  get => _plato;
        set
        {
            _plato = value;
            _esNuevo = esNuevo(value);
            OnPropertyChanged(nameof(plato));
        }
    }
    private bool _esNuevo;

    public GestionPlatosPage(IRestConexionApi restConexionApi)
	{
		InitializeComponent();
		this.restConexionApi = restConexionApi;
		BindingContext = this;
	}
	private bool esNuevo(Plato plato) {
		if (plato.Id == 0)
			return true;
		return false;
	}
	async void OnCancelarClic(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
    async void OnGuardarClic(object sender, EventArgs e)
    {
        if (_esNuevo)
        {
            await restConexionApi.CrearPlatoAsync(_plato);
        }
        else
        {
            await restConexionApi.ActualizarPlatoAsync(_plato);
        }
        await Shell.Current.GoToAsync("..");
    }
    async void OnEliminarClic(object sender, EventArgs e)
    {
        if (!_esNuevo)
        {
            await restConexionApi.EliminarPlatoAsync(_plato.Id);
        }
        await Shell.Current.GoToAsync("..");
    }
}