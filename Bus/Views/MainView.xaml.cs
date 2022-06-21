using Bus.Stores;
using Bus.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Bus.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {

            InitializeComponent();

            NavigationStore navigation = new NavigationStore();
            navigation.SelectedViewModel = new DriverViewModel(navigation);
            DataContext = new MainViewModel(navigation);
        }


        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
