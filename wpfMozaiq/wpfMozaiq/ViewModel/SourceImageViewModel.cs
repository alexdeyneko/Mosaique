using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace wpfMozaiq.ViewModel
{
    public class SourceImageViewModel : ViewModelBase 
    {
        public SourceImageViewModel()
        {            
            MessengerInstance.Register<NotificationMessage<string>>(this, (newImagePath) =>
            {
               if (newImagePath.Notification == "SourceImageViewModel")
                {
                    SourceImagePath = newImagePath.Content;
                }
            });
        }

        private string _sourceImagePath;
        public string SourceImagePath
        {
            set
            {
                _sourceImagePath = value;
                RaisePropertyChanged(() => SourceImagePath);
            }
            get { return _sourceImagePath; }
        }
    }
}
