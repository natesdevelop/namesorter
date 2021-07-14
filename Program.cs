using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace namesorter
{

    //Person Class Defining the Person Object
    public class Person {

        private string firstNames;
        private string lastName;
        private int id;

        public Person (string first, string last, int id){

            this.firstNames = first;
            this.lastName = last;
            this.id = id;
        }

        //return last name of person 
        public string getLastName(){
            return this.lastName;
        }

        //retrun full name of person 
        public override string ToString() {
        return firstNames + " " + lastName;
        }

    }


    class Program
    {

        static void Main(string[] args)
        {
            
            //creating an empty Person List 
            List<Person> PersonList = new List<Person>();

            
            //reading text file into a variable
            var data = File.ReadAllLines(args[0]);

            //new file to output result 
            //using StreamWriter file = new("sorted-names-list.txt");

            //turing varibale into a list object for sorting 

            int i = 0;


            foreach (var item in data) {
                
                //removing unessacary whitespace from names 
                //the whitespacecan cause unexpected behaviour
                var temp = item.Trim();
                
                //spliting the the string into a string array 
                string[] tempArr = temp.Split(" ");

                string firstName;

                //the last name will be the last item in the string array 
                string lastName = tempArr[tempArr.Length - 1];
                
                //depending on the amount of the names wether its 
                //1 - 4 names we are re - building the first names 
                int length = tempArr.Length;

                switch(length){

                    case 2:
                        firstName = tempArr[0];
                        break;
                    case 3: 
                        firstName = tempArr[0] + " " + tempArr[1];
                        break;
                    case 4: 
                        firstName = tempArr[0] + " " + tempArr[1] + " " + tempArr[2];
                        break;
                    default:
                        firstName = tempArr[0];
                        break;

                }
                
                //creating a new person object 
                PersonList.Add(new Person(firstName, lastName, i));
                
                i++;
            } 

            //Casting the Person List to a Persaon Array for sorting 
            Person[] persons = PersonList.ToArray();

            //Using a LINQ to sort the people by last name
            var list = from p in persons
                orderby p.getLastName()
                select p;

            //converting bac to an array to print to a text file
            var write = list.ToArray();

            //
            foreach(var item in write){
                Console.WriteLine(item);
            }

            //Writing names to Files 
            File.WriteAllLines("sorted-names-list.txt", Array.ConvertAll(write, x => x.ToString()));
            

        }
    }
}

