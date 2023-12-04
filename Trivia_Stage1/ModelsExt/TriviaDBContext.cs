using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class TriviaDBContext : DbContext
{
    const int PLAYER_MANAGER = 3;
    const int PLAYER_MASTER = 2;
    const int PLAYER_ROOKIE = 1;

    public PlayersTab AddSignUp(string email, string name, string password)
    {
        PlayersTab p1 = new PlayersTab
        {
            Name = name,
            Mail = email,
            Password = password,
            Score = 0,
            Idlevel = PLAYER_ROOKIE,
        };
        this.PlayersTabs.Add(p1);
        SaveChanges();
        return p1;

    }
    public PlayersTab Login(string email, string password)
    {
        PlayersTab? p = this.PlayersTabs.Where(pp=>pp.Mail == email && pp.Password==password).FirstOrDefault();
        return p;

    }

}
