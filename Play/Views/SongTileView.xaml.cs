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
    public partial class SongTileView : UserControl, IViewFor<SongTileViewModel>
    {
        public SongTileView()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.Model.name, x => x.Title.Text);
        }

        public SongTileViewModel ViewModel {
            get { return (SongTileViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(SongTileViewModel), typeof(SongTileView), new PropertyMetadata(null));

        object IViewFor.ViewModel {
            get { return ViewModel; }
            set { ViewModel = (SongTileViewModel) value; }
        }
    }
}
