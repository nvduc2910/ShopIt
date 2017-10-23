using MvvmCross.Core.ViewModels;
using ShopIt.Core;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;

public enum TourViewMode
{
	FirstTime,
	Menu
}

public class TourViewModel : BaseViewModel
{
	#region Constructors
	public TourViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
	{
	}

	#endregion

	#region Properties

	#region Mode
	private TourViewMode mMode;

	public TourViewMode Mode
	{
		get
		{
			return mMode;
		}
		set
		{
			mMode = value;
			RaisePropertyChanged();
		}
	}
	#endregion

	#region CurrentPage
	private int mCurrentPage = 0;

	public int CurrentPage
	{
		get
		{
			return mCurrentPage;
		}
		set
		{
			mCurrentPage = value;
			if (mCurrentPage < 2)
				ButtonTitle = "Skip";
			else {
				ButtonTitle = "Done";
			}
			RaisePropertyChanged();
		}
	}
	#endregion

	#region Title
    private string mButtonTitle = "Skip";

	public string ButtonTitle
	{
		get
		{
			return mButtonTitle;
		}
		set
		{
			mButtonTitle = value;
			RaisePropertyChanged();
		}
	}
	#endregion

	#endregion

	#region Init
	public void Init(TourViewMode mode)
	{
		this.Mode = mode;
	}
	#endregion

	#region Commands

	#region SkipCommand
    private MvxCommand mSkipCommand;

	public MvxCommand SkipCommand
	{
		get
		{
			if (mSkipCommand == null)
			{
				mSkipCommand = new MvxCommand(this.Skip);
			}
			return mSkipCommand;
		}
	}


	private void Skip()
	{
		if (Mode == TourViewMode.FirstTime)
		{
			ShowViewModel<LoginViewModel>();
		}
		else
		{
			Close(this);
		}
	}
	#endregion

	#endregion

	#region Methods

	#endregion
}