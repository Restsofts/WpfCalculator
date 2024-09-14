using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace WpfCalculator
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string _displayText = "";
        public string DisplayText
        {
            get => _displayText;
            set
            {
                _displayText = value;
                OnPropertyChanged();
            }
        }

        public ICommand NumberCommand { get; }
        public ICommand OperationCommand { get; }
        public ICommand CalculateCommand { get; }

        public CalculatorViewModel()
        {
            NumberCommand = new CustomCommand(ExecuteNumber);
            OperationCommand = new CustomCommand(ExecuteOperation);
            CalculateCommand = new CustomCommand(ExecuteCalculate);
        }

        private void ExecuteNumber(object parameter)
        {
            DisplayText += parameter.ToString();
        }

        private void ExecuteOperation(object parameter)
        {
            DisplayText += " " + parameter.ToString() + " ";
        }

        private void ExecuteCalculate(object parameter)
        {
            try
            {
                var result = new DataTable().Compute(DisplayText, null);
                DisplayText = result.ToString();
            }
            catch (Exception ex)
            {
                // تخصيص رسالة الخطأ للمستخدم
                MessageBox.Show("حدث خطأ أثناء الحساب: " + ex.Message);
                DisplayText = "خطأ";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
