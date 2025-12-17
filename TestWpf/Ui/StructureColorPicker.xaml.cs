using Esapi.Services;
using Esapi.Wrappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using VMS.TPS.Common.Model.API;

namespace TestWpf.Ui
{
    /// <summary>
    /// Interaction logic for StructureColorPicker.xaml
    /// </summary>
    public partial class StructureColorPicker : UserControl
    {
        private readonly DispatcherTimer _debounceTimer;
        private bool _isUpdatingFromStructure;

        public StructureColorPicker()
        {
            InitializeComponent();

            // Setup Debounce Timer (only update ESAPI when user stops sliding)
            _debounceTimer = new DispatcherTimer();
            _debounceTimer.Interval = TimeSpan.FromMilliseconds(200);
            _debounceTimer.Tick += DebounceTimer_Tick;
        }

        // --- Dependancy Properties -- //
        // 1. The Structure Input
        public static readonly DependencyProperty StructureProperty =
            DependencyProperty.Register(nameof(Structure), typeof(AsyncStructure), typeof(StructureColorPicker),
                new PropertyMetadata(null, OnStructureChanged));

        public AsyncStructure Structure
        {
            get => (AsyncStructure)GetValue(StructureProperty);
            set => SetValue(StructureProperty, value);
        }

        // 2. Internal Color Properties (bound to sliders)
        public static readonly DependencyProperty RedProperty =
            DependencyProperty.Register(nameof(Red), typeof(byte), typeof(StructureColorPicker),
                new PropertyMetadata((byte)0, OnColorComponentChanged));

        public byte Red
        {
            get => (byte)GetValue(RedProperty);
            set => SetValue(RedProperty, value);
        }

        public static readonly DependencyProperty GreenProperty =
            DependencyProperty.Register(nameof(Green), typeof(byte), typeof(StructureColorPicker),
                new PropertyMetadata((byte)0, OnColorComponentChanged));

        public byte Green
        {
            get => (byte)GetValue(GreenProperty);
            set => SetValue(GreenProperty, value);
        }

        public static readonly DependencyProperty BlueProperty =
            DependencyProperty.Register(nameof(Blue), typeof(byte), typeof(StructureColorPicker),
                new PropertyMetadata((byte)0, OnColorComponentChanged));

        public byte Blue
        {
            get => (byte)GetValue(BlueProperty);
            set => SetValue(BlueProperty, value);
        }

        // 3. Current Color (Computed for Preview)
        public static readonly DependencyProperty CurrentColorProperty =
            DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(StructureColorPicker),
                new PropertyMetadata(Colors.Black));

        public Color CurrentColor
        {
            get => (Color)GetValue(CurrentColorProperty);
            private set => SetValue(CurrentColorProperty, value);
        }

        // 4. Status Message
        public static readonly DependencyProperty StatusMessageProperty =
             DependencyProperty.Register(nameof(StatusMessage), typeof(string), typeof(StructureColorPicker), new PropertyMetadata(""));

        public string StatusMessage
        {
            get => (string)GetValue(StatusMessageProperty);
            set => SetValue(StatusMessageProperty, value);
        }

        // --- Event Handlers --- //

        private static void OnStructureChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is StructureColorPicker control && e.NewValue is AsyncStructure structure)
            {
                control.LoadFromStructure(structure);
            }
        }

        private static void OnColorComponentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is StructureColorPicker control)
            {
                // Update the Preview Color immediately
                control.CurrentColor = Color.FromRgb(control.Red, control.Green, control.Blue);

                // If this change came from user input (not from loading the structure), trigger save
                if (!control._isUpdatingFromStructure)
                {
                    control.StatusMessage = "Waiting...";
                    control._debounceTimer.Stop();
                    control._debounceTimer.Start();
                }
            }
        }

        // --- Logic --- //
        private void LoadFromStructure(AsyncStructure structure)
        {
            _isUpdatingFromStructure = true;
            try
            {
                // Read initial values from the wrapper
                var c = structure.Color;
                Red = c.R;
                Green = c.G;
                Blue = c.B;
                CurrentColor = c;
                StatusMessage = "Loaded";
            }
            finally
            {
                _isUpdatingFromStructure = false;
            }
        }

        private async void DebounceTimer_Tick(object sender, EventArgs e)
        {
            _debounceTimer.Stop();

            if (Structure == null) return;

            try
            {
                StatusMessage = "Saving...";

                // Construct the new color
                var newColor = Color.FromRgb(Red, Green, Blue);

                // Call the Async Method on your wrapper
                await Structure.SetColorAsync(newColor);

                StatusMessage = "Saved";
            }
            catch (Exception ex)
            {
                StatusMessage = "Error";
                MessageBox.Show($"Failed to set color: {ex.Message}");
            }
        }
    }
}
