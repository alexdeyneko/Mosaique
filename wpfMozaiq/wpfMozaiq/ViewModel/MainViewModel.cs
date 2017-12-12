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
using System.Windows.Forms;

namespace wpfMozaiq.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string TEMP_DIRECTORY = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\Temp\\";
        private string TECH_DOC_PATH = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\Temp\\" + "techDoc" + ".mzq";
	    private string PATH_DEFAULT_IMAGE = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) +
	                                        "\\Catalog\\" + "default.bmp";

		//public Catalog catalog;
		public OriginalImage originalImage;
	    public MozaicPanel panno;	 
     
        private NewProjectView newProjectView;

		public MainViewModel()
		{
			VisibilityProgressBar = "Hidden";

			new TempFilesService().DeleteTempFilesInDirectory(TEMP_DIRECTORY);

		    Messenger.Default.Register<MozaicPanel>(this, (newPanno) =>
		    {
			    ImageHeight = 0;
			    ImageWidth = 0;
				VisibilityProgressBar = "Visible";
				panno = newPanno;
 
				Thread myThread = new Thread(GenerateMozaicPanno); //
			    myThread.Start();
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

	    private ObservableCollection<Mozaic> _mozaicsListForRemoving;
	    public ObservableCollection<Mozaic> MozaicsListForRemoving
		{
		    set
		    {
			    _mozaicsListForRemoving = value;
			    RaisePropertyChanged(() => MozaicsListForRemoving);
		    }
		    get
		    {
			    return _mozaicsListForRemoving;
		    }
	    }

		private Mozaic _selectedMozaic;
	    public Mozaic SelectedMozaic
	    {
		    set
		    {
			    _selectedMozaic = value;
			    SelectedMozaic.FullPath = PATH_DEFAULT_IMAGE;
			    RaisePropertyChanged(() => SelectedMozaic);


			   int i = 0;
		    }
		    get
		    {
			    return _selectedMozaic;
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

	    private string _visibilityProgressBar;
	    public string VisibilityProgressBar
		{
		    set
		    {
			    _visibilityProgressBar = value;
			    RaisePropertyChanged(() => VisibilityProgressBar);
		    }
		    get
		    {
			    return _visibilityProgressBar;
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

                if (panno != null)
                {
                    SourceImageView sourceImageView = new SourceImageView();
                    MessengerInstance.Send<NotificationMessage<string>>(new NotificationMessage<string>(panno.Image.SourcePath, "SourceImageViewModel"));
                    sourceImageView.Show();
                }                
				
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

        private ICommand _saveProject;
        public ICommand SaveProject
        {
            get => _saveProject ?? (_saveProject = new RelayCommand(() =>
            {
                if (panno != null)
                {
                    SaveFileDialog saveFileDialog= new SaveFileDialog();
                    saveFileDialog.Filter = "Mozaic project(*.mzq)|*.mzq";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        new TxtTechDocWriter(panno, saveFileDialog.FileName).WriteDocumentation();
                    }

                }
                
            }));
        }

        private ICommand _importProject;
        public ICommand ImportProject
        {
            get => _importProject ?? (_importProject = new RelayCommand(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Mozaic project(*.mzq)|*.mzq";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    panno = new ImportService(openFileDialog.FileName).ImportPanno();
                    GenereateInputImage();                }
                
            }));
        }

        private void GenerateMozaicPanno()
	    {
			try { new MatrixGridService(panno).CreateImageGrid(); } catch( Exception e) { VisibilityProgressBar = "Hidden"; }
		    try { new MozaicSelectService(panno).GenerateForGrid(); } catch (Exception e) { VisibilityProgressBar = "Hidden"; }
		    try
		    {
			    TechDocGenerateService tdgs = new TechDocGenerateService(panno);
				tdgs.GenerateMostUsedMozaics();
			    tdgs.ReplaceMosaicNameToID();
			} catch (Exception e) { VisibilityProgressBar = "Hidden"; }

			try { new MatrixSeparateService(panno).GenerateMatrixArray(); } catch (Exception e) { VisibilityProgressBar = "Hidden"; }
		    try { GenereateInputImage(); } catch (Exception e) { VisibilityProgressBar = "Hidden"; }

		}

        private void GenereateInputImage()
        {
            ImageBitmap = new DrawService(panno).DrawPanno();
            string tempPath = TEMP_DIRECTORY + new Random().Next() + ".bmp";
            ImageBitmap.Save(tempPath);

            ImageHeight = ImageBitmap.Height;
            ImageWidth = ImageBitmap.Width;

            while (true)
            {
                if (File.Exists(tempPath))
                {
	                VisibilityProgressBar = "Hidden";
					ImagePath = tempPath;
                    break;
                }
            }
	        MozaicsList = new ObservableCollection<Mozaic>();
	        foreach (Mozaic var in panno.Catalog.Mozaics)
	        {
		        if (var.CountInPanno != 0)
		        {
			        MozaicsList.Add(var);
		        }
	        }
	        MozaicsListForRemoving = new ObservableCollection<Mozaic>();
		}

 
		
    }

}
