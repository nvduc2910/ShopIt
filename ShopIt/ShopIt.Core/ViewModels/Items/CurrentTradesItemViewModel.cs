using System;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Models;

namespace ViewModels.Items
{
	public class CurrentTradesItemViewModel : MvxViewModel
	{
		#region Constructor
		public CurrentTradesItemViewModel(EditProjectViewModel editProjectViewModel, Invite currentTrades)
		{
			this.EditProjectViewModel = editProjectViewModel;
			this.CurrentTrades = currentTrades;
		}
		#endregion

		#region Properties

		#region EditProjectViewModel
		private EditProjectViewModel mEditProjectViewModel;

		public EditProjectViewModel EditProjectViewModel
		{
			get
			{
				return mEditProjectViewModel;
			}
			set
			{
				mEditProjectViewModel = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region CurrentTrades
        private Invite mCurrentTrades;

		public Invite CurrentTrades
		{
			get
			{
				return mCurrentTrades;
			}
			set
			{
				mCurrentTrades = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#endregion

		#region Commands 

		#region DeleteCurrentTradesCommand
		private MvxCommand mDeleteCurrentTradesCommand;

		public MvxCommand DeleteCurrentTradesCommand
		{
			get
			{
				if (mDeleteCurrentTradesCommand == null)
				{
					mDeleteCurrentTradesCommand = new MvxCommand(this.DeleteCurrentTrades);
				}
				return mDeleteCurrentTradesCommand;
			}
		}

		private void DeleteCurrentTrades()
		{
			EditProjectViewModel.CurrentTradesItemViewModel = this;
			EditProjectViewModel.RemoveCurrentTradesCommand.Execute(this);
		}
		#endregion

		#endregion
	}
}
