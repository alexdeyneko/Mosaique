using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using wpfMozaiq.Models;

namespace wpfMozaiq.ViewModel
{
	class ChoiseCatalogDialogViewModel : ViewModelBase
	{

		private ObservableCollection<string> _arrCatalogs;
		public ObservableCollection<string> ArrCatalogs
		{
			set
			{
				_arrCatalogs = value;
			}
			get { return _arrCatalogs; }
		}


		private string _selectedCatalog;
		public string SelectedCatalog
		{
			set
			{
				_selectedCatalog = value;
			}
			get { return _selectedCatalog; }
		}
		
		public ChoiseCatalogDialogViewModel()
		{
			ArrCatalogs = new ObservableCollection<string>();
			string CatalogPath =Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))+"\\Catalog\\";
			List<string> dirs = Directory.GetDirectories(CatalogPath).ToList();
			foreach (var VARIABLE in dirs)
			{
				string[] split = VARIABLE.Split(new Char[] { '\\'});
				ArrCatalogs.Add(split[split.Length - 1]);
			}
		}

		private ICommand _okCommand;
		public ICommand OkCommand
		{
			get => _okCommand ?? (_okCommand = new RelayCommand(() =>
			{
				string[] split = SelectedCatalog.Split(new Char[] { '_' });
				Catalog catalog = new Catalog(split[split.Length - 2], Convert.ToInt32(split[split.Length - 1])
                    ,
                    Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
				Messenger.Default.Send(catalog);
				Messenger.Default.Send("CloseWindowChoiseCatalogDialogViewModel");
			}));

		}

		private ICommand _cancelCommand;
		public ICommand CancelCommand
		{
			get => _cancelCommand ?? (_cancelCommand = new RelayCommand(() =>
			{
				Messenger.Default.Send("CloseWindowChoiseCatalogDialogViewModel");
			}));
		}



	}
}
