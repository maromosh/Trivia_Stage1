using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using Trivia_Stage1.Models;

namespace Trivia_Stage1.UI
{
    public class TriviaScreensImp:ITriviaScreens
    {

        //Place here any state you would like to keep during the app life time
        //For example, player login details...
        private PlayersTab currentPlayer;

        //Implememnt interface here
        public bool ShowLogin()
        {
            CleareAndTtile("Log in");
            Console.WriteLine("please enter mail");
            string? mail = Console.ReadLine();
            Console.WriteLine("please enter password");
            string? password = Console.ReadLine();
            char c = ' ';          
            PerformLogin(mail,password);

            while ( this.currentPlayer == null && c != 'B')
            {
                Console.WriteLine("Login Failed! (B)ack or other key to try again");
                c = Console.ReadKey(true).KeyChar;
                if (c != 'B' && c != 'b')
                {
                    Console.WriteLine("You should try again");
                    Console.WriteLine("please enter mail");
                    mail = Console.ReadLine();
                    Console.WriteLine("please enter password");
                    password = Console.ReadLine();
                    PerformLogin(mail, password);
                }
                
            }         
            return (this.currentPlayer!=null);
        }
        private void PerformLogin(string mail, string password)
        {
            TriviaDBContext dbContext = new TriviaDBContext();
            try
            {
                this.currentPlayer = dbContext.Login(mail, password);
            }
            catch(Exception ex)
            {
                this.currentPlayer = null;
            }
        }
        public bool ShowSignup()
        {
            CleareAndTtile("Sign up");
            //Logout user if anyone is logged in!
            //A reference to the logged in user should be stored as a member variable
            //in this class! Example:
            this.currentPlayer = null;
            bool success = true;
            //Loop through inputs until a user/player is created or 
            //user choose to go back to menu
            char c = ' ';
            while (c != 'B' && c != 'b' && this.currentPlayer == null)
            {
                //Clear screen
                CleareAndTtile("Signup");

                Console.Write("Please Type your email: ");
                string? email = Console.ReadLine();
                while (!IsEmailValid(email))
                {
                    Console.Write("Bad Email Format! Please try again:");
                    email = Console.ReadLine();
                }

                Console.Write("Please Type your password: ");
                string? password = Console.ReadLine();
                while (!IsPasswordValid(password))
                {
                    Console.Write("password must be at least 4 characters! Please try again: ");
                    password = Console.ReadLine();
                }

                Console.Write("Please Type your Name: ");
                string? name = Console.ReadLine();
                while (!IsNameValid(name))
                {
                    Console.Write("name must be at least 3 characters! Please try again: ");
                    name = Console.ReadLine();
                }

                
                Console.WriteLine("Connecting to Server...");
                //Create instance of Business Logic and call the signup method
                //For example:
                try
                {
                    TriviaDBContext db = new TriviaDBContext();
                    this.currentPlayer = db.AddSignUp(email, password, name);
                    Console.WriteLine("Player was added successfully! Hip Hip Hurray!!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to signup! Email may already exist in DB!");
                    success = false;
                }



                //Provide a proper message for example:
                Console.WriteLine("Press (B)ack to go back or any other key to signup again...");
                //Get another input from user
                c = Console.ReadKey(true).KeyChar;
            }
            //return true if signup suceeded!
            return (success);
        }
        public void ShowAddQuestion()
        {
            CleareAndTtile("Show Add Question");
            TriviaDBContext db = new TriviaDBContext();
            char c = ' ';
            bool success = false;
            
            if (this.currentPlayer.Score >= 100 || this.currentPlayer.Idlevel == 3)
            {
                QuestionTab q = new QuestionTab();
                q.PlayerId = this.currentPlayer.Id;
                q.StatusId = 1;
                while (c != 'b' && c != 'B' && (this.currentPlayer.Idlevel == 3 || this.currentPlayer.Score>= 100)) 
                {
                    if(currentPlayer.Score >= 100 || this.currentPlayer.Idlevel == 3)
                    {
                        
                        Console.WriteLine("add your qustion");
                        string qustion = Console.ReadLine();
                        q.QuestionText = qustion;

                        Console.WriteLine("enter 3 wrong");
                        string wrong1 = Console.ReadLine();
                        q.BadAnswer1 = wrong1;
                        string wrong2 = Console.ReadLine();
                        q.BadAnswer2 = wrong2;
                        string wrong3 = Console.ReadLine();
                        q.BadAnswer3 = wrong3;

                        Console.WriteLine("ENTER RIGHT QUSTION");
                        string right = Console.ReadLine();
                        q.RightAnswer = right;

                        try
                        {
                            db.EnterQustion(q);
                            Console.WriteLine("added qustion seccseed");
                            success = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Faild added qustion. Try agin or (B)ack...");
                        }
                        if (success && this.currentPlayer.Idlevel == 1)
                        {
                            this.currentPlayer.Score -= 100;
                            db.UpdatePlayer(currentPlayer);
                            Console.WriteLine("your score now is 0. Byyy");
                        }
                    }
                    
                    Console.WriteLine("press (B)ack to go back to menu");
                    c = Console.ReadKey(true).KeyChar;
                }
            }
            else
            {
                Console.WriteLine("youre not sutted for the job go back!!!!!");
            }
        }

