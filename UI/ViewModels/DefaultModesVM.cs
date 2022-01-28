using UI.ViewModels.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UI.ViewModels
{
    public class DefaultModesVM
    {
        #region private properties
        private DelegateCommand navigate;
        private bool isEasyButtonPressed;
        private bool isMediumButtonPressed;
        private bool isHardButtonPressed;
        #endregion
        #region public properties
        public bool IsEasyButtonPressed { get => isEasyButtonPressed; set => isEasyButtonPressed = value; }
        public bool IsMediumButtonPressed { get => isMediumButtonPressed; set => isMediumButtonPressed = value; }
        public bool IsHardButtonPressed { get => isHardButtonPressed; set => isHardButtonPressed = value; }
        #endregion
        #region commands
        public DelegateCommand Navigate
        {
            get
            {
                if (navigate == null)
                {
                    navigate = new DelegateCommand(navigate_Execute);
                }
                return navigate;
            }
        }
        #endregion
        #region commands actions
        private void navigate_Execute()
        {
            Frame navigation = Window.Current.Content as Frame;

            if (isEasyButtonPressed)
            {
                navigation.Navigate(typeof(Prueba));
                isEasyButtonPressed = false;
            }
            else if (isMediumButtonPressed)
            {
                navigation.Navigate(typeof(PruebaMedio));
                isMediumButtonPressed = false;
            }
            else if (isHardButtonPressed)
            {
                navigation.Navigate(typeof(PruebaDificil));
                isHardButtonPressed = false;
            }
        }
        #endregion
    }
}
