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
	    private MozaicDialogView mozaicDialogView;
	    private CreateCatalogOrSubcatalog createCatalogOrSubcatalogView;
	    private CreateCatalogView createCatalogView;


		private Mozaic deletedMozaic;
	    private int indexDeletedMozaic;


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
			    if (createCatalogOrSubcatalogView != null && newMessage == "CloseWindowCreateCatalogOrSubcatalogViewModel")
			    {
				    createCatalogOrSubcatalogView.Close();
			    }
		    });

		    Messenger.Default.Register<string>(this, (newMessage) =>
		    {
			    if (createCatalogView != null && newMessage == "CloseWindowCreateCatalogViewModel")
			    {
				    createCatalogView.Close();
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

		    Messenger.Default.Register<string>(this, (newMessage) =>
		    {
			    string[] split = newMessage.Split(new Char[] { '-' });
			    if (split[0] == "CreateCatalogOrSubcatalogViewModel")
			    {
				    catalogChangeService = new CatalogChangeService(split[1], split[2]);
					catalogChangeService.AddCatalog();

				}
		    });

		    Messenger.Default.Register<string>(this, (newMessage) =>
		    {
			    string[] split = newMessage.Split(new Char[] { '-' });
			    if (split[0] == "CreateCatalogViewModel")
			    {
				    catalogChangeService = new CatalogChangeService(split[1]+"_"+ split[2], "");
				    catalogChangeService.AddCatalog();

			    }
		    });

			MessengerInstance.Register<NotificationMessage<string>>(this, (message) =>
		    {
			    if (message.Notification == "MozaicDialogViewModel")
			    {
				    if (message.Content == "DeleteMozaic" && catalogChangeService!=null)
				    {
					   // MozaicsList.Remove(deletedMozaic);
						MozaicsList.RemoveAt(indexDeletedMozaic);
						//catalogChangeService.DeleteMozaic(deletedMozaic.Name);
					}

				}
		    });

		    MessengerInstance.Register<NotificationMessage<string>>(this, (message) =>
		    {
			    if (message.Notification == "MozaicDialogViewModel")
			    {
				    if (message.Content == "NoDeleteMozaic" && catalogChangeService != null)
				    {
					    MozaicsList[indexDeletedMozaic] = deletedMozaic;
				    }

			    }
		    });

		    Messenger.Default.Register<string>(this, (newMessage) =>
		    {
			    if (mozaicDialogView != null && newMessage == "CloseMozaicDialogViewModel")
			    {
				    mozaicDialogView.Close();
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

				choiseCatalogDialogView = new ChoiseCatalogAndSubcatalogView();


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
				if (catalogChangeService != null && SelectedMozaic!=null)
				{
					indexDeletedMozaic = MozaicsList.IndexOf(SelectedMozaic);
					deletedMozaic = SelectedMozaic;
					
					//SelectedMozaic.FullPath = null;
					//MozaicsList[indexDeletedMozaic] = SelectedMozaic;
					int i = 0;

					MozaicsList[indexDeletedMozaic]=null;

					mozaicDialogView = new MozaicDialogView();
					mozaicDialogView.Show();
				}
			}));
	    }

	    private ICommand _createSubCatalog;
	    public ICommand CreateSubCatalog
		{
		    get => _createSubCatalog ?? (_createSubCatalog = new RelayCommand(() =>
		    {
			    if (catalogChangeService != null)
			    {
				    catalogChangeService = null;
				    catalog = null;
				    MozaicsList = null;

				}

			    createCatalogOrSubcatalogView = new CreateCatalogOrSubcatalog();
			    createCatalogOrSubcatalogView.Show();
			}));
	    }

	    private ICommand _createCatalog;
	    public ICommand CreateCatalog
	    {
		    get => _createCatalog ?? (_createCatalog = new RelayCommand(() =>
		    {
			    if (catalogChangeService != null)
			    {
				    catalogChangeService = null;
				    catalog = null;
				    MozaicsList = null;

			    }

			    createCatalogView = new CreateCatalogView();
			    createCatalogView.Show();
		    }));
	    }


	}
}
