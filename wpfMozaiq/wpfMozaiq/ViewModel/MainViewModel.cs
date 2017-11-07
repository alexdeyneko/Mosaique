using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using wpfMozaiq.Models;
using wpfMozaiq.Models.Services;
using wpfMozaiq.Veiw;

namespace wpfMozaiq.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
		public Catalog catalog;
	    public OriginalImage originalImage;
	    public MozaicPanel panno;
	    public ObservableCollection<Mozaic> MozaicsList { get; set; }

	    private NewProjectView newProjectView;
	    private ImageView imageView;


		public MainViewModel()
		{
		    Messenger.Default.Register<MozaicPanel>(this, (newPanno) =>
		    {
			    panno = newPanno;
			    catalog = panno.Catalog;

				GenerateOutCatalogMozaic();
			    GenerateMozaicPanno();
		    });

		    Messenger.Default.Register<string>(this, (newMessage) =>
		    {
			    if (newProjectView != null && newMessage == "CloseWindowNewProjectViewModel")
			    {
				    newProjectView.Close();
			    }

			});

			ImagePath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\Temp\\" + "tempImage.bmp";
			

		}

	    private string _imagePath;
	    public string ImagePath
	    {
		    set
		    {
			    _imagePath = value;
				RaisePropertyChanged(() => ImagePath);
			}
		    get
		    {
			    return _imagePath;
		    }
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
                newProjectView= new NewProjectView();
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


	    private void GenerateOutCatalogMozaic ()
	    {
			MozaicsList = new ObservableCollection<Mozaic>();
			ObservableCollection<Mozaic> temp = new ObservableCollection<Mozaic>(catalog.Mozaics);
			foreach (var VARIABLE in temp)
			{
				MozaicsList.Add(VARIABLE);
			}
		}

	    private void GenerateMozaicPanno()
	    {
		    imageView = null; 

		    new MatrixGridService(panno).CreateImageGrid();
			new MozaicSelectService(panno).GenerateForGrid();

		    TechDocGenerateService tdgs = new TechDocGenerateService(panno);
		    tdgs.GenerateMostUsedMozaics();
		    tdgs.ReplaceMosaicNameToID();

			new MatrixSeparateService(panno).GenerateMatrixArray();
			
			if  (File.Exists(ImagePath) == true)
		    {
				File.Delete(ImagePath);
			}

			new DrawService(panno).DrawPanno().Save(ImagePath);
		    imageView = new ImageView();
			imageView.Show();
		}

	}
}
