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
    public class CreateCatalogOrSubcatalogViewModel : ViewModelBase, INotifyPropertyChanged
	{
	    private ObservableCollection<string> _arrCatalogsAndSubcatalog;
	    public ObservableCollection<string> ArrCatalogsAndSubcatalog
	    {
		    set
		    {
			    _arrCatalogsAndSubcatalog = value;
		    }
		    get { return _arrCatalogsAndSubcatalog; }
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

		public CreateCatalogOrSubcatalogViewModel()
	    {
		    ArrCatalogsAndSubcatalog = new ObservableCollection<string>();
		    string CatalogPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\Catalog\\";
		    List<string> dirs = Directory.GetDirectories(CatalogPath).ToList();

		    foreach (var VARIABLE in dirs)
		    {
			    System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(VARIABLE);
			    System.IO.DirectoryInfo[] subDirs = info.GetDirectories();


				string[] split = VARIABLE.Split(new Char[] { '\\' });
				ArrCatalogsAndSubcatalog.Add(split[split.Length - 1]);
			

		    }
	    }

	    private ICommand _okCommand;
	    public ICommand OkCommand
	    {
		    get => _okCommand ?? (_okCommand = new RelayCommand(() =>
		    {
			    Messenger.Default.Send("CreateCatalogOrSubcatalogViewModel" + "-" + SelectedCatalog + "-" + NameCatalog);
			    Messenger.Default.Send("CloseWindowCreateCatalogOrSubcatalogViewModel");
		    }));

	    }

	    private ICommand _cancelCommand;
	    public ICommand CancelCommand
	    {
		    get => _cancelCommand ?? (_cancelCommand = new RelayCommand(() =>
		    {
			    Messenger.Default.Send("CloseWindowCreateCatalogOrSubcatalogViewModel");
		    }));
	    }
	}
}
