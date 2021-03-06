﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using System.Windows.Input;
using System.Windows.Threading;
using CompleetKassa.ViewModels.Commands;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;


namespace CompleetKassa.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		public ObservableCollection<BaseViewModel> PageViewModels
		{
			get;
			private set;
		}


        public BaseViewModel DefaultViewModel { get; set; }

        BaseViewModel _currentPageViewModel;
		public BaseViewModel CurrentPageViewModel
		{
			get { return _currentPageViewModel; }
			private set
			{
				if (Equals (value, _currentPageViewModel)) return;
				_currentPageViewModel = value;
				OnPropertyChanged ();
			}
		}

		public ICommand OnChangePageCommand { get; private set; }


        private string textBinding;
        public string TextBinding
        {
            get { return textBinding; }
            set
            {
                textBinding = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel () : base ("Main","#fff", "Icons/product.png")
		{
            DefaultViewModel = new SalesViewModel();

			this.CreateContentViewModels ();

            _currentPageViewModel = DefaultViewModel;

            OnChangePageCommand = new BaseCommand (ChangePageCommand);

            TextBinding = "This is from view model to view";

        }
     
        void ChangePageCommand (object obj)
		{
			var page = (BaseViewModel)obj;

			if(page != CurrentPageViewModel) {
				CurrentPageViewModel = page;
			}
		}

		public void CreateContentViewModels ()
		{
			PageViewModels = new ObservableCollection<BaseViewModel>
			{
                new ProductsViewModel() {
                    OnClosePageCommand = new BaseCommand (ClosePage)
                },
                new CustomersViewModel {
					OnClosePageCommand = new BaseCommand (ClosePage)
				},
                   new TransactionsViewModel{
                    OnClosePageCommand = new BaseCommand (ClosePage)
                },

                         new TotalsViewModel{
					OnClosePageCommand = new BaseCommand (ClosePage)
				},
						 new UsersViewModel{
					OnClosePageCommand = new BaseCommand (ClosePage)
				},
						 new SettingsViewModel{
					OnClosePageCommand = new BaseCommand (ClosePage)
				},
						 new SupportViewModel{
					OnClosePageCommand = new BaseCommand (ClosePage)
				},
						 new LockViewModel{
					OnClosePageCommand = new BaseCommand (ClosePage)
				},
            };
		}

		private void ClosePage (object obj)
		{
            CurrentPageViewModel = DefaultViewModel;
		}
	}
}
