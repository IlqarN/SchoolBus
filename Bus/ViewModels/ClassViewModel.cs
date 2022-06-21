using Bus.Commands;
using Bus.Models;
using Bus.Stores;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bus.ViewModels
{
    class ClassViewModel : BaseViewModel
    {

        public SchoolBusContext context { get; set; }

        public ObservableCollection<Group> Classes { get; set; }

        public ICommand DeleteClas { get; set; }

        public ICommand AddClass { get; set; }

        public ICommand UpdateClass { get; set; }

        public ICommand SearchCommand { get; set; }


        private Group _selectedClass;

        public Group SelectedClass
        {
            get { return _selectedClass; }
            set
            {
                _selectedClass = value;
                OnPropertChanged("SelectedClass");
            }
        }


        private string _search;

        public string Search
        {
            get { return _search; }
            set
            {
                _search = value;
                SearchDrivers(_search);
                OnPropertChanged("Search");
            }
        }


        public ClassViewModel(NavigationStore navigation)
        {
            context = new SchoolBusContext();
            Classes = new ObservableCollection<Group>(context.Groups.Include(g => g.Students).ToList());
            DeleteClas = new RelayCommand(d => DeleteClass());
            AddClass = new UpdateViewCommand<AddClassViewModel>(navigation, () => new AddClassViewModel(navigation));
            UpdateClass = new UpdateViewCommand<UpdateClassViewModel>(navigation, () => new UpdateClassViewModel(navigation, SelectedClass));
        }


        public void DeleteClass()
        {
            var tempClass = SelectedClass;

            context.Groups.Remove(tempClass);

            context.SaveChanges();

            Classes.Remove(tempClass);
        }


        public async void SearchDrivers(string Name)
        {
            if (Name.Length > 0)
            {
                var classes = await Task.Run(() => context.Groups.Where(d => d.Title.ToLower().StartsWith(Name.ToLower())));

                Classes.Clear();

                foreach (var item in classes)
                {
                    Classes.Add(item);
                }
            }
            else
            {
                Classes.Clear();
                foreach (var item in context.Groups)
                {
                    Classes.Add(item);
                }
            }
        }

    }
}
