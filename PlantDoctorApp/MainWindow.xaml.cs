using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PlantDoctorApp
{
    public partial class MainWindow : Window
    {
        private readonly PlantClassifierService _classifier = new PlantClassifierService();
        private readonly OpenAIService _openai = new OpenAIService();
        private readonly HistoryService _history = new HistoryService();

        public MainWindow()
        {
            InitializeComponent();
            LoadHistory();
        }

        private void LanguageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: implement language switching
        }

        private void ImageDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                AnalyzeFiles(files);
            }
        }

        private void SelectImages_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "Image Files|*.png;*.jpg;*.jpeg", Multiselect = true };
            if (dlg.ShowDialog() == true)
            {
                AnalyzeFiles(dlg.FileNames);
            }
        }

        private async void AnalyzeFiles(IEnumerable<string> files)
        {
            int count = 0;
            foreach (var file in files)
            {
                if (count >= 2) break;
                var plant = _classifier.Classify(file);
                var advice = await _openai.GetAdviceAsync(plant.Name, plant.Condition);
                _history.SaveResult(file, plant.Name, plant.Condition, advice);
                HistoryList.Items.Insert(0, $"{plant.Name} - {plant.Condition}");
                count++;
            }
        }

        private void LoadHistory()
        {
            foreach (var item in _history.LoadHistory())
            {
                HistoryList.Items.Add($"{item.Name} - {item.Condition}");
            }
        }
    }
}
