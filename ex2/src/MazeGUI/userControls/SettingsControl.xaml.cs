using System.Windows;
using System.Windows.Controls;

namespace MazeGUI.userControls
{
    /// <summary>
    /// Settings user control, has, rows colums and user control
    /// Interaction logic for SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        /// <summary>
        /// Using a DependencyProperty as the backing store for Rows.
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(SettingsControl),
                                        new PropertyMetadata(default(int)));

        /// <summary>
        /// Using a DependencyProperty as the backing store for Cols.
        /// </summary>
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(SettingsControl),
                                        new PropertyMetadata(default(int)));

        /// <summary>
        /// Using a DependencyProperty as the backing store for MazeName. 
        /// </summary>        
        public static readonly DependencyProperty MazeNameProperty =
            DependencyProperty.Register("MazeName", typeof(string), typeof(SettingsControl),
                                        new PropertyMetadata(default(string)));

        /// <summary>
        /// constructor
        /// </summary>
        public SettingsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// a property of rows 
        /// </summary>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>
        /// a property of cols 
        /// </summary>
        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        /// <summary>
        /// a property of mazeName 
        /// </summary>
        public string MazeName
        {
            get { return (string)GetValue(MazeNameProperty); }
            set { SetValue(MazeNameProperty, value); }
        }
    }
}
