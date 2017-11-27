using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OGameWorker.Code;
using Worker.Objects.Structures.Buildings;

namespace OGameWorker.Views.Main.Buildings
{
    public class BuildingsViewModel : OGameWorkerBaseViewModel
    {
        private ObservableCollection<BuildingBase> _buildingsQueue = new ObservableCollection<BuildingBase>();
        public ObservableCollection<BuildingBase> BuildingsQueue
        {
            get => _buildingsQueue;
            set => _buildingsQueue = value;
        }
    }
}
