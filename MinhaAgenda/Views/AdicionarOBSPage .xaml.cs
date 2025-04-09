using CasosDeUso.Interface;
using CoreBusiness.Entidades;
using MinhaAgenda.Views.Controls;

namespace MinhaAgenda.Views;

[QueryProperty(nameof(ContatoId), "Id")]
public partial class AdicionarOBSPage : ContentPage
{
    private readonly IAdicionarObservacaoUseCase _adicionarObservacaoUseCase;
    private readonly IVisualizarContatosUseCase _visualizarContatosUseCase;

    public string ContatoId { get; set; } = string.Empty;

    public AdicionarOBSPage(IAdicionarObservacaoUseCase adicionarObservacaoUseCase,
                                 IVisualizarContatosUseCase visualizarContatosUseCase)
    {
        InitializeComponent();
        _adicionarObservacaoUseCase = adicionarObservacaoUseCase;
        _visualizarContatosUseCase = visualizarContatosUseCase;

        // Verifique se observacoesCtrl não é nulo antes de atribuir os eventos
        if (observacoesCtrl != null)
        {
            observacoesCtrl.OnSave += OnSaveHandler;
            observacoesCtrl.OnCancel += OnCancelHandler;
            observacoesCtrl.OnError += OnErrorHandler;
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarContato();
    }

    private async Task CarregarContato()
    {
        if (!string.IsNullOrEmpty(ContatoId) && Guid.TryParse(ContatoId, out var contatoId))
        {
            var contato = await _visualizarContatosUseCase.ExecutaAsync(contatoId);
            if (contato != null && observacoesCtrl != null)
            {
                observacoesCtrl.ContactName = contato.Nome;
            }
        }
    }

    private async void OnSaveHandler(object? sender, EventArgs e)
    {
        if (observacoesCtrl == null || string.IsNullOrEmpty(ContatoId)) return;

        var observacao = new Observacao
        {
            ContatoId = Guid.Parse(ContatoId),
            Texto = observacoesCtrl.Observacao,
            Data = observacoesCtrl.Data
        };

        await _adicionarObservacaoUseCase.ExecutaAsync(observacao);
        await Shell.Current.GoToAsync($"//{nameof(ContatosPage)}");
    }

    private async void OnCancelHandler(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(ContatosPage)}");
    }

    private void OnErrorHandler(object? sender, string e)
    {
        DisplayAlert("Erro", e, "Ok");
    }
}