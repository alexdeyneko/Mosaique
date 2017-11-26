using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using wpfMozaiq.Models;
using wpfMozaiq.Veiw;

namespace wpfMozaiq.ViewModel
{
    public class CatalogViewModel : ViewModelBase
    {
	    private ChoiseCatalogDialogView choiseCatalogDialogView;
	    private Catalog catalog;

		public CatalogViewModel()
	    {
		    Messenger.Default.Register<Catalog>(this, (newCatalog) =>
		    {
			    this.catalog = newCatalog;
			    MozaicsList = new ObservableCollection<Mozaic>();
			    foreach (Mozaic var in  catalog.Mozaics)
			    {
					MozaicsList.Add(var);
			    }
			});

			Messenger.Default.Register<string>(this, (newMessage) =>
		    {
			    if (choiseCatalogDialogView != null && newMessage == "CloseWindowChoiseCatalogDialogViewModel")
			    {
				    choiseCatalogDialogView.Close();
			    }
		    });
		}

		private string _nameCatalog;
	    public string NameCatalog
	    {
		    set
		    {
			    _nameCatalog = value;
			    RaisePropertyChanged(() => NameCatalog);
		    }
		    get { return _nameCatalog; }
	    }

	    private ObservableCollection<Mozaic> _mozaicsList;
	    public ObservableCollection<Mozaic> MozaicsList
	    {
		    set
		    {
			    _mozaicsList = value;
			    RaisePropertyChanged(() => MozaicsList);
		    }
		    get
		    {
			    return _mozaicsList;
		    }
	    }

		private ICommand _choiseFileMosaicPack;
	    public ICommand ChoiseFileMosaicPack
	    {
		    get => _choiseFileMosaicPack ?? (_choiseFileMosaicPack = new RelayCommand(() =>
		    {
			    if (choiseCatalogDialogView == null)
			    {
				    choiseCatalogDialogView = new ChoiseCatalogDialogView();
			    }
			    choiseCatalogDialogView = new ChoiseCatalogDialogView();
			    choiseCatalogDialogView.Show();

		    }));
	    }

	    private ICommand _cancelCatalog;
	    public ICommand CanselCatalog
	    {
		    get => _cancelCatalog ?? (_cancelCatalog = new RelayCommand(() =>
		    {
			    catalog = null;
			    MozaicsList = null;
		    }));
	    }

	}
}
