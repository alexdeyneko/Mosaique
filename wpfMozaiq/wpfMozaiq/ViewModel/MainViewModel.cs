using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using wpfMozaiq.Models;
using wpfMozaiq.Veiw;

namespace wpfMozaiq.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
		public Catalog catalog;
	    public OriginalImage originalImage;
	    public ObservableCollection<Mozaic> MozaicsList { get; set; }

	    public MainViewModel()
	    {

		    Messenger.Default.Register<Catalog>(this, (newCatalog) =>
		    {
			    MozaicsList = new ObservableCollection<Mozaic>();
			    catalog = newCatalog;
			    ObservableCollection<Mozaic> temp = new ObservableCollection<Mozaic>(catalog.Mozaics);

			    foreach (var VARIABLE in temp)
			    {
				    MozaicsList.Add(VARIABLE);
			    }
			});

		    Messenger.Default.Register<OriginalImage>(this, (newOriginalImage) =>
		    {
			    originalImage = newOriginalImage;
			});

		    Messenger.Default.Register<NotificationMessage>(this, (message) =>
		    {
			    string msg = message.Notification;
			    int i = 0;
		    });
	    }

		private string _filenameImage;
	    public string FilenameImage
	    {
		    set
		    {
			    _filenameImage = value;
			    RaisePropertyChanged(() => FilenameImage);
		    }
		    get { return _filenameImage; }
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

	    private ICommand _showCatalogView;
	    public ICommand ShowCatalogView
		{
		    get => _showCatalogView ?? (_showCatalogView = new RelayCommand(() =>
		    {
				CatalogView catalogView = new CatalogView();
				catalogView.Show();
		    }));
	    }

	    private ICommand _showSourceImageView;
	    public ICommand ShowSourceImageView
		{
		    get => _showSourceImageView ?? (_showSourceImageView = new RelayCommand(() =>
		    {
				SourceImageView sourceImageView= new SourceImageView();
				sourceImageView.Show();
		    }));
	    }

	    private ICommand _showTechDocView;
	    public ICommand ShowTechDocView
		{
		    get => _showTechDocView ?? (_showTechDocView = new RelayCommand(() =>
		    {
				TechDocView techDocView = new TechDocView();
				techDocView.Show();
		    }));
	    }

	    private ICommand _showUserGuideView;
	    public ICommand ShowUserGuideView
		{
		    get => _showUserGuideView ?? (_showUserGuideView = new RelayCommand(() =>
		    {
				UserGuideView userGuideView = new UserGuideView();
				userGuideView.Show();
		    }));
	    }
		
	}

	
}
