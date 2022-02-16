using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UI.ViewModels;


namespace UI.Views
{

    public sealed partial class PruebaMoverCanvas : Page
    {
        public PruebaMoverCanvas()
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
