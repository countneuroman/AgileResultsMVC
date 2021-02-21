using System.ComponentModel.DataAnnotations;

namespace AgileResultsMVC.ViewModels
{
    public class TaskEditModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
