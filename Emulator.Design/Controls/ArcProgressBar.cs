using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Emulator.Design.Controls
{
    public class ArcProgressBar: ProgressBar
    {
        public static readonly DependencyProperty MinValueLabelProperty = DependencyProperty.Register("MinValueLabel",
                                typeof(string),
                                typeof(ArcProgressBar),
                                new PropertyMetadata(string.Empty));

        [Bindable(true)]
        public string MinValueLabel
        {
            get
            {
                return (string)this.GetValue(MinValueLabelProperty);
            }
            set
            {
                this.SetValue(MinValueLabelProperty, value);
            }
        }

        public static readonly DependencyProperty MaxValueLabelProperty = DependencyProperty.Register("MaxValueLabel",
                                typeof(string),
                                typeof(ArcProgressBar),
                                new PropertyMetadata(string.Empty));

        [Bindable(true)]
        public string MaxValueLabel
        {
            get
            {
                return (string)this.GetValue(MaxValueLabelProperty);
            }
            set
            {
                this.SetValue(MaxValueLabelProperty, value);
            }
        }
    }
}