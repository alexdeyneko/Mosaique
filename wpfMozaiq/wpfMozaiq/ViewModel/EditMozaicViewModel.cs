using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using wpfMozaiq.Models;

namespace wpfMozaiq.ViewModel
{
	public class EditMozaicViewModel : ViewModelBase, INotifyPropertyChanged
	{
		public EditMozaicViewModel()
		{
			MessengerInstance.Register<NotificationMessage<Mozaic>>(this, (newSelectedMozaic) =>
			{
				if (newSelectedMozaic.Notification == "EditMozaicViewModel")
				{
					EditedMozaic = newSelectedMozaic.Content;
					ImagePathMozaic = EditedMozaic.FullPath;
					EditImagePath = EditedMozaic.FullPath;
					EditSubcatalog = EditedMozaic.SubCatalog;
					EditNameMozaic = EditedMozaic.Name;
					int i = 0;
				}
			});

		}

		private Mozaic _editedMozaic;
		public Mozaic EditedMozaic
		{
			set
			{
				_editedMozaic = value;
				RaisePropertyChanged(() => EditedMozaic);
			}
			get
			{
				return _editedMozaic;
			}
		}

		private string _imagePathMozaic;
		public string ImagePathMozaic
		{
			set
			{
				_imagePathMozaic = value;
				RaisePropertyChanged(() => ImagePathMozaic);
			}
			get
			{
				return _imagePathMozaic;
			}
		}

		private string _editImagePath;
		public string EditImagePath
		{
			set
			{
				_editImagePath = value;
				RaisePropertyChanged(() => EditImagePath);
			}
			get { return _editImagePath; }
		}


		private string _editSubcatalog;
		public string EditSubcatalog
		{
			set
			{
				_editSubcatalog = value;
				RaisePropertyChanged(() => EditSubcatalog);
			}
			get { return _editSubcatalog; }
		}


		private string _editNameMozaic;
		public string EditNameMozaic
		{
			set
			{
				_editNameMozaic = value;
				RaisePropertyChanged(() => EditNameMozaic);
			}
			get { return _editNameMozaic; }
		}

	}
}
