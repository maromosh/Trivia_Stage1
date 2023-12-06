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
        Entry(p1).State=EntityState.Added; //חדש
        SaveChanges();
        return p1;

    }
    public PlayersTab? Login(string email, string password)
    {
        PlayersTab? p = this.PlayersTabs.Where(pp=>pp.Mail == email && pp.Password==password).FirstOrDefault();
        return p;
    }

    public void UpdatePlayer(PlayersTab p)
    {
        Entry(p).State = EntityState.Modified; //קיים ועודכן
        SaveChanges();
    }
    public void EnterQustion(QuestionTab qustion)
    {
        Entry(qustion).State = EntityState.Added;
        SaveChanges();
    }
    public List<QuestionTab> BringQuestion()
    {
        return QuestionTabs.Where(q => q.StatusId == 2).ToList();
        
    }
    //public string SendSubject()
    //{
    //    QuestionTab q1 = new QuestionTab();
    //    SubjectTab s1 = new SubjectTab();
    //    string q = this.QuestionTab.Where(q1 => q1.SubjectId == s1.SubId).FirstOrDefault();

    //}

}
