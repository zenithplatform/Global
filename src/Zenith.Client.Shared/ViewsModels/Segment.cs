using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zenith.Client.Shared.ViewModel
{
    public class Segment:INotifyPropertyChanged
    {
        private ObservableCollection<SegmentPart> _parts = null;

        public Segment()
        {
            _parts = new ObservableCollection<SegmentPart>();
        }

        public string Title { get; set; }

        public ObservableCollection<SegmentPart> Parts
        {
            get { return _parts; }
            set { _parts = value; }
        }

        public Visibility SegmentVisible { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class SegmentPart:INotifyPropertyChanged
    {
        string _value = "";

        public string Caption { get; set; }

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyPropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }

    public interface ISegmentPresenter<T> where T : Segment
    {
        T CreateSegment();
    }

    public interface ISegmentValuesPresenter<T> where T : SegmentPart
    {
        ObservableCollection<SegmentPart> CreateSegmentValues();
    }
}
