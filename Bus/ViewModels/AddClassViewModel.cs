using Bus.Commands;
using Bus.Models;
using Bus.Stores;
using System;
using System.Windows.Input;

namespace Bus.ViewModels
{
    class AddClassViewModel : BaseViewModel
    {

        public ICommand BackCommand { get; set; }

        public ICommand AddClassCommand { get; set; }

        public SchoolBusContext context { get; set; }


        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertChanged("Title");
            }
        }




        public AddClassViewModel(NavigationStore navigation)
        {
            context = new SchoolBusContext();
            AddClassCommand = new RelayCommand(AddClass);
            BackCommand = new UpdateViewCommand<ClassViewModel>(navigation, () => new ClassViewModel(navigation));
        }


        public void AddClass(Object obj)
        {
            var temp = new Group() { Title = Title };
            context.Add(temp);
            context.SaveChanges();
            BackCommand.Execute(obj);
        }

    }
}
