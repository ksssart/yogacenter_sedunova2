using Avalonia.Controls;
using Avalonia.Interactivity;

namespace yogacenter_sedunova2;

public partial class MainWindow : Window
{
    private void Autorization(object? sender, RoutedEventArgs e)
    {
        if (login.Text == "admin" && password.Text == "admin")
        {
            var adm = new yogaForm();
            adm.Show();
            this.Hide();
            
        }
        else
        {
            var adm = new yogaForm();
            adm.Show();
            this.Hide();
        }
    }

    public MainWindow()
    {
        InitializeComponent();
    }
    
    
}