using GalaSoft.MvvmLight;
using RicercaLocalizzata.Data;
using RicercaLocalizzata.WPF.Model.Interfaces;
using System;
using System.Collections;

namespace RicercaLocalizzata.WPF.Model.Wrapper
{
    public class MyItemWrapper : ViewModelBase, IChangeTracker
    {


        public MyItemWrapper(MyItem myItem)
        {
            _model = myItem;
        }


        /// <summary>
        /// Il modello da wrappare.
        /// </summary>
        private readonly MyItem _model;

        /// <summary>
        /// Il modello wrappato
        /// </summary>
        public MyItem Model
        {
            get { return _model; }
        }

        /// <summary>
        /// Property read only che identifica il parametro.
        /// </summary>
        public string Code
        {
            get
            {
                return _model.Code;
            }
        }

        /// <summary>
        /// Property read only che identifica la categoria.
        /// </summary>
        public string Category
        {
            get
            {
                return _model.Category;
            }
        }

        /// <summary>
        /// La descrizione (traduce il Code in lingua).
        /// </summary>
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

        /// <summary>
        /// La descrizione della categoria (tradotta in lingua).
        /// </summary>
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


        /// <summary>
        /// Torna true se il valore è stato modificato.
        /// </summary>
        public bool IsValueChanged
        {
            get
            {
                return OriginalValue != null;
            }
        }

        /// <summary>
        /// Valore originario.
        /// Null se il valore non è stato modificato, quindi il valore originario coincide con il valore attuale.
        /// </summary>
        public object OriginalValue { get; set; } = null;


        public object Value
        {
            get { return _model.Value; }
            set
            {
                if (_model.Value != value)
                {
                    if(OriginalValue == null)
                    {
                        OriginalValue = _model.Value;
                    }
                    else if (value.Equals(OriginalValue))
                    {
                        //sto ripristinando il valore originale.
                        //è come se ho annullato la modifica.
                        OriginalValue = null;
                    }


                    _model.Value = Convert(value);
                    RaisePropertyChanged(nameof(Value));
                }
            }
        }


        private object Convert(object obj)
        {
            Type objType = obj.GetType();

            if(objType == _model.Type)
            {
                return obj;
            }
            else
            {
                if(objType.FullName == "System.String")
                {
                    if(_model.Type.FullName == "System.Int32")
                    {
                        return int.Parse(obj.ToString());
                    }
                    else if(_model.Type.FullName == "System.Double")
                    {
                        return double.Parse(obj.ToString());
                    }
                }
            }

            //quando passo di qui???
            return obj;
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
            else if (Code == MyItemDataProvider.Element_TaccaBloccaggio)
            {
                Description = Resources.Resource1.Tacca_Bloccaggio;
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

        public void AcceptChanges()
        {
            OriginalValue = null;
            RaisePropertyChanged(nameof(IsValueChanged));
        }

        public void RejectChanges()
        {
            if (OriginalValue != null)
            {
                Value = OriginalValue;
                OriginalValue = null;

                RaisePropertyChanged(nameof(IsValueChanged));
            }
        }
    }
}
