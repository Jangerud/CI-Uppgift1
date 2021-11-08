using System;
using System.Collections.Generic;

namespace CI_Uppgift1
{
    public class Menu
    {
        private readonly List<string> _menuOptions;
        private int _choice;
        public Menu(List<string> menuOptions){
            _menuOptions = menuOptions;
        }

        public void CreateMenu(){
            PrintMenuOptions();
            CheckInput();
        }

        public int Choice {get {return _choice;}}
        private void PrintMenuOptions(){
            for (int i = 0; i < _menuOptions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_menuOptions[i]}");
            }

            Console.Write("> ");
        }

        private void CheckInput(){
            bool isNumeric;
            int choice;

            do
            {
                isNumeric = int.TryParse(Console.ReadLine(), out choice);
                if (!isNumeric || choice < 1 || choice > _menuOptions.Count){
                    Console.WriteLine("Please enter a valid number.");
                    Console.Write("> ");
                }
            } while (!isNumeric || choice < 1 || choice > _menuOptions.Count);

            _choice = choice;
        }
    }
}