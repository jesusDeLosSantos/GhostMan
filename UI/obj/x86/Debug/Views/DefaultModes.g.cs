﻿#pragma checksum "E:\GitKraken-Repos\GhostMan\UI\Views\DefaultModes.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7ADB9BC71933FA21A1DC688941FE1BCE2BA060AF9F0AD5F75F4B1D1C72EF0F91"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UI.Views
{
    partial class DefaultModes : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 15: // Views\DefaultModes.xaml line 192
                {
                    this.brdChooseDifficulty = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 16: // Views\DefaultModes.xaml line 199
                {
                    this.relativePanel = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 17: // Views\DefaultModes.xaml line 230
                {
                    this.btnBack = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnBack).Click += this.btnBack_Click;
                }
                break;
            case 18: // Views\DefaultModes.xaml line 200
                {
                    this.btnEasy = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnEasy).Click += this.NavigateToPlay;
                }
                break;
            case 19: // Views\DefaultModes.xaml line 209
                {
                    this.btnMedium = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnMedium).Click += this.NavigateToPlay;
                }
                break;
            case 20: // Views\DefaultModes.xaml line 219
                {
                    this.btnHard = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnHard).Click += this.NavigateToPlay;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
