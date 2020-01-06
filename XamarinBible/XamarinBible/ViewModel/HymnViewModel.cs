using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBible.Database;
using XamarinBible.DI.Service;
using XamarinBible.Model;

namespace XamarinBible.ViewModel
{
    public class HymnViewModel : CarouselViewModel
    {
        private ObservableCollection<HymnModel> hymnPages { get; set; }
        private HymnDataAccess dataAccess = null;

        public ICommand CategoryVisibilityCommand { get; private set; }
        public ICommand CategorySelectCommand { get; private set; }

        private bool categoryVisibility;
        public bool CategoryVisibility
        {
            get => categoryVisibility;
            set
            {
                categoryVisibility = value;
                OnPropertyChanged("CategoryVisibility");
            }
        }

        private IKoreanMatchService _matchService;
        public IKoreanMatchService MatchService
        {
            get => _matchService;
        }

        public HymnViewModel(IKoreanMatchService matchService) : base()
        {
            _matchService = matchService;
            dataAccess = HymnDataAccess.Instance();
            HymnPages = new ObservableCollection<HymnModel>();
            CategorySelectCommand = new Command<int>(SelectCategory);
            CategoryVisibilityCommand = new Command(ChangeCatoegoryVisibility);
            TabCount = 3;

            for (int i = 0; i < TabCount; i++)
            {
                HymnModel model;
                if (i==0)
                {
                    model = new HymnDialModel(this);
                }
                else if(i==1)
                {
                    model = new HymnListModel(this);
                    ((HymnListModel)model).Items = dataAccess.GetList();
                }
                else
                {
                    model = new HymnAlbumModel(this);
                    ((HymnAlbumModel)model).Items = dataAccess.GetAlbumList(); 
                }
                model.Position = i;
                HymnPages.Add(model);
            }
        }

        public ObservableCollection<HymnModel> HymnPages
        {
            get 
            {
                return hymnPages;
            }
            set
            {
                hymnPages = value;
            }
        }

        private void SelectCategory(int param)
        {
            switch(param)
            {
                case 1:
                    dataAccess.SetTableName(1);
                    break;
                case 2:
                    dataAccess.SetTableName(2);
                    break;
                case 3:
                    dataAccess.SetTableName(3);
                    break;
            }
            ((HymnListModel)HymnPages[1]).Items = dataAccess.GetList();
            CategoryVisibility = !CategoryVisibility;
        }
        private void ChangeCatoegoryVisibility()
        {
            CategoryVisibility = !CategoryVisibility;
        }
    }
}
