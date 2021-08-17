using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Timerom.App.ViewModels.Category
{
    public class CategoriesViewModel : INotifyPropertyChanged
    {
        public IList<Model.Category> ProductiveCategories { get; private set; }

        public CategoriesViewModel()
        {
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
