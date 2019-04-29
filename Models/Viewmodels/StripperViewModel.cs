using ResumeStripper.Models.AccountModels;

namespace ResumeStripper.Models.Viewmodels
{
    public class StripperViewModel
    {
        public string FileName { get; set; }
        public string ServerPath { get; set; }
        public CV ResultCv { get; set; }
        public User CurrentUser { get; set; }

        public StripperViewModel()
        {
            ResultCv = new CV();
        }
    }
}