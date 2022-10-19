using System;
using System.Linq;
using Windows.UI.Xaml;
using Microsoft.Xaml.Interactivity;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Common.ViewModelLayer.DataGrid;

namespace Hamburger.Actions.DataGrid
{
    public class SortAction : DependencyObject, IAction
    {
        private static DataGridSortDirection?[] SortingDirectionsOrder = new DataGridSortDirection?[] { null, DataGridSortDirection.Ascending, DataGridSortDirection.Descending };

        public object Execute(object sender, object parameter)
        {
            var args = (DataGridColumnEventArgs)parameter;

            var column = args.Column;

            if (!column.CanUserSort) return null;

            var columnName = (string)column.Tag;

            var dataGrid = (Microsoft.Toolkit.Uwp.UI.Controls.DataGrid)sender;

            var viewModel = (ISortableViewModel)dataGrid.DataContext;

            var nextDirection = GetNextSortingDirection(column.SortDirection);

            foreach (var anotherColumn in dataGrid.Columns.Where(c => c != column)) anotherColumn.SortDirection = null;

            column.SortDirection = nextDirection;

            viewModel.Sort(columnName, ConvertToBoolean(nextDirection));

            return null;
        }

        private static DataGridSortDirection? GetNextSortingDirection(DataGridSortDirection? current)
        {
            var index = Array.IndexOf(SortingDirectionsOrder, current);

            index++;

            if (index >= SortingDirectionsOrder.Length) index = 0;

            return SortingDirectionsOrder[index];
        }

        private bool? ConvertToBoolean(DataGridSortDirection? direction)
        {
            bool? isAscending = null;

            if (direction.HasValue) isAscending = direction.Value == DataGridSortDirection.Ascending;

            return isAscending;
        }
    }
}
