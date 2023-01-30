using Calculadora.Models;
using Calculadora.Utils;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculadora.ViewModels
{
    class MainPageViewModel : BaseViewModel
    {
        private readonly ICalculator _calculator;
        private string _numberText;
        private string _previousNumberText;
        private string _operationText;
        private bool _clearTextOnNextPress;

        public ICommand NumberCommand { get; }
        public ICommand ClearLastCommand { get; }
        public ICommand ClearAllCommand { get; }
        public ICommand OperationCommand { get; }
        public ICommand CalculateCommand { get; }


        public string OperationText
        {
            get
            {
                return _operationText;
            }
            private set
            {
                _operationText = value;
                NotifyPropertyChanged(nameof(OperationText));
                NotifyPropertyChanged(nameof(BottomInfoText));
            }
        }

        public string NumberText
        {
            get
            {
                return _numberText;
            }
            private set
            {
                _numberText = value;
                NotifyPropertyChanged(nameof(NumberText));
            }
        }

        public string PreviousNumberText
        {
            get
            {
                return _previousNumberText;
            }
            private set
            {
                _previousNumberText = value;
                NotifyPropertyChanged(nameof(PreviousNumberText));
                NotifyPropertyChanged(nameof(BottomInfoText));
            }
        }

        public string BottomInfoText
        {
            get
            {
                return $"{PreviousNumberText} {OperationText}";
            }
        }

        public MainPageViewModel()
        {
            NumberText = "";
            OperationText = "";

            NumberCommand = new Command(NumberButtonClick);
            ClearAllCommand = new Command(ClearAllButtonClick);
            ClearLastCommand = new Command(ClearLastButtonClick);
            OperationCommand = new Command(OperationClick);
            CalculateCommand = new Command(CalculateClick);
            _calculator = new Models.Calculator();
        }

        private bool NumberIsValid(string numberText)
        {
            // Verifica si la variable es una cadena vacía o contiene solo espacios en blanco, o si termina con un punto "."
            if (string.IsNullOrWhiteSpace(numberText) || numberText.EndsWith("."))
                return false;

            return true;
        }

        /// <summary>
        /// Este método limpia todos los valores de las variables.
        /// </summary>
        private void ClearAllButtonClick()
        {
            NumberText = "";
            OperationText = "";
            PreviousNumberText = "";
        }

        /// <summary>
        /// Este método remueve el último carácter en la cadena NumberText.
        /// </summary>
        private void ClearLastButtonClick() => 
            NumberText = NumberText.Length > 0 ? NumberText.Remove(NumberText.Length - 1) : NumberText;

        /// <summary>
        /// Este método agrega texto a la propiedad NumberText cuando se hace clic en un botón numérico.
        /// Si _clearTextOnNextPress es verdadero, se limpia NumberText.
        /// Se evita agregar un punto decimal si ya existe uno en NumberText.
        /// Se notifica a la propiedad de cambios de NumberText.
        /// </summary>
        /// <param name="sender"></param>
        private void NumberButtonClick(object sender)
        {
            // Convierte el objeto que fue pasado como un argumento a un objeto de tipo Button
            Button button = sender as Button;

            // Variable texto
            var text = button.Text;

            if (_clearTextOnNextPress)
            {
                _clearTextOnNextPress = false;
                NumberText = "";
            }

            NumberText = NumberText.StartsWith("0") ? "" : NumberText;
            NumberText += text == "." && NumberText.Contains(".") ? "" : text;
            NotifyPropertyChanged(nameof(NumberText));
        }

        private void OperationClick(object sender)
        {
            // Convierte el objeto que fue pasado como un argumento a un objeto de tipo Button
            Button button = sender as Button;

            // Variable temporal
            decimal tempNumber;

            if (decimal.TryParse(NumberText, out tempNumber))
            {
                _calculator.Number1 = tempNumber;

                _clearTextOnNextPress = true;
                OperationText = button.Text;
                PreviousNumberText = NumberText;
            }
            else if (string.IsNullOrWhiteSpace(NumberText) && button.Text == _calculator.SubstractSymbol) NumberText += "-";
        }


        private void CalculateClick()
        {
            // Variable temporal
            decimal tempNumber;

            // Verifica con NumberIsValid y el método TryParse intenta convertir "NumberText" a un número decimal
            if (NumberIsValid(NumberText) && decimal.TryParse(NumberText, out tempNumber))
            {
                _calculator.Number2 = tempNumber;
                NumberText = _calculator.Calculate(OperationText).ToString();
                OperationText = "";
                PreviousNumberText = "";
            }
            else
            {
                Alert.Display("Número inválido", "El número es demasiado grande o no es válido", "OK");
            }
        }
    }
}
