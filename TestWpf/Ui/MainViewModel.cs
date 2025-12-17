using Esapi.Interfaces;
using Esapi.Services;
using Esapi.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace TestWpf.Ui {
    public class MainViewModel : ObservableObject {
        private readonly IEsapiService _service;

        private bool _isBusy;
        public bool IsBusy {
            get => _isBusy;
            set {
                SetProperty(ref _isBusy, value);
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }
        public bool IsNotBusy => !IsBusy;

        public ObservableCollection<IPlanSetup> Plans { get; }
        public ObservableCollection<IStructure> Structures { get; }
        public ObservableCollection<string> StructureDetails { get; }

        private IPatient _patient;
        private IPlanSetup _selectedPlan;
        private IStructure _selectedStructure;
        private MeshGeometry3D _geometry;

        public IPatient Patient {
            get => _patient;
            set => SetProperty(ref _patient, value);
        }

        public IPlanSetup SelectedPlan {
            get => _selectedPlan;
            set {
                if (SetProperty(ref _selectedPlan, value) && value != null) {
                    _ = LoadStructuresAsync();
                }
            }
        }

        public IStructure SelectedStructure {
            get => _selectedStructure;
            set {
                if (SetProperty(ref _selectedStructure, value) && value != null) {
                    _ = LoadStructureDetailsAsync();
                }
            }
        }

        // --- Commands --- //
        public ICommand LoadPatientCommand { get; }
        public ICommand SaveAndCloseCommand { get; }
        public ICommand CloseCommand { get; }

        // --- Constructor -- //
        public MainViewModel(IEsapiService service) {
            Plans = new ObservableCollection<IPlanSetup>();
            Structures = new ObservableCollection<IStructure>();
            StructureDetails = new ObservableCollection<string>();

            _service = service;

            LoadPatientCommand = new RelayCommand(async () => await LoadPatientAsync(), () => IsNotBusy);

            // Save, then Close
            SaveAndCloseCommand = new RelayCommand(async windowObj =>
            {
                try
                {
                    IsBusy = true;
                    // 1. Trigger the Save on the ESAPI Thread
                    await _service.SavePatientAsync();
                    await _service.ClosePatient();

                    // 2. Close the Window (triggers StandaloneRunner shutdown)
                    (windowObj as Window)?.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving: {ex.Message}");
                }
                finally
                {
                    IsBusy = false;
                }
            });

            // Just Close
            CloseCommand = new RelayCommand(windowObj =>
            {
                // No save, just exit
                (windowObj as Window)?.Close();
            });
        }

        private async Task LoadPatientAsync() {
            IsBusy = true;
            ClearAll();
            try {
                // This call goes to the "post office" and returns a Task
                Patient = await _service.GetPatientAsync();

                // Now that we have the patient, get the plans
                // example of using RunAsync with a lambda
                var courses = await Patient.GetCoursesAsync(); // .RunAsync(patient =>
                var plans = await Patient.RunAsync(p => p.Courses
                    .SelectMany(c => c.PlanSetups)
                    .Select(pp => (IPlanSetup)new AsyncPlanSetup(pp, _service))
                    .ToList());

                foreach (var plan in plans) {
                    Plans.Add(plan);
                }
            } catch (Exception ex) {
                System.Windows.MessageBox.Show($"Error loading patient: {ex.Message}");
            } finally {
                IsBusy = false;
            }
        }

        private async Task LoadStructuresAsync() {
            if (SelectedPlan is null) return;

            IsBusy = true;
            Structures.Clear();
            StructureDetails.Clear();
            try {
                // Get the StructureSet
                var ss = await SelectedPlan.RunAsync(plan => {
                    if (plan.StructureSet is null) { return null; }
                    return (IStructureSet)new AsyncStructureSet(plan.StructureSet, _service);
                });
                if (ss != null) {
                    // Get the non-empty structures
                    var structures = await ss.GetStructuresAsync();
                    foreach (var s in structures) {
                        Structures.Add(s);
                    }
                }
            } catch (Exception ex) {
                System.Windows.MessageBox.Show($"Error loading structures: {ex.Message}");
            } finally {
                IsBusy = false;
            }
        }

        private async Task LoadStructureDetailsAsync() {
            if (SelectedStructure == null) return;

            IsBusy = true;
            StructureDetails.Clear();
            try {
                // Make two *parallel* async calls
                // These will be queued one after another on the
                // ESAPI thread, but from the UI's point of view,
                // we just 'await' them both.
                var volumeTask = SelectedStructure.RunAsync(s => s.Volume);
                var colorTask = SelectedStructure.RunAsync(s => s.Color);

                await Task.WhenAll(volumeTask, colorTask);

                var volume = volumeTask.Result;
                var color = colorTask.Result;

                StructureDetails.Add($"ID: {SelectedStructure.Id}");
                StructureDetails.Add($"Volume: {volume:F2} cc");
                StructureDetails.Add($"Color: {color}");

                // Cross-thread access testing
                // this MeshGeometry belongs on the ESAPI thread
                // if our library is working, it'll freeze the mesh before sending it
                var geometry = await SelectedStructure.GetMeshGeometryAsync();
                if (geometry is null || !geometry.Positions.Any()) { return; } // if thread-locked, this is fault
                _geometry = geometry;
            } catch (Exception ex) {
                System.Windows.MessageBox.Show($"Error loading details: {ex.Message}");
            } finally {
                IsBusy = false;
            }
        }

        private async Task SaveAndClose()
        {

        }

        private void ClearAll() {
            Patient = null;
            Plans.Clear();
            Structures.Clear();
            StructureDetails.Clear();
        }
    }
}
