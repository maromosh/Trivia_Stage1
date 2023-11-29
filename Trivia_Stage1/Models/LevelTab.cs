using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("LevelTab")]
public partial class LevelTab
{
    [Key]
    [Column("LevelID")]
    public int LevelId { get; set; }

    [Column("LEVELSName")]
    [StringLength(20)]
    public string Levelsname { get; set; } = null!;

    [InverseProperty("IdlevelNavigation")]
    public virtual ICollection<PlayersTab> PlayersTabs { get; set; } = new List<PlayersTab>();
}
