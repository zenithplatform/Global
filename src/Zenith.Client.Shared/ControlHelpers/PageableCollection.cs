﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Client.Shared.ControlHelpers
{
    /*
        PageableCollection class for making Item controls pageable.
        Taken from : https://gist.github.com/dimitrispaxinos/5decda1d12a2de89b3cf#file-pageablecollection-cs
        Credits    : Dimitris Paxinos (https://gist.github.com/dimitrispaxinos)
    */
    public class PageableCollection<T> : INotifyPropertyChanged
    {
        #region Public Properties

        // Default Entries per page Number
        private int _pageSize = 5;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (_pageSize != value)
                {
                    _pageSize = value;
                    SendPropertyChanged(() => PageSize);
                    Reset();
                }
            }
        }

        public int TotalPagesNumber
        {
            get
            {
                if (AllObjects != null && PageSize > 0)
                {
                    return (AllObjects.Count - 1) / PageSize + 1;
                }
                return 0;
            }
        }

        private int _currentPageNumber = 1;
        public int CurrentPageNumber
        {
            get
            {
                return _currentPageNumber;
            }

            protected set
            {
                if (_currentPageNumber != value)
                {
                    _currentPageNumber = value;
                    SendPropertyChanged(() => CurrentPageNumber);
                }
            }
        }

        private ObservableCollection<T> _currentPageItems;
        public ObservableCollection<T> CurrentPageItems
        {
            get
            {
                return _currentPageItems;
            }
            private set
            {
                if (_currentPageItems != value)
                {
                    _currentPageItems = value;
                    SendPropertyChanged(() => CurrentPageItems);
                }
            }
        }

        #endregion

        #region Protected Properties

        protected ObservableCollection<T> AllObjects { get; set; }

        #endregion

        #region ctor

        protected PageableCollection()
        {
        }

        public PageableCollection(IEnumerable<T> allObjects, int? entriesPerPage = null)
        {
            AllObjects = new ObservableCollection<T>(allObjects);
            if (entriesPerPage != null)
                PageSize = (int)entriesPerPage;
            Calculate(CurrentPageNumber);
        }

        #endregion

        #region Public Methods

        public void GoToNextPage()
        {
            if (CurrentPageNumber != TotalPagesNumber)
            {
                CurrentPageNumber++;
                Calculate(CurrentPageNumber);
            }
        }

        public void GoToPreviousPage()
        {
            if (CurrentPageNumber > 1)
            {
                CurrentPageNumber--;
                Calculate(CurrentPageNumber);
            }
        }

        public virtual void Remove(T item)
        {
            AllObjects.Remove(item);

            // Update the total number of pages
            SendPropertyChanged(() => TotalPagesNumber);

            // if the last item on the last page is removed
            if (CurrentPageNumber > TotalPagesNumber)
                CurrentPageNumber--;

            Calculate(CurrentPageNumber);
        }

        public virtual void Add(T item)
        {
            // Go back to the first page
            CurrentPageNumber = 1;
            Calculate(CurrentPageNumber);

            // Keep the same size and put it on top
            CurrentPageItems.RemoveAt(CurrentPageItems.Count - 1);
            CurrentPageItems.Insert(0, item);

            // Add it to the original collection
            AllObjects.Add(item);
            SendPropertyChanged(() => TotalPagesNumber);
        }

        #endregion

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        public void SendPropertyChanged<T>(Expression<Func<T>> expression)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(((MemberExpression)expression.Body).Member.Name));
            }
        }

        #endregion

        #region Private Methods

        protected void Calculate(int pageNumber)
        {
            int upperLimit = pageNumber * PageSize;

            CurrentPageItems =
                new ObservableCollection<T>(
                    AllObjects.Where(x => AllObjects.IndexOf(x) > upperLimit - (PageSize + 1) && AllObjects.IndexOf(x) < upperLimit));
        }

        private void Reset()
        {
            CurrentPageNumber = 1;
            Calculate(CurrentPageNumber);
            SendPropertyChanged(() => TotalPagesNumber);
        }

        #endregion
    }
}
