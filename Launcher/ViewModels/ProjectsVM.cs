using Launcher.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Launcher.WPF.ViewModels
{
    class ProjectsVM : BindableBase
    {
        ObservableCollection<Project> Projects = new ObservableCollection<Project>();
        public ProjectsVM()
        {
            //projects.Add(new Project { Title = "Проект 1" });
            //projects.Add(new Project { Title = "Проект 12" });
            //projects.Add(new Project { Title = "Проект 123" });
        }
    }
}
