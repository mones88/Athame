using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AthameWPF.Annotations;
using AthameWPF.UI.Windows;

namespace AthameWPF.UI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            AddCommand = new ViewModelCommand(AddCommand_Execute);
            SettingsCommand = new ViewModelCommand(SettingsCommand_Execute);
        }

        private void SettingsCommand_Execute(object o)
        {
            new SettingsWindow().ShowDialog();
        }

        public ViewModelCommand AddCommand { get; }
        public ViewModelCommand SettingsCommand { get; set; }

        private void AddCommand_Execute(object o)
        {
            new SearchWindow().ShowDialog();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
