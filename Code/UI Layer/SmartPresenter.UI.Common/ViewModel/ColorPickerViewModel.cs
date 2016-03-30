using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace SmartPresenter.UI.Common.ViewModel
{
    public class ColorPickerViewModel : BindableBase
    {
        #region Constructor

        public ColorPickerViewModel()
        {
            AllColors = new ObservableCollection<SolidColorBrush>();
            RecentColors = new ObservableCollection<SolidColorBrush>();

            CreateColorList();
        }

        #endregion

        #region Properties

        public ObservableCollection<SolidColorBrush> AllColors { get; set; }

        public ObservableCollection<SolidColorBrush> RecentColors { get; set; }

        #endregion

        #region Methods

        #region Private Methods

        private void CreateColorList()
        {
            AllColors.Add(new SolidColorBrush() { Color = Colors.Black });
            AllColors.Add(new SolidColorBrush() { Color = Colors.White });
            AllColors.Add(new SolidColorBrush() { Color = Colors.DarkBlue });
            AllColors.Add(new SolidColorBrush() { Color = Colors.Tan });
            AllColors.Add(new SolidColorBrush() { Color = Colors.Blue });
            AllColors.Add(new SolidColorBrush() { Color = Colors.Red });
            AllColors.Add(new SolidColorBrush() { Color = Colors.DarkOliveGreen });
            AllColors.Add(new SolidColorBrush() { Color = Colors.Purple });
            AllColors.Add(new SolidColorBrush() { Color = Colors.Aqua });
            AllColors.Add(new SolidColorBrush() { Color = Colors.Orange });

            List<SolidColorBrush> colors = new List<SolidColorBrush>();
            foreach (SolidColorBrush brush in AllColors)
            {
                colors.Add(new SolidColorBrush(Color.FromRgb((byte)(brush.Color.R * 0.1f), (byte)(brush.Color.G * 0.1f), (byte)(brush.Color.B * 0.1f))));
                colors.Add(new SolidColorBrush(Color.FromRgb((byte)(brush.Color.R * 0.2f), (byte)(brush.Color.G * 0.2f), (byte)(brush.Color.B * 0.2f))));
                colors.Add(new SolidColorBrush(Color.FromRgb((byte)(brush.Color.R * 0.3f), (byte)(brush.Color.G * 0.3f), (byte)(brush.Color.B * 0.3f))));
                colors.Add(new SolidColorBrush(Color.FromRgb((byte)(brush.Color.R * 0.4f), (byte)(brush.Color.G * 0.4f), (byte)(brush.Color.B * 0.4f))));
                colors.Add(new SolidColorBrush(Color.FromRgb((byte)(brush.Color.R * 0.5f), (byte)(brush.Color.G * 0.5f), (byte)(brush.Color.B * 0.5f))));
                colors.Add(new SolidColorBrush(Color.FromRgb((byte)(brush.Color.R * 0.6f), (byte)(brush.Color.G * 0.6f), (byte)(brush.Color.B * 0.6f))));
            }

            int index = 1;
            foreach (SolidColorBrush brush in colors)
            {
                AllColors.Insert(index, brush);
                index += 10;
                if (index > 60)
                {
                    index -= 60;
                }
            }
        }

        #endregion

        #endregion
    }
}
