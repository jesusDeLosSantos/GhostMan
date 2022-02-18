using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UI.ViewModels;


namespace UI.Views
{

    public sealed partial class Play : Page
    {
        public Play()
        {
            this.InitializeComponent();
            
        }
        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            PlayVM playVM = (PlayVM)DataContext;
            Window.Current.Content.KeyDown += playVM.moverFantasma;
        }
    }
}
