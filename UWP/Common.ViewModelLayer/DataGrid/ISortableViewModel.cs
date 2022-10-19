using Common.DI.Attributes;

namespace Common.ViewModelLayer.DataGrid
{
    [NotForDI]

    public interface ISortableViewModel
    {
        void Sort(string columnName, bool? ascending);
    }
}
