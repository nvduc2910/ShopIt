using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Platform;

namespace ShopIt.Core.Services
{
	public interface ITaskManagementService
	{
		CancelableTask AddTask(Task task, CancellationTokenSource cancellationTokenSource);
		void CancelAllTask();
		bool IsAllTaskDone();
		void CancelTask(CancelableTask task);
		int NumberOfTasksRunning();
	}

	public class CancelableTask
	{
		public Task Task { get; set; }
		public CancellationTokenSource CancelTokenSource { get; set; }
	}

	public class TaskManagementService : ITaskManagementService
	{
		private List<CancelableTask> mTaskList = new List<CancelableTask>();

		public CancelableTask AddTask(Task task, CancellationTokenSource cancellationTokenSource)
		{
			CancelableTask cancelableTask = new CancelableTask();
			cancelableTask.Task = task;
			cancelableTask.CancelTokenSource = cancellationTokenSource;
			mTaskList.Add(cancelableTask);
			Mvx.Resolve<IPlatformService>().ShowNetworkIndicator();

			try
			{
				task.ContinueWith( (Task arg1, object arg2) =>  
				{
					mTaskList.Remove(cancelableTask);
					CheckIfNeedHideNetworkIndicator();

				}, TaskContinuationOptions.NotOnCanceled);
			}
			catch (Exception ex)
			{
				mTaskList.Remove(cancelableTask);
				CheckIfNeedHideNetworkIndicator();
			}

			return cancelableTask;
		}

		public void CancelAllTask()
		{
			for (int i = mTaskList.Count-1; i >= 0; i++)
			{
				var task = mTaskList[i];
				task.CancelTokenSource.Cancel();
			}
			CheckIfNeedHideNetworkIndicator();
		}

		public void CancelTask(CancelableTask task)
		{
			if (task.CancelTokenSource != null && !task.CancelTokenSource.IsCancellationRequested)
			{
				task.CancelTokenSource.Cancel();
			}

			if (mTaskList.Contains(task))
			{
				mTaskList.Remove(task);
			}
			CheckIfNeedHideNetworkIndicator();
		}

		public bool IsAllTaskDone()
		{
			return mTaskList.Count == 0;
		}

		public int NumberOfTasksRunning()
		{
			return mTaskList.Count;
		}

		private void CheckIfNeedHideNetworkIndicator()
		{
			if (IsAllTaskDone())
			{
				Mvx.Resolve<IPlatformService>().HideNetworkIndicator();
			}
		}
	}
}
