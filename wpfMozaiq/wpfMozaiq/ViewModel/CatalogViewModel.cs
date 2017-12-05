using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using wpfMozaiq.Models;
using wpfMozaiq.Models.Services;
using wpfMozaiq.Veiw;
using System.IO;

namespace wpfMozaiq.ViewModel
{
    public class CatalogViewModel : ViewModelBase, INotifyPropertyChanged
    {
	    private ChoiseCatalogAndSubcatalogView choiseCatalogDialogView;
	    private Catalog catalog;
	    private CatalogChangeService catalogChangeService;

		public CatalogViewModel()
	    {
		    Messenger.Default.Register<Catalog>(this, (newCatalog) =>
		    {
			    this.catalog = newCatalog;
			    MozaicsList = new ObservableCollection<Mozaic>(catalog.Mozaics);
			});

			Messenger.Default.Register<string>(this, (newMessage) =>
		    {
			    if (choiseCatalogDialogView != null && newMessage == "CloseWindowChoiseCatalogAndSubcatalogViewModel")
			    {
				    choiseCatalogDialogView.Close();
			    }
		    });

			Messenger.Default.Register<string>(this, (newMessage) =>
			{
				string[] split = newMessage.Split(new Char[] { '-' });
				if (split[0]== "ChoiseCatalogAndSubcatalogViewModel")
				{
					string[] split2 = split[1].Split(new Char[] { '\\' });
					//проверка на подкаталоги
					if (split2.Count() == 1)
					{
						catalogChangeService= new CatalogChangeService(split2[0], "");
					}
					else catalogChangeService = new CatalogChangeService(split2[0], split2[1]);
				

					string[] split3 = split2[0].Split(new Char[] { '_' });
					Catalog catalog = new Catalog(split3[split3.Length - 2], Convert.ToInt32(split3[split3.Length - 1])
						,
						Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));

					//проверка на подкаталоги
					if (split2.Count() == 1)
					{
						MozaicsList = new ObservableCollection<Mozaic>(catalog.Mozaics);
					}
					else
					{
						MozaicsList = new ObservableCollection<Mozaic>();
						foreach (Mozaic var in catalog.Mozaics)
						{
							if (var.SubCatalog == split2[1])
							{
								MozaicsList.Add(var);
							}
						}

					}				

				}
			});
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

	    private Mozaic _selectedMozaic;
		public Mozaic SelectedMozaic
	    {
		    set
		    {
			    _selectedMozaic = value;
			    RaisePropertyChanged(() => SelectedMozaic);
		    }
		    get
		    {
			    return _selectedMozaic;
		    }
	    }



		private ICommand _choiseFileMosaicPack;
	    public ICommand ChoiseFileMosaicPack
	    {
		    get => _choiseFileMosaicPack ?? (_choiseFileMosaicPack = new RelayCommand(() =>
		    {
			    if (choiseCatalogDialogView == null)
			    {
				    choiseCatalogDialogView = new ChoiseCatalogAndSubcatalogView();
			    }
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

		//MessengerInstance.Send<NotificationMessage<string>>(new NotificationMessage<string>(panno.Image.SourcePath, "SourceImageViewModel"));


	    private ICommand _showEditMozaicView;
	    public ICommand ShowEditMozaicView
		{
		    get => _showEditMozaicView ?? (_showEditMozaicView = new RelayCommand(() =>
		    {
			    EditMozaicView editMozaicView = new EditMozaicView();
			    MessengerInstance.Send<NotificationMessage<Mozaic>>(new NotificationMessage<Mozaic>(SelectedMozaic, "EditMozaicViewModel"));
				editMozaicView.Show();
		    }));
	    }


	    private ICommand _deleteMozaic;
	    public ICommand DeleteMozaic
	    {
		    get => _deleteMozaic ?? (_deleteMozaic = new RelayCommand(() =>
		    {
				if (catalogChangeService != null)
				{
					Mozaic deletedMozaic = SelectedMozaic;				
					MozaicsList.Remove(deletedMozaic);
					

 
					FileInfo fileImage = new FileInfo(deletedMozaic.FullPath);					
					fileImage.Refresh();
					//fileImage.Delete();

					catalogChangeService.DeleteMozaic(deletedMozaic.Name);
					
				}
			}));
	    }

	}
}
