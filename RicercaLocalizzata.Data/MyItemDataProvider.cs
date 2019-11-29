using System.Collections.Generic;

namespace RicercaLocalizzata.Data
{
    public static class MyItemDataProvider
    {
        public const string Cat_Dati_Base = "Dati base";
        public const string Cat_Opz_Ciclo = "Opz ciclo";
        public const string Cat_Opz_Macchina = "Opz macchina";
        public const string Cat_Opz_Sequenza = "Opz sequenza";

        public const string Element_Cognome = "Cognome";
        public const string Element_Nome = "Nome";
        public const string Element_Tavolo = "Tavolo";
        public const string Element_Porta = "Porta";
        public const string Element_TipoTubo = "Tipo tubo";
        public const string Element_PosizioneAnima = "Posiz anima";
        public const string Element_Anima = "Anima";

        public static IEnumerable<MyItem> GetSampleData()
        {
            return new List<MyItem>() 
            { 
                new MyItem(){Code = Element_Nome, Category = Cat_Opz_Ciclo, Value = "Andrea"},
                new MyItem(){Code = Element_TipoTubo, Category = Cat_Dati_Base, Value = ETipoTubo.Ovale},
                new MyItem(){Code = Element_PosizioneAnima, Category = Cat_Opz_Macchina, Value = 10.5},
                new MyItem(){Code = Element_Anima, Category = Cat_Opz_Macchina, Value = true},
            };
        }
    }
}
