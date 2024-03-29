﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyTracker.UI.MVVM
{
    public class TaskWatcher<TResult> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Task<TResult> Task { get; private set; }


        public TaskWatcher(Task<TResult> task)
        {
            Task = task;
            if (!task.IsCompleted)
            {
                WatchTaskAsync(task);
            }
        }

        private async Task WatchTaskAsync(Task task)
        {
            try
            {
                await task;
            }
            catch (Exception)
            {

            }

            //if no error occurs these task properties will change
            RaisePropertyChanged(this, "Status");
            RaisePropertyChanged(this, "IsCompleted");
            RaisePropertyChanged(this, "IsNotCompleted");

            if (task.IsFaulted)
            {
                RaisePropertyChanged(this, "IsFaulted");
                RaisePropertyChanged(this, "Exception");
                RaisePropertyChanged(this, "InnerException");
                RaisePropertyChanged(this, "ErrorMessage");
            }

            else if (task.IsCanceled)
            {
                RaisePropertyChanged(this, "IsCanceled");
            }

            else
            {
                RaisePropertyChanged(this, "IsSuccessfullyCompleted");
                RaisePropertyChanged(this, "Result");
            }
        }

        public void RaisePropertyChanged(object sender, string property)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(sender, new PropertyChangedEventArgs(property));
        }

        public TResult Result
        {
            get
            {
                return (Task.Status == TaskStatus.RanToCompletion) ? Task.Result : default(TResult);
            }
        }

    }
}
