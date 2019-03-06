using ClassLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp13
{
    public class MyClass : IDisposable
    {
        public MyClass()
        {
            //con.Open()
        }
        ~MyClass()
        {
            //con.Close();
            Console.WriteLine("Hello from Destructor");
        }

        public void Dispose()
        {
            Console.WriteLine("Отработал Dispose");
        }
    }

    [Serializable]
    public class Person
    {
        public Person()
        {

        }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }


        [NonSerialized]

        public string iin;//муха

        public Person(string Name, int Year)
        {
            this.Name = Name;
            this.Year = Year;
        }

        public void Method1()
        {
            Console.WriteLine("Hola");
        }
    }
    public class Service<T>
    {
        public List<T> list = new List<T>();
        
        public string SerializeObj(string path)
        {
            FileInfo fi = new FileInfo(path);

            string msg = "";

            if (!fi.Exists)
                msg = "Файл создан новый";
            else
                msg = "Файл перезаписан";

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fr = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fr, list.ToArray());
            }

            return msg;
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Service<PC> service = new Service<PC>();
            for (int i = 0; i < 3; i++)
            {
                service.list.Add(new PC("Mark" + i, "SN" + i, "Model" + i));
            }

            Console.WriteLine(service.SerializeObj("listSerial.txt"));



            Person person1 = new Person("Сет", 30);
            Person person2 = new Person("Mет", 30);
            Person[] persons = new Person[] { person1, person2 };

            Service<Person> service2 = new Service<Person>();
            service2.list.AddRange(persons.ToList());
            Console.WriteLine(service2.SerializeObj("listSerial2.txt"));
        }

        static void Exmpl001()
        {
            Person person1 = new Person("Сет", 30);
            Person person2 = new Person("Mет", 30);
            Person[] persons = new Person[] { person1, person2 };


            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("people.data", FileMode.OpenOrCreate))
            {
                //formatter.Serialize(fs, person1);
                formatter.Serialize(fs, persons);
                Console.WriteLine("Object serialized");
            }

            using (FileStream fs = new FileStream("people.data", FileMode.Open))
            {
                //Person data = (Person)formatter.Deserialize(fs); 
                Person[] data2 = (Person[])formatter.Deserialize(fs);
            }
        }

        static void Exmpl02()
        {
            Person person1 = new Person("Сет", 30);
            Person person2 = new Person("Mет", 30);
            Person[] persons = new Person[] { person1, person2 };


            SoapFormatter formatter = new SoapFormatter();
            using (FileStream fs = new FileStream("people.soap", FileMode.OpenOrCreate))
            {
                //formatter.Serialize(fs, person1);
                formatter.Serialize(fs, persons);
                Console.WriteLine("Object serialized");
            }

            using (FileStream fs = new FileStream("people.soap", FileMode.Open))
            {
                //Person data = (Person)formatter.Deserialize(fs); 
                Person[] data2 = (Person[])formatter.Deserialize(fs);
            }
        }

        static void Exmpl03()
        {
            Person person1 = new Person("Сет", 30);
            Person person2 = new Person("Mет", 30);
            Person[] persons = new Person[] { person1, person2 };


            XmlSerializer formatter = new XmlSerializer(typeof(Person[]));

            using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
            {
                //formatter.Serialize(fs, person1);
                formatter.Serialize(fs, persons);
                Console.WriteLine("Object serialized");
            }

            using (FileStream fs = new FileStream("people.xml", FileMode.Open))
            {
                //Person data = (Person)formatter.Deserialize(fs); 
                Person[] data2 = (Person[])formatter.Deserialize(fs);
            }
        }

        static void Exmpl04()
        {
            Person person1 = new Person("Сет", 30);
            Person person2 = new Person("Mет", 30);
            Person[] persons = new Person[] { person1, person2 };

            string json = JsonConvert.SerializeObject(persons);
            Console.WriteLine(json);
            Person[] data2 = JsonConvert.DeserializeObject<Person[]>(json);
        }

        static void Exmpl05()
        {
            FileInfo fileInfo = new FileInfo("PhoneBook.csv");
            if (!fileInfo.Exists)
            {
                using (FileStream fs = fileInfo.Create())
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            var user = RandomUser.GenerateUser.GetUser();
                            string str =
                                string.Format("{0};{1};{2};{3}",
                                              user.name.title,
                                              user.name.first,
                                              GetPrhone(),
                                              user.location.city);

                            sw.WriteLine(str);
                        }
                    }
                }
            }
        }
        static string GetPrhone()
        {
            Random rnd = new Random();
            string phone = string.Format("+7 {0} {1}-{2}-{3}",
                rnd.Next(111, 999),
                rnd.Next(111, 999),
                rnd.Next(11, 99),
                rnd.Next(11, 99));

            return phone;
        }

        static void Task1()
        {
            List<Person> persons = new List<Person>();
            using (StreamReader sr = new StreamReader("PhoneBook.csv"))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    //mr;benjamin;+7 199 726-14-27;wollongong
                    string[] arr = line.Split(';');
                    Person p1 = new Person();
                    p1.Title = arr[0];
                    p1.Name = arr[1];
                    p1.Phone = arr[2];
                    p1.City = arr[3];
                    persons.Add(p1);
                }
            }

            SoapFormatter formatter = new SoapFormatter();
            using (FileStream fs = new FileStream("PhoneBook.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, persons.ToArray());
            }
        }
    }
}
