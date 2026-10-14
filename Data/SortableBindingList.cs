using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ProductClass.Data
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool _isSorted;
        private ListSortDirection _sortDirection;
        private PropertyDescriptor _sortProperty;

        public SortableBindingList() : base() { }
        public SortableBindingList(IList<T> list) : base(list) { }

        //  Tell Grid that it can sort
        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => _isSorted;
        protected override ListSortDirection SortDirectionCore => _sortDirection;
        protected override PropertyDescriptor SortPropertyCore => _sortProperty;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            var innerList = Items as List<T>;
            if (innerList == null) return;

            //  Sort a copy by the property's value
            var sorted = direction == ListSortDirection.Ascending
                ? innerList.OrderBy(x => prop.GetValue(x)).ToList()
                : innerList.OrderByDescending(x => prop.GetValue(x)).ToList();

            //  Put the sorted items back
            innerList.Clear();
            innerList.AddRange(sorted);

            //  Remember the state so the grid can draw the sort arrow
            _sortProperty = prop;
            _sortDirection = direction;
            _isSorted = true;

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void RemoveSortCore()
        {
            _isSorted = false;
            _sortProperty = null;
        }
    }
}