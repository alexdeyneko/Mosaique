using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using wpfMozaiq.Models;
using wpfMozaiq.Models.Services;
using wpfMozaiq.Veiw;

namespace wpfMozaiq.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string TEMP_DIRECTORY = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\Temp\\";
        private string TECH_DOC_PATH = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\Temp\\" + "techDoc" + ".txt";

        public Catalog catalog;
	    public OriginalImage originalImage;
	    public MozaicPanel panno;	 
     
        private NewProjectView newProjectView;
        
		public MainViewModel()
		{
			new TempFilesService().DeleteTempFilesInDirectory(TEMP_DIRECTORY);

		    Messenger.Default.Register<MozaicPanel>(this, (newPanno) =>
		    {
			    panno = newPanno;
			    catalog = panno.Catalog;
                			
			    GenerateMozaicPanno();
                MozaicsList = new ObservableCollection<Mozaic>(catalog.Mozaics);               
            });

		    Messenger.Default.Register<string>(this, (newMessage) =>
		    {
			    if (newProjectView != null && newMessage == "CloseWindowNewProjectViewModel")
			    {
				    newProjectView.Close();
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
		
		private Bitmap _imageBitmap;
	    public Bitmap ImageBitmap
	    {
		    set
		    {
			    _imageBitmap = value;
			    RaisePropertyChanged(() => ImageBitmap);
		    }
		    get
		    {
			    return _imageBitmap;
		    }
	    }

		private int _imageWidth;
	    public int ImageWidth
	    {
		    set
		    {
			    _imageWidth = value;
			    RaisePropertyChanged(() => ImageWidth);
		    }
		    get
		    {
			    return _imageWidth;
		    }
	    }

	    private int _imageHeight;
	    public int ImageHeight
	    {
		    set
		    {
			    _imageHeight = value;
			    RaisePropertyChanged(() => ImageHeight);
		    }
		    get
		    {
			    return _imageHeight;
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
                SourceImageView sourceImageView = new SourceImageView();

                if (panno != null)
                {
                    MessengerInstance.Send<NotificationMessage<string>>(new NotificationMessage<string>(panno.Image.SourcePath, "SourceImageViewModel"));
                }
                
				sourceImageView.Show();
		    }));
	    }

	    private ICommand _showTechDocView;
	    public ICommand ShowTechDocView
		{
		    get => _showTechDocView ?? (_showTechDocView = new RelayCommand(() =>
		    {
			    if (panno != null)
			    {
				    if (File.Exists(TECH_DOC_PATH))
				    {
					    File.Delete(TECH_DOC_PATH);
				    }
					new TxtTechDocWriter(panno, TECH_DOC_PATH).WriteDocumentation();
				    TechDocView techDocView = new TechDocView();
				    techDocView.Show();
				}

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

		private ICommand _pressPlus;
	    public ICommand PressPlus
	    {
		    get => _pressPlus ?? (_pressPlus = new RelayCommand(() =>
		    {
			    if (!String.IsNullOrEmpty(ImagePath))
			    {
				    if (File.Exists(ImagePath))
				    {
					    ImageHeight += 100;
					    ImageWidth += 100;
				    }
			    }

		    }));
	    }

	    private ICommand _pressMinus;
	    public ICommand PressMinus
	    {
		    get => _pressMinus ?? (_pressMinus = new RelayCommand(() =>
		    {
				if (!String.IsNullOrEmpty(ImagePath)) { 
					if (File.Exists(ImagePath))
					{
						ImageHeight -= 100;
						ImageWidth -= 100;
					}
				}
			}));
	    }

	    private void GenerateMozaicPanno()
	    {
			new MatrixGridService(panno).CreateImageGrid();
			new MozaicSelectService(panno).GenerateForGrid();

		    TechDocGenerateService tdgs = new TechDocGenerateService(panno);
		    tdgs.GenerateMostUsedMozaics();
		    tdgs.ReplaceMosaicNameToID();

			new MatrixSeparateService(panno).GenerateMatrixArray();
			
			ImageBitmap = new DrawService(panno).DrawPanno();
		    string tempPath = TEMP_DIRECTORY + new Random().Next() + ".bmp";
			ImageBitmap.Save(tempPath);

		    ImageHeight = ImageBitmap.Height;
		    ImageWidth = ImageBitmap.Width;

		    while (true)
		    {
			    if (File.Exists(tempPath))
			    {
				    ImagePath = tempPath;
					break;
			    }
		    }

		}
		
    }

}
