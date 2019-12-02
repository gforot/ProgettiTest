using RicercaLocalizzata.Data;
using RicercaLocalizzata.WPF.Model.Wrapper;
using System;

namespace RicercaLocalizzata.WPF.Model.Factory
{
    public static class MyItemWrapperFactory
    {
        public static MyItemWrapper Create(MyItem myItem)
        {
            Type valueType = myItem.Value.GetType();
            if (valueType.IsEnum)
            {
                MyItemWrapper m = new MyItemWrapper(myItem);
                m.Items = Enum.GetValues(valueType);
                return m;
            }
            else
            {
                return new MyItemWrapper(myItem);
            }
        }
    }
}
