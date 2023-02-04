using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AgileResultsMVC.Models;

public class AllTask
{
    public int Id { get; set; }
    [Required]
    //Атрибут для проверки вводимого значения;
    [Remote(action: "VertifyPeriod", controller:"AllTasks")]
    public string Period { get; set; }       
    public string Title { get; set; }
    public string Description { get; set; }
    [DataType(DataType.Date)]
    public DateTime CreateData { get; set; }
    [DataType(DataType.Date)]
    public DateTime CompletionDate { get; set; }
    //Определяем отношение один к одному
    //(здесь хранятся значения пользователя, к которому относятся задачи).
    public string UserId  { get; set; }
    public User User { get; set; }
}