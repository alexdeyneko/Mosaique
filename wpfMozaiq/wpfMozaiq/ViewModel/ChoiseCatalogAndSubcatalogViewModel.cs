﻿using System;
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
using wpfMozaiq.Models;
using wpfMozaiq.Models.Services;

namespace wpfMozaiq.ViewModel
{
	public class ChoiseCatalogAndSubcatalogViewModel : ViewModelBase, INotifyPropertyChanged
	{
		private bool isDeletedCatalog;

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

		public ChoiseCatalogAndSubcatalogViewModel()
		{
			isDeletedCatalog = false;


			Messenger.Default.Register<string>(this, (newMessage) =>
			{
				string[] split = newMessage.Split(new Char[] { '-' });
				if (split[0] == "ChoiseCatalogAndSubcatalogView")
				{
					if (split[1] == "DeleteCatalog")
					{
						isDeletedCatalog = true;
					}
				}
			});

			ArrCatalogsAndSubcatalog = new ObservableCollection<string>();
			string CatalogPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\Catalog\\";
			List<string> dirs = Directory.GetDirectories(CatalogPath).ToList();

			foreach (var VARIABLE in dirs)
			{
				System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(VARIABLE);
				System.IO.DirectoryInfo[] subDirs = info.GetDirectories();
				
				if (subDirs.Count() != 0)
				{
					foreach (var subDir in subDirs)
					{
						string[] split = VARIABLE.Split(new Char[] { '\\' });
						ArrCatalogsAndSubcatalog.Add(split[split.Length - 1] + "\\" + subDir);
					}
				}
				else
				{
					string[] split = VARIABLE.Split(new Char[] { '\\' });
					ArrCatalogsAndSubcatalog.Add(split[split.Length - 1] );
				}				
			}
		}

		private ICommand _okCommand;
		public ICommand OkCommand
		{
			get => _okCommand ?? (_okCommand = new RelayCommand(() =>
			{
				if (isDeletedCatalog)
				{
					Messenger.Default.Send("ChoiseDeleteCatalogAndSubcatalogViewModel" + "-" + SelectedCatalog);
				}
				else
				{
					Messenger.Default.Send("ChoiseCatalogAndSubcatalogViewModel" + "-" + SelectedCatalog);
				}
				

				Messenger.Default.Send("CloseWindowChoiseCatalogAndSubcatalogViewModel");
			}));

		}

		private ICommand _cancelCommand;
		public ICommand CancelCommand
		{
			get => _cancelCommand ?? (_cancelCommand = new RelayCommand(() =>
			{
				Messenger.Default.Send("CloseWindowChoiseCatalogAndSubcatalogViewModel");

			}));
		}

	}
}
