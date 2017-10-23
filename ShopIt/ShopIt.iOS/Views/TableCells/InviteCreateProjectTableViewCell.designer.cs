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
    [Register ("InviteCreateProjectTableViewCell")]
    partial class InviteCreateProjectTableViewCell
    {
        [Outlet]
        UIKit.UIButton btnRemoveInvite { get; set; }


        [Outlet]
        UIKit.UILabel lbEmailName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnRemoveInvite != null) {
                btnRemoveInvite.Dispose ();
                btnRemoveInvite = null;
            }

            if (lbEmailName != null) {
                lbEmailName.Dispose ();
                lbEmailName = null;
            }
        }
    }
}