using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Models;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels.Items;
using ViewModels;
using ViewModels.Items;

namespace ShopIt.Core.ViewModels
{
	public class ProjectsViewModel : BaseViewModel
	{
		#region Constructors
		public ProjectsViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
		}

		#endregion

		#region HomeVM

		public HomeViewModel HomeVM { get; set; }

		#endregion

		#region Properties

		#region Projects
		private List<ProjectHeading> mProjects;
		#endregion

		#region ProjectsCreated
		private MvxObservableCollection<ProjectItemViewModel> mProjectsCreated = new MvxObservableCollection<ProjectItemViewModel>();

		public MvxObservableCollection<ProjectItemViewModel> ProjectsCreated
		{
			get
			{
				return mProjectsCreated;
			}
			set
			{
				mProjectsCreated = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region ProjectsJoined
		private MvxObservableCollection<ProjectItemViewModel> mProjectsJoined = new MvxObservableCollection<ProjectItemViewModel>();

		public MvxObservableCollection<ProjectItemViewModel> ProjectsJoined
		{
			get
			{
				return mProjectsJoined;
			}
			set
			{
				mProjectsJoined = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region HaveProjectsCreated
		private bool mHaveProjectsCreated = true;

		public bool HaveProjectsCreated
		{
			get
			{
				return mHaveProjectsCreated;
			}
			set
			{
				mHaveProjectsCreated = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#region HaveProjectsJoined
		private bool mHaveProjectsJoined = true;

		public bool HaveProjectsJoined
		{
			get
			{
				return mHaveProjectsJoined;
			}
			set
			{
				mHaveProjectsJoined = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#endregion

		#region Init
		public void Init()
		{

		}
		#endregion

		#region Commands

		#region AddProjectCommand
		private MvxCommand mAddProjectCommand;

		public MvxCommand AddProjectCommand
		{
			get
			{
				if (mAddProjectCommand == null)
				{
					mAddProjectCommand = new MvxCommand(this.AddProject);
				}
				return mAddProjectCommand;
			}
		}

		private void AddProject()
		{
			if (HomeVM.ProfileVM.UserProfileCompleted())
			{
				mCacheService.ProjectItems = ProjectsCreated;

				ShowViewModel<CreateProjectViewModel>();
			}
			else
			{
				mMessageboxService.ShowToast("You must complete your personal profile first!");
			}
		}
		#endregion

		#region ShowMenuCommand
		private MvxCommand mShowMenuCommand;

		public MvxCommand ShowMenuCommand
		{
			get
			{
				if (mShowMenuCommand == null)
				{
					mShowMenuCommand = new MvxCommand(this.ShowMenu);
				}
				return mShowMenuCommand;
			}
		}

		private void ShowMenu()
		{
			HomeVM.MainVM.ShowMenuCommand.Execute();
		}
		#endregion

		#region SelectProjectItemCommand
		private MvxCommand<ProjectItemViewModel> mSelectProjectItemCommand;

		public MvxCommand<ProjectItemViewModel> SelectProjectItemCommand
		{
			get
			{
				if (mSelectProjectItemCommand == null)
				{
					mSelectProjectItemCommand = new MvxCommand<ProjectItemViewModel>(this.SelectProjectItem);
				}
				return mSelectProjectItemCommand;
			}
		}

		private void SelectProjectItem(ProjectItemViewModel projectItem)
		{
			mCacheService.ProjectHeadingItem = projectItem;
			ShowViewModel<ViewProjectViewModel>();
		}
		#endregion

		#endregion

		#region Methods

		#region LoadData
		public async void LoadData()
		{
			mPlatformService.ShowNetworkIndicator();
			mProgressDialogService.ShowProgressDialog();

			var response = await mApiService.GetProjectList();

			mPlatformService.HideNetworkIndicator();
			mProgressDialogService.HideProgressDialog();

			ProjectsCreated.Clear();
			ProjectsJoined.Clear();

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				mProjects = new List<ProjectHeading>(response.Data);
				mProjects.Sort((ProjectHeading x, ProjectHeading y) => 0 - x.CreatedJoined.CompareTo(y.CreatedJoined));

				var createds = mProjects.Where(x => x.UserIsCreator == true);

				if (createds != null)
				{
					foreach (var item in createds)
					{
						var itemModel = new ProjectItemViewModel(item);
						ProjectsCreated.Add(itemModel);
					}
				}

				HaveProjectsCreated = ProjectsCreated.Count > 0;

				var joineds = mProjects.Where(x => x.UserIsCreator == false);
				if (joineds != null)
				{
					foreach (var item in joineds)
					{
						var itemModel = new ProjectItemViewModel(item);
						ProjectsJoined.Add(itemModel);
					}
				}

				HaveProjectsJoined = ProjectsJoined.Count > 0;
			}
			else
			{
				HaveProjectsCreated = false;
				HaveProjectsJoined = false;
			}
		}
		#endregion

		#region RemoveProject

		public void RemoveProject(long projectId)
		{
			mProjects.RemoveAll((obj) => obj.ProjectId == projectId);
			ProjectsCreated.Remove(s => s.ProjectHeading.ProjectId == projectId);
			HaveProjectsCreated = ProjectsCreated.Count > 0;
		}

		#endregion

		#region UpdateProject

		public void UpdateProject(Project project)
		{
			var matchProjectsCreate = ProjectsCreated.Where(s => s.ProjectHeading.ProjectId == project.Id);
			if (matchProjectsCreate != null && matchProjectsCreate.Any())
			{
				var matchProject = matchProjectsCreate.First();
				matchProject.ProjectHeading.Title = project.Title;
				matchProject.ProjectHeading.Stage = project.Stage;
				//matchProject.RaisePropertyChanged("ProjectHeading");
			}
		}

		#endregion

		#region AddProjectHeading

		public void AddProjectHeading(ProjectHeading projectHeading)
		{
			mProjects.Insert(0, projectHeading);
			var itemModel = new ProjectItemViewModel(projectHeading);
			ProjectsCreated.Insert(0, itemModel);
			HaveProjectsCreated = true;
		}

		#endregion

		#endregion
	}
}
