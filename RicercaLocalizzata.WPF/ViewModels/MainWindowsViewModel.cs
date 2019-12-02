using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RicercaLocalizzata.Data;
using RicercaLocalizzata.WPF.Model.Factory;
using RicercaLocalizzata.WPF.Model.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Data;

namespace RicercaLocalizzata.WPF.ViewModels
{
    public class MainWindowsViewModel : ViewModelBase
    {
        private const string _title = "VGP3D";

        private string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    MyCollectionView.Refresh();
                    RaisePropertyChanged(nameof(SearchText));
                }
            }
        }

        public string Title
        {
            get
            {
                if (Modified)
                {
                    return $"{_title} *";
                }
                else
                {
                    return _title;
                }
            }
        }
    

        public RelayCommand<string> ChangeLanguageCommand { get; private set; }

        public ObservableCollection<MyItemWrapper> MyItems { get; private set; }

        public ICollectionView MyCollectionView { get; private set; }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand AcceptChangesCommand { get; private set; }
        public RelayCommand RejectChangesCommand { get; private set; }

        public MainWindowsViewModel(IEnumerable<MyItem> myItems)
        {
            MyItems = new ObservableCollection<MyItemWrapper>();

            foreach(MyItem i in myItems)
            {
                MyItemWrapper iw = MyItemWrapperFactory.Create(i);

                iw.PropertyChanged += (s, e)=>
                {
                    if(e.PropertyName == nameof(MyItemWrapper.Value))
                    {
                        //se ho cambiato il valore, notifico la modifica di Modified
                        RaisePropertyChanged(nameof(Modified));
                        RaisePropertyChanged(nameof(Title));
                        SaveCommand.RaiseCanExecuteChanged();
                        AcceptChangesCommand.RaiseCanExecuteChanged();
                        RejectChangesCommand.RaiseCanExecuteChanged();
                    }
                };

                MyItems.Add(iw);
            }

            //MyItems.Add(MyItemWrapperFactory.Create(MyItemWrapper.Element_Cognome, MyItemWrapper.Cat_2, 10));



            //MyItems.Add(MyItemWrapperFactory.Create(MyItemWrapper.Element_Cognome, MyItemWrapper.Cat_2, 10));
            //MyItems.Add(MyItemWrapperFactory.Create(MyItemWrapper.Element_Nome, MyItemWrapper.Cat_1, "Andrea"));
            //MyItems.Add(MyItemWrapperFactory.CreateForEnum(MyItemWrapper.Element_Palla, MyItemWrapper.Cat_3, (int)ETipoPalla.Rugby, typeof(ETipoPalla)));
            //MyItems.Add(MyItemWrapperFactory.CreateForEnum(MyItemWrapper.Element_Tavolo, MyItemWrapper.Cat_2, (int)ETipoTavolo.Pentagonale, typeof(ETipoTavolo)));


            //MyItems.Add(new MyItemWrapper(new MyItem() { Code = MyItemWrapper.Element_Cognome, Category = MyItemWrapper.Cat_2, Value = 10 }));
            //MyItems.Add(new MyItemWrapper(new MyItem() { Code = MyItemWrapper.Element_Nome, Category = MyItemWrapper.Cat_1, Value = "Andrea" }));
            //MyItems.Add(enumItem1);
            //MyItems.Add(new MyItemWrapper(new MyItem() { Code = MyItemWrapper.Element_Telefono, Category = MyItemWrapper.Cat_3, Value = true }));
            //MyItems.Add(new MyItemWrapper(new MyItem() { Code = MyItemWrapper.Element_Vetro, Category = MyItemWrapper.Cat_4, Value = "Bicchiere" }));
            //MyItems.Add(new MyItemWrapper(new MyItem() { Code = MyItemWrapper.Element_Ciao, Category = MyItemWrapper.Cat_3, Value = 12.5d  }));
            //MyItems.Add(new MyItemWrapper(new MyItem() { Code = MyItemWrapper.Element_Perche, Category = MyItemWrapper.Cat_2, Value = 17.4d }));
            //MyItems.Add(enumItem2);
            //MyItems.Add(new MyItemWrapper(new MyItem() { Code = MyItemWrapper.Element_Porta, Category = MyItemWrapper.Cat_2, Value = "A soffietto" }));
            //MyItems.Add(new MyItemWrapper(new MyItem() { Code = MyItemWrapper.Element_Benzina, Category = MyItemWrapper.Cat_2, Value = "Diesel 75 ottani"}));

            //COMANDI
            SaveCommand = new RelayCommand(Save, CanSave);
            AcceptChangesCommand = new RelayCommand(AcceptChanges, CanAcceptChanges);
            RejectChangesCommand = new RelayCommand(RejectChanges, CanRejectChanges);

            // Collection which will take your ObservableCollection
            var _itemSourceList = new CollectionViewSource() { Source = MyItems };
            GroupDescription gd = new PropertyGroupDescription("CategoryDescription");

            _itemSourceList.GroupDescriptions.Add(gd);

            // ICollectionView the View/UI part 
            MyCollectionView = _itemSourceList.View;

            // your Filter
            var yourCostumFilter = new Predicate<object>(ComplexFilter);

            //now we add our Filter
            MyCollectionView.Filter = yourCostumFilter;

            ChangeLanguageCommand = new RelayCommand<string>(ChangeLanguage);

            SetLanguage("it-IT");
        }

        public bool Modified
        {
            get { return MyItems.Any(i=>i.IsValueChanged); }
        }


        private bool ComplexFilter(object obj)
        {
            if (string.IsNullOrEmpty(SearchText)) return true;
            string description = (obj as MyItemWrapper).Description;
            string categoryDescription = (obj as MyItemWrapper).CategoryDescription;

            //if ((!string.IsNullOrEmpty(description)) && (description.Contains(SearchText))) return true;
            //if ((!string.IsNullOrEmpty(categoryDescription)) && (categoryDescription.Contains(SearchText))) return true;


            if ((!string.IsNullOrEmpty(description)) &&
                (Thread.CurrentThread.CurrentUICulture.CompareInfo.IndexOf(description, SearchText, CompareOptions.IgnoreCase) >= 0)) return true;

            if ((!string.IsNullOrEmpty(categoryDescription)) &&
                (Thread.CurrentThread.CurrentUICulture.CompareInfo.IndexOf(categoryDescription, SearchText, CompareOptions.IgnoreCase) >= 0)) return true;

            return false;
        }

        private void ChangeLanguage(string obj)
        {
            SetLanguage(obj);
        }

        public void SetLanguage(string code)
        {
            CultureInfo ci = new CultureInfo(code);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            foreach (var i in MyItems)
            {
                i.SetDescription();
                i.SetCategoryDescription();
            }

            MyCollectionView.Refresh();
        }

        private void Save()
        {
            MyItemSerializer s = new MyItemSerializer();
            s.Save(this.MyItems.Select(mi => mi.Model), @"c:\Temp\elementi.txt");
        }

        private bool CanSave()
        {
            return Modified;
        }

        private void RejectChanges()
        {
            foreach (MyItemWrapper miw in MyItems)
            {
                miw.RejectChanges();
            }
        }

        private bool CanRejectChanges()
        {
            return Modified;
        }

        private void AcceptChanges()
        {
            foreach(MyItemWrapper miw in MyItems)
            {
                miw.AcceptChanges();
            }
        }

        private bool CanAcceptChanges()
        {
            return Modified;
        }

    }
}
