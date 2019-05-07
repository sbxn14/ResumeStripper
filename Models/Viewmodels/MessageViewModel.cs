namespace ResumeStripper.Models.Viewmodels
{
    public class MessageViewModel
    {
        public string FileName { get; set; }
        public string ServerPath { get; set; }
        public CV ResultCv { get; set; }

        public MessageViewModel()
        {
            ResultCv = new CV();
        }
    }
}