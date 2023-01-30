using Calculadora.Utils;
using Calculadora.ViewModels;
using System;
using System.Net;
using Xamarin.Forms;

namespace Calculadora
{
    public partial class MainPage : ContentPage
    {
        private readonly Author author;

        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new MainPageViewModel();

            this.author = new Author();
        }
    }
}
