using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("QuestionsStatusTab")]
public partial class QuestionsStatusTab
{
    [Key]
    public int StatusId { get; set; }

    [StringLength(15)]
    public string? StatusName { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<QuestionTab> QuestionTabs { get; set; } = new List<QuestionTab>();
}
