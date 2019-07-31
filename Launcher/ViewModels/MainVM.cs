using Launcher.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Launcher.WPF.ViewModels
{
    class MainVM : BindableBase
    {
        private Project currentProject;

        public ObservableCollection<Project> Projects { get; set; } = new ObservableCollection<Project>();

        public Project CurrentProject
        {
            get => currentProject; set
            {
                currentProject = value;
                RaisePropertyChanged();
            }
        }

        public MainVM()
        {
            Projects.Add(new Project { Title = "Проект 1" });
            Projects.Add(new Project { Title = "Проект 12" });
            Projects.Add(new Project { Title = "Проект 123" });
            Projects.Add(new Project { Title = "Проект 123" });
        }
    }
}
