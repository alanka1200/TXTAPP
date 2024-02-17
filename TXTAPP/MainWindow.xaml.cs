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

namespace TXTAPP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        } 
        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            // Применение жирного шрифта к выделенному тексту
            if (richTextBox.Selection.GetPropertyValue(TextElement.FontWeightProperty) is FontWeight fontWeight && fontWeight == FontWeights.Bold)
            {
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
            else
            {
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
        }
        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            // Применение курсивного шрифта к выделенному тексту
            if (richTextBox.Selection.GetPropertyValue(TextElement.FontStyleProperty) is FontStyle fontStyle && fontStyle == FontStyles.Italic)
            {
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
        }
        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            // Применение подчеркивания к выделенному тексту
            if (richTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) is TextDecorationCollection textDecorations && textDecorations.Count > 0)
            {
                richTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
            else
            {
                richTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }
        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.Document = new FlowDocument();
        }
        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var fileStream = openFileDialog.OpenFile();
                var flowDocument = new FlowDocument();
                var textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
                textRange.Load(fileStream, DataFormats.Rtf);
                richTextBox.Document = flowDocument;
            }
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                var fileStream = saveFileDialog.OpenFile();
                var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                textRange.Save(fileStream, DataFormats.Rtf);
            }
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedItem.Content is string fontSizeString)
                {
                    if (double.TryParse(fontSizeString, out double fontSize))
                    {
                        richTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
                    }
                }
            }
        }
    }
}
