namespace RicercaLocalizzata.WPF.Model.Interfaces
{
    public interface IChangeTracker
    {
        void AcceptChanges();
        void RejectChanges();
    }
}
