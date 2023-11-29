using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("QuestionTab")]
public partial class QuestionTab
{
    [Key]
    [Column("QuestionID")]
    public int QuestionId { get; set; }

    [Column("PlayerID")]
    public int? PlayerId { get; set; }

    [Column("SubjectID")]
    public int? SubjectId { get; set; }

    public int? StatusId { get; set; }

    [StringLength(100)]
    public string RightQuestions { get; set; } = null!;

    [StringLength(100)]
    public string Questions1 { get; set; } = null!;

    [StringLength(100)]
    public string Questions2 { get; set; } = null!;

    [StringLength(100)]
    public string Questions3 { get; set; } = null!;

    [ForeignKey("PlayerId")]
    [InverseProperty("QuestionTabs")]
    public virtual PlayersTab? Player { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("QuestionTabs")]
    public virtual QuestionsStatusTab? Status { get; set; }

    [ForeignKey("SubjectId")]
    [InverseProperty("QuestionTabs")]
    public virtual SubjectTab? Subject { get; set; }
}
