using System;
using System.Windows;
using System.Windows.Controls;

namespace RicercaLocalizzata.WPF.TemplateSelector
{
    public class MyTemplateSelector : DataTemplateSelector
    {
        public DataTemplate IntTemplate { get; set; }
        public DataTemplate DoubleTemplate { get; set; }
        public DataTemplate StringTemplate { get; set; }
        public DataTemplate BoolTemplate { get; set; }
        public DataTemplate EnumTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null) return null;
            if(item is MyItemWrapper)
            {
                MyItemWrapper miw = item as MyItemWrapper;
                if(miw.Value is bool)
                {
                    return BoolTemplate;
                }
                else if (miw.Value is Enum)
                {
                    return EnumTemplate;
                }
                else if (miw.Value is string)
                {
                    return StringTemplate;
                }
                else if(miw.Value is double)
                {
                    return DoubleTemplate;
                }
                else if (miw.Value is int)
                {
                    return IntTemplate;
                }

                return StringTemplate;
            }

            return null;
        }

    }
}
