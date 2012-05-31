using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SSRS.Controls
{
    public class FormFieldTemplateSelector : UserControl
    {
        public Collection<TemplateSelectorDataTemplate> DataTemplates { get; set; }

        public static readonly DependencyProperty DataTypeProperty = DependencyProperty.Register("DataType", typeof(string), typeof(FormFieldTemplateSelector), new PropertyMetadata(string.Empty));
        public string DataType
        {
            get { return (string)GetValue(DataTypeProperty); }
            set { SetValue(DataTypeProperty, value); }
        }

        public FormFieldTemplateSelector()
        {
            DataTemplates = new Collection<TemplateSelectorDataTemplate>();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var fieldType = DataType;

            foreach (var t in DataTemplates.Where(t => fieldType == t.DataType))
            {
                Content = t.LoadContent() as UIElement;
                return;
            }
            Content = null;
        }
    }
}
