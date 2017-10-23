using System;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Models;
using ViewModels;

namespace ShopIt.Core.ViewModels.Items
{
	public class PendingTradesItemViewModel : MvxViewModel
	{
		#region Constructor
		public PendingTradesItemViewModel(EditProjectViewModel editProjectViewModel, Invite pendingTrades)
		{
			this.EditProjectViewModel = editProjectViewModel;
			this.PendingTrades = pendingTrades;
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
		private Invite mPendingTrades;

		public Invite PendingTrades
		{
			get
			{
				return mPendingTrades;
			}
			set
			{
				mPendingTrades = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#endregion
		#region Commands 

		#region DeleteCurrentTradesCommand
		private MvxCommand mDeletePendingTradesCommand;

		public MvxCommand DeletePendingTradesCommand
		{
			get
			{
				if (mDeletePendingTradesCommand == null)
				{
					mDeletePendingTradesCommand = new MvxCommand(this.DeletePendingTrades);
				}
				return mDeletePendingTradesCommand;
			}
		}

		private void DeletePendingTrades()
		{
			EditProjectViewModel.PendingTradesItemViewModel = this;
			EditProjectViewModel.RemovePendingTradesCommand.Execute(this);
		}
		#endregion

		#endregion
	}
}
