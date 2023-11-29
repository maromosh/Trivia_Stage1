using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class TriviaDBContext : DbContext
{
    const int playerType = 3;
    const int playerType2 = 2;
    const int playerType3 = 1;

    public PlayersTab AddSignUp(string email, string name, string password)
    {
        PlayersTab p1 = new PlayersTab
        {
            Name = name,
            Mail = email,
            Password = password,
            Score = 0,
            Idlevel = 1,
        };
        this.PlayersTabs.Add(p1);
        SaveChanges();
    }
}
