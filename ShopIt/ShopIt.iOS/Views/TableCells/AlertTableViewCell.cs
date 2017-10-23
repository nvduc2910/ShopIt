using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using ViewModels.Items;
using CoreGraphics;

namespace ShopIt.iOS.Views.TableCells
{
	public interface IAlertTableViewCellDelegate
	{
		void DidShowAllButtons(AlertTableViewCell cell);
	}

	public partial class AlertTableViewCell : MvxTableViewCell, IUIGestureRecognizerDelegate
	{
		public static readonly NSString Key = new NSString("AlertTableViewCell");
		public static readonly UINib Nib;

		public UIPanGestureRecognizer panRecognizer;
		private CGPoint panStartPoint;
		public nfloat startingRightLayoutConstraintConstant;

		public IAlertTableViewCellDelegate Delegate;

		public AlertItemViewModel Model { get; set; }
		private bool mExpanded = false;

		static AlertTableViewCell()
		{
			Nib = UINib.FromName("AlertTableViewCell", NSBundle.MainBundle);
		}

		protected AlertTableViewCell(IntPtr handle) : base(handle)
		{
			this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<AlertTableViewCell, AlertItemViewModel>();

				set.Bind(lbText).To(vm => vm.Alert.Title);
				set.Bind(btnDelete).To(vm => vm.DeleteAlertCommand);
				set.Bind(btnFlag).To(vm => vm.FlagAlertCommand);
				set.Bind(lbTime).To(vm => vm.Time);
				set.Bind(lbDescription).To(vm => vm.Alert.Body);
				set.Bind(ivRead).For(v => v.Hidden).To(vm => vm.IsRead);

				set.Apply();

			});
		}

		public static AlertTableViewCell Create()
		{
			return (AlertTableViewCell)Nib.Instantiate(null, null)[0];
		}

		public void CreatePangesture()
		{
			if (panRecognizer == null)
			{
				panRecognizer = new UIPanGestureRecognizer(PanThisCell);
				panRecognizer.Delegate = this;
				vMyContent.AddGestureRecognizer(panRecognizer);
			}
		}

		public override void PrepareForReuse()
		{
			base.PrepareForReuse();
			ResetConstraintsConstantsToZero(false);
		}

		#region Pan Gesture
		public override bool ShouldRecognizeSimultaneously(UIGestureRecognizer gestureRecognizer, UIGestureRecognizer otherGestureRecognizer)
		{
			//return base.ShouldRecognizeSimultaneously(gestureRecognizer, otherGestureRecognizer);
			if (gestureRecognizer is UIPanGestureRecognizer)
			{
				nfloat yVelocity = ((UIPanGestureRecognizer)gestureRecognizer).VelocityInView(gestureRecognizer.View).Y;

				return Math.Abs(yVelocity) <= 0.25;
			}


			return true;
		}

		public override bool GestureRecognizerShouldBegin(UIGestureRecognizer gestureRecognizer)
		{
			//return base.GestureRecognizerShouldBegin(gestureRecognizer);
			if(gestureRecognizer == panRecognizer) {
				CGPoint translation = ((UIPanGestureRecognizer)gestureRecognizer).TranslationInView(gestureRecognizer.View);
				return Math.Abs(translation.Y) <= Math.Abs(translation.X);
			} else {
				return true;
			}
		}

		void PanThisCell(UIPanGestureRecognizer panGesture)
		{
			switch (panRecognizer.State)
			{
				case UIGestureRecognizerState.Began:
					this.panStartPoint = panRecognizer.TranslationInView(vMyContent);

					startingRightLayoutConstraintConstant = contentViewRightConstraint.Constant;

					Console.WriteLine("Pan began at " + this.panStartPoint.X + " / " + this.panStartPoint.Y);

					break;
				case UIGestureRecognizerState.Changed:

					var currentPoint = panRecognizer.TranslationInView(vMyContent);

					var deltaX = currentPoint.X - this.panStartPoint.X;

					//cell.leftConstraint.Constant = deltaX;
					//cell.rightConstraint.Constant = (deltaX * -1);

					bool panningToLeft = false;

					if (currentPoint.X < this.panStartPoint.X)
					{
						panningToLeft = true;
					}

					if (this.startingRightLayoutConstraintConstant == 0)
					{
						if (panningToLeft == false)
						{
							nfloat constant = (nfloat)Math.Max(-deltaX, 0);
							if (constant == 0)
							{
								ResetConstraintsConstantsToZero();
							}
							else
							{

								contentViewRightConstraint.Constant = constant;
								UIView.Animate(0.1, () =>
								{
									LayoutIfNeeded();
								});
							}
						}
						else
						{
							nfloat constant = (nfloat)Math.Min(-deltaX, ButtonTotalWidth());
							if (constant == ButtonTotalWidth())
							{
								SetConstrainsToShowAllButtons();
							}
							else
							{
								contentViewRightConstraint.Constant = constant;
								UIView.Animate(0.1, () =>
								{
									LayoutIfNeeded();
								});
							}

						}
					}
					else
					{
						var adjustment = this.startingRightLayoutConstraintConstant - deltaX;
						if (panningToLeft == false)
						{
							nfloat constant = (nfloat)Math.Max(adjustment, 0);

							if (constant == 0)
							{
								this.ResetConstraintsConstantsToZero();

							}
							else
							{
								contentViewRightConstraint.Constant = constant;
								UIView.Animate(0.1, () =>
								{
									LayoutIfNeeded();
								});
							}

						}
						else
						{
							nfloat constant = (nfloat)Math.Min(adjustment, ButtonTotalWidth());

							if (constant == ButtonTotalWidth())
							{
								this.SetConstrainsToShowAllButtons();
							}
							else
							{
								contentViewRightConstraint.Constant = constant;
								UIView.Animate(0.1, () =>
								{
									LayoutIfNeeded();
								});
							}
						}
					}
					contentViewLeftConstraint.Constant = -contentViewRightConstraint.Constant;

					Console.WriteLine("Pan Move: " + deltaX);
					break;
				case UIGestureRecognizerState.Ended:

					if (this.startingRightLayoutConstraintConstant == 0)
					{
						nfloat halfOfDeleteButton = btnDelete.Frame.Size.Width;
						if (contentViewRightConstraint.Constant >= halfOfDeleteButton)
						{
							this.SetConstrainsToShowAllButtons();
						}
						else
						{
							this.ResetConstraintsConstantsToZero();
						}
					}
					else
					{
						nfloat flagButtonPlusHalfOfDeleteButton = btnDelete.Frame.Size.Width + (btnFlag.Frame.Size.Width / 2);
						if (contentViewRightConstraint.Constant >= flagButtonPlusHalfOfDeleteButton)
						{
							this.SetConstrainsToShowAllButtons();
						}
						else
						{
							this.ResetConstraintsConstantsToZero();
						}
					}

					Console.WriteLine("Pan ended");
					break;
				case UIGestureRecognizerState.Cancelled:
					Console.WriteLine("Pan Cancel");
					break;
				default:
					break;

			}
		}

		nfloat ButtonTotalWidth()
		{
			return btnFlag.Frame.Width * 2;
		}

		static nfloat BounceValue = 20f;
		public void ResetConstraintsConstantsToZero(bool animate = true)
		{

			if (this.startingRightLayoutConstraintConstant == 0 && contentViewRightConstraint.Constant == 0)
			{
				return;
			}

			//this.cell.rightConstraint.Constant = -BounceValue;
			//this.cell.leftConstraint.Constant = BounceValue;


			if (animate)
			{
				UIView.Animate(0.1, () =>
				{
					contentViewLeftConstraint.Constant = 0;
					contentViewRightConstraint.Constant = 0;

					LayoutIfNeeded();

				});
			}
			else
			{
				contentViewLeftConstraint.Constant = 0;
				contentViewRightConstraint.Constant = 0;
			}

			//UIView.Animate(0.1, () =>
			//{
			this.startingRightLayoutConstraintConstant = contentViewRightConstraint.Constant;
			//	LayoutIfNeeded();

			//});

			mExpanded = false;
		}

		void SetConstrainsToShowAllButtons()
		{
			if (this.startingRightLayoutConstraintConstant == ButtonTotalWidth() && contentViewRightConstraint.Constant == ButtonTotalWidth())
			{
				return;
			}

			//this.cell.leftConstraint.Constant = -ButtonTotalWidth() - BounceValue;
			//this.cell.rightConstraint.Constant = ButtonTotalWidth() + BounceValue;

			UIView.Animate(0.1, () =>
			{
				contentViewLeftConstraint.Constant = -ButtonTotalWidth();
				contentViewRightConstraint.Constant = ButtonTotalWidth();
				LayoutIfNeeded();
			});

			UIView.Animate(0.1, () =>
			{
				this.startingRightLayoutConstraintConstant = contentViewRightConstraint.Constant;
				LayoutIfNeeded();
			});

			if (Delegate != null)
			{
				Delegate.DidShowAllButtons(this);
			}

			mExpanded = true;
		}


		#endregion
	}
}
