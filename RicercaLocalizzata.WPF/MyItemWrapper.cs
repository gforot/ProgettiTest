using GalaSoft.MvvmLight;
using RicercaLocalizzata.Data;
using System;
using System.Collections;

namespace RicercaLocalizzata.WPF
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


    public class MyItemWrapper : ViewModelBase
    {



        private readonly MyItem _myItem;

        public MyItemWrapper(MyItem myItem)
        {
            _myItem = myItem;
        }

        public string Code
        {
            get
            {
                return _myItem.Code;
            }
        }

        public string Category
        {
            get
            {
                return _myItem.Category;
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    RaisePropertyChanged(nameof(Description));
                }
            }
        }

        public object Value
        {
            get { return _myItem.Value; }
            set
            {
                if (_myItem.Value != value)
                {
                    _myItem.Value = value;
                    RaisePropertyChanged(nameof(Value));
                }
            }
        }

        private string _categoryDescription;
        public string CategoryDescription
        {
            get
            {
                return _categoryDescription;
            }
            set
            {
                if (_categoryDescription != value)
                {
                    _categoryDescription = value;
                    RaisePropertyChanged(nameof(CategoryDescription));
                }
            }
        }

        private IEnumerable _items;
        public IEnumerable Items 
        {
            get 
            { 
                return _items; 
            }
            set
            {

                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public void SetDescription()
        {
            if (Code == MyItemDataProvider.Element_Cognome)
            {
                Description = Resources.Resource1.Cognome;
            }
            else if (Code == MyItemDataProvider.Element_Nome)
            {
                Description = Resources.Resource1.Nome;
            }
            else if (Code == MyItemDataProvider.Element_Tavolo)
            {
                Description = Resources.Resource1.Tavolo;
            }
            else if (Code == MyItemDataProvider.Element_PosizioneAnima)
            {
                Description = Resources.Resource1.Posizione_Anima;
            }
            else if (Code == MyItemDataProvider.Element_TipoTubo)
            {
                Description = Resources.Resource1.Tipo_Tubo;
            }
            else if (Code == MyItemDataProvider.Element_Porta)
            {
                Description = Resources.Resource1.Porta;
            }
            else if (Code == MyItemDataProvider.Element_Anima)
            {
                Description = Resources.Resource1.Anima;
            }
        }

        public void SetCategoryDescription()
        {
            switch (Category)
            {
                case MyItemDataProvider.Cat_Dati_Base:
                    CategoryDescription = Resources.Resource1.Dati_base;
                    break;
                case MyItemDataProvider.Cat_Opz_Ciclo:
                    CategoryDescription = Resources.Resource1.Opzioni_ciclo;
                    break;
                case MyItemDataProvider.Cat_Opz_Macchina:
                    CategoryDescription = Resources.Resource1.Opzioni_macchina;
                    break;
                case MyItemDataProvider.Cat_Opz_Sequenza:
                    CategoryDescription = Resources.Resource1.Opzioni_sequenza;
                    break;
            }
        }

        public override string ToString()
        {
            return $"{CategoryDescription} - {Description}";
        }
    }
}
