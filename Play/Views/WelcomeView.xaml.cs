using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Play.ViewModels;
using ReactiveUI;
using ReactiveUI.Xaml;

namespace Play.Views
{
    public partial class WelcomeView : PhoneApplicationPage, IViewFor<WelcomeViewModel>
    {
        public WelcomeView()
        {
            InitializeComponent();

            this.Bind(ViewModel, x => x.BaseUrl);
            this.Bind(ViewModel, x => x.Token);
            this.BindCommand(ViewModel, x => x.OpenTokenPage);
            this.BindCommand(ViewModel, x => x.OkButton);
        }

        public WelcomeViewModel ViewModel {
            get { return (WelcomeViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(WelcomeViewModel), typeof(WelcomeView), new PropertyMetadata(null));

        object IViewFor.ViewModel {
            get { return ViewModel; }
            set { ViewModel = (WelcomeViewModel)value; }
        }
    }
}