// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace ShopIt.iOS.Views.TableCells
{
    [Register ("CurrentTradesCompany")]
    partial class CurrentTradesCompany
    {
        [Outlet]
        UIKit.UIButton btnDeleteInvitee { get; set; }


        [Outlet]
        UIKit.UILabel lbInviteeName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnDeleteInvitee != null) {
                btnDeleteInvitee.Dispose ();
                btnDeleteInvitee = null;
            }

            if (lbInviteeName != null) {
                lbInviteeName.Dispose ();
                lbInviteeName = null;
            }
        }
    }
}