        public void ShowPendingQuestions()
        {
            CleareAndTtile("Pending Questions");
            TriviaDBContext db = new TriviaDBContext();
            char c = ' ';
            List<QuestionTab> Qs = db.ShowPending();
            int counter = 0;
            int counter2 = 0;
            foreach (QuestionTab q1 in Qs)
            {
                counter++;
            }
            if (currentPlayer.Idlevel == 3 || currentPlayer.Idlevel == 2)
            {
                if(counter == 0)
                {
                    Console.WriteLine("No more pending Question");
                }
                else
                {
                    foreach (QuestionTab q in Qs)
                    {
                        while (c != 'B' && c != 'b')
                        {

                            Console.WriteLine(q.QuestionText);
                            Console.WriteLine(q.BadAnswer1);
                            Console.WriteLine(q.BadAnswer2);
                            Console.WriteLine(q.BadAnswer3);
                            Console.WriteLine($"Right answer - {q.RightAnswer}");
                            Console.WriteLine("Press (A)pprove the question \n If you want to (D)ecline the question \n (S)kip question \n (B)ack");
                            c = Console.ReadKey(true).KeyChar;
                            if (c == 'A' || c == 'a')
                            {
                                try
                                {
                                    db.ChangeToApproved(q);
                                    Console.WriteLine("The question is now Aprrove!");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Failed to Aprrove");
                                }
                            }
                            if (c == 'D' || c == 'd')
                            {
                                try
                                {
                                    Console.WriteLine("The question is now Declined!");
                                    db.ChangeToDeclined(q);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Failed to Decline");
                                }
                            }
                            if (c == 's' || c == 'S')
                            {
                            }
                            counter2--;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Only manager and master can change the question status!!!!"); 
                Console.WriteLine("press (B)ack to go back to menu");
                c = Console.ReadKey(true).KeyChar;
            }
        }
        public void ShowGame()
        {
            CleareAndTtile("Game");
            TriviaDBContext db = new TriviaDBContext();
            char c = ' ';
            bool success = false;
            List<QuestionTab> Qs = db.BringQuestion();
            string TheAnswer = " ";
            int counter = 0;
            int counter2 = 0;
            foreach (QuestionTab q1 in Qs)
            {
                counter++;
            }
            
            while ((c != 'B') && (c != 'b') && (counter2 < counter))
            {
                foreach (QuestionTab q in Qs)
                {
                    Console.WriteLine(q.QuestionText);
                    Console.WriteLine(q.BadAnswer1);
                    Console.WriteLine(q.BadAnswer2);
                    Console.WriteLine(q.BadAnswer3);
                    Console.WriteLine(q.RightAnswer);

                    Console.WriteLine("what is your final answer?");
                    TheAnswer = Console.ReadLine();
                    if (TheAnswer == q.RightAnswer)
                    {
                        this.currentPlayer.Score += 10;
                        Console.WriteLine("Great job!!!!");
                        success = true;
                    }
                    else
                    {
                        Console.WriteLine("you're incorrect:(");
                    }
                    counter2++;
                }
                
                Console.WriteLine("press (B)ack to go back to menu");
                c = Console.ReadKey(true).KeyChar;
            }
        }    
        public void ShowProfile()
        {
            CleareAndTtile("PROFILE");
            TriviaDBContext db = new TriviaDBContext();
            char c = ' ';
            
            while (c != 'b' && c != 'B')
            {
                if (currentPlayer == null)
                {
                    Console.WriteLine("Log in first!!!");
                    Console.ReadKey(true);
                    return;
                }
                Console.WriteLine($"Name: {this.currentPlayer.Name}");
                Console.WriteLine($"Mail: {this.currentPlayer.Mail}");
                Console.WriteLine($"Passworde: {this.currentPlayer.Password}");
                Console.WriteLine($"Player Id: {this.currentPlayer.Id}");
                Console.WriteLine($"Score: {this.currentPlayer.Score}");


                Console.WriteLine("Update (M)ail, (N)ame, (P)assword, (B)ack...");
                c = Console.ReadKey(true).KeyChar;
                bool updated = false;
                if (c == 'm' || c == 'M')
                {
                    Console.WriteLine("Enter youre new Email");
                    string? mail = Console.ReadLine();
                    while (!IsEmailValid(mail))
                    {
                        Console.Write("Bad Email Format! Please try again:");
                        mail = Console.ReadLine();
                    }
                    this.currentPlayer.Mail = mail;
                    updated = true;
                }
                if (c == 'n' || c == 'N')
                {
                    Console.WriteLine("Enter youre new name");
                    string? name = Console.ReadLine();
                    while (!IsNameValid(name))
                    {
                        Console.Write("name must be at least 3 characters! Please try again: ");
                        name = Console.ReadLine();
                    }
                    this.currentPlayer.Name = name;
                    updated = true;
                }
                if (c == 'p' || c == 'P')
                {
                    Console.Write("Please Type your password: ");
                    string? password = Console.ReadLine();
                    while (!IsPasswordValid(password))
                    {
                        Console.Write("password must be at least 4 characters! Please try again: ");
                        password = Console.ReadLine();
                    }
                    this.currentPlayer.Name = password;
                    updated = true;
                }
                if(updated == true)
                {
                    try
                    {
                        db.Update(this.currentPlayer);
                        Console.WriteLine("youre changes succeeded");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Failed changes!");
                    }
                }
            }
        }

        //Private helper methodfs down here...
        private void CleareAndTtile(string title)
        {
            Console.Clear();
            Console.WriteLine($"\t\t\t\t\t{title}");
            Console.WriteLine();
        }

        private bool IsEmailValid(string emailAddress)
        {
            var pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            var regex = new Regex(pattern);
            return regex.IsMatch(emailAddress);
        }

        private bool IsPasswordValid(string password)
        {
            return password != null && password.Length >= 3;
        }

        private bool IsNameValid(string name)
        {
            return name != null && name.Length >= 3;
        }


        //scaffold-DbContext "Server = (localdb)\MSSQLLocalDB; Database=TriviaDB; Trusted_Connection = True; TrustServerCertificate = True;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context TriviaDBContext -DataAnnotations -force
    }
}
