using Avalonia.Controls;
using Avalonia;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tmds.DBus.Protocol;

namespace yogacenter_sedunova2;

public partial class yogaForm : Window
{
    private List<clients> _clients;
    private string connStr = "server=localhost;database=yogaCenter;port=3301;User Id=root;password=root;";
    private MySqlConnection conn;
    
    public yogaForm()
    {
        InitializeComponent();
        string table = "select*from clients";
        ShowData(table);
        Filter();
    }

    private void SearchName(object? sender, TextChangedEventArgs textChangedEventArgs)
    {
            var lev = _clients;
            lev = lev.Where(x => x.name.Contains(Search.Text)).ToList();
            ClientsGrid.ItemsSource = lev;
    }
    
    public void ShowData(string sql)
    {
        _clients = new List<clients>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CCClients = new clients
            {
                id = reader.GetInt32("id"),
                surname = reader.GetString("surname"),
                name = reader.GetString("name"),
                phone = reader.GetString("phone"),
                dateBirth = reader.GetString("dateBirth"),
                level = reader.GetString("level"),
                discount = reader.GetString("discount")
  
            };
            _clients.Add(CCClients);
        }
        conn.Close();
        ClientsGrid.ItemsSource = _clients;
    }
    
    private void Cmb_Level(object? sender, SelectionChangedEventArgs e)
    {
        var level = (ComboBox)sender;
        var CCClients = level.SelectedItem as clients;
        var filtrLevel = _clients.Where(x => x.level == CCClients.level).ToList();
        ClientsGrid.ItemsSource = filtrLevel;
    }
    public void Filter()
    {
        _clients = new List<clients>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("select * from clients", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CCClients = new clients()
            {
                id = reader.GetInt32("id"),
                surname = reader.GetString("surname"),
                name = reader.GetString("name"),
                phone = reader.GetString("phone"),
                dateBirth = reader.GetString("dateBirth"),
                level = reader.GetString("level"),
                discount = reader.GetString("discount")
            };
            _clients.Add(CCClients);
        }
        conn.Close();
        var cliCmb = this.Find<ComboBox>(name: "CmbLevel");
        cliCmb.ItemsSource = _clients;
    }
    
    private void OrderASC(object? sender, RoutedEventArgs routedEventArgs)
    {
       
    }
    private void OrderDESC(object? sender, RoutedEventArgs routedEventArgs)
    {
        var sortedSurname = ClientsGrid.ItemsSource.Cast<clients>().OrderByDescending(s => s.surname).ToList();
        ClientsGrid.ItemsSource = sortedSurname;
    }
    
    private void DeleteData(object? sender, RoutedEventArgs e)
    {
        try
        {
            clients ccclients = ClientsGrid.SelectedItem as clients;
            if (ccclients == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM clients WHERE id = " + ccclients.id;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            _clients.Remove(ccclients);
            ShowTable("SELECT id, surname, name, phone, dateBirth, level, discount FROM clients;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ShowTable(string selectIdSurnameNamePhoneDatebirthLevelDiscountFromClients)
    {
        throw new NotImplementedException();
    }
}