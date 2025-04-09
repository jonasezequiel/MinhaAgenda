using static AndroidX.Core.App.ComponentActivity;

namespace MinhaAgenda.Views.Controls;

public partial class ObservacoesControl : ContentView
{
    // Declare os eventos como nullable
    public event EventHandler<string>? OnError;
    public event EventHandler<EventArgs>? OnSave;
    public event EventHandler<EventArgs>? OnCancel;

    public ObservacoesControl()
    {
        InitializeComponent();
        entryData.Date = DateTime.Now;
    }

    public string ContactName
    {
        set => lblContactName.Text = value;
    }

    public string Observacao
    {
        get => entryObservacao.Text;
        set => entryObservacao.Text = value;
    }

    public DateTime Data
    {
        get => entryData.Date;
        set => entryData.Date = value;
    }

    private void btnSave_Clicked(object sender, EventArgs e)
    {
        if (obsValidator.IsNotValid)
        {
            OnError?.Invoke(sender, "A observação deve ter pelo menos 5 caracteres");
            return;
        }

        OnSave?.Invoke(sender, e);
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        OnCancel?.Invoke(sender, e);
    }
}