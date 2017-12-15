using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace wpfMozaiq.ViewModel
{
    public class CreateCatalogViewModel : ViewModelBase, INotifyPropertyChanged
	{
		private string _nameCatalog;
		public string NameCatalog
		{
			set
			{
				_nameCatalog = value;
			}
			get { return _nameCatalog; }
		}

		private string _sizeMozaicInCatalog;
		public string SizeMozaicInCatalog
		{
			set
			{
				_sizeMozaicInCatalog = value;
			}
			get { return _sizeMozaicInCatalog; }
		}



		private ICommand _okCommand;
		public ICommand OkCommand
		{
			get => _okCommand ?? (_okCommand = new RelayCommand(() =>
			{
				Messenger.Default.Send("CreateCatalogViewModel" + "-" + NameCatalog + "_" + SizeMozaicInCatalog);
				Messenger.Default.Send("CloseWindowCreateCatalogViewModel");
			}));

		}

		private ICommand _cancelCommand;
		public ICommand CancelCommand
		{
			get => _cancelCommand ?? (_cancelCommand = new RelayCommand(() =>
			{
				Messenger.Default.Send("CloseWindowCreateCatalogViewModel");
			}));
		}
	}
}
