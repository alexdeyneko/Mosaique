using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using wpfMozaiq.Veiw;

namespace wpfMozaiq.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private ICommand _exit;
        public ICommand Exit
        {
            get => _exit ?? (_exit = new RelayCommand(() =>
            {
                Environment.Exit(0);
            }));
        }

        private ICommand _showNewProjectView;
        public ICommand ShowNewProjectView
        {
            get => _showNewProjectView ?? (_showNewProjectView = new RelayCommand(() =>
            {
                NewProjectView newProjectView= new NewProjectView();
                newProjectView.Show();
            }));
        }
    }
}
