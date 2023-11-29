using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("SubjectTab")]
public partial class SubjectTab
{
    [Key]
    [Column("SubID")]
    public int SubId { get; set; }

    [StringLength(20)]
    public string SubName { get; set; } = null!;

    [InverseProperty("Subject")]
    public virtual ICollection<QuestionTab> QuestionTabs { get; set; } = new List<QuestionTab>();
}
