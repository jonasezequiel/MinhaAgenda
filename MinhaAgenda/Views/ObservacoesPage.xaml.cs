using CasosDeUso.Interface;
using CoreBusiness.Entidades;
using System.Collections.ObjectModel;

namespace MinhaAgenda.Views;

[QueryProperty(nameof(ContatoId), "Id")]
public partial class ObservacoesPage : ContentPage
{
    private readonly IVisualizarObservacoesUseCase _visualizarObservacoesUseCase;
    private readonly IVisualizarContatosUseCase _visualizarContatosUseCase;
    private readonly IApagarObservacaoUseCase _apagarObservacaoUseCase;

    public ObservacoesPage(IVisualizarObservacoesUseCase visualizarObservacoesUseCase,
                         IVisualizarContatosUseCase visualizarContatosUseCase,
                         IApagarObservacaoUseCase apagarObservacaoUseCase)
    {
        InitializeComponent();
        _visualizarObservacoesUseCase = visualizarObservacoesUseCase;
        _visualizarContatosUseCase = visualizarContatosUseCase;
        _apagarObservacaoUseCase = apagarObservacaoUseCase;
    }

    public string ContatoId { get; set; }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var contato = await _visualizarContatosUseCase.ExecutaAsync(Guid.Parse(ContatoId));
        lblContactName.Text = contato?.Nome ?? "Observações";

        await CarregarObservacoes();
    }

    private async Task CarregarObservacoes()
    {
        var observacoes = new ObservableCollection<Observacao>(
            await _visualizarObservacoesUseCase.ExecutaListAsync(Guid.Parse(ContatoId)));

        listaObservacoes.ItemsSource = observacoes;
    }

    private async void listaObservacoes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (listaObservacoes.SelectedItem != null)
        {
            var observacao = (Observacao)listaObservacoes.SelectedItem;
            await Shell.Current.GoToAsync($"{nameof(EditarObservacaoPage)}?Id={observacao.Id}");
        }
    }

    private void listaObservacoes_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listaObservacoes.SelectedItem = null;
    }

    private async void btnAdicionar_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(AdicionarObservacaoPage)}?Id={ContatoId}");
    }
}