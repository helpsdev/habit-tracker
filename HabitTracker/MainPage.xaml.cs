using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HabitTracker
{
    public partial class MainPage : ContentPage
    {
        public static ObservableCollection<Habit> HabitList { get; set; } = [];

        public MainPage()
        {
            InitializeComponent();
        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            HabitName.Text = string.Empty;
            ClearFrequencyCheckboxes();

        }

        private void ClearFrequencyCheckboxes()
        {
            MondayCheckbox.IsChecked =
            TuesdayCheckbox.IsChecked =
            WednesdayCheckbox.IsChecked =
            ThursdayCheckbox.IsChecked =
            FridayCheckbox.IsChecked =
            SaturdayCheckbox.IsChecked =
            SundayCheckbox.IsChecked = false;
        }

        private void SetFrequencyCheckboxes(IEnumerable<DayOfWeek> frequency)
        {
            SundayCheckbox.IsChecked = frequency.Contains(DayOfWeek.Sunday);
            MondayCheckbox.IsChecked = frequency.Contains(DayOfWeek.Monday); ;
            TuesdayCheckbox.IsChecked = frequency.Contains(DayOfWeek.Tuesday); ;
            WednesdayCheckbox.IsChecked = frequency.Contains(DayOfWeek.Wednesday); ;
            ThursdayCheckbox.IsChecked = frequency.Contains(DayOfWeek.Thursday); ;
            FridayCheckbox.IsChecked = frequency.Contains(DayOfWeek.Friday); ;
            SaturdayCheckbox.IsChecked = frequency.Contains(DayOfWeek.Saturday); 
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Habit? habitToAdd = new() { Name = HabitName.Text, Id = Guid.NewGuid() };

            habitToAdd.Frequency = GetFrequency();

            HabitList.Add(habitToAdd);

            HabitName.Text = string.Empty;

            ClearFrequencyCheckboxes();
        }

        private IEnumerable<DayOfWeek> GetFrequency()
        {
            var frequency = new List<DayOfWeek>();

            if (MondayCheckbox.IsChecked)
            {
                frequency.Add(DayOfWeek.Monday);
            }

            if (TuesdayCheckbox.IsChecked)
            {
                frequency.Add(DayOfWeek.Tuesday);
            }

            if (WednesdayCheckbox.IsChecked)
            {
                frequency.Add(DayOfWeek.Wednesday);
            }

            if (ThursdayCheckbox.IsChecked)
            {
                frequency.Add(DayOfWeek.Thursday);
            }

            if (FridayCheckbox.IsChecked)
            {
                frequency.Add(DayOfWeek.Friday);
            }

            if (SaturdayCheckbox.IsChecked)
            {
                frequency.Add(DayOfWeek.Saturday);
            }

            if (SundayCheckbox.IsChecked)
            {
                frequency.Add(DayOfWeek.Sunday);
            }

            return frequency;
        }

        private void EditButton_Clicked(object sender, EventArgs e)
        {
            Habit? habitToEdit = HabitName.BindingContext as Habit;
            
            if (habitToEdit != null)
            {
                var index = HabitList.IndexOf(habitToEdit);
                if (index >= 0)
                {
                    habitToEdit.Frequency = GetFrequency();
                    habitToEdit.Name = HabitName.Text;

                    HabitList[index] = habitToEdit;
                }
            }
            HabitName.Text = string.Empty;

            AddButton.IsVisible = true;
            ClearButton.IsVisible = true;
            CancelButton.IsVisible = false;
            EditButton.IsVisible = false;
            ClearFrequencyCheckboxes();
        }
        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            ClearFrequencyCheckboxes();
            HabitName.Text = string.Empty;

            AddButton.IsVisible = true;
            EditButton.IsVisible = false;
            ClearButton.IsVisible = true;
            CancelButton.IsVisible = false;

        }
        private void OnDeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem itemClicked = (MenuItem)sender;
            Habit habitToDelete = (Habit)itemClicked.BindingContext;
            HabitList.Remove(habitToDelete);
        }

        private void OnEditMenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem itemClicked = (MenuItem)sender;
            Habit habitToEdit = (Habit)itemClicked.BindingContext;
            SetFrequencyCheckboxes(habitToEdit.Frequency);
            HabitName.Text = habitToEdit.Name;
            HabitName.BindingContext = habitToEdit;
            AddButton.IsVisible = false;
            EditButton.IsVisible = true;
            ClearButton.IsVisible = false;
            CancelButton.IsVisible = true;

        }
    }

    public class Habit
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public IEnumerable<DayOfWeek>? Frequency { get; set; }
    }
}
