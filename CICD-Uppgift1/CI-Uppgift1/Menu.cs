using System;
using System.Collections.Generic;

namespace CI_Uppgift1
{
    public class Menu
    {
        /// <summary>
        /// Instance variable <c>_menuOptions</c> represents the menu options.
        /// </summary>
        private readonly List<string> _menuOptions;

        /// <summary>
        /// Instance variable <c>_choice</c> represents the users menu choice.
        /// </summary>
        private int _choice;

        /// <summary>
        /// The constructor initializes the new Menu with the
        /// <paramref name="menuOptions"/>.
        /// </summary>
        /// <param name="menuOptions"></param>
        public Menu(List<string> menuOptions){
            _menuOptions = menuOptions;
        }

        /// <summary>
        /// The users choice.
        /// </summary>
        /// <value>Property <c>Choice</c> represents the users choice from the
        /// menu.</value>
        public int Choice {get {return _choice;}}

        /// <summary>
        /// This method creates the menu.
        /// </summary>
        public void CreateMenu(){
            PrintMenuOptions();
            CheckInput();
        }

        /// <summary>
        /// This method prints all menu options.
        /// </summary>
        private void PrintMenuOptions(){
            for (int i = 0; i < _menuOptions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_menuOptions[i]}");
            }

            Console.Write("> ");
        }

        /// <summary>
        /// This method checks if the users input is valid.
        /// </summary>
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