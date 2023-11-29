using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("PlayersTab")]
[Index("Mail", Name = "UQ__PlayersT__7A2129045C8A6A75", IsUnique = true)]
public partial class PlayersTab
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("mail")]
    [StringLength(20)]
    public string Mail { get; set; } = null!;

    [Column("name")]
    [StringLength(20)]
    public string Name { get; set; } = null!;

    [Column("password")]
    [StringLength(20)]
    public string Password { get; set; } = null!;

    [Column("score")]
    public int? Score { get; set; }

    [Column("IDlevel")]
    public int? Idlevel { get; set; }

    [ForeignKey("Idlevel")]
    [InverseProperty("PlayersTabs")]
    public virtual LevelTab? IdlevelNavigation { get; set; }

    [InverseProperty("Player")]
    public virtual ICollection<QuestionTab> QuestionTabs { get; set; } = new List<QuestionTab>();
}
