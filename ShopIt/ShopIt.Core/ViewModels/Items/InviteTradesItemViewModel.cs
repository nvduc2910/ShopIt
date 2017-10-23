using System;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Models;

namespace ViewModels.Items
{
	public class InviteTradesItemViewModel : MvxViewModel
	{

		#region Constructor
		public InviteTradesItemViewModel()
		{
			
		}

		#endregion

		#region Properties

		#region EditProjectViewModel 		private EditProjectViewModel mEditProjectViewModel;  		public EditProjectViewModel EditProjectViewModel
		{ 			get
			{ 				return mEditProjectViewModel; 			} 			set
			{ 				mEditProjectViewModel = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region InviteTrades         private Invite mInviteTrades;  		public Invite InviteTrades
		{ 			get
			{ 				return mInviteTrades; 			} 			set
			{ 				mInviteTrades = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#endregion


		#region Commands



		#endregion


		#region Methods


		#endregion
	}
}
