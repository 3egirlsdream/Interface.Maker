using System;
using System.Collections.Generic;
using Xu.Common;

namespace GenerateToolbox
{
    public class TABLE_COLUMNS : ValidationBase
    {
        public string COLUMN_NAME { get; set; }
        public string DATA_TYPE { get; set; }
        public string TABLE_NAME { get; set; }
        public string COMMENTS { get; set; }
        public string OWNER { get; set; }

        public DateTime? START_TIME { get; set; }
        public DateTime?  END_TIME { get; set; }
        private bool isEnabled;
        public bool IsEnabled 
        { 
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
                NotifyPropertyChanged(nameof(IsEnabled));
            }
        } 
        private bool? isChecked;
        public bool? IsChecked 
        { 
            get
            {
                return isChecked;
            } 
            set
            {
                isChecked = value;
                NotifyPropertyChanged(nameof(IsChecked));
                
            }
        }


        /// <summary>
        ///
        /// </summary>
        private List<string> _ComboBoxSource = new List<string>
        {
            "GUID",
            "DateTime.Now",
            "CONST"
        };
        public List<string> ComboBoxSource
        {
            get
            {
                return _ComboBoxSource;
            }
            set
            {
                _ComboBoxSource = value;
                NotifyPropertyChanged("ComboBoxSource");
            }
        }

        /// <summary>
        ///
        /// </summary>
        private string _SelectedItem;
        public string SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                NotifyPropertyChanged("SelectedItem");
                IsEnabled = value != "DateTime.Now" && DATA_TYPE == "DATE";
                if(value == "CONST")
                {
                    Visibility = "Visible";
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        private string _CONST_STRING;
        public string CONST_STRING
        {
            get
            {
                return _CONST_STRING;
            }
            set
            {
                _CONST_STRING = value;
                NotifyPropertyChanged("CONST_STRING");
            }
        }

        /// <summary>
        ///
        /// </summary>
        private string _Visibility = "Collapsed";
        public string Visibility
        {
            get
            {
                return _Visibility;
            }
            set
            {
                _Visibility = value;
                NotifyPropertyChanged("Visibility");
            }
        }
    }
}