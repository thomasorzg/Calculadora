using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculadora.Utils
{
    class Alert
    {
        /// <summary>
        /// Método Display para asignar título, mensaje y botón.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="buttonOk"></param>
        /// <returns></returns>
        public static Task Display(string title, string message, string buttonOk)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, buttonOk);
        }
    }
}
