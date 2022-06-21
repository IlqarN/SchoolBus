using Bus.Commands;
using Bus.Models;
using Bus.Stores;
using System;
using System.Windows.Input;

namespace Bus.ViewModels
{

    class AddDriverViewModel : BaseViewModel
    {
        public ICommand BackCommand { get; set; }

        public ICommand AddDriverCommand { get; set; }

        public SchoolBusContext context { get; set; }


        private Driver _driver;

        public Driver Driver
        {
            get { return _driver; }
            set {
                _driver = value;
                OnPropertChanged("Driver");
            }
        }


        public AddDriverViewModel(NavigationStore navigation)
        {
            context = new SchoolBusContext();
            AddDriverCommand = new RelayCommand(AddDriver);
            BackCommand = new UpdateViewCommand<DriverViewModel>(navigation, () => new DriverViewModel(navigation));
            Driver = new Driver();
        }


        public void AddDriver(Object obj)
        {
            context.Add(Driver);
            context.SaveChanges();
            BackCommand.Execute(obj);
        }

    }
}
