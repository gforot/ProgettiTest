﻿
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RicercaLocalizzata.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Data;

namespace RicercaLocalizzata.WPF
{
    public class MainWindowsViewModel : ViewModelBase
    {
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
    

        public RelayCommand<string> ChangeLanguageCommand { get; private set; }

        public ObservableCollection<MyItemWrapper> MyItems { get; private set; }

        public ICollectionView MyCollectionView { get; private set; }

        public MainWindowsViewModel(IEnumerable<MyItem> myItems)
        {
            MyItems = new ObservableCollection<MyItemWrapper>();

            foreach(MyItem i in myItems)
            {
                MyItems.Add(MyItemWrapperFactory.Create(i));
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


    }
}