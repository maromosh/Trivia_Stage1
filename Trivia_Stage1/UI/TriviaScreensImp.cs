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
                string email = Console.ReadLine();
                while (!IsEmailValid(email))
                {
                    Console.Write("Bad Email Format! Please try again:");
                    email = Console.ReadLine();
                }

                Console.Write("Please Type your password: ");
                string password = Console.ReadLine();
                while (!IsPasswordValid(password))
                {
                    Console.Write("password must be at least 4 characters! Please try again: ");
                    password = Console.ReadLine();
                }

                Console.Write("Please Type your Name: ");
                string name = Console.ReadLine();
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
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }

        public void ShowPendingQuestions()
        {
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }
        public void ShowGame()
        {
           
        }
        public void ShowProfile()
        {
            CleareAndTtile("PROFILE");
            TriviaDBContext db = new TriviaDBContext();
            if(currentPlayer == null)
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

            char c = ' ';
            Console.WriteLine("Update (M)ail, (N)ame, (P)assword, (B)ack...");
            c = Console.ReadKey(true).KeyChar;

            if(c == 'm' || c == 'M')
            {
                Console.WriteLine("Enter youre new Email");
                string? mail = Console.ReadLine();
                while (!IsEmailValid(mail))
                {
                    Console.Write("Bad Email Format! Please try again:");
                    mail = Console.ReadLine();
                }
                this.currentPlayer.Mail = mail;
                db.Add(mail);
                db.SaveChanges();
            }
            if (c == 'n')
            {
                Console.WriteLine("Enter youre new name");
                string? name = Console.ReadLine();
                while (!IsNameValid(name))
                {
                    Console.Write("name must be at least 3 characters! Please try again: ");
                    name = Console.ReadLine();
                }
                this.currentPlayer.Name = name;
                db.Add(name);
                db.SaveChanges();
            }
            if (c == 'p')
            {
                Console.Write("Please Type your password: ");
                string? password = Console.ReadLine();
                while (!IsPasswordValid(password))
                {
                    Console.Write("password must be at least 4 characters! Please try again: ");
                    password = Console.ReadLine();
                }
                this.currentPlayer.Name = password;
                db.Add(password);
                db.SaveChanges();
            }

            Console.ReadKey(true);
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

    }
}
