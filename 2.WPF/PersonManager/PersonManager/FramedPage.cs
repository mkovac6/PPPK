using PersonManager.ViewModels;
using System.Windows.Controls;

namespace Zadatak
{
    public class FramedPage : Page
    {
        public FramedPage(PersonViewModel personViewModel)
        {
            PersonViewModel = personViewModel;
        }
        public PersonViewModel PersonViewModel { get; }
        public Frame Frame { get; set; }
    }
}
