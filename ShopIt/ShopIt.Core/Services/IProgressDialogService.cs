using System;
namespace ShopIt.Core.Services
{
	public interface IProgressDialogService
	{
		void ShowProgressDialog(string message = "");

		void HideProgressDialog();
	}
}
