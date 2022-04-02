using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ProjectManager
{
    public class PlaceTextBox : TextBox
    {

        /// <summary>Text on background</summary>
        public string PlaceText
        {
            get => (string)GetValue(PlaceTextProperty);
            set => SetValue(PlaceTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for PlaceText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceTextProperty =
            DependencyProperty.Register(nameof(PlaceText), typeof(string), typeof(PlaceTextBox),
                new PropertyMetadata("Start typing", (d, e) => ((PlaceTextBox)d).PlaceChanged()));


        /// <summary>Brush for PlaceText</summary>
        public Brush PlaceBrush
        {
            get => (Brush)GetValue(PlaceBrushProperty);
            set => SetValue(PlaceBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for PlaceBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceBrushProperty =
            DependencyProperty.Register(nameof(PlaceBrush), typeof(Brush), typeof(PlaceTextBox),
                new PropertyMetadata(Brushes.LightGray, (d, e) => ((PlaceTextBox)d).PlaceChanged()));


        /// <summary>Margin for PlaceText</summary>
        public Thickness PlaceMargin
        {
            get => (Thickness)GetValue(PlaceMarginProperty);
            set => SetValue(PlaceMarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for PlaceMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceMarginProperty =
            DependencyProperty.Register(nameof(PlaceMargin), typeof(Thickness), typeof(PlaceTextBox),
                new PropertyMetadata(new Thickness(1), (d, e) => ((PlaceTextBox)d).PlaceChanged()));


        /// <summary>Text property value is empty</summary>
        public bool IsTextEmpty
        {
            get => (bool)GetValue(IsTextEmptyProperty);
            private set => SetValue(IsTextEmptyPropertyKey, value);
        }

        // Using a DependencyProperty as the backing store for IsTextEmpty.  This enables animation, styling, binding, etc...
        private static readonly DependencyPropertyKey IsTextEmptyPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(IsTextEmpty), typeof(bool), typeof(PlaceTextBox),
                new PropertyMetadata(true, (d, e) => ((PlaceTextBox)d).IsTextEmptyChanged((bool)e.NewValue)));
        public static readonly DependencyProperty IsTextEmptyProperty = IsTextEmptyPropertyKey.DependencyProperty;

        /// <summary>Метод применяющий одну из кистей
        /// при изменении свойства IsTextEmpty</summary>
        private void IsTextEmptyChanged(bool newValue)
        {
            IsInChanged = true;
            if (newValue)
            {
                /// Фон для пустого поля
                if (Background != BrushPlace)
                    Background = BrushPlace;
            }
            else
            {
                /// Фонт для поля с введённым текстом
                if (Background != BrushClean)
                    Background = BrushClean;
            }
            IsInChanged = false;
        }





        /// <summary>Метод рисующий кисть и применяющий её
        /// Должен вызываться при изменени любого из свойств: PlaceText, PlaceBrush, Background,
        /// ActualWidth, ActualHeight, FontFamily, FontStyle, FontWeight, FontStretch, FontSize</summary>
        private void PlaceChanged()
        {
            if (double.IsNaN(ActualWidth) || double.IsNaN(ActualHeight) || ActualWidth <= 0 || ActualHeight <= 0)
                return;

            //https://docs.microsoft.com/ru-ru/dotnet/framework/wpf/advanced/how-to-draw-text-to-a-control-background

            // Create a new DrawingGroup of the control.
            DrawingGroup drawingGroup = new DrawingGroup();

            // Open the DrawingGroup in order to access the DrawingContext.
            using (DrawingContext drawingContext = drawingGroup.Open())
            {
                // Create the formatted text based on the properties set.
                FormattedText formattedText = new FormattedText(
                    PlaceText,
                    CultureInfo.InvariantCulture,
                    FlowDirection.LeftToRight,
                    new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                    FontSize,
                    Brushes.Black, // This brush does not matter since we use the geometry of the text. 
                    VisualTreeHelper.GetDpi(this).PixelsPerDip);

                // Build the geometry object that represents the text.
                Geometry textGeometry = formattedText.BuildGeometry(new Point(PlaceMargin.Left, PlaceMargin.Top));


                double width = Math.Max(ActualWidth, formattedText.Width + PlaceMargin.Left + PlaceMargin.Right);
                double height = Math.Max(ActualHeight, formattedText.Height + PlaceMargin.Top + PlaceMargin.Bottom);

                // Draw a rounded rectangle under the text that is slightly larger than the text.
                drawingContext.DrawRoundedRectangle(BrushClean, null, new Rect(new Size(width, height)), 0, 0);

                // Draw the outline based on the properties that are set.
                drawingContext.DrawGeometry(PlaceBrush, null, textGeometry);

                // Return the updated DrawingGroup content to be used by the control.
                //    return drawingGroup;

                BrushPlace = new DrawingBrush(drawingGroup);
            }

            /// Перерисовка актуальными кистями
            IsTextEmptyChanged(IsTextEmpty);
        }



        public PlaceTextBox() => BrushClean = Background;

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            /// Изменение свойства пустого значения
            IsTextEmpty = string.IsNullOrWhiteSpace(Text);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            /// Метод используется для прослушки изменений свойств в базовом классе
            base.OnPropertyChanged(e);

            /// Если "снаружи" изменилось значение свойства Background
            if (e.Property == BackgroundProperty && !IsInChanged)
            {
                /// Запомнить его и перерисовать фон
                BrushClean = Background;
                PlaceChanged();
            }

            /// Если изменилось одно из свойств
            else if (
                new DependencyProperty[] { ActualWidthProperty, ActualHeightProperty,
                    FontFamilyProperty, FontStyleProperty, FontWeightProperty,
                    FontStretchProperty, FontSizeProperty }.Contains(e.Property))
                /// Перерисовать фон
                PlaceChanged();
        }


        /// <summary>Кисть без текста</summary>
        private Brush BrushClean;
        /// <summary>Кисть с текстом</summary>
        private Brush BrushPlace;
        /// <summary>Изменение значения "изнутри"</summary>
        private bool IsInChanged;

    }
}
