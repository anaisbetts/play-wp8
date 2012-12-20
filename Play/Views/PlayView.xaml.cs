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

namespace Play.Views
{
    public partial class PlayView : PhoneApplicationPage, IViewFor<PlayViewModel>
    {
        public PlayView()
        {
            InitializeComponent();
            this.OneWayBind(ViewModel, x => x.AllSongs);
        }

        public PlayViewModel ViewModel {
            get { return (PlayViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(PlayViewModel), typeof(PlayView), new PropertyMetadata(null));

        object IViewFor.ViewModel {
            get { return ViewModel; }
            set { ViewModel = (PlayViewModel)value; }
        }
    }
}