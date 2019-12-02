using System;

namespace RicercaLocalizzata.Data
{
    public class MyItem
    {
        public string Code { get; set; }

        public string Category { get; set; }

        public object Value { get; set; }

        public Type Type { get; set; }



        public override string ToString()
        {
            return $"{Code} - {Category} - {Value} - {Value.GetType().FullName}";
        }
    }
}
