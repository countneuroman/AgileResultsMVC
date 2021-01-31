using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AgileResultsMVC.Models
{
    public class AllTask
    {
        public int Id { get; set; }
        //Атрибут для проверки вводимого значения(реализация в данном контроллере).
        //Проверка на null реализуется с помощью атрибута Required.
        [Required]
        [Remote(action: "VertifyPeriod", controller:"AllTasks")]
        public string Period { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateData { get; set; }
        [DataType(DataType.Date)]
        public DateTime CompletionDate { get; set; }
    }
}